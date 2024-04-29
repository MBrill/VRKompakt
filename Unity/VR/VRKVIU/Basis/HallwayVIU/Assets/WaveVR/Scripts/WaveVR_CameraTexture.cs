// "WaveVR SDK
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

#pragma warning disable 0162

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WVR_Log;
using System;
using wvr;
using System.Runtime.InteropServices;
using System.Threading;

public class WaveVR_CameraTexture
{
	private static string LOG_TAG = "WVR_CameraTexture";

	private WVR_CameraInfo_t camerainfo;
	private bool mStarted = false;
	private IntPtr nativeTextureId = IntPtr.Zero;
	private IntPtr mframeBuffer = IntPtr.Zero;
	private IntPtr threadframeBuffer = IntPtr.Zero;
	private bool syncPose = false;
	private WVR_PoseState_t mPoseState;
	private WVR_PoseOriginModel origin = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnGround;
	private Thread mthread;
	private bool toThreadStop = false;
	private bool updateFramebuffer = false;

	public bool isStarted
	{
		get
		{
			return mStarted;
		}
	}

	public delegate void UpdateCameraCompleted(System.IntPtr nativeTextureId);
	public static event UpdateCameraCompleted UpdateCameraCompletedDelegate = null;

	public delegate void StartCameraCompleted(bool result);
	public static event StartCameraCompleted StartCameraCompletedDelegate = null;

	private static WaveVR_CameraTexture mInstance = null;
	private const bool DEBUG = false;

	private void PrintDebugLog(string msg)
	{
		if (DEBUG)
		{
			Log.d(LOG_TAG, msg);
		}
	}

	public static WaveVR_CameraTexture instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = new WaveVR_CameraTexture();
			}

			return mInstance;
		}
	}

	private void OnUpdateCameraCompleted(params object[] args)
	{
		//bool texUpdated = (bool)args[0];

		if (UpdateCameraCompletedDelegate != null) UpdateCameraCompletedDelegate(nativeTextureId);
	}

	public IntPtr getNativeTextureId()
	{
		if (!mStarted) return IntPtr.Zero;
		return nativeTextureId;
	}

	[Obsolete("Please use void startCamera(bool enableSyncPose) instead and listen to StartCameraCompleted delegate to get result")]
	public bool startCamera()
	{
		return false;
	}

	public void startCamera(bool enable)
	{
		if (mStarted) return ;
		syncPose = enable;
		WaveVR_Utils.Event.Listen("DrawCameraCompleted", OnUpdateCameraCompleted);

		if (syncPose)
		{
			mPoseState = new WVR_PoseState_t();
			mStarted = Interop.WVR_StartCamera(ref camerainfo);

			Log.i(LOG_TAG, "startCamera, result = " + mStarted + " format: " + camerainfo.imgFormat + " size: " + camerainfo.size
			+ " width: " + camerainfo.width + " height: " + camerainfo.height);
			PrintDebugLog("allocate frame buffer");
			mframeBuffer = Marshal.AllocHGlobal((int)camerainfo.size);

			//zero out buffer
			for (int i = 0; i < camerainfo.size; i++)
			{
				Marshal.WriteByte(mframeBuffer, i, 0);
			}
			if (StartCameraCompletedDelegate != null) StartCameraCompletedDelegate(mStarted);
		}
		else
		{
			mthread = new Thread(() => CameraThread());
			if (mthread.IsBackground == false)
				mthread.IsBackground = true;
			toThreadStop = false;
			mthread.Start();
		}
	}

	void CameraThread()
	{
		mStarted = Interop.WVR_StartCamera(ref camerainfo);

		Log.i(LOG_TAG, "startCamera, result = " + mStarted + " format: " + camerainfo.imgFormat + " size: " + camerainfo.size
		+ " width: " + camerainfo.width + " height: " + camerainfo.height);

		if (StartCameraCompletedDelegate != null) StartCameraCompletedDelegate(mStarted);
		if (!mStarted)
		{
			Log.i(LOG_TAG, "Camera start failed, camera thread stop.");
			return;
		}

		//Keep call WVR_GetFrameBufferWithPoseState
		Log.i(LOG_TAG, "Start CameraThread, Camera is Started? " + mStarted.ToString() + "CameraThread.ThreadState=" + mthread.ThreadState + "CameraThread.IsBackground=" + mthread.IsBackground);
		PrintDebugLog("allocate frame buffer");

		threadframeBuffer = Marshal.AllocHGlobal((int)camerainfo.size);

		//zero out buffer
		for (int i = 0; i < camerainfo.size; i++)
		{
			Marshal.WriteByte(threadframeBuffer, i, 0);
		}
		updateFramebuffer = false;
		int counter = 0;
		while (!toThreadStop)
		{
			if (threadframeBuffer != IntPtr.Zero)
			{
				updateFramebuffer = Interop.WVR_GetCameraFrameBuffer(threadframeBuffer, camerainfo.size);

				if (!updateFramebuffer)
				{
					counter++;
					if (counter > 100)
					{
						Log.i(LOG_TAG, "get framebuffer failed, break while ");
						break;
					}
					Log.i(LOG_TAG, "counter : " + counter);
				}
				else counter = 0;
			}
			else
			{
				Log.i(LOG_TAG, "threadframeBuffer = null, break while ");
				break;
			} 
		}
		WaveVR_Utils.Event.Remove("DrawCameraCompleted", OnUpdateCameraCompleted);
		updateFramebuffer = false;

		if (threadframeBuffer != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(threadframeBuffer);
			threadframeBuffer = IntPtr.Zero;
		}

		Interop.WVR_StopCamera();
		mStarted = false;

		Log.i(LOG_TAG, "End of CameraThread");
	}

	[Obsolete("Please use getImageType instead")]
	public WVR_CameraImageType GetCameraImageType()
	{
		return camerainfo.imgType;
	}

	public WVR_CameraImageType getImageType()
	{
		if (!mStarted) return WVR_CameraImageType.WVR_CameraImageType_Invalid;
		return camerainfo.imgType;
	}

	[Obsolete("Please use getImageFormat instead")]
	public WVR_CameraImageFormat GetCameraImageFormat()
	{
		if (!mStarted) return 0;
		return camerainfo.imgFormat;
	}

	public WVR_CameraImageFormat getImageFormat()
	{
		if (!mStarted) return WVR_CameraImageFormat.WVR_CameraImageFormat_Invalid;
		return camerainfo.imgFormat;
	}

	[Obsolete("Please use getImageWidth instead")]
	public uint GetCameraImageWidth()
	{
		if (!mStarted) return 0;
		return camerainfo.width;
	}

	public uint getImageWidth()
	{
		if (!mStarted) return 0;
		return camerainfo.width;
	}

	[Obsolete("Please use getImageHeight instead")]
	public uint GetCameraImageHeight()
	{
		if (!mStarted) return 0;
		return camerainfo.height;
	}

	public uint getImageHeight()
	{
		if (!mStarted) return 0;
		return camerainfo.height;
	}

	public uint getImageSize()
	{
		if (!mStarted) return 0;
		return camerainfo.size;
	}

	public bool isEnableSyncPose()
	{
		if (!mStarted) return false;
		return syncPose;
	}

	public IntPtr getNativeFrameBuffer()
	{
		if (!mStarted) return IntPtr.Zero;
		if (syncPose) return mframeBuffer;
		else return threadframeBuffer;
	}

	public void stopCamera()
	{
		if (!mStarted) return;

		if (syncPose)
		{
			WaveVR_Utils.Event.Remove("DrawCameraCompleted", OnUpdateCameraCompleted);
			Log.i(LOG_TAG, "Reset WaveVR_Render submit pose");
			WaveVR_Render.ResetPoseUsedOnSubmit();
			Interop.WVR_StopCamera();
			if (mframeBuffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(mframeBuffer);
				mframeBuffer = IntPtr.Zero;
			}
			mStarted = false;
		}
		else
		{
			if (mthread != null && mthread.IsAlive)
			{
				toThreadStop = true;
				Log.i(LOG_TAG, "to thread stop");
			}
		}

		Log.i(LOG_TAG, "Release native texture resources");
		WaveVR_Utils.SendRenderEvent(WaveVR_Utils.RENDEREVENTID_ReleaseTexture);
	}

	public bool getFramePose(ref WVR_PoseState_t pose)
	{
		if (!syncPose) return false;
		pose = mPoseState;
		return true;
	}

	public void updateTexture(IntPtr textureId)
	{
		if (!mStarted)
		{
			Log.w(LOG_TAG, "Camera not start yet");
			return;
		}

		//PrintDebugLog("updateTexture start, syncPose = " + syncPose.ToString() + " updateFramebuffer = " + updateFramebuffer.ToString());

		nativeTextureId = textureId;
		if (WaveVR_Render.Instance != null)
			origin = WaveVR_Render.Instance.origin;

		if (syncPose)
		{
			if (mframeBuffer != IntPtr.Zero)
			{
				uint predictInMs = 0;
				PrintDebugLog("updateTexture frameBuffer and PoseState, predict time:" + predictInMs);

				Interop.WVR_GetFrameBufferWithPoseState(mframeBuffer, camerainfo.size, origin, predictInMs, ref mPoseState);

				PrintDebugLog("Sync camera frame buffer with poseState, timeStamp: " + mPoseState.PoseTimestamp_ns);
				WaveVR_Render.SetPoseUsedOnSubmit(mPoseState);

				PrintDebugLog("send event to draw OpenGL");
				WaveVR_Utils.SendRenderEvent(WaveVR_Utils.RENDEREVENTID_DrawTextureWithBuffer);
			}
		}
		else
		{
			if (updateFramebuffer && (threadframeBuffer != IntPtr.Zero))
			{
				PrintDebugLog("updateFramebuffer camera frame buffer");
				nativeTextureId = textureId;
				PrintDebugLog("send event to draw OpenGL");
				WaveVR_Utils.SendRenderEvent(WaveVR_Utils.RENDEREVENTID_DrawTextureWithBuffer);
				updateFramebuffer = false;
			} else
			{
				// thread frame buffer is not updated and native texture is not updated, send complete delegate back
				if (UpdateCameraCompletedDelegate != null) UpdateCameraCompletedDelegate(nativeTextureId);
			}
		}
	}
}
