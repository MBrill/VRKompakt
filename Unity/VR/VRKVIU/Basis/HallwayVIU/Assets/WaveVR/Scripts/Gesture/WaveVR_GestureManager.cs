// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using wvr;
using WVR_Log;
using System.Threading;
using System;

[DisallowMultipleComponent]
public class WaveVR_GestureManager : MonoBehaviour {
	private const string LOG_TAG = "WaveVR_GestureManager";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d (LOG_TAG, msg, true);
	}
	private static WaveVR_GestureManager instance = null;
	public static WaveVR_GestureManager Instance
	{
		get
		{
			return instance;
		}
	}


	// ------------------- Pointer Related begins -------------------
	#region Pointer variables.
	public enum EGestureHand
	{
		RIGHT = 0,
		LEFT = 1
	};

	[HideInInspector]
	public static EGestureHand GestureFocusHand = EGestureHand.RIGHT;
	#endregion
	// ------------------- Pointer Related ends -------------------


	public enum EStaticGestures
	{
		UNKNOWN = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Unknown,
		FIST	= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Fist,
		FIVE	= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Five,
		OK		= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_OK,
		THUMBUP = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_ThumbUp,
		INDEXUP = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_IndexUp,
	}

	public bool EnableHandGesture = true;
	private bool preEnableHandGesture = true;
	private WVR_HandGestureData_t handGestureData = new WVR_HandGestureData_t();

	public bool EnableHandTracking = true;
	private bool preEnableHandTracking = true;

	private bool hasHandGestureData = false;
	private WVR_HandGestureType prevStaticGestureLeft = WVR_HandGestureType.WVR_HandGestureType_Invalid;
	private WVR_HandGestureType currStaticGestureLeft = WVR_HandGestureType.WVR_HandGestureType_Invalid;
	private WVR_HandGestureType prevStaticGestureRight = WVR_HandGestureType.WVR_HandGestureType_Invalid;
	private WVR_HandGestureType currStaticGestureRight = WVR_HandGestureType.WVR_HandGestureType_Invalid;

	#region MonoBehaviour Overrides
	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Start()
	{
		preEnableHandGesture = this.EnableHandGesture;
		if (this.EnableHandGesture)
		{
			DEBUG("Start() Start hand gesture.");
			StartHandGesture();
		}

		preEnableHandTracking = this.EnableHandTracking;
		if (this.EnableHandTracking)
		{
			DEBUG("Start() Start hand tracking.");
			StartHandTracking();
		}
	}

	void Update()
	{
		if (preEnableHandGesture != this.EnableHandGesture)
		{
			preEnableHandGesture = this.EnableHandGesture;
			if (this.EnableHandGesture)
			{
				DEBUG("Update() Start hand gesture.");
				StartHandGesture();
			}
			if (!this.EnableHandGesture)
			{
				DEBUG("Update() Stop hand gesture.");
				StopHandGesture();
			}
		}

		if (preEnableHandTracking != this.EnableHandTracking)
		{
			preEnableHandTracking = this.EnableHandTracking;
			if (this.EnableHandTracking)
			{
				DEBUG("Update() Start hand tracking.");
				StartHandTracking();
			}
			if (!this.EnableHandTracking)
			{
				DEBUG("Update() Stop hand tracking.");
				StopHandTracking();
			}
		}

		if (this.EnableHandGesture)
		{
			GetHandGestureData(ref handGestureData);
			if (hasHandGestureData)
			{
				UpdateLeftHandGestureData(handGestureData);
				UpdateRightHandGestureData(handGestureData);
			}
		}

		if (this.EnableHandTracking)
			GetHandTrackingData();
	}

	void OnEnable()
	{
		WaveVR_Utils.Event.Listen (WaveVR_Utils.Event.ALL_VREVENT, OnEvent);
	}

	void OnDisable()
	{
		WaveVR_Utils.Event.Remove (WaveVR_Utils.Event.ALL_VREVENT, OnEvent);
	}
	#endregion

	private void UpdateLeftHandGestureData(WVR_HandGestureData_t data)
	{
		prevStaticGestureLeft = currStaticGestureLeft;
		currStaticGestureLeft = data.left;

		if (currStaticGestureLeft != prevStaticGestureLeft)
		{
			DEBUG ("UpdateLeftHandGestureData() Receives " + currStaticGestureLeft);
			WaveVR_Utils.Event.Send (WaveVR_Utils.Event.HAND_STATIC_GESTURE_LEFT, currStaticGestureLeft);
		}
	}

	public ulong GetCurrentLeftHandStaticGesture()
	{
		ulong gesture_value = 0;
		switch (currStaticGestureLeft)
		{
			case WVR_HandGestureType.WVR_HandGestureType_Fist:
				gesture_value = (ulong)EStaticGestures.FIST;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_Five:
				gesture_value = (ulong)EStaticGestures.FIVE;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_IndexUp:
				gesture_value = (ulong)EStaticGestures.INDEXUP;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_ThumbUp:
				gesture_value = (ulong)EStaticGestures.THUMBUP;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_OK:
				gesture_value = (ulong)EStaticGestures.OK;
				break;
			default:
				break;
		}
		return gesture_value;
	}

	private void UpdateRightHandGestureData(WVR_HandGestureData_t data)
	{
		prevStaticGestureRight = currStaticGestureRight;
		currStaticGestureRight = data.right;

		if (currStaticGestureRight != prevStaticGestureRight)
		{
			DEBUG ("UpdateLeftHandGestureData() Receives " + currStaticGestureRight);
			WaveVR_Utils.Event.Send (WaveVR_Utils.Event.HAND_STATIC_GESTURE_RIGHT, currStaticGestureRight);
		}
	}

	public ulong GetCurrentRightHandStaticGesture()
	{
		ulong gesture_value = 0;
		switch (currStaticGestureRight)
		{
			case WVR_HandGestureType.WVR_HandGestureType_Fist:
				gesture_value = (ulong)EStaticGestures.FIST;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_Five:
				gesture_value = (ulong)EStaticGestures.FIVE;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_IndexUp:
				gesture_value = (ulong)EStaticGestures.INDEXUP;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_ThumbUp:
				gesture_value = (ulong)EStaticGestures.THUMBUP;
				break;
			case WVR_HandGestureType.WVR_HandGestureType_OK:
				gesture_value = (ulong)EStaticGestures.OK;
				break;
			default:
				break;
		}
		return gesture_value;
	}

	void OnEvent(params object[] args)
	{
		WVR_Event_t event_t = (WVR_Event_t)args [0];
		switch (event_t.common.type)
		{
		case WVR_EventType.WVR_EventType_HandGesture_Abnormal:
			DEBUG ("OnEvent() WVR_EventType_HandGesture_Abnormal, restart the hand gesture component.");
			RestartHandGesture ();
			break;
		case WVR_EventType.WVR_EventType_HandTracking_Abnormal:
			DEBUG ("OnEvent() WVR_EventType_HandTracking_Abnormal, restart the hand tracking component.");
			RestartHandTracking ();
			break;
		default:
			break;
		}
	}

	#region Hand Gesture Lifecycle
	private WaveVR_Utils.HandGestureStatus handGestureStatus = WaveVR_Utils.HandGestureStatus.NOT_START;
	private static ReaderWriterLockSlim handGestureStatusRWLock = new ReaderWriterLockSlim ();
	private void SetHandGestureStatus(WaveVR_Utils.HandGestureStatus status)
	{
		try
		{
			handGestureStatusRWLock.TryEnterWriteLock(2000);
			handGestureStatus = status;
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, "SetHandGestureStatus() " + e.Message, true);
			throw;
		}
		finally
		{
			handGestureStatusRWLock.ExitWriteLock();
		}
	}

	public WaveVR_Utils.HandGestureStatus GetHandGestureStatus()
	{
		ulong feature = WaveVR.Instance.GetSupportedFeatures();
		if ((feature & (ulong)WVR_SupportedFeature.WVR_SupportedFeature_HandGesture) == 0)
			return WaveVR_Utils.HandGestureStatus.UNSUPPORT;

		try
		{
			handGestureStatusRWLock.TryEnterReadLock(2000);
			return handGestureStatus;
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, "GetHandGestureStatus() " + e.Message, true);
			throw;
		}
		finally
		{
			handGestureStatusRWLock.ExitReadLock();
		}
	}

	private object handGestureThreadLock = new object();
	private event WaveVR_Utils.HandGestureResultDelegate handGestureResultCB = null;
	private void StartHandGestureLock()
	{
		bool result = false;

		if (WaveVR.Instance.IsHandGestureEnabled ())
		{
			result = true;
			SetHandGestureStatus (WaveVR_Utils.HandGestureStatus.AVAILABLE);
		}

		WaveVR_Utils.HandGestureStatus status = GetHandGestureStatus ();
		if (this.EnableHandGesture &&
			(
				status == WaveVR_Utils.HandGestureStatus.NOT_START ||
				status == WaveVR_Utils.HandGestureStatus.START_FAILURE
			)
		)
		{
			SetHandGestureStatus (WaveVR_Utils.HandGestureStatus.STARTING);
			result = WaveVR.Instance.StartHandGesture ();
			SetHandGestureStatus (result ? WaveVR_Utils.HandGestureStatus.AVAILABLE : WaveVR_Utils.HandGestureStatus.START_FAILURE);
		}

		status = GetHandGestureStatus ();
		DEBUG ("StartHandGestureLock() " + result + ", status: " + status);
		WaveVR_Utils.Event.Send (WaveVR_Utils.Event.HAND_GESTURE_STATUS, status);

		if (handGestureResultCB != null)
		{
			handGestureResultCB (this, result);
			handGestureResultCB = null;
		}
	}

	private void StartHandGestureThread()
	{
		lock(handGestureThreadLock)
		{
			DEBUG ("StartHandGestureThread()");
			StartHandGestureLock ();
		}
	}

	private void StartHandGesture()
	{
		Thread hand_gesture_t = new Thread (StartHandGestureThread);
		hand_gesture_t.Start ();
	}

	private void StopHandGestureLock()
	{
		if (!WaveVR.Instance.IsHandGestureEnabled ())
			SetHandGestureStatus (WaveVR_Utils.HandGestureStatus.NOT_START);

		WaveVR_Utils.HandGestureStatus status = GetHandGestureStatus ();
		if (status == WaveVR_Utils.HandGestureStatus.AVAILABLE)
		{
			DEBUG ("StopHandGestureLock()");
			SetHandGestureStatus (WaveVR_Utils.HandGestureStatus.STOPING);
			WaveVR.Instance.StopHandGesture ();
			SetHandGestureStatus (WaveVR_Utils.HandGestureStatus.NOT_START);
			hasHandGestureData = false;
		}

		status = GetHandGestureStatus ();
		WaveVR_Utils.Event.Send (WaveVR_Utils.Event.HAND_GESTURE_STATUS, status);
	}

	private void StopHandGestureThread()
	{
		lock(handGestureThreadLock)
		{
			DEBUG ("StopHandGestureThread()");
			StopHandGestureLock ();
		}
	}

	private void StopHandGesture()
	{
		Thread hand_gesture_t = new Thread (StopHandGestureThread);
		hand_gesture_t.Start ();
	}

	private void RestartHandGestureThread()
	{
		lock (handGestureThreadLock)
		{
			DEBUG ("RestartHandGestureThread()");
			StopHandGestureLock ();
			StartHandGestureLock ();
		}
	}

	public void RestartHandGesture()
	{
		Thread hand_gesture_t = new Thread (RestartHandGestureThread);
		hand_gesture_t.Start ();
	}

	public void RestartHandGesture(WaveVR_Utils.HandGestureResultDelegate callback)
	{
		if (handGestureResultCB == null)
			handGestureResultCB = callback;
		else
			handGestureResultCB += callback;

		RestartHandGesture ();
	}

	private void GetHandGestureData(ref WVR_HandGestureData_t data)
	{
		WaveVR_Utils.HandGestureStatus status = GetHandGestureStatus ();
		if (status == WaveVR_Utils.HandGestureStatus.AVAILABLE)
			hasHandGestureData = WaveVR.Instance.GetHandGestureData (ref data);
	}
	#endregion

	#region Hand Tracking Lifecycle
	private WaveVR_Utils.HandTrackingStatus handTrackingStatus = WaveVR_Utils.HandTrackingStatus.NOT_START;
	private static ReaderWriterLockSlim handTrackingStatusRWLock = new ReaderWriterLockSlim ();
	private void SetHandTrackingStatus(WaveVR_Utils.HandTrackingStatus status)
	{
		try
		{
			handTrackingStatusRWLock.TryEnterWriteLock(2000);
			handTrackingStatus = status;
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, "SetHandTrackingStatus() " + e.Message, true);
			throw;
		}
		finally
		{
			handTrackingStatusRWLock.ExitWriteLock();
		}
	}

	public WaveVR_Utils.HandTrackingStatus GetHandTrackingStatus()
	{
		ulong feature = WaveVR.Instance.GetSupportedFeatures();
		if ((feature & (ulong)WVR_SupportedFeature.WVR_SupportedFeature_HandTracking) == 0)
			return WaveVR_Utils.HandTrackingStatus.UNSUPPORT;

		try
		{
			handTrackingStatusRWLock.TryEnterReadLock(2000);
			return handTrackingStatus;
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, "GetHandTrackingStatus() " + e.Message, true);
			throw;
		}
		finally
		{
			handTrackingStatusRWLock.ExitReadLock();
		}
	}

	private object handTrackingThreadLocker = new object();
	private event WaveVR_Utils.HandTrackingResultDelegate handTrackingResultCB = null;
	private void StartHandTrackingLock()
	{
		bool result = false;

		if (WaveVR.Instance.IsHandTrackingEnabled ())
		{
			result = true;
			SetHandTrackingStatus (WaveVR_Utils.HandTrackingStatus.AVAILABLE);
		}

		WaveVR_Utils.HandTrackingStatus status = GetHandTrackingStatus ();
		if (this.EnableHandTracking &&
			(
				status == WaveVR_Utils.HandTrackingStatus.NOT_START ||
				status == WaveVR_Utils.HandTrackingStatus.START_FAILURE
			)
		)
		{
			SetHandTrackingStatus (WaveVR_Utils.HandTrackingStatus.STARTING);
			result = WaveVR.Instance.StartHandTracking ();
			SetHandTrackingStatus (result ? WaveVR_Utils.HandTrackingStatus.AVAILABLE : WaveVR_Utils.HandTrackingStatus.START_FAILURE);
		}

		status = GetHandTrackingStatus ();
		DEBUG ("StartHandTrackingLock() " + result + ", status: " + status);
		WaveVR_Utils.Event.Send (WaveVR_Utils.Event.HAND_TRACKING_STATUS, status);

		if (handTrackingResultCB != null)
		{
			handTrackingResultCB (this, result);
			handTrackingResultCB = null;
		}
	}

	private void StartHandTrackingThread()
	{
		lock(handTrackingThreadLocker)
		{
			DEBUG ("StartHandTrackingThread()");
			StartHandTrackingLock ();
		}
	}

	private void StartHandTracking()
	{
		Thread hand_tracking_t = new Thread (StartHandTrackingThread);
		hand_tracking_t.Start ();
	}

	private void StopHandTrackingLock()
	{
		if (!WaveVR.Instance.IsHandTrackingEnabled ())
			SetHandTrackingStatus (WaveVR_Utils.HandTrackingStatus.NOT_START);

		WaveVR_Utils.HandTrackingStatus status = GetHandTrackingStatus ();
		if (status == WaveVR_Utils.HandTrackingStatus.AVAILABLE)
		{
			DEBUG ("StopHandTrackingLock()");
			SetHandTrackingStatus (WaveVR_Utils.HandTrackingStatus.STOPING);
			WaveVR.Instance.StopHandTracking ();
			SetHandTrackingStatus (WaveVR_Utils.HandTrackingStatus.NOT_START);
			hasHandTrackingData = false;
		}

		status = GetHandTrackingStatus ();
		WaveVR_Utils.Event.Send (WaveVR_Utils.Event.HAND_TRACKING_STATUS, status);
	}

	private void StopHandTrackingThread()
	{
		lock(handTrackingThreadLocker)
		{
			DEBUG ("StopHandTrackingThread()");
			StopHandTrackingLock ();
		}
	}

	private void StopHandTracking()
	{
		Thread hand_tracking_t = new Thread (StopHandTrackingThread);
		hand_tracking_t.Start ();
	}

	private void RestartHandTrackingThread()
	{
		lock (handTrackingThreadLocker)
		{
			DEBUG ("RestartHandTrackingThread()");
			StopHandTrackingLock ();
			StartHandTrackingLock ();
		}
	}

	public void RestartHandTracking()
	{
		Thread hand_tracking_t = new Thread (RestartHandTrackingThread);
		hand_tracking_t.Start ();
	}

	public void RestartHandTracking(WaveVR_Utils.HandTrackingResultDelegate callback)
	{
		if (handTrackingResultCB == null)
			handTrackingResultCB = callback;
		else
			handTrackingResultCB += callback;

		RestartHandTracking ();
	}

	private bool hasHandTrackingData = false;
	private WVR_HandSkeletonData_t handSkeletonData = new WVR_HandSkeletonData_t();
	private WVR_HandPoseData_t handPoseData = new WVR_HandPoseData_t();
	public void GetHandTrackingData()
	{
		WaveVR_Utils.HandTrackingStatus status = GetHandTrackingStatus ();
		if (status == WaveVR_Utils.HandTrackingStatus.AVAILABLE)
			hasHandTrackingData = WaveVR.Instance.GetHandTrackingData (ref handSkeletonData, ref handPoseData, WaveVR_Render.Instance.origin);
	}

	public bool GetHandSkeletonData(ref WVR_HandSkeletonData_t skeleton)
	{
		skeleton = handSkeletonData;
		return hasHandTrackingData;
	}

	public bool GetHandPoseData(ref WVR_HandPoseData_t pose)
	{
		pose = handPoseData;
		return hasHandTrackingData;
	}

	public float GetHandConfidence(EGestureHand hand)
	{
		if (hasHandTrackingData)
		{
			if (hand == EGestureHand.LEFT)
				return handSkeletonData.left.confidence;
			if (hand == EGestureHand.RIGHT)
				return handSkeletonData.right.confidence;
		}
		return 0;
	}

	public bool IsHandPoseValid(EGestureHand hand)
	{
		if (hasHandTrackingData)
		{
			if (hand == EGestureHand.LEFT)
				return (handSkeletonData.left.confidence > 0.1f);
			if (hand == EGestureHand.RIGHT)
				return (handSkeletonData.right.confidence > 0.1f);
		}
		return false;
	}
	#endregion
}
