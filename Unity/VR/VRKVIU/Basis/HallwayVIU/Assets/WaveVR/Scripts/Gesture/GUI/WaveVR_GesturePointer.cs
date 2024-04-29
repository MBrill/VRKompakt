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
using WVR_Log;

/// <summary>
/// Draws a pointer of controller to indicate to which object is pointed.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class WaveVR_GesturePointer : MonoBehaviour
{
	private const string LOG_TAG = "WaveVR_GesturePointer";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d(LOG_TAG, this.PointerType + ", " + msg, true);
	}
	private void INFO(string msg)
	{
		Log.i(LOG_TAG, this.PointerType + ", " + msg, true);
	}

	#region Exported Variables
	public WaveVR_GestureManager.EGestureHand PointerType = WaveVR_GestureManager.EGestureHand.RIGHT;
	public bool ShowPointer = true;
	public float PointerOuterDiameterMin = 0.01f;
	private float pointerOuterDiameter = 0;

	[HideInInspector]
	public bool UseTexture = true;
	//[Tooltip("Blinking only when not using texture.")]
	[HideInInspector]
	public bool Blink = false;
	[Tooltip("True for using the default texture, false for using the custom texture.")]
	public bool UseDefaultTexture = true;
	private const string m_WhitePointer = "Textures/tControllerPointerWhite01";
	private const string m_BluePointer = "Textures/tControllerPointerBlue01";
	private Texture2D m_WhitePointerTexture = null, m_BluePointerTexture = null;
	public Texture2D CustomTexture = null;

	private const float pointerDistanceMin = 0.1f;      // Min length of Beam
	[HideInInspector]
	public const float pointerDistanceMax = 100.0f;     // Max length of Beam + 0.5m
	public float PointerDistanceInMeters = 50f;         // Current distance of the pointer (in meters)

	private Vector3 pointerWorldPosition = Vector3.zero;

	/// <summary>
	/// Material resource of pointer.
	/// It contains shader **WaveVR/CtrlrPointer** and there are 5 attributes can be changed in runtime:
	/// <para>
	/// - _OuterDiameter
	/// - _DistanceInMeters
	/// - _MainTex
	/// - _Color
	/// - _useTexture
	///
	/// If _useTexture is set (default), the texture assign in _MainTex will be used.
	/// </summary>
	private const string pointerMaterialResource = "ControllerPointer";
	private Material pointerMaterial = null;
	private Material pointerMaterialInstance = null;

	private Color colorFactor = Color.white;               // The color variable of the pointer
	[HideInInspector]
	public Color PointerColor = Color.white;               // #FFFFFFFF
	[HideInInspector]
	public Color borderColor = new Color(119, 119, 119, 255);     // #777777FF
	[HideInInspector]
	public Color focusColor = new Color(255, 255, 255, 255);       // #FFFFFFFF
	[HideInInspector]
	public Color focusBorderColor = new Color(119, 119, 119, 255); // #777777FF

	private const int PointerRenderQueueMin = 1000;
	private const int PointerRenderQueueMax = 5000;
	public int PointerRenderQueue = PointerRenderQueueMax;
	#endregion

	private MeshFilter meshFilter = null;
	private Mesh mesh = null;
	private Renderer m_Renderer = null;

	/**
	 * OEM Config
	 * \"pointer\": {
	 * \"diameter\": 0.01,
	 * \"distance\": 1.3,
	 * \"use_texture\": true,
	 * \"color\": \"#FFFFFFFF\",
	 * \"border_color\": \"#777777FF\",
	 * \"focus_color\": \"#FFFFFFFF\",
	 * \"focus_border_color\": \"#777777FF\",
	 * \"texture_name\":  null,
	 * \"Blink\": false
	 * },
	 **/

	private const int reticleSegments = 20;
	private float pointerGrowthMultiple = 0.03f;                // Angle at which to expand the pointer when intersecting with an object (in degrees).
	private float colorFlickerTime = 0;                   // The color flicker timestamp

	#region MonoBehaviour overrides
	private bool mEnabled = false;
	void OnEnable()
	{
		if (!mEnabled)
		{
			// Load default pointer material resource and create instance.
			pointerMaterial = Resources.Load(pointerMaterialResource) as Material;
			if (pointerMaterial != null)
				pointerMaterialInstance = Instantiate<Material>(pointerMaterial);
			if (pointerMaterialInstance == null)
				INFO("OnEnable() Can NOT load default material");
			else
				INFO("OnEnable() Controller pointer material: " + pointerMaterialInstance.name);

			// Load default pointer texture resource.
			// If developer does not specify custom texture, default texture will be used.
			m_WhitePointerTexture = (Texture2D)Resources.Load(m_WhitePointer);
			if (m_WhitePointerTexture == null)
				Log.e(LOG_TAG, "OnEnable() Can NOT load the white pointer texture", true);
			m_BluePointerTexture = (Texture2D)Resources.Load(m_BluePointer);
			if (m_BluePointerTexture == null)
				Log.e(LOG_TAG, "OnEnable() Can NOT load the blue pointer texture", true);

			// Get MeshFilter instance.
			meshFilter = GetComponent<MeshFilter>();

			// Get Quad mesh as default pointer mesh.
			// If developer does not use texture, pointer mesh will be created in CreatePointerMesh()
			GameObject prim_go = GameObject.CreatePrimitive(PrimitiveType.Quad);
			mesh = Instantiate(prim_go.GetComponent<MeshFilter>().sharedMesh);
			mesh.name = "CtrlQuadPointer";
			prim_go.SetActive(false);
			Destroy(prim_go);

			InitializePointer();
			WaveVR_GesturePointerProvider.Instance.SetGesturePointer(this.PointerType, gameObject);
			mEnabled = true;
		}
	}

	void OnDisable()
	{
		INFO("OnDisable()");
		pointerInitialized = false;
		mEnabled = false;
	}

	/// <summary>
	/// The attributes
	/// <para>
	/// - _Color
	/// - _OuterDiameter
	/// - _DistanceInMeters
	/// can be updated directly by changing
	/// - colorFactor
	/// - pointerOuterDiameter
	/// - PointerDistanceInMeters
	/// But if developer need to update texture in runtime, developer should
	/// 1.set ShowPointer to false to hide pointer first.
	/// 2.assign CustomTexture
	/// 3.set UseSystemConfig to false
	/// 4.set UseDefaultTexture to false
	/// 5.set ShowPointer to true to generate new pointer.
	/// </summary>
	void Update()
	{
		ActivatePointer(this.ShowPointer);

		// Pointer distance.
		this.PointerDistanceInMeters = Mathf.Clamp(this.PointerDistanceInMeters, pointerDistanceMin, pointerDistanceMax);
		pointerWorldPosition = transform.position + transform.forward.normalized * this.PointerDistanceInMeters;

		if (this.Blink == true)
		{
			if (Time.unscaledTime - colorFlickerTime >= 0.5f)
			{
				colorFlickerTime = Time.unscaledTime;
				colorFactor = (colorFactor != Color.white) ? colorFactor = Color.white : colorFactor = Color.black;
				DEBUG("Color: " + colorFactor.ToString());
			}
		}

		UpdatePointerDiameter();
		if (pointerMaterialInstance != null)
		{
			pointerMaterialInstance.renderQueue = this.PointerRenderQueue;
			pointerMaterialInstance.SetColor("_Color", colorFactor);
			pointerMaterialInstance.SetFloat("_useTexture", this.UseTexture ? 1.0f : 0.0f);
			pointerMaterialInstance.SetFloat("_OuterDiameter", pointerOuterDiameter);
			pointerMaterialInstance.SetFloat("_DistanceInMeters", this.PointerDistanceInMeters);
		}
		else
		{
			if (Log.gpl.Print)
				DEBUG("Update() Pointer material is null!!");
		}

		if (Log.gpl.Print)
		{
			DEBUG("Update() " + gameObject.name
				+ " is " + (this.ShowPointer ? "shown" : "hidden")
				+ ", pointer color: " + colorFactor
				+ ", use texture: " + this.UseTexture
				+ ", pointer outer diameter: " + pointerOuterDiameter
				+ ", pointer distance: " + this.PointerDistanceInMeters
				+ ", render queue: " + this.PointerRenderQueue);
		}
	}
	#endregion

	#region Pointer Activate
	private void CreatePointerMesh()
	{
		int vertexCount = (reticleSegments + 1) * 2;
		Vector3[] vertices = new Vector3[vertexCount];
		for (int vi = 0, si = 0; si <= reticleSegments; si++)
		{
			float angle = (float)si / (float)reticleSegments * Mathf.PI * 2.0f;
			float x = Mathf.Sin(angle);
			float y = Mathf.Cos(angle);
			vertices[vi++] = new Vector3(x, y, 0.0f);
			vertices[vi++] = new Vector3(x, y, 1.0f);
		}

		int indicesCount = (reticleSegments + 1) * 6;
		int[] indices = new int[indicesCount];
		int vert = 0;
		for (int ti = 0, si = 0; si < reticleSegments; si++)
		{
			indices[ti++] = vert + 1;
			indices[ti++] = vert;
			indices[ti++] = vert + 2;
			indices[ti++] = vert + 1;
			indices[ti++] = vert + 2;
			indices[ti++] = vert + 3;

			vert += 2;
		}

		DEBUG("CreatePointerMesh() Create Mesh and add MeshFilter component.");

		mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = indices;
		mesh.name = "WaveVR_Mesh_Q";
		mesh.RecalculateBounds();
	}

	private bool pointerInitialized = false;                     // true: the mesh of reticle is created, false: the mesh of reticle is not ready
	private void InitializePointer()
	{
		if (pointerInitialized)
		{
			INFO("InitializePointer() Pointer is already initialized.");
			return;
		}

		if (this.UseTexture == false)
		{
			colorFlickerTime = Time.unscaledTime;
			CreatePointerMesh();
			DEBUG("InitializePointer() Create a mesh. ( WaveVR_Mesh_Q mesh )");
		}
		else
		{
			DEBUG("InitializePointer() Use default mesh. ( CtrlQuadPointer mesh )");
		}

		meshFilter.mesh = mesh;

		if (pointerMaterialInstance != null)
		{
			if (this.UseDefaultTexture || (null == this.CustomTexture))
			{
				DEBUG("InitializePointer() Use default texture.");
				pointerMaterialInstance.mainTexture = m_WhitePointerTexture;
				pointerMaterialInstance.SetTexture("_MainTex", m_WhitePointerTexture);
			}
			else
			{
				DEBUG("InitializePointer() Use custom texture.");
				pointerMaterialInstance.mainTexture = this.CustomTexture;
				pointerMaterialInstance.SetTexture("_MainTex", this.CustomTexture);
			}
		}
		else
		{
			Log.e(LOG_TAG, "InitializePointer() Pointer material is null!!", true);
		}

		m_Renderer = GetComponent<Renderer>();
		m_Renderer.material = pointerMaterialInstance;
		m_Renderer.sortingOrder = 32767;

		pointerInitialized = true;
	}

	private void ActivatePointer(bool show)
	{
		if (!pointerInitialized)
			InitializePointer();

		if (m_Renderer.enabled != show)
		{
			m_Renderer.enabled = show;
			DEBUG("ActivatePointer() " + m_Renderer.enabled);
		}
	}
	#endregion

	#region Pointer Data
	public Vector3 GetPointerPosition()
	{
		return pointerWorldPosition;
	}

	public void OnPointerEnter(GameObject target, Vector3 intersectionPosition, bool isInteractive)
	{
		this.ShowPointer = true;
		if (isInteractive)
			SetPointerTarget(intersectionPosition, isInteractive);
	}

	public void OnPointerExit(GameObject target)
	{
		this.ShowPointer = false;
		//DEBUG("OnPointerExit() " + (target != null ? target.name : "null"));
	}

	public void SetEffectivePointer(bool effective)
	{
		if (!effective)
		{
			pointerMaterialInstance.mainTexture = m_WhitePointerTexture;
			pointerMaterialInstance.SetTexture("_MainTex", m_WhitePointerTexture);
		}
		else
		{
			pointerMaterialInstance.mainTexture = m_BluePointerTexture;
			pointerMaterialInstance.SetTexture("_MainTex", m_BluePointerTexture);
		}
	}

	private void SetPointerTarget(Vector3 target, bool interactive)
	{
		Vector3 targetLocalPosition = transform.InverseTransformPoint(target);
		this.PointerDistanceInMeters = Mathf.Clamp(targetLocalPosition.z, pointerDistanceMin, pointerDistanceMax);
	}

	private void UpdatePointerDiameter()
	{
		pointerOuterDiameter = PointerOuterDiameterMin + ((this.PointerDistanceInMeters - 1) * pointerGrowthMultiple);
	}
	#endregion
}
