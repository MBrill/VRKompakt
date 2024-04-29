// "Wave SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;

public abstract class IWaveVR_BonePose : MonoBehaviour
{
	private static WaveVR_BonePoseImpl instance = null;
	public static WaveVR_BonePoseImpl Instance
	{
		get
		{
			if (instance == null)
				instance = new WaveVR_BonePoseImpl();
			return instance;
		}
	}

	//private static WVR_HandTrackingData_t handTrackingData = new WVR_HandTrackingData_t();
	void OnEnable()
	{
		if (instance == null)
			instance = new WaveVR_BonePoseImpl ();
	}

	public WaveVR_Utils.RigidTransform GetBoneTransform(WaveVR_BonePoseImpl.Bones bone_type)
	{
		return Instance.GetBoneTransform (bone_type);
	}

	public WaveVR_Utils.RigidTransform GetBoneTransform(int index, bool isLeft)
	{
		return Instance.GetBoneTransform(index, isLeft);
	}

	public bool IsBonePoseValid(WaveVR_BonePoseImpl.Bones bone_type)
	{
		return Instance.IsBonePoseValid (bone_type);
	}

	public bool IsHandPoseValid(WaveVR_GestureManager.EGestureHand hand)
	{
		return Instance.IsHandPoseValid (hand);
	}

	public float GetBoneConfidence(WaveVR_BonePoseImpl.Bones bone_type)
	{
		return Instance.GetBoneConfidence(bone_type);
	}

	public float GetHandConfidence(WaveVR_GestureManager.EGestureHand hand)
	{
		return Instance.GetHandConfidence(hand);
	}
}
