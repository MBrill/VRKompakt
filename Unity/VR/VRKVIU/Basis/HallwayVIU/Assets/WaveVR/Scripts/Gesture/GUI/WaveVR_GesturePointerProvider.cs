// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections.Generic;
using UnityEngine;
using WVR_Log;

public class WaveVR_GesturePointerProvider {
	private const string LOG_TAG = "WaveVR_GesturePointerProvider";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d (LOG_TAG, msg, true);
	}

	private class GesturePointer
	{
		public WaveVR_GestureManager.EGestureHand Hand { get; set; }
		public GameObject Pointer { get; set; }

		public GesturePointer(WaveVR_GestureManager.EGestureHand type, GameObject pointer)
		{
			Hand = type;
			Pointer = pointer;
		}
	}
	private List<GesturePointer> gesturePointers = new List<GesturePointer>();
	public static readonly WaveVR_GestureManager.EGestureHand[] GestureHandList = new WaveVR_GestureManager.EGestureHand[] {
		WaveVR_GestureManager.EGestureHand.RIGHT,
		WaveVR_GestureManager.EGestureHand.LEFT
	};

	private static  WaveVR_GesturePointerProvider instance = null;
	public static WaveVR_GesturePointerProvider Instance
	{
		get {
			if (instance == null)
				instance = new WaveVR_GesturePointerProvider ();
			return instance;
		}
	}

	private WaveVR_GesturePointerProvider(){
		for (int i = 0; i < GestureHandList.Length; i++)
			gesturePointers.Add (new GesturePointer (GestureHandList [i], null));
	}

	public void SetGesturePointer(WaveVR_GestureManager.EGestureHand hand, GameObject pointer)
	{
		DEBUG ("SetGesturePointer() " + hand + ", pointer: " + (pointer != null ? pointer.name : "null"));

		for (int i = 0; i < GestureHandList.Length; i++)
		{
			if (GestureHandList [i] == hand)
			{
				gesturePointers [i].Pointer = pointer;
				break;
			}
		}
	}

	public GameObject GetGesturePointer(WaveVR_GestureManager.EGestureHand hand)
	{
		int index = 0;
		for (int i = 0; i < GestureHandList.Length; i++)
		{
			if (GestureHandList [i] == hand)
			{
				index = i;
				break;
			}
		}

		return gesturePointers [index].Pointer;
	}
}
