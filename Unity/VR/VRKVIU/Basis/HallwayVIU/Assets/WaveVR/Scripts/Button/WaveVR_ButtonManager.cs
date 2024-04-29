// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

#pragma warning disable 0618

using System.Collections.Generic;
using UnityEngine;
using wvr;
using WVR_Log;

[DisallowMultipleComponent]
public class WaveVR_ButtonManager : MonoBehaviour {
	private static string LOG_TAG = "WaveVR_ButtonManager";
	private void INFO(string msg) { Log.i (LOG_TAG, msg, true); }
	private void DEBUG(string msg) {
		if (Log.EnableDebugLog)
			Log.d (LOG_TAG, msg, true);
	}

	public WaveVR_ButtonList.EButtons GetEButtonsType(WVR_InputId button)
	{
		WaveVR_ButtonList.EButtons btn_type = WaveVR_ButtonList.EButtons.Unavailable;
		switch (button)
		{
			case WVR_InputId.WVR_InputId_Alias1_Menu:
				btn_type = WaveVR_ButtonList.EButtons.Menu;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Grip:
				btn_type = WaveVR_ButtonList.EButtons.Grip;
				break;
			case WVR_InputId.WVR_InputId_Alias1_DPad_Up:
				btn_type = WaveVR_ButtonList.EButtons.DPadUp;
				break;
			case WVR_InputId.WVR_InputId_Alias1_DPad_Right:
				btn_type = WaveVR_ButtonList.EButtons.DPadRight;
				break;
			case WVR_InputId.WVR_InputId_Alias1_DPad_Down:
				btn_type = WaveVR_ButtonList.EButtons.DPadDown;
				break;
			case WVR_InputId.WVR_InputId_Alias1_DPad_Left:
				btn_type = WaveVR_ButtonList.EButtons.DPadLeft;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Volume_Up:
				btn_type = WaveVR_ButtonList.EButtons.VolumeUp;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Volume_Down:
				btn_type = WaveVR_ButtonList.EButtons.VolumeDown;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Bumper:
				btn_type = WaveVR_ButtonList.EButtons.Bumper;
				break;
			case WVR_InputId.WVR_InputId_Alias1_A:
				btn_type = WaveVR_ButtonList.EButtons.A_X;
				break;
			case WVR_InputId.WVR_InputId_Alias1_B:
				btn_type = WaveVR_ButtonList.EButtons.B_Y;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Back:
				btn_type = WaveVR_ButtonList.EButtons.Back;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Enter:
				btn_type = WaveVR_ButtonList.EButtons.Enter;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Touchpad:
				btn_type = WaveVR_ButtonList.EButtons.Touchpad;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Trigger:
				btn_type = WaveVR_ButtonList.EButtons.Trigger;
				break;
			case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
				btn_type = WaveVR_ButtonList.EButtons.Thumbstick;
				break;
			default:
				btn_type = WaveVR_ButtonList.EButtons.Unavailable;
				break;
		}

		return btn_type;
	}

	[System.Serializable]
	public class HmdButtonOption
	{
		public bool Menu = false;
		public bool VolumeUp = false;
		public bool VolumeDown = false;
		public bool Enter = true;
		public bool Back = false;

		public ulong optionValue { get; private set; }
		public void UpdateOptionValue()
		{
			optionValue = 0;
			if (Menu)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Menu;
			if (VolumeUp)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.VolumeUp;
			if (VolumeDown)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.VolumeDown;
			if (Enter)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Enter;
			if (Back)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Back;
		}
	}

	[System.Serializable]
	public class ControllerButtonOption
	{
		public bool Menu = false;
		public bool Grip = false;
		public bool DPadUp = false;
		public bool DPadRight = false;
		public bool DPadDown = false;
		public bool DPadLeft = false;
		public bool Bumper = false;
		public bool A_X = false;
		public bool B_Y = false;
		public bool Touchpad = true;
		public bool Trigger = true;
		public bool Thumbstick = false;

		public ulong optionValue { get; private set; }
		public void UpdateOptionValue()
		{
			optionValue = 0;
			if (Menu)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Menu;
			if (Grip)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Grip;
			if (DPadUp)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.DPadUp;
			if (DPadRight)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.DPadRight;
			if (DPadDown)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.DPadDown;
			if (DPadLeft)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.DPadLeft;
			if (Bumper)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Bumper;
			if (A_X)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.A_X;
			if (B_Y)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.B_Y;
			if (Touchpad)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Touchpad;
			if (Trigger)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Trigger;
			if (Thumbstick)
				optionValue |= 1UL << (int)WaveVR_ButtonList.EButtons.Thumbstick;
		}
	}

	[SerializeField]
	private HmdButtonOption m_HmdOptions = new HmdButtonOption();
	public HmdButtonOption HmdOptions { get { return m_HmdOptions; } set { m_HmdOptions = value; } }

	[SerializeField]
	private ControllerButtonOption m_DominantOptions = new ControllerButtonOption();
	public ControllerButtonOption DominantOptions { get { return m_DominantOptions; } set { m_DominantOptions = value; } }

	[SerializeField]
	private ControllerButtonOption m_NonDominantOptions = new ControllerButtonOption();
	public ControllerButtonOption NonDominantOptions { get { return m_NonDominantOptions; } set { m_NonDominantOptions = value; } }

	private ulong m_HmdOptionValue = 0, m_DominantOptionValue = 0, m_NonDominantOptionValue = 0;
	private void UpdateOptionValues()
	{
		m_HmdOptions.UpdateOptionValue();
		m_DominantOptions.UpdateOptionValue();
		m_NonDominantOptions.UpdateOptionValue();
	}

	private WVR_InputAttribute_t[] inputAttributes_hmd;
	private List<WVR_InputId> usableButtons_hmd = new List<WVR_InputId> ();

	private WVR_InputAttribute_t[] inputAttributes_Dominant;
	private List<WVR_InputId> usableButtons_dominant = new List<WVR_InputId> ();

	private WVR_InputAttribute_t[] inputAttributes_NonDominant;
	private List<WVR_InputId> usableButtons_nonDominant = new List<WVR_InputId> ();

	private const uint inputTableSize = (uint)WVR_InputId.WVR_InputId_Max;
	private WVR_InputMappingPair_t[] inputTableHMD = new WVR_InputMappingPair_t[inputTableSize];
	private uint inputTableHMDSize = 0;
	private WVR_InputMappingPair_t[] inputTableDominant = new WVR_InputMappingPair_t[inputTableSize];
	private uint inputTableDominantSize = 0;
	private WVR_InputMappingPair_t[] inputTableNonDominant = new WVR_InputMappingPair_t[inputTableSize];
	private uint inputTableNonDominantSize = 0;

	private static WaveVR_ButtonManager m_Instance = null;
	public static WaveVR_ButtonManager Instance {
		get
		{
			return m_Instance;
		}
	}

	#region MonoBehaviour overrides
	void Awake()
	{
		if (m_Instance == null)
			m_Instance = this;

		UpdateOptionValues();
		m_HmdOptionValue = m_HmdOptions.optionValue;
		m_DominantOptionValue = m_DominantOptions.optionValue;
		m_NonDominantOptionValue = m_NonDominantOptions.optionValue;
	}

	void Start ()
	{
		INFO ("Start()");
		ResetAllInputRequest ();
	}

	private bool connectionHmd = false, connectionDominant = false, connectionNonDominant = false;
	void Update ()
	{
		/**
		 * 1. Resets the input requests when the input options change.
		 **/
		UpdateOptionValues();
		if (m_HmdOptionValue != m_HmdOptions.optionValue)
		{
			m_HmdOptionValue = m_HmdOptions.optionValue;
			DEBUG("Update() HMD options changed to " + m_HmdOptionValue);
			ResetInputRequest(WaveVR_Controller.EDeviceType.Head);
		}
		if (m_DominantOptionValue != m_DominantOptions.optionValue)
		{
			m_DominantOptionValue = m_DominantOptions.optionValue;
			DEBUG("Update() Dominant options changed to " + m_DominantOptionValue);
			ResetInputRequest(WaveVR_Controller.EDeviceType.Dominant);
		}
		if (m_NonDominantOptionValue != m_NonDominantOptions.optionValue)
		{
			m_NonDominantOptionValue = m_NonDominantOptions.optionValue;
			DEBUG("Update() Dominant options changed to " + m_NonDominantOptionValue);
			ResetInputRequest(WaveVR_Controller.EDeviceType.NonDominant);
		}

		/**
		 * 2. [Important] Resets the input requests when a device is going to be connected.
		 * Considers below two cases when using only 1 controller:
		 * Case 1.
		 * 1.	Right-handed mode
		 * 2.	Right (dominant): connected, left (non-dominant): disconnected
		 * 3.	Connection events.
		 * 4.	Right: connected -> disconnected, left: disconnected -> connected,
		 * 5.	Dominant (right): connected -> disconnected, non-dominant (left): disconnected -> connected
		 * 6.	SetInputRequest (non-dominant)
		 * 7.	Role change to left-handed mode
		 * 8.	Dominant (left): disconnected -> connected. non-dominant (right): connected -> disconnected
		 * 9.	SetInputRequest (dominant)
		 * Case 2.
		 * 1.	Right-handed mode
		 * 2.	Right (dominant): connected, left (non-dominant): disconnected
		 * 3.	Role change to left-handed mode
		 * 4.	Dominant (left): connected -> disconnected, non-dominant (right): disconnected -> connected
		 * 5.	SetInputRequest (non-dominant)
		 * 6.	Connection events.
		 * 7.	Right: connected -> disconnected, left: disconnected -> connected
		 * 8.	Dominant (left): disconnected -> connected, non-dominant (right): connected -> disconnected
		 * 9.	SetInputRequest (dominant)
		 **/
		if (connectionHmd != WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.Head).connected)
		{
			connectionHmd = WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.Head).connected;
			if (connectionHmd)
			{
				DEBUG("Update() HMD is connected.");
				ResetInputRequest(WaveVR_Controller.EDeviceType.Head);
			}
		}
		if (connectionDominant != WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.Dominant).connected)
		{
			connectionDominant = WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.Dominant).connected;
			if (connectionDominant)
			{
				DEBUG("Update() Dominant controller is connected.");
				ResetInputRequest(WaveVR_Controller.EDeviceType.Dominant);
			}
		}
		if (connectionNonDominant != WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.NonDominant).connected)
		{
			connectionNonDominant = WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.NonDominant).connected;
			if (connectionNonDominant)
			{
				DEBUG("Update() Non-Dominant controller is connected.");
				ResetInputRequest(WaveVR_Controller.EDeviceType.NonDominant);
			}
		}
	}
	#endregion

	public bool GetInputMappingPair(WaveVR_Controller.EDeviceType device, ref WaveVR_ButtonList.EButtons destination)
	{
		WVR_InputId id = (WVR_InputId)destination;

		bool ret = GetInputMappingPair(device, ref id);
		if (ret)
			destination = GetEButtonsType (id);

		return ret;
	}

	public bool GetInputMappingPair(WaveVR_Controller.EDeviceType device, ref WVR_InputId destination)
	{
		if (!WaveVR.Instance.Initialized)
			return false;

		// Default true in editor mode, destination will be equivallent to source.
		bool result = false;
		int index = 0;

		switch (device)
		{
		case WaveVR_Controller.EDeviceType.Head:
			if (inputTableHMDSize > 0)
			{
				for (index = 0; index < (int)inputTableHMDSize; index++)
				{
					if (inputTableHMD [index].destination.id == destination)
					{
						destination = inputTableHMD [index].source.id;
						result = true;
					}
				}
			}
			break;
		case WaveVR_Controller.EDeviceType.Dominant:
			if (inputTableDominantSize > 0)
			{
				for (index = 0; index < (int)inputTableDominantSize; index++)
				{
					if (inputTableDominant [index].destination.id == destination)
					{
						destination = inputTableDominant [index].source.id;
						result = true;
					}
				}
			}
			break;
		case WaveVR_Controller.EDeviceType.NonDominant:
			if (inputTableNonDominantSize > 0)
			{
				for (index = 0; index < (int)inputTableNonDominantSize; index++)
				{
					if (inputTableNonDominant [index].destination.id == destination)
					{
						destination = inputTableNonDominant [index].source.id;
						result = true;
					}
				}
			}
			break;
		default:
			break;
		}

		return result;
	}

	private void setupButtonAttributes(WaveVR_Controller.EDeviceType device, List<WaveVR_ButtonList.EButtons> buttons, WVR_InputAttribute_t[] inputAttributes, int count)
	{
		WVR_DeviceType dev_type = WaveVR_Controller.Input (device).DeviceType;

		for (int i = 0; i < count; i++)
		{
			switch (buttons[i])
			{
				case WaveVR_ButtonList.EButtons.Menu:
				case WaveVR_ButtonList.EButtons.Grip:
				case WaveVR_ButtonList.EButtons.DPadLeft:
				case WaveVR_ButtonList.EButtons.DPadUp:
				case WaveVR_ButtonList.EButtons.DPadRight:
				case WaveVR_ButtonList.EButtons.DPadDown:
				case WaveVR_ButtonList.EButtons.VolumeUp:
				case WaveVR_ButtonList.EButtons.VolumeDown:
				case WaveVR_ButtonList.EButtons.Bumper:
				case WaveVR_ButtonList.EButtons.A_X:
				case WaveVR_ButtonList.EButtons.B_Y:
				case WaveVR_ButtonList.EButtons.Back:
				case WaveVR_ButtonList.EButtons.Enter:
					inputAttributes[i].id = (WVR_InputId)buttons[i];
					inputAttributes[i].capability = (uint)WVR_InputType.WVR_InputType_Button;
					inputAttributes[i].axis_type = WVR_AnalogType.WVR_AnalogType_None;
					break;
				case WaveVR_ButtonList.EButtons.Touchpad:
				case WaveVR_ButtonList.EButtons.Thumbstick:
					inputAttributes[i].id = (WVR_InputId)buttons[i];
					inputAttributes[i].capability = (uint)(WVR_InputType.WVR_InputType_Button | WVR_InputType.WVR_InputType_Touch | WVR_InputType.WVR_InputType_Analog);
					inputAttributes[i].axis_type = WVR_AnalogType.WVR_AnalogType_2D;
					break;
				case WaveVR_ButtonList.EButtons.Trigger:
					inputAttributes[i].id = (WVR_InputId)buttons[i];
					inputAttributes[i].capability = (uint)(WVR_InputType.WVR_InputType_Button | WVR_InputType.WVR_InputType_Touch | WVR_InputType.WVR_InputType_Analog);
					inputAttributes[i].axis_type = WVR_AnalogType.WVR_AnalogType_1D;
					break;
				default:
					break;
			}

			DEBUG ("setupButtonAttributes() " + device + " (" + dev_type + ") " + buttons [i]
				+ ", capability: " + inputAttributes [i].capability
				+ ", analog type: " + inputAttributes [i].axis_type);
		}
	}

	private void createHmdRequestAttributes()
	{
		INFO ("createHmdRequestAttributes()");

		List<WaveVR_ButtonList.EButtons> list = new List<WaveVR_ButtonList.EButtons>();
		if (m_HmdOptions.Back)
			list.Add(WaveVR_ButtonList.EButtons.Back);
		if (m_HmdOptions.Enter)
			list.Add(WaveVR_ButtonList.EButtons.Enter);
		if (m_HmdOptions.Menu)
			list.Add(WaveVR_ButtonList.EButtons.Menu);
		if (m_HmdOptions.VolumeDown)
			list.Add(WaveVR_ButtonList.EButtons.VolumeDown);
		if (m_HmdOptions.VolumeUp)
			list.Add(WaveVR_ButtonList.EButtons.VolumeUp);

		if (!list.Contains (WaveVR_ButtonList.EButtons.Enter))
			list.Add (WaveVR_ButtonList.EButtons.Enter);

		inputAttributes_hmd = new WVR_InputAttribute_t[list.Count];
		setupButtonAttributes (WaveVR_Controller.EDeviceType.Head, list, inputAttributes_hmd, list.Count);
	}

	private void createDominantRequestAttributes()
	{
		INFO ("createDominantRequestAttributes()");

		List<WaveVR_ButtonList.EButtons> list = new List<WaveVR_ButtonList.EButtons>();
		if (m_DominantOptions.Menu)
			list.Add(WaveVR_ButtonList.EButtons.Menu);
		if (m_DominantOptions.Grip)
			list.Add(WaveVR_ButtonList.EButtons.Grip);
		if (m_DominantOptions.DPadUp)
			list.Add(WaveVR_ButtonList.EButtons.DPadUp);
		if (m_DominantOptions.DPadRight)
			list.Add(WaveVR_ButtonList.EButtons.DPadRight);
		if (m_DominantOptions.DPadDown)
			list.Add(WaveVR_ButtonList.EButtons.DPadDown);
		if (m_DominantOptions.DPadLeft)
			list.Add(WaveVR_ButtonList.EButtons.DPadLeft);
		if (m_DominantOptions.Bumper)
			list.Add(WaveVR_ButtonList.EButtons.Bumper);
		if (m_DominantOptions.A_X)
			list.Add(WaveVR_ButtonList.EButtons.A_X);
		if (m_DominantOptions.B_Y)
			list.Add(WaveVR_ButtonList.EButtons.B_Y);
		if (m_DominantOptions.Touchpad)
			list.Add(WaveVR_ButtonList.EButtons.Touchpad);
		if (m_DominantOptions.Trigger)
			list.Add(WaveVR_ButtonList.EButtons.Trigger);
		if (m_DominantOptions.Thumbstick)
			list.Add(WaveVR_ButtonList.EButtons.Thumbstick);

		inputAttributes_Dominant = new WVR_InputAttribute_t[list.Count];
		setupButtonAttributes (WaveVR_Controller.EDeviceType.Dominant, list, inputAttributes_Dominant, list.Count);
	}

	private void createNonDominantRequestAttributes()
	{
		INFO ("createNonDominantRequestAttributes()");

		List<WaveVR_ButtonList.EButtons> list = new List<WaveVR_ButtonList.EButtons>();
		if (m_DominantOptions.Menu)
			list.Add(WaveVR_ButtonList.EButtons.Menu);
		if (m_DominantOptions.Grip)
			list.Add(WaveVR_ButtonList.EButtons.Grip);
		if (m_DominantOptions.DPadUp)
			list.Add(WaveVR_ButtonList.EButtons.DPadUp);
		if (m_DominantOptions.DPadRight)
			list.Add(WaveVR_ButtonList.EButtons.DPadRight);
		if (m_DominantOptions.DPadDown)
			list.Add(WaveVR_ButtonList.EButtons.DPadDown);
		if (m_DominantOptions.DPadLeft)
			list.Add(WaveVR_ButtonList.EButtons.DPadLeft);
		if (m_DominantOptions.Bumper)
			list.Add(WaveVR_ButtonList.EButtons.Bumper);
		if (m_DominantOptions.A_X)
			list.Add(WaveVR_ButtonList.EButtons.A_X);
		if (m_DominantOptions.B_Y)
			list.Add(WaveVR_ButtonList.EButtons.B_Y);
		if (m_DominantOptions.Touchpad)
			list.Add(WaveVR_ButtonList.EButtons.Touchpad);
		if (m_DominantOptions.Trigger)
			list.Add(WaveVR_ButtonList.EButtons.Trigger);
		if (m_DominantOptions.Thumbstick)
			list.Add(WaveVR_ButtonList.EButtons.Thumbstick);

		inputAttributes_NonDominant = new WVR_InputAttribute_t[list.Count];
		setupButtonAttributes (WaveVR_Controller.EDeviceType.NonDominant, list, inputAttributes_NonDominant, list.Count);
	}

	public bool IsButtonAvailable(WaveVR_Controller.EDeviceType device, WaveVR_ButtonList.EButtons button)
	{
		return IsButtonAvailable (device, (WVR_InputId)button);
	}

	public bool IsButtonAvailable(WaveVR_Controller.EDeviceType device, WVR_InputId button)
	{
		if (device == WaveVR_Controller.EDeviceType.Head)
			return this.usableButtons_hmd.Contains (button);
		if (device == WaveVR_Controller.EDeviceType.Dominant)
			return this.usableButtons_dominant.Contains (button);
		if (device == WaveVR_Controller.EDeviceType.NonDominant)
			return this.usableButtons_nonDominant.Contains (button);

		return false;
	}

	private void SetHmdInputRequest()
	{
		this.usableButtons_hmd.Clear ();
		if (!WaveVR.Instance.Initialized)
			return;

		WVR_DeviceType dev_type = WaveVR_Controller.Input (WaveVR_Controller.EDeviceType.Head).DeviceType;
		bool ret = Interop.WVR_SetInputRequest (dev_type, inputAttributes_hmd, (uint)inputAttributes_hmd.Length);
		if (ret)
		{
			inputTableHMDSize = Interop.WVR_GetInputMappingTable (dev_type, inputTableHMD, inputTableSize);
			if (inputTableHMDSize > 0)
			{
				for (int i = 0; i < (int)inputTableHMDSize; i++)
				{
					if (inputTableHMD [i].source.capability != 0)
					{
						this.usableButtons_hmd.Add (inputTableHMD [i].destination.id);
						DEBUG ("SetHmdInputRequest() " + dev_type
							+ " button: " + inputTableHMD [i].source.id + "(capability: " + inputTableHMD [i].source.capability + ")"
							+ " is mapping to HMD input ID: " + inputTableHMD [i].destination.id);
					} else
					{
						DEBUG ("SetHmdInputRequest() " + dev_type
							+ " source button " + inputTableHMD [i].source.id + " has invalid capability.");
					}
				}
			}
			WaveVR_Controller.Input (WaveVR_Controller.EDeviceType.Head).UpdateButtonEvents ();
		}
	}

	private void SetDominantInputRequest()
	{
		this.usableButtons_dominant.Clear ();
		if (!WaveVR.Instance.Initialized)
			return;

		WVR_DeviceType dev_type = WaveVR_Controller.Input (WaveVR_Controller.EDeviceType.Dominant).DeviceType;
		bool ret = Interop.WVR_SetInputRequest (dev_type, this.inputAttributes_Dominant, (uint)this.inputAttributes_Dominant.Length);
		if (ret)
		{
			inputTableDominantSize = Interop.WVR_GetInputMappingTable (dev_type, inputTableDominant, inputTableSize);
			if (inputTableDominantSize > 0)
			{
				for (int i = 0; i < (int)inputTableDominantSize; i++)
				{
					if (inputTableDominant [i].source.capability != 0)
					{
						this.usableButtons_dominant.Add (inputTableDominant [i].destination.id);
						DEBUG ("SetDominantInputRequest() " + dev_type
							+ " button: " + inputTableDominant [i].source.id + "(capability: " + inputTableDominant [i].source.capability + ")"
							+ " is mapping to Dominant input ID: " + inputTableDominant [i].destination.id);
					} else
					{
						DEBUG ("SetDominantInputRequest() " + dev_type
							+ " source button " + inputTableDominant [i].source.id + " has invalid capability.");
					}
				}
			}
			WaveVR_Controller.Input (WaveVR_Controller.EDeviceType.Dominant).UpdateButtonEvents ();
		}
	}

	private void SetNonDominantInputRequest()
	{
		this.usableButtons_nonDominant.Clear ();
		if (!WaveVR.Instance.Initialized)
			return;

		WVR_DeviceType dev_type = WaveVR_Controller.Input (WaveVR_Controller.EDeviceType.NonDominant).DeviceType;
		bool ret = Interop.WVR_SetInputRequest (dev_type, this.inputAttributes_NonDominant, (uint)this.inputAttributes_NonDominant.Length);
		if (ret)
		{
			inputTableNonDominantSize = Interop.WVR_GetInputMappingTable (dev_type, inputTableNonDominant, inputTableSize);
			if (inputTableNonDominantSize > 0)
			{
				for (int i = 0; i < (int)inputTableNonDominantSize; i++)
				{
					if (inputTableNonDominant [i].source.capability != 0)
					{
						this.usableButtons_nonDominant.Add (inputTableNonDominant [i].destination.id);
						DEBUG ("SetNonDominantInputRequest() " + dev_type
							+ " button: " + inputTableNonDominant [i].source.id + "(capability: " + inputTableNonDominant [i].source.capability + ")"
							+ " is mapping to NonDominant input ID: " + inputTableNonDominant [i].destination.id);
					} else
					{
						DEBUG ("SetNonDominantInputRequest() " + dev_type
							+ " source button " + inputTableNonDominant [i].source.id + " has invalid capability.");
					}
				}
			}
			WaveVR_Controller.Input (WaveVR_Controller.EDeviceType.NonDominant).UpdateButtonEvents ();
		}
	}

	private void ResetInputRequest(WaveVR_Controller.EDeviceType device)
	{
		DEBUG ("ResetInputRequest() " + device);
		switch (device)
		{
		case WaveVR_Controller.EDeviceType.Head:
			createHmdRequestAttributes ();
			SetHmdInputRequest ();
			break;
		case WaveVR_Controller.EDeviceType.Dominant:
			createDominantRequestAttributes ();
			SetDominantInputRequest ();
			break;
		case WaveVR_Controller.EDeviceType.NonDominant:
			createNonDominantRequestAttributes ();
			SetNonDominantInputRequest ();
			break;
		default:
			break;
		}
	}

	public void ResetAllInputRequest()
	{
		DEBUG ("ResetAllInputRequest()");
		ResetInputRequest (WaveVR_Controller.EDeviceType.Head);
		ResetInputRequest (WaveVR_Controller.EDeviceType.Dominant);
		ResetInputRequest (WaveVR_Controller.EDeviceType.NonDominant);
	}
}
