using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe des Keyboards
/// innerhalb eines Rechtecks in x und z-Koordinaten.
/// </summary>
/// <remarks>
/// Das Rechteck wird durch die Bounding Box eines Objekts
/// in der Szene definiert. Es bietet sich an in dieser Szene
/// den Boden oder den Kernbereich einzusetzen.
/// 
/// Wir verwenden eine InputAction, die in dieser Klasse
/// integriert ist. Das Binding f�r die Tasten kann im Inspector
/// ver�ndert werden. Als default wird WASD  verwendet.
/// M�glich sind auch die Cursor-Tasten.
/// Die y-Koordinate des gesteuerten Objekts wird abgefragt und
/// nicht ver�ndert.
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
	/// Objekt in der Szene, deren Ma�e der BBox
	/// in Weltkoordinaten wir in x und z f�r
	/// ein Clamp auf die m�gliche Bewegung einsetzen.
	/// </summary>
	[Tooltip("Begrenzer f�r die m�gliche Bewegung")]
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
	/// Grenzender Bewegung in x und z. Wir fragen diese Gr��en
	/// durch die Skalierungsfaktoren des Objekts im Inspektor ab.
	/// </summary>
	private float m_MinX, m_MaxX, m_MinZ, m_MaxZ;
	
	/// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Start abgefragt.
    /// </summary>
    private float m_Y;

	/// <summary>
    /// Eine Action hat verschiedene Zust�nde, f�r
    /// die wir Callbacks regristieren k�nnen.
    /// Wir k�nnten wie in der Unity-Dokumentation
    /// teilweise gezeigt das hier gleich mit implementieren.
    /// Hier entscheiden wir uns daf�r, die Funktion
    /// OnPress zu registrieren, die wir implementieren
    /// und die den Wert von IsFollowing toggelt.
    /// </summary>
    private void Awake()
    {
	    PlayAction.performed += OnMove;
    }
    
    /// <summary>
    /// In Enable f�r die Szene aktivieren wir auch unsere Action.
    /// </summary>
    private void OnEnable()
    {
	    PlayAction.Enable();
    }
    
    /// <summary>
    /// Der maximale Bewegungsbereich f�r den Player
    /// ist durch die Ausmasse eines Objekts  in der
    /// Basis-Szene gegeben.
    /// </summary>
    /// <remarks>
    /// Sinnvoll sind zum Beispiel der Boden oder der Kernbereich.
    /// </remarks>
    private void Start()
    {
	    // y-Koordinaten abfragen, damit wir sie konstant halten k�nnen.
	    m_Y = transform.position.y;
	    // Renderer des Boundary-Objekts abfragen 
	    // und die Ma�e der BBox davon als Werte
	    // die Grenzen der Bewegung in x und z verwenden!
	    var rend = Bounds.GetComponent<Renderer>();
	    if (rend == null) return;
	    var bounds = rend.bounds;
	    m_MinX = m_MinZ = -bounds.extents[0];
	    m_MaxX = m_MaxZ = bounds.extents[2];
    }
    
    /// <summary>
    /// In Disable f�r die Szene de-aktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
	    PlayAction.Disable();
    }

    /// <summary>
	/// Callback f�r die Composite Action PlayAction.
	///<summary>
    private void OnMove(InputAction.CallbackContext ctx)
    {
	    var results = ctx.ReadValue<Vector2>();
	    var trans = new Vector3(results.x, 0.0f, results.y);
	    var newPos = transform.position + trans;
	    
	    newPos = Vector3.MoveTowards(
		    transform.position,
		    newPos,
		    Speed * Time.deltaTime);
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
}