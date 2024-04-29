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
using System.Collections.Generic;
using WVR_Log;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaveVR_GestureBeam: MonoBehaviour
{
	private static string LOG_TAG = "WaveVR_GestureBeam";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d (LOG_TAG, this.BeamType + " " + msg, true);
	}

	public bool ShowBeam = true;
	public WaveVR_GestureManager.EGestureHand BeamType = WaveVR_GestureManager.EGestureHand.RIGHT;
	public float StartWidth = 0.002f;
	public float EndWidth = 0.004f;

	private const float minimal_length = 0.1f;
	private float preStartOffset = 0.015f;
	public float StartOffset = 0.01f;

	[HideInInspector]
	public float endOffsetMin = 0.01f;	// Minimum distance of end offset (in meters).
	[HideInInspector]
	public float endOffsetMax = 99.5f;	// Maximum distance of end offset (in meters).
	private float preEndOffset = 0.8f;
	public float EndOffset = 0.5f;
	public void SetEndOffset(float end_offset)
	{
		this.EndOffset = end_offset;
		DEBUG ("SetEndOffset() " + this.EndOffset);
	}

	public void SetEndOffset (Vector3 target, bool interactive)
	{
		Vector3 targetLocalPosition = transform.InverseTransformPoint (target);

		this.EndOffset = targetLocalPosition.z - endOffsetMin;
		/*DEBUG ("SetEndOffset() targetLocalPosition.z: " + targetLocalPosition.z
			+ ", EndOffset: " + this.EndOffset
			+ ", endOffsetMin: " + endOffsetMin
			+ ", endOffsetMax: " + endOffsetMax);*/
	}

	public void ResetEndOffset()
	{
		this.EndOffset = endOffsetMax;

		DEBUG ("ResetEndOffset() EndOffset: " + this.EndOffset
			+ ", endOffsetMin: " + endOffsetMin
			+ ", endOffsetMax: " + endOffsetMax);
	}

	private Color32 preStartColor = new Color32 (255, 255, 255, 255);
	public Color32 StartColor = new Color32 (255, 255, 255, 255);

	private Color32 TailColor = new Color32 (255, 255, 255, 255);

	private Color32 preEndColor = new Color32 (255, 255, 255, 255);
	public Color32 EndColor = new Color32 (255, 255, 255, 100);

	private const string CtrColorBeam3 = "CtrColorBeam3";
	public bool UseDefaultMaterial = true;
	public Material CustomMaterial = null;
	public bool UpdateEachFrame = false;

	private bool Color32Equal(Color32 color1, Color32 color2)
	{
		if (color1.r == color2.r &&
		    color1.g == color2.g &&
		    color1.b == color2.b &&
		    color1.a == color2.a)
			return true;

		return false;
	}
	private void ValidateBeamAttributes()
	{
		if (this.EndOffset < this.endOffsetMin || this.EndOffset > this.endOffsetMax)
			this.EndOffset = preEndOffset;

		if (preEndOffset != this.EndOffset)
		{
			preEndOffset = this.EndOffset;
			toUpdateBeam = true;
			//DEBUG ("ValidateBeamAttributes() EndOffset: " + this.EndOffset);
		}

		if (this.StartOffset >= (this.EndOffset - minimal_length))
			this.StartOffset = preStartOffset;

		if (preStartOffset != this.StartOffset)
		{
			preStartOffset = this.StartOffset;
			toUpdateBeam = true;
			DEBUG ("ValidateBeamAttributes() StartOffset: " + this.StartOffset);
		}

		if (!Color32Equal (preStartColor, this.StartColor))
		{
			preStartColor = this.StartColor;
			toUpdateBeam = true;
			DEBUG ("ValidateBeamAttributes() StartColor: " + this.StartColor.ToString ());
		}

		if (!Color32Equal (preEndColor, this.EndColor))
		{
			this.preEndColor = this.EndColor;
			toUpdateBeam = true;
			DEBUG ("ValidateBeamAttributes() EndColor: " + this.EndColor.ToString ());
		}
	}

	/**
	 * OEM Config
	 * \"beam\": {
	   \"start_width\": 0.000625,
	   \"end_width\": 0.00125,
	   \"start_offset\": 0.015,
	   \"length\":  0.8,
	   \"start_color\": \"#FFFFFFFF\",
	   \"end_color\": \"#FFFFFF4D\"
	   },
	 **/

	private int count = 3;
	private bool makeTail = true; // Offset from 0

	//private bool useTexture = true;
	private string textureName;

	private int maxUVAngle = 30;
	private const float epsilon = 0.0001f;

	private void Validate()
	{

		if (this.StartWidth < epsilon)
			this.StartWidth = epsilon;

		if (this.EndWidth < epsilon)
			this.EndWidth = epsilon;

		if (this.StartOffset < epsilon)
			this.StartOffset = epsilon;

		if (this.EndOffset < epsilon * 2)
			this.EndOffset = epsilon * 2;

		if (this.EndOffset < this.StartOffset)
			this.EndOffset = this.StartOffset + epsilon;

		if (count < 3)
			count = 3;

		/**
		 * The texture pattern should be a radiated image starting 
		 * from the texture center.
		 * If the mesh's count is too low, the uv map can't keep a 
		 * good radiation shap.  Therefore the maxUVAngle should be
		 * reduced to avoid the uv area cutting the radiation circle.
		**/
		int uvAngle = 360 / count;
		if (uvAngle > 30)
			maxUVAngle = 30;
		else
			maxUVAngle = uvAngle;
	}

	private int Count = -1/*, verticesCount = -1, indicesCount = -1*/;

	private List<Vector3> vertices = new List<Vector3> ();
	private List<Vector2> uvs = new List<Vector2> ();
	private List<Vector3> normals = new List<Vector3> ();
	private List<int> indices = new List<int> ();
	private List<Color32> colors = new List<Color32> ();

	private Mesh emptyMesh;
	private Mesh updateMesh;
	private Material materialComp;
	private MeshFilter m_MeshFilter;
	private MeshRenderer m_MeshRenderer;
	private bool meshIsCreated = false;

	private bool toUpdateBeam = false;

	#region Monobehaviour overrides
	void Awake()
	{
		this.emptyMesh = new Mesh();
		this.updateMesh = new Mesh();
	}

	void Start()
	{
	}

	private bool mEnabled = false;
	void OnEnable()
	{
		if (!mEnabled)
		{
			TailColor = this.StartColor;

			Count = count + 1;
			//verticesCount = Count * 2 + (makeTail ? 1 : 0);
			//indicesCount = Count * 6 + (makeTail ? count * 3 : 0);

			GameObject parentGo = transform.parent.gameObject;

			m_MeshFilter = GetComponent<MeshFilter>();
			m_MeshRenderer = GetComponent<MeshRenderer> ();

			if (!this.UseDefaultMaterial && this.CustomMaterial != null)
			{
				DEBUG ("OnEnable() Use custom config and material");
				m_MeshRenderer.material = CustomMaterial;
			}
			else
			{
				DEBUG ("OnEnable() Use default material");
				var tmp = Resources.Load(CtrColorBeam3) as Material;
				if (tmp == null)
					DEBUG ("OnEnable() Can NOT load default material");
				m_MeshRenderer.material = tmp;
			}

			// Not draw mesh in OnEnable(), thus set the m_MeshRenderer to disable.
			m_MeshRenderer.enabled = false;

			preEndOffset = this.EndOffset;
			preStartOffset = this.StartOffset;
			preStartColor = this.StartColor;

			WaveVR_GestureBeamProvider.Instance.SetGestureBeam(this.BeamType, gameObject);
			mEnabled = true;

			DEBUG("OnEnable() parent name: " + parentGo.name
				+ ", localPos: " + parentGo.transform.localPosition.x + ", " + parentGo.transform.localPosition.y + ", " + parentGo.transform.localPosition.z
				+ ", parent local EulerAngles: " + parentGo.transform.localEulerAngles.ToString()
				+ ", show beam: " + m_MeshRenderer.enabled + ", StartWidth: " + this.StartWidth
				+ ", EndWidth: " + this.EndWidth + ", StartOffset: " + this.StartOffset + ", EndOffset: " + this.EndOffset
				+ ", StartColor: " + this.StartColor.ToString() + ", EndColor: " + this.EndColor.ToString()
			);
		}
	}

	void OnDisable()
	{
		DEBUG ("OnDisable()");
		ShowBeamMesh (false);
		mEnabled = false;
	}

	void OnApplicationPause(bool pauseStatus)
	{
		//if (!pauseStatus) // resume
	}

	public void Update()
	{
		ValidateBeamAttributes ();

		// Redraw mesh if updated.
		if (this.UpdateEachFrame || toUpdateBeam)
		{
			CreateBeamMesh ();

			if (toUpdateBeam)
				toUpdateBeam = !toUpdateBeam;
		}

		ShowBeamMesh(this.ShowBeam);

		if (Log.gpl.Print)
			DEBUG ("Update() " + gameObject.name + " is " + (this.ShowBeam ? "shown" : "hidden")
				+ ", start offset: " + this.StartOffset
				+ ", end offset: " + this.EndOffset
				+ ", start width: " + this.StartWidth
				+ ", end width: " + this.EndWidth
				+ ", start color: " + this.StartColor
				+ ", end color: " + this.EndColor);
	}
	#endregion

	private Matrix4x4 mat44_rot = Matrix4x4.zero;
	private Matrix4x4 mat44_uv = Matrix4x4.zero;
	private Vector3 vec3_vertices_start = Vector3.zero;
	private Vector3 vec3_vertices_end = Vector3.zero;

	private readonly Vector2 vec2_05_05 = new Vector2 (0.5f, 0.5f);
	private readonly Vector3 vec3_0_05_0 = new Vector3 (0, 0.5f, 0);
	private void createMesh()
	{
		updateMesh.Clear ();
		uvs.Clear ();
		vertices.Clear ();
		normals.Clear ();
		indices.Clear ();
		colors.Clear ();

		mat44_rot = Matrix4x4.zero;
		mat44_uv = Matrix4x4.zero;

		for (int i = 0; i < Count; i++)
		{
			int angle = (int) (i * 360.0f / count);
			int UVangle = (int)(i * maxUVAngle / count);
			// make rotation matrix
			mat44_rot.SetTRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.forward), Vector3.one);
			mat44_uv.SetTRS(Vector3.zero, Quaternion.AngleAxis(UVangle, Vector3.forward), Vector3.one);

			// start
			vec3_vertices_start.y = this.StartWidth;
			vec3_vertices_start.z = this.StartOffset;
			vertices.Add (mat44_rot.MultiplyVector (vec3_vertices_start));
			uvs.Add (vec2_05_05);
			colors.Add (this.StartColor);
			normals.Add (mat44_rot.MultiplyVector (Vector3.up).normalized);

			// end
			vec3_vertices_end.y = this.EndWidth;
			vec3_vertices_end.z = this.EndOffset;
			vertices.Add (mat44_rot.MultiplyVector (vec3_vertices_end));
			Vector2 uv = mat44_uv.MultiplyVector (vec3_0_05_0);
			uv.x = uv.x + 0.5f;
			uv.y = uv.y + 0.5f;
			uvs.Add(uv);
			colors.Add (this.EndColor);
			normals.Add(mat44_rot.MultiplyVector(Vector3.up).normalized);
		}

		for (int i = 0; i < count; i++)
		{
			// bd
			// ac
			int a, b, c, d;
			a = i * 2;
			b = i * 2 + 1;
			c = i * 2 + 2;
			d = i * 2 + 3;

			// first
			indices.Add(a);
			indices.Add(d);
			indices.Add(b);

			// second
			indices.Add(a);
			indices.Add(c);
			indices.Add(d);
		}

		// Make Tail
		if (makeTail)
		{
			vertices.Add (Vector3.zero);
			colors.Add (TailColor);
			uvs.Add (vec2_05_05);
			normals.Add (Vector3.zero);
			int tailIdx = count * 2;
			for (int i = 0; i < count; i++)
			{
				int idx = i * 2;

				indices.Add(tailIdx);
				indices.Add(idx + 2);
				indices.Add(idx);
			}
		}
		updateMesh.vertices = vertices.ToArray();
		//updateMesh.SetUVs(0, uvs);
		//updateMesh.SetUVs(1, uvs);
		updateMesh.colors32  = colors.ToArray ();
		updateMesh.normals = normals.ToArray();
		updateMesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
		updateMesh.name = "Beam";
	}

	private void CreateBeamMesh()
	{
		Validate ();
		createMesh ();  // generate this.updateMesh
		this.meshIsCreated = true;
	}

	private void ShowBeamMesh(bool show)
	{
		if (!this.meshIsCreated && show == true)
			CreateBeamMesh ();

		if (m_MeshRenderer.enabled != show)
		{
			m_MeshFilter.mesh = show ? this.updateMesh : this.emptyMesh;
			m_MeshRenderer.enabled = show;
			DEBUG("ShowBeamMesh() shown? " + m_MeshRenderer.enabled);
		}
	}

	private Color32 StringToColor32(string color_string)
	{
		try
		{
			byte[] _color_r = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(1, 2), 16));
			byte[] _color_g = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(3, 2), 16));
			byte[] _color_b = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(5, 2), 16));
			byte[] _color_a = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(7, 2), 16));

			return new Color32(_color_r[0], _color_g[0], _color_b[0], _color_a[0]);
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, "StringToColor32 failed: " + e.ToString());
			return new Color32(255, 255, 255, 77);
		}
	}

	private void UpdateStartColor(string color_string)
	{

		try
		{
			byte[] _color_r = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(1, 2), 16));
			byte[] _color_g = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(3, 2), 16));
			byte[] _color_b = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(5, 2), 16));
			byte[] _color_a = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(7, 2), 16));

			this.StartColor.r = _color_r[0];
			this.StartColor.g = _color_g[0];
			this.StartColor.b = _color_b[0];
			this.StartColor.a = _color_a[0];
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, "UpdateStartColor failed: " + e.ToString());
			this.StartColor = new Color32(255, 255, 255, 255);
		}
	}

	private void UpdateEndColor(string color_string)
	{
		try
		{
			byte[] _color_r = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(1, 2), 16));
			byte[] _color_g = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(3, 2), 16));
			byte[] _color_b = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(5, 2), 16));
			byte[] _color_a = BitConverter.GetBytes(Convert.ToInt32(color_string.Substring(7, 2), 16));

			this.EndColor.r = _color_r[0];
			this.EndColor.g = _color_g[0];
			this.EndColor.b = _color_b[0];
			this.EndColor.a = _color_a[0];
		}
		catch (Exception e)
		{
			string defaultEndColor = "#FFFFFF4D";

			Log.e(LOG_TAG, "UpdateEndColor failed: " + e.ToString());
			this.EndColor = StringToColor32(defaultEndColor);
		}
	}

	private Color32 colorBlue = new Color32(0, 255, 255, 177);
	private Color32 colorWhite = new Color32(255, 255, 255, 255);
	public void SetEffectiveBeam(bool effective)
	{
		this.StartColor = effective ? colorBlue : colorWhite;
		//this.EndColor = effective ? colorBlue : colorWhite;
	}
}
