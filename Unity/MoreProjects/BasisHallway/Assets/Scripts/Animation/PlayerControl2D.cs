using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe des Keyboards
/// innerhalb eines Rechtecks in x und z-Koordinaten.
///
/// Die Bewegung wird in Update ausgeführt. Damit stellen wir
/// sicher, dass wir uns so lange bewegen wie einer der Tasten
/// gedrückt gehalten wird.
/// </summary>
/// <remarks>
/// Das Rechteck wird durch die Bounding Box eines Objekts
/// in der Szene definiert. Es bietet sich an in dieser Szene
/// den Boden oder den Kernbereich einzusetzen.
/// 
/// Wir verwenden eine InputAction, die in dieser Klasse
/// integriert ist. Das Binding für die Tasten kann im Inspector
/// verändert werden. Als default wird WASD  verwendet.
/// Möglich sind auch die Cursor-Tasten.
/// Die y-Koordinate des gesteuerten Objekts wird abgefragt und
/// nicht verändert.
/// </remarks>
public class PlayerControl2D : MonoBehaviour
{
	/// <summary>
	/// Geschwindigkeit der Bewegung
	/// </summary>
	 [Tooltip("Geschwindigkeit")]
	[Range(1.0f, 20.0f)]
	public float Speed = 10.0f;
	
	/// <summary>
	/// Objekt in der Szene, deren Maße der BBox
	/// in Weltkoordinaten wir in x und z für
	/// ein Clamp auf die mögliche Bewegung einsetzen.
	/// </summary>
	[Tooltip("Begrenzer für die mögliche Bewegung")]
	public GameObject Bounds;
	/// <summary>
	/// Wir folgen der Dokumentation von Unity
	/// und dem Abschnitt "Embedding Actions in MonoBehaviours".
	///
	/// Im Inspektor  erzeugen wir eine Composite-Action,
	/// die als Ergebnis einen Vector2D erzeugt. 
	/// </summary>
	public InputAction PlayAction;

	/// <summary>
	/// Grenzender Bewegung in x und z. Wir fragen diese Größen
	/// durch die Skalierungsfaktoren des Objekts im Inspektor ab.
	/// </summary>
	private float m_MinX, m_MaxX, m_MinZ, m_MaxZ;
	
	/// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Start abgefragt.
    /// </summary>
    private float m_Y;

	/// <summary>
    /// Eine Action hat verschiedene Zustände, für
    /// die wir Callbacks regristieren können.
    /// Wir könnten wie in der Unity-Dokumentation
    /// teilweise gezeigt das hier gleich mit implementieren.
    /// Hier entscheiden wir uns dafür, die Funktion
    /// OnPress zu registrieren, die wir implementieren
    /// und die den Wert von IsFollowing toggelt.
    /// </summary>
    private void Awake()
    {
	    PlayAction.performed += OnMove;
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir auch unsere Action.
    /// </summary>
    private void OnEnable()
    {
	    PlayAction.Enable();
    }
    
    /// <summary>
    /// Der maximale Bewegungsbereich für den Player
    /// ist durch die Ausmasse eines Objekts  in der
    /// Szene gegeben.
    /// </summary>
    /// <remarks>
    /// Sinnvoll sind zum Beispiel der Boden oder der Kernbereich.
    /// </remarks>
    private void Start()
    {
	    // y-Koordinaten abfragen, damit wir sie konstant halten können.
	    m_Y = transform.position.y;
	    // Renderer des Boundary-Objekts abfragen 
	    // und die Maße der AABB  als Werte
	    // die Grenzen der Bewegung in x und z verwenden!
	    // Wir verwenden minimale und maximale x- und z-Werte.
	    var rend = Bounds.GetComponent<Renderer>();
	    if (rend == null) return;
	    // Wir fragen die AABB des Objekts ab, 
	    // dafür gibt es die Datenstruktur Bounds.
	    var aabb = rend.bounds;
	    var center = aabb.center;
	    var extents = aabb.extents;

	    m_MinX = center[0] - extents[0];
	    m_MaxX = center[0] + extents[0];
	    m_MinZ = center[2] - extents[2];
	    m_MaxZ = center[2] + extents[2];
    }

    /// <summary>
    /// Ausführen der Bewegung, falls eine der Tasten
    /// gedrückt gehalten wird.
    /// </summary>
    private void Update()
    {
	    if (!m_Moving)
		    return;
	    m_DoTheMove();
    }
    /// <summary>
    /// In Disable für die Szene de-aktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
	    PlayAction.Disable();
    }

    /// <summary>
	/// Callback für die Composite Action PlayAction.
	///<summary>
    private void OnMove(InputAction.CallbackContext ctx)
    {
	    // Wir führen eine Bewegung durch solange Taste gedrückt wird.
	    // Bei Press erhalten wir True, bei Release erhalten wir False.
	    m_Moving = ctx.control.IsPressed();
	    var results = ctx.ReadValue<Vector2>();
	    m_Delta = new Vector3(results.x, 0.0f, results.y);
    }

    /// <summary>
    /// Bewegung ausführen.
    /// </summary>
    private void m_DoTheMove()
    {
	    // Neue Position berechnen
	    var newPos = Vector3.MoveTowards(
		    transform.position,
		    transform.position + m_Delta,
		    Speed * Time.deltaTime);
	    
	    // Clamping
	    newPos.x = Mathf.Clamp(newPos.x,
		    m_MinX,
		    m_MaxX);
	    newPos.z = Mathf.Clamp(newPos.z,
		    m_MinZ,
		    m_MaxZ);
	    transform.position = new Vector3(
		    newPos.x, 
		    m_Y, 
		    newPos.z);	    
    }
    
    /// <summary>
    /// Wird eine Bewegung ausgeführt oder nicht?
    /// </summary>
    private bool m_Moving = false;
    /// <summary>
    /// Vektor für die Speicherung der Veränderung der Position
    /// aus dem Input System.
    /// </summary>
    private Vector3 m_Delta = new Vector3();
}