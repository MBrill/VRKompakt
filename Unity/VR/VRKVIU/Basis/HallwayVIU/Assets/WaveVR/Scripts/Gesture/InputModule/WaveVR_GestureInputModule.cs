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
using UnityEngine.EventSystems;
using wvr;
using WVR_Log;
using UnityEngine.UI;
using System;

[DisallowMultipleComponent]
public class WaveVR_GestureInputModule : BaseInputModule
{
	private const string LOG_TAG = "WaveVR_GestureInputModule";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d (LOG_TAG, msg, true);
	}
	private void INFO(string msg)
	{
		Log.i (LOG_TAG, msg, true);
	}


	#region Customized Variables
	[Tooltip("If not selected, no events will be sent.")]
	public bool EnableEvent = true;
	public GameObject RightPinchSelector = null;
	public GameObject LeftPinchSelector = null;
	[SerializeField]
	[Tooltip("The threshold of pinch on.")]
	[Range(0.5f, 1)]
	private float m_PinchOnThreshold = 0.7f;
	public float PinchOnThreshold { get { return m_PinchOnThreshold; } set { m_PinchOnThreshold = value; } }
	[SerializeField]
	[Tooltip("After this pinch on interval, start dragging.")]
	private float m_DragInterval = 1.0f;
	public float DragInterval { get { return m_DragInterval; } set { m_DragInterval = value; } }
	[SerializeField]
	[Range(0.5f, 1)]
	[Tooltip("The threshold of pinch off.")]
	private float m_PinchOffThreshold = 0.7f;
	public float PinchOffThreshold { get { return m_PinchOffThreshold; } set { m_PinchOffThreshold = value; } }
	#endregion


	private WVR_HandPoseType currentGestureRight = WVR_HandPoseType.WVR_HandPoseType_Invalid;
	private WVR_HandPoseType currentGestureLeft = WVR_HandPoseType.WVR_HandPoseType_Invalid;
	private void ActivateBeamPointer(WaveVR_GestureManager.EGestureHand hand, bool active)
	{
		GameObject beam = WaveVR_GestureBeamProvider.Instance.GetGestureBeam(hand);
		if (beam != null && beam.GetComponent<WaveVR_GestureBeam>() != null)
			beam.GetComponent<WaveVR_GestureBeam>().ShowBeam = active;

		GameObject pointer = WaveVR_GesturePointerProvider.Instance.GetGesturePointer(hand);
		if (pointer != null && pointer.GetComponent<WaveVR_GesturePointer>() != null)
			pointer.GetComponent<WaveVR_GesturePointer>().ShowPointer = active;
	}

	private GameObject m_TrackerObject = null;
	private WaveVR_GesturePointerTracker gesturePointerTracker = null;
	private GameObject beamObject = null;
	private WaveVR_GestureBeam gestureBeam = null;
	private GameObject pointerObject = null;
	private WaveVR_GesturePointer gesturePointer = null;
	private Camera eventCamera = null;
	private PhysicsRaycaster pointerPhysicsRaycaster = null;
	private bool ValidateParameters()
	{
		if (!this.EnableEvent)
		{
			ActivateBeamPointer(WaveVR_GestureManager.EGestureHand.RIGHT, false);
			ActivateBeamPointer(WaveVR_GestureManager.EGestureHand.LEFT, false);
			return false;
		}

		// Validates the pinch on/off threshold.
		if (m_PinchOffThreshold > m_PinchOnThreshold)
			m_PinchOffThreshold = m_PinchOnThreshold;

		// Validates the beam and pointer.
		GameObject new_beam = WaveVR_GestureBeamProvider.Instance.GetGestureBeam(WaveVR_GestureManager.GestureFocusHand);
		if (new_beam != null && !ReferenceEquals(beamObject, new_beam))
		{
			beamObject = new_beam;
			gestureBeam = beamObject.GetComponent<WaveVR_GestureBeam>();
		}
		if (beamObject == null)
			gestureBeam = null;

		GameObject new_pointer = WaveVR_GesturePointerProvider.Instance.GetGesturePointer(WaveVR_GestureManager.GestureFocusHand);
		if (new_pointer != null && !GameObject.ReferenceEquals(pointerObject, new_pointer))
		{
			pointerObject = new_pointer;
			gesturePointer = pointerObject.GetComponent<WaveVR_GesturePointer>();
		}
		if (pointerObject == null)
			gesturePointer = null;

		if (gestureBeam == null || gesturePointer == null)
		{
			if (Log.gpl.Print)
			{
				if (gestureBeam == null)
					Log.i(LOG_TAG, "ValidateParameters() No beam of " + WaveVR_GestureManager.GestureFocusHand, true);
				if (gesturePointer == null)
					Log.i(LOG_TAG, "ValidateParameters() No pointer of " + WaveVR_GestureManager.GestureFocusHand, true);
			}
			return false;
		}

		// Validates the camera and physicsRaycaster.
		if (gesturePointerTracker != null)
		{
			if (eventCamera == null)
				eventCamera = gesturePointerTracker.GetPointerTrackerCamera();
			if (pointerPhysicsRaycaster == null)
				pointerPhysicsRaycaster = gesturePointerTracker.GetPhysicsRaycaster();
		}

		if (eventCamera == null)
		{
			if (Log.gpl.Print)
			{
				if (eventCamera == null)
					Log.i(LOG_TAG, "ValidateParameters() Forget to put GesturePointerTracker??", true);
			}
			return false;
		}

		return true;
	}

	private PointerEventData mPointerEventData = null;
	private void ResetPointerEventData()
	{
		if (mPointerEventData == null)
		{
			mPointerEventData = new PointerEventData (eventSystem);
			mPointerEventData.pointerCurrentRaycast = new RaycastResult ();
		}

		mPointerEventData.Reset ();
		mPointerEventData.position = new Vector2 (0.5f * eventCamera.pixelWidth, 0.5f * eventCamera.pixelHeight); // center of screen
		firstRaycastResult.Clear();
		mPointerEventData.pointerCurrentRaycast = firstRaycastResult;
	}

	private GameObject prevRaycastedObject = null;
	private GameObject GetRaycastedObject()
	{
		if (mPointerEventData == null)
			return null;

		return mPointerEventData.pointerCurrentRaycast.gameObject;
	}

	private Vector3 GetIntersectionPosition(RaycastResult raycastResult)
	{
		if (eventCamera == null)
			return Vector3.zero;

		float intersectionDistance = raycastResult.distance + eventCamera.nearClipPlane;
		Vector3 intersectionPosition = eventCamera.transform.forward * intersectionDistance + eventCamera.transform.position;
		return intersectionPosition;
	}


	#region BaseInputModule Overrides
	private bool mInputModuleEnabled = false;
	protected override void OnEnable()
	{
		if (!mInputModuleEnabled)
		{
			base.OnEnable ();
			DEBUG ("OnEnable()");

			Destroy(GetComponent<StandaloneInputModule>());
			if (WaveVR_GesturePointerTracker.Instance == null)
			{
				if (WaveVR_Render.Instance != null)
				{
					m_TrackerObject = new GameObject("GesturePointerTracker");
					m_TrackerObject.transform.SetParent(WaveVR_Render.Instance.gameObject.transform, false);
					m_TrackerObject.transform.localPosition = Vector3.zero;
					gesturePointerTracker = m_TrackerObject.AddComponent<WaveVR_GesturePointerTracker>();
				}
			}
			else
			{
				gesturePointerTracker = WaveVR_GesturePointerTracker.Instance;
			}

			mInputModuleEnabled = true;
		}
	}

	protected override void OnDisable()
	{
		if (mInputModuleEnabled)
		{
			base.OnDisable ();
			DEBUG ("OnDisable()");

			mInputModuleEnabled = false;
		}
	}

	private Quaternion toRotation = Quaternion.identity;
	private void RotateSelector(GameObject selector, Quaternion fromRotation)
	{
		if (WaveVR_GestureManager.GestureFocusHand == WaveVR_GestureManager.EGestureHand.RIGHT)
			toRotation = IWaveVR_BonePose.Instance.GetBoneTransform(WaveVR_BonePoseImpl.Bones.RIGHT_WRIST).rot * Quaternion.Inverse(fromRotation);
		else
			toRotation = IWaveVR_BonePose.Instance.GetBoneTransform(WaveVR_BonePoseImpl.Bones.LEFT_WRIST).rot * Quaternion.Inverse(fromRotation);

		selector.transform.rotation *= toRotation;
	}
	private void MoveSelector(GameObject selector, Vector3 offset)
	{
		if (WaveVR_GestureManager.GestureFocusHand == WaveVR_GestureManager.EGestureHand.RIGHT)
			selector.transform.position = IWaveVR_BonePose.Instance.GetBoneTransform(WaveVR_BonePoseImpl.Bones.RIGHT_WRIST).pos + offset;
		if (WaveVR_GestureManager.GestureFocusHand == WaveVR_GestureManager.EGestureHand.LEFT)
			selector.transform.position = IWaveVR_BonePose.Instance.GetBoneTransform(WaveVR_BonePoseImpl.Bones.LEFT_WRIST).pos + offset;
	}

	private bool hasHandPoseData = false;
	private WVR_HandPoseData_t handPoseData = new WVR_HandPoseData_t();
	private bool enableEvent = false;
	private bool isPinch = false;

	private const uint PINCH_FRAME_COUNT = 10;
	private uint pinchFrame = 0, unpinchFrame = 0;
	//private float positionLerp = 0.1f, rotationLerp = 0.1f;
	public override void Process()
	{
		if (!ValidateParameters ())
			return;

		// Save previous raycasted object.
		prevRaycastedObject = GetRaycastedObject ();


		// ------------------- Raycast Actions begins -------------------
		if ((mPointerEventData == null) ||
			(mPointerEventData != null && !mPointerEventData.dragging))
		{
			ResetPointerEventData();
			GraphicRaycast();
			PhysicsRaycast();
		}
		// ------------------- Raycast Actions ends -------------------


		GameObject curr_raycasted_object = GetRaycastedObject ();


		// ------------------- Check if pinching begins -------------------
		hasHandPoseData = WaveVR_GestureManager.Instance.GetHandPoseData(ref handPoseData);
		if (hasHandPoseData)
		{
			currentGestureRight = handPoseData.right.state.type;
			currentGestureLeft = handPoseData.left.state.type;
		}
		else
		{
			currentGestureRight = WVR_HandPoseType.WVR_HandPoseType_Invalid;
			currentGestureLeft = WVR_HandPoseType.WVR_HandPoseType_Invalid;
		}

		if (WaveVR_GestureManager.GestureFocusHand == WaveVR_GestureManager.EGestureHand.RIGHT)
		{
			enableEvent = (WaveVR_Utils.GetPosition(handPoseData.right.pinch.origin) != Vector3.zero);

			// Switch the focus hand to left.
			if ((currentGestureLeft == WVR_HandPoseType.WVR_HandPoseType_Pinch) && (handPoseData.left.pinch.strength >= m_PinchOnThreshold))
			{
				WaveVR_GestureManager.GestureFocusHand = WaveVR_GestureManager.EGestureHand.LEFT;
				return;
			}

			if (hasHandPoseData && (this.RightPinchSelector != null))
			{
				//this.RightPinchSelector.transform.position = Vector3.Lerp(this.RightPinchSelector.transform.position, WaveVR_Utils.GetPosition(handPoseData.right.pinch.origin), positionLerp);
				//this.RightPinchSelector.transform.rotation = Quaternion.Lerp(this.RightPinchSelector.transform.rotation, Quaternion.LookRotation(WaveVR_Utils.GetPosition(handPoseData.right.pinch.direction)), rotationLerp);
				this.RightPinchSelector.transform.position = WaveVR_Utils.GetPosition(handPoseData.right.pinch.origin);
				this.RightPinchSelector.transform.rotation = Quaternion.LookRotation(WaveVR_Utils.GetPosition(handPoseData.right.pinch.direction));
			}

			if (!isPinch)
			{
				if ((currentGestureRight == WVR_HandPoseType.WVR_HandPoseType_Pinch) && (handPoseData.right.pinch.strength >= m_PinchOnThreshold))
				{
					pinchFrame++;
					if (pinchFrame > PINCH_FRAME_COUNT)
					{
						isPinch = true;
						gestureBeam.SetEffectiveBeam(true);
						gesturePointer.SetEffectivePointer(true);
						unpinchFrame = 0;
					}
				}
			}
			else
			{
				if ((currentGestureRight != WVR_HandPoseType.WVR_HandPoseType_Pinch) || (handPoseData.right.pinch.strength < m_PinchOffThreshold))
				{
					unpinchFrame++;
					if (unpinchFrame > PINCH_FRAME_COUNT)
					{
						DEBUG("Pinch is released. currentGestureRight: " + currentGestureRight + ", strength: " + handPoseData.right.pinch.strength);
						isPinch = false;
						gestureBeam.SetEffectiveBeam(false);
						gesturePointer.SetEffectivePointer(false);
						pinchFrame = 0;
					}
				}
			}
		}
		else // GestureFocusHand == LEFT
		{
			enableEvent = (WaveVR_Utils.GetPosition(handPoseData.left.pinch.origin) != Vector3.zero);

			// Switch the focus hand to right.
			if ((currentGestureRight == WVR_HandPoseType.WVR_HandPoseType_Pinch) && (handPoseData.right.pinch.strength >= m_PinchOnThreshold))
			{
				WaveVR_GestureManager.GestureFocusHand = WaveVR_GestureManager.EGestureHand.RIGHT;
				return;
			}

			if (hasHandPoseData && (this.LeftPinchSelector != null))
			{
				//this.LeftPinchSelector.transform.position = Vector3.Lerp(this.LeftPinchSelector.transform.position, WaveVR_Utils.GetPosition(handPoseData.left.pinch.origin), positionLerp);
				//this.LeftPinchSelector.transform.rotation = Quaternion.Lerp(this.LeftPinchSelector.transform.rotation, Quaternion.LookRotation(WaveVR_Utils.GetPosition(handPoseData.left.pinch.direction)), rotationLerp);
				this.LeftPinchSelector.transform.position = WaveVR_Utils.GetPosition(handPoseData.left.pinch.origin);
				this.LeftPinchSelector.transform.rotation = Quaternion.LookRotation(WaveVR_Utils.GetPosition(handPoseData.left.pinch.direction));
			}

			if (!isPinch)
			{
				if ((currentGestureLeft == WVR_HandPoseType.WVR_HandPoseType_Pinch) && (handPoseData.left.pinch.strength >= m_PinchOnThreshold))
				{
					pinchFrame++;
					if (pinchFrame > PINCH_FRAME_COUNT)
					{
						isPinch = true;
						gestureBeam.SetEffectiveBeam(true);
						gesturePointer.SetEffectivePointer(true);
						unpinchFrame = 0;
					}
				}
			}
			else
			{
				if ((currentGestureLeft != WVR_HandPoseType.WVR_HandPoseType_Pinch) || (handPoseData.left.pinch.strength < m_PinchOffThreshold))
				{
					unpinchFrame++;
					if (unpinchFrame > PINCH_FRAME_COUNT)
					{
						DEBUG("Pinch is released. currentGestureLeft: " + currentGestureLeft + ", strength: " + handPoseData.left.pinch.strength);
						isPinch = false;
						gestureBeam.SetEffectiveBeam(false);
						gesturePointer.SetEffectivePointer(false);
						pinchFrame = 0;
					}
				}
			}
		}
		// ------------------- Check if pinching ends -------------------


		if (WaveVR_GestureManager.GestureFocusHand == WaveVR_GestureManager.EGestureHand.RIGHT)
		{
			ActivateBeamPointer(WaveVR_GestureManager.EGestureHand.LEFT, false);

			bool valid_pose = IWaveVR_BonePose.Instance.IsHandPoseValid(WaveVR_GestureManager.EGestureHand.RIGHT);
			ActivateBeamPointer(WaveVR_GestureManager.EGestureHand.RIGHT, valid_pose);
		}
		else
		{
			ActivateBeamPointer(WaveVR_GestureManager.EGestureHand.RIGHT, false);

			bool valid_pose = IWaveVR_BonePose.Instance.IsHandPoseValid(WaveVR_GestureManager.EGestureHand.LEFT);
			ActivateBeamPointer(WaveVR_GestureManager.EGestureHand.LEFT, valid_pose);
		}

		if (curr_raycasted_object != null)
			gesturePointer.OnPointerEnter(curr_raycasted_object, Vector3.zero, false);
		else
			gesturePointer.OnPointerExit(prevRaycastedObject);


		// ------------------- Event Handling begins -------------------
		if (enableEvent)
		{
			OnGraphicPointerEnterExit();
			OnPhysicsPointerEnterExit();

			OnPointerHover();

			if (!mPointerEventData.eligibleForClick)
			{
				if (isPinch)
					OnPointerDown();
			}
			else if (mPointerEventData.eligibleForClick)
			{
				if (isPinch)
				{
					// Down before, and receives the selected gesture continuously.
					OnPointerDrag();

				}
				else
				{
					DEBUG("Focus hand: " + WaveVR_GestureManager.GestureFocusHand
						+ ", right strength: " + handPoseData.right.pinch.strength
						+ ", left strength: " + handPoseData.left.pinch.strength);
					// Down before, but not receive the selected gesture.
					OnPointerUp();
				}
			}
		}
		// ------------------- Event Handling ends -------------------


		/*Vector3 intersection_position = GetIntersectionPosition (mPointerEventData.pointerCurrentRaycast);
		if (WaveVR_GestureManager.GestureFocusHand == WaveVR_GestureManager.EGestureHand.RIGHT)
			WaveVR_RaycastResultProvider.Instance.SetRaycastResult(WaveVR_Controller.EDeviceType.Dominant, mPointerEventData.pointerCurrentRaycast.gameObject, intersection_position);
		if (WaveVR_GestureManager.GestureFocusHand == WaveVR_GestureManager.EGestureHand.LEFT)
			WaveVR_RaycastResultProvider.Instance.SetRaycastResult(WaveVR_Controller.EDeviceType.NonDominant, mPointerEventData.pointerCurrentRaycast.gameObject, intersection_position);*/
	}
	#endregion

	#region Raycast Actions
	private List<RaycastResult> GetResultList(List<RaycastResult> originList)
	{
		List<RaycastResult> result_list = new List<RaycastResult>();
		for (int i = 0; i < originList.Count; i++)
		{
			if (originList[i].gameObject != null)
				result_list.Add(originList[i]);
		}
		return result_list;
	}

	private RaycastResult SelectRaycastResult(RaycastResult currResult, RaycastResult nextResult)
	{
		if (currResult.gameObject == null)
			return nextResult;
		if (nextResult.gameObject == null)
			return currResult;

		if (currResult.worldPosition == Vector3.zero)
			currResult.worldPosition = GetIntersectionPosition(currResult);

		float curr_distance = (float)Math.Round(Mathf.Abs(currResult.worldPosition.z - currResult.module.eventCamera.transform.position.z), 3);

		if (nextResult.worldPosition == Vector3.zero)
			nextResult.worldPosition = GetIntersectionPosition(nextResult);

		float next_distance = (float)Math.Round(Mathf.Abs(nextResult.worldPosition.z - currResult.module.eventCamera.transform.position.z), 3);

		// 1. Check the distance.
		if (next_distance > curr_distance)
			return currResult;

		if (next_distance < curr_distance)
		{
			DEBUG("SelectRaycastResult() "
				+ nextResult.gameObject.name + ", position: " + nextResult.worldPosition
				+ ", distance: " + next_distance
				+ " is smaller than "
				+ currResult.gameObject.name + ", position: " + currResult.worldPosition
				+ ", distance: " + curr_distance
				);

			return nextResult;
		}

		// 2. Check the "Order in Layer" of the Canvas.
		if (nextResult.sortingOrder > currResult.sortingOrder)
			return nextResult;

		return currResult;
	}

	private RaycastResult m_Result = new RaycastResult();
	private RaycastResult FindFirstResult(List<RaycastResult> resultList)
	{
		m_Result = resultList[0];
		for (int i = 1; i < resultList.Count; i++)
			m_Result = SelectRaycastResult(m_Result, resultList[i]);
		return m_Result;
	}

	private RaycastResult firstRaycastResult = new RaycastResult ();
	private GraphicRaycaster[] graphic_raycasters;
	private List<RaycastResult> graphicRaycastResults = new List<RaycastResult>();
	private List<GameObject> graphicRaycastObjects = new List<GameObject>(), preGraphicRaycastObjects = new List<GameObject>();
	private GameObject raycastTarget = null;

	private void GraphicRaycast()
	{
		if (eventCamera == null)
			return;

		// Find GraphicRaycaster
		graphic_raycasters = GameObject.FindObjectsOfType<GraphicRaycaster>();

		graphicRaycastResults.Clear();
		graphicRaycastObjects.Clear();

		for (int i = 0; i < graphic_raycasters.Length; i++)
		{
			// Ignore the Blocker of Dropdown.
			if (graphic_raycasters[i].gameObject.name.Equals("Blocker"))
				continue;

			// Change the Canvas' event camera.
			if (graphic_raycasters[i].gameObject.GetComponent<Canvas>() != null)
				graphic_raycasters[i].gameObject.GetComponent<Canvas>().worldCamera = eventCamera;
			else
				continue;

			// 1. Get the raycast results list.
			graphic_raycasters[i].Raycast(mPointerEventData, graphicRaycastResults);
			graphicRaycastResults = GetResultList(graphicRaycastResults);
			if (graphicRaycastResults.Count == 0)
				continue;

			// 2. Get the raycast objects list.
			firstRaycastResult = FindFirstResult(graphicRaycastResults);

			//DEBUG ("GraphicRaycast() device: " + event_controller.device + ", camera: " + firstRaycastResult.module.eventCamera + ", first result = " + firstRaycastResult);
			mPointerEventData.pointerCurrentRaycast = SelectRaycastResult(mPointerEventData.pointerCurrentRaycast, firstRaycastResult);
			graphicRaycastResults.Clear();
		} // for (int i = 0; i < graphic_raycasters.Length; i++)

		raycastTarget = mPointerEventData.pointerCurrentRaycast.gameObject;
		while (raycastTarget != null)
		{
			graphicRaycastObjects.Add(raycastTarget);
			raycastTarget = (raycastTarget.transform.parent != null ? raycastTarget.transform.parent.gameObject : null);
		}
	}

	private List<RaycastResult> physicsRaycastResults = new List<RaycastResult>();
	private List<GameObject> physicsRaycastObjects = new List<GameObject> (), prePhysicsRaycastObjects = new List<GameObject>();

	private void PhysicsRaycast()
	{
		if (eventCamera == null || pointerPhysicsRaycaster == null)
			return;

		// Clear cache values.
		physicsRaycastResults.Clear ();
		physicsRaycastObjects.Clear ();

		// Raycasting.
		pointerPhysicsRaycaster.Raycast (mPointerEventData, physicsRaycastResults);
		if (physicsRaycastResults.Count == 0)
			return;

		for (int i = 0; i < physicsRaycastResults.Count; i++)
		{
			// Ignore the GameObject with WaveVR_BonePose component.
			if (physicsRaycastResults [i].gameObject.GetComponent<WaveVR_BonePose> () != null)
				continue;

			physicsRaycastObjects.Add (physicsRaycastResults [i].gameObject);
		}

		firstRaycastResult = FindFirstRaycast (physicsRaycastResults);

		//DEBUG ("PhysicsRaycast() device: " + event_controller.device + ", camera: " + firstRaycastResult.module.eventCamera + ", first result = " + firstRaycastResult);
		mPointerEventData.pointerCurrentRaycast = SelectRaycastResult(mPointerEventData.pointerCurrentRaycast, firstRaycastResult);
	}
	#endregion

	#region Event Handling
	private void OnGraphicPointerEnterExit()
	{
		if (graphicRaycastObjects.Count != 0)
		{
			for (int i = 0; i < graphicRaycastObjects.Count; i++)
			{
				if (graphicRaycastObjects [i] != null && !preGraphicRaycastObjects.Contains (graphicRaycastObjects [i]))
				{
					ExecuteEvents.Execute (graphicRaycastObjects [i], mPointerEventData, ExecuteEvents.pointerEnterHandler);
					DEBUG ("OnGraphicPointerEnterExit() enter: " + graphicRaycastObjects [i]);
				}
			}
		}

		if (preGraphicRaycastObjects.Count != 0)
		{
			for (int i = 0; i < preGraphicRaycastObjects.Count; i++)
			{
				if (preGraphicRaycastObjects [i] != null && !graphicRaycastObjects.Contains (preGraphicRaycastObjects [i]))
				{
					ExecuteEvents.Execute (preGraphicRaycastObjects [i], mPointerEventData, ExecuteEvents.pointerExitHandler);
					DEBUG ("OnGraphicPointerEnterExit() exit: " + preGraphicRaycastObjects [i]);
				}
			}
		}

		CopyList (graphicRaycastObjects, preGraphicRaycastObjects);
	}

	private void OnPhysicsPointerEnterExit()
	{
		if (physicsRaycastObjects.Count != 0)
		{
			for (int i = 0; i < physicsRaycastObjects.Count; i++)
			{
				if (physicsRaycastObjects [i] != null && !prePhysicsRaycastObjects.Contains (physicsRaycastObjects [i]))
				{
					ExecuteEvents.Execute (physicsRaycastObjects [i], mPointerEventData, ExecuteEvents.pointerEnterHandler);
					DEBUG ("OnPhysicsPointerEnterExit() enter: " + physicsRaycastObjects [i]);
				}
			}
		}

		if (prePhysicsRaycastObjects.Count != 0)
		{
			for (int i = 0; i < prePhysicsRaycastObjects.Count; i++)
			{
				if (prePhysicsRaycastObjects [i] != null && !physicsRaycastObjects.Contains (prePhysicsRaycastObjects [i]))
				{
					ExecuteEvents.Execute (prePhysicsRaycastObjects [i], mPointerEventData, ExecuteEvents.pointerExitHandler);
					DEBUG ("OnPhysicsPointerEnterExit() exit: " + prePhysicsRaycastObjects [i]);
				}
			}
		}

		CopyList (physicsRaycastObjects, prePhysicsRaycastObjects);
	}

	private void OnPointerHover()
	{
		GameObject go = GetRaycastedObject ();
		if (go != null && prevRaycastedObject == go)
			ExecuteEvents.ExecuteHierarchy(go, mPointerEventData, WaveVR_ExecuteEvents.pointerHoverHandler);
	}

	private void OnPointerDown()
	{
		GameObject go = GetRaycastedObject ();
		if (go == null) return;

		// Send a Pointer Down event. If not received, get handler of Pointer Click.
		mPointerEventData.pressPosition = mPointerEventData.position;
		mPointerEventData.pointerPressRaycast = mPointerEventData.pointerCurrentRaycast;
		mPointerEventData.pointerPress =
			ExecuteEvents.ExecuteHierarchy(go, mPointerEventData, ExecuteEvents.pointerDownHandler)
			?? ExecuteEvents.GetEventHandler<IPointerClickHandler>(go);

		DEBUG ("OnPointerDown() send Pointer Down to " + mPointerEventData.pointerPress + ", current GameObject is " + go);

		// If Drag Handler exists, send initializePotentialDrag event.
		mPointerEventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(go);
		if (mPointerEventData.pointerDrag != null)
		{
			DEBUG ("OnPointerDown() send initializePotentialDrag to " + mPointerEventData.pointerDrag + ", current GameObject is " + go);
			ExecuteEvents.Execute(mPointerEventData.pointerDrag, mPointerEventData, ExecuteEvents.initializePotentialDrag);
		}

		// Press happened (even not handled) object.
		mPointerEventData.rawPointerPress = go;
		// Allow to send Pointer Click event
		mPointerEventData.eligibleForClick = true;
		// Reset the screen position of press, can be used to estimate move distance
		mPointerEventData.delta = Vector2.zero;
		// Current Down, reset drag state
		mPointerEventData.dragging = false;
		mPointerEventData.useDragThreshold = true;
		// Record the count of Pointer Click should be processed, clean when Click event is sent.
		mPointerEventData.clickCount = 1;
		// Set clickTime to current time of Pointer Down instead of Pointer Click
		// since Down & Up event should not be sent too closely. (< CLICK_TIME)
		mPointerEventData.clickTime = Time.unscaledTime;
	}

	private void OnPointerDrag()
	{
		if (Time.unscaledTime - mPointerEventData.clickTime < m_DragInterval)
			return;
		if (mPointerEventData.pointerDrag == null)
			return;

		if (!mPointerEventData.dragging)
		{
			DEBUG("OnPointerDrag() send BeginDrag to " + mPointerEventData.pointerDrag);
			ExecuteEvents.Execute(mPointerEventData.pointerDrag, mPointerEventData, ExecuteEvents.beginDragHandler);
			mPointerEventData.dragging = true;
		}
		else
		{
			ExecuteEvents.Execute(mPointerEventData.pointerDrag, mPointerEventData, ExecuteEvents.dragHandler);
		}
	}

	private void OnPointerUp()
	{
		GameObject go = GetRaycastedObject ();
		// The "go" may be different with mPointerEventData.pointerDrag so we don't check null.

		if (mPointerEventData.pointerPress != null)
		{
			// In the frame of button is pressed -> unpressed, send Pointer Up
			DEBUG ("OnPointerUp() send Pointer Up to " + mPointerEventData.pointerPress);
			ExecuteEvents.Execute (mPointerEventData.pointerPress, mPointerEventData, ExecuteEvents.pointerUpHandler);
		}

		if (mPointerEventData.eligibleForClick)
		{
			GameObject click_object = ExecuteEvents.GetEventHandler<IPointerClickHandler> (go);
			if (click_object != null)
			{
				if (click_object == mPointerEventData.pointerPress)
				{
					// In the frame of button from being pressed to unpressed, send Pointer Click if Click is pending.
					DEBUG("OnPointerUp() send Pointer Click to " + mPointerEventData.pointerPress);
					ExecuteEvents.Execute(mPointerEventData.pointerPress, mPointerEventData, ExecuteEvents.pointerClickHandler);
				}
				else
				{
					DEBUG("OnTriggerUpMouse() pointer down object " + mPointerEventData.pointerPress + " is different with click object " + click_object);
				}
			}

			if (mPointerEventData.dragging)
			{
				GameObject drop_object = ExecuteEvents.GetEventHandler<IDropHandler> (go);
				if (drop_object == mPointerEventData.pointerDrag)
				{
					// In the frame of button from being pressed to unpressed, send Drop and EndDrag if dragging.
					DEBUG ("OnPointerUp() send Pointer Drop to " + mPointerEventData.pointerDrag);
					ExecuteEvents.Execute (mPointerEventData.pointerDrag, mPointerEventData, ExecuteEvents.dropHandler);
				}

				DEBUG ("OnPointerUp() send Pointer endDrag to " + mPointerEventData.pointerDrag);
				ExecuteEvents.Execute (mPointerEventData.pointerDrag, mPointerEventData, ExecuteEvents.endDragHandler);

				mPointerEventData.pointerDrag = null;
				mPointerEventData.dragging = false;
			}
		}

		// Down object.
		mPointerEventData.pointerPress = null;
		// Press happened (even not handled) object.
		mPointerEventData.rawPointerPress = null;
		// Clear pending state.
		mPointerEventData.eligibleForClick = false;
		// Click event is sent, clear count.
		mPointerEventData.clickCount = 0;
		// Up event is sent, clear the time limitation of Down event.
		mPointerEventData.clickTime = 0;
	}
	#endregion

	private void CopyList(List<GameObject> src, List<GameObject> dst)
	{
		dst.Clear ();
		for (int i = 0; i < src.Count; i++)
			dst.Add (src [i]);
	}
}
