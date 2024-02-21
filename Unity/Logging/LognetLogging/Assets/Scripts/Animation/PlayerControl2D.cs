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
/// Die y-Koordinate des gesteuerten Objekts wird abgefragt und
/// nicht verändert.
///
/// Wir erzeugen Protokollausgaben mit Log4Net.
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
	/// Grenzender Bewegung in x und z. Wir fragen diese Größen
	/// durch die Skalierungsfaktoren des Objekts im Inspektor ab.
	/// </summary>
	private float m_MinX, m_MaxX, m_MinZ, m_MaxZ;
	
	/// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Start abgefragt.
    /// </summary>
    private float m_Y;

	/// <summary>
	/// Instanz einesLog4Net Loggers
	/// </summary>
	private static readonly log4net.ILog Logger 
		= log4net.LogManager.GetLogger(typeof(PlayerControl2D));
	
	/// <summary>
    /// Der maximale Bewegungsbereich für den Player
    /// ist durch die Ausmasse eines Objekts  in der
    /// Basis-Szene gegeben.
    /// </summary>
    /// <remarks>
    /// Sinnvoll sind zum Beispiel der Boden oder der Kernbereich.
    /// </remarks>
    private void Start()
    {
	    // y-Koordinaten abfragen, damit wir sie konstant halten können.
	    m_Y = transform.position.y;
	    // Renderer des Boundary-Objekts abfragen 
	    // und die Maße der BBox davon als Werte
	    // die Grenzen der Bewegung in x und z verwenden!
	    var rend = Bounds.GetComponent<Renderer>();
	    if (rend == null) return;
	    var bounds = rend.bounds;
	    m_MinX = m_MinZ = -bounds.extents[0];
	    m_MaxX = m_MaxZ = bounds.extents[2];
    }

	/// <summary>
	/// Callback für die Composite Action Move.
	///<summary>
    private void OnMove(InputValue value)
    {
	    var results = value.Get<Vector2>();
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
	    
	    object[] args = {gameObject.name, 
		    gameObject.transform.position.x,
		    gameObject.transform.position.y,           
		    gameObject.transform.position.z,            
	    };
	    Logger.InfoFormat("{0}; {1}; {2}; {3}", args);
    }
}