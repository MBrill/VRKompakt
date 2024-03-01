//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Bewegung eines GameObjects
/// innerhalb eines Rechtecks in x und z-Koordinaten.
/// </summary>
/// <remarks>
/// Das Rechteck wird durch die Bounding Box eines Objekts
/// in der Szene definiert.
/// Die y-Koordinate des gesteuerten Objekts wird abgefragt und
/// nicht verändert.
/// </remarks>
public class Player2D : MonoBehaviour
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
	[Tooltip("Objekt, das die Bewegungen begrenzt")]
	public GameObject Bounds;
	
	/// <summary>
	/// Dateiname für die Logs
	/// </summary>
	[Tooltip("Name der Log-Datei")]
	public string fileName = "player.csv";

	/// <summary>
	/// Eigener LogHandler
	/// </summary>
	private CustomLogHandler csvLogHandler;

	/// <summary>
	/// Instanz des Default-Loggers in Unity
	/// </summary>
	private static readonly ILogger s_Logger = Debug.unityLogger;
	

	/// <summary>
	/// Grenzen der Bewegung in x und z. Wir fragen diese Größen
	/// durch die AABB  des Objekts ab.
	/// </summary>
	private float m_MinX, m_MaxX, m_MinZ, m_MaxZ;
	
	/// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Start abgefragt.
    /// </summary>
    private float m_Y;

	/// <summary>
    /// Der maximale Bewegungsbereich für den Player2D
    /// ist durch die Ausmasse eines Objekts  in der
    /// Szene gegeben. Wir fragen die AABB des Objekts ab
    /// und setzen damit die Grenzen in x- und z-Richtung.
    /// </summary>
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
	    var center = rend.bounds.center;
	    var extents = rend.bounds.extents;
	    m_MinX = center[0] - extents[0];
	    m_MaxX = center[0] + extents[0];
	    m_MinZ = center[2] - extents[2];
	    m_MaxZ = center[2] + extents[2];
	    
		    csvLogHandler = new CustomLogHandler(fileName);
    }

    /// <summary>
    /// Ausführen der Bewegung.
    /// </summary>
    private void Update()
    {
	    if (!m_Moving)
		    return;
	    m_Move();
	    
	    object[] args = {gameObject.name, 
		    gameObject.transform.position.x,
		    gameObject.transform.position.y,           
		    gameObject.transform.position.z,            
	    };
	    s_Logger.LogFormat(LogType.Log, gameObject,
		    "{0:c};{1:G}; {2:G}; {3:G}", args);
    }
    
    /// <summary>
    /// Bewegung ausführen.
    /// </summary>
    private void m_Move()
    {
	    var newPos = Vector3.MoveTowards(
		    transform.position, 
		    transform.position + m_Delta,
		    Speed * Time.deltaTime);
	    
	    newPos.x = Mathf.Clamp(newPos.x, m_MinX, m_MaxX);
	    newPos.z = Mathf.Clamp(newPos.z, m_MinZ, m_MaxZ );
	    
	    transform.position = new Vector3(newPos.x, m_Y, newPos.z);	    
    }
    
    protected void OnDisable()
    {
	    csvLogHandler.CloseTheLog();
    }
    /// <summary>
    /// Wird eine Bewegung ausgeführt oder nicht?
    /// </summary>
    protected bool m_Moving = false;
    /// <summary>
    /// Vektor für die Speicherung der Veränderung der Position
    /// aus dem Input System.
    /// </summary>
    protected Vector3 m_Delta = new Vector3();
}
