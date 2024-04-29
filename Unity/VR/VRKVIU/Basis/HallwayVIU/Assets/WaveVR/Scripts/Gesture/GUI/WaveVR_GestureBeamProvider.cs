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

public class WaveVR_GestureBeamProvider
{
	private const string LOG_TAG = "WaveVR_GestureBeamProvider";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d(LOG_TAG, msg, true);
	}

	private class GestureBeam
	{
		public WaveVR_GestureManager.EGestureHand Hand { get; set; }
		public GameObject Beam { get; set; }

		public GestureBeam(WaveVR_GestureManager.EGestureHand type, GameObject beam)
		{
			Hand = type;
			Beam = beam;
		}
	}
	private List<GestureBeam> gestureBeams = new List<GestureBeam>();
	public static readonly WaveVR_GestureManager.EGestureHand[] GestureHandList = new WaveVR_GestureManager.EGestureHand[] {
		WaveVR_GestureManager.EGestureHand.RIGHT,
		WaveVR_GestureManager.EGestureHand.LEFT
	};

	private static WaveVR_GestureBeamProvider instance = null;
	public static WaveVR_GestureBeamProvider Instance
	{
		get
		{
			if (instance == null)
				instance = new WaveVR_GestureBeamProvider();
			return instance;
		}
	}

	private WaveVR_GestureBeamProvider()
	{
		for (int i = 0; i < GestureHandList.Length; i++)
			gestureBeams.Add(new GestureBeam(GestureHandList[i], null));
	}

	public void SetGestureBeam(WaveVR_GestureManager.EGestureHand hand, GameObject beam)
	{
		DEBUG("SetGestureBeam() " + hand + ", beam: " + (beam != null ? beam.name : "null"));

		for (int i = 0; i < GestureHandList.Length; i++)
		{
			if (GestureHandList[i] == hand)
			{
				gestureBeams[i].Beam = beam;
				break;
			}
		}
	}

	public GameObject GetGestureBeam(WaveVR_GestureManager.EGestureHand hand)
	{
		int index = 0;
		for (int i = 0; i < GestureHandList.Length; i++)
		{
			if (GestureHandList[i] == hand)
			{
				index = i;
				break;
			}
		}

		return gestureBeams[index].Beam;
	}
}
