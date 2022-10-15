using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechecks in x und z-Koordinaten. Die y-Koordinate 
/// des bewegten Objekts wird abgefragt und nicht verändert.
/// </summary>
public class PlayerControl2D : MonoBehaviour
{
	/// <summary>
	/// Geschwindigkeit der Bewegung
	/// </summary>
	 [Tooltip("Geschwindigkeit")]
	[Range(1.0f, 20.0f)]
	public float Speed = 10.0f;
	
	/// <summary>
	/// Wir folgen der Dokumentation von Unity
	/// und dem Abschnitt "Embedding Actions in MonoBehaviours".
	///
	/// Wir erzeugen eine Composite-Action, die als Ergebnis
	/// einen Vector2D erzeugt. 
	/// </summary>
	public InputAction PlayAction;
	
	/// <summary>
	/// Grenzender Bewegung in x und z. Wir fragen diese Größen
	/// durch die Skalierungsfaktoren des Bodens ab.
	/// </summary>
	private float m_minx, m_maxx, m_minz, m_maxz;
	
	/// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt.
    /// </summary>
    private float m_y;

	/// <summary>
    /// Die Deltas für die Bewegung mit Hilfe der Tasten
    /// Left, Right, Up, Down.
    /// </summary>
    /// <remarks>
    /// Up und Down beziehen sich hier auf die z-Achse,
    /// wir lassen die y-Koordinaten des Verfolgers unverändert!
    /// </remarks>
    private float m_dx, m_dz;

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
    /// ist durch die Ausmasse des Bodens in der
    /// Basis-Szene gegeben.
    /// </summary>
    private void Start()
    {
	    m_minx = m_minz -3.0f;
	    m_maxx = m_maxz = 3.0f;

	    // y-Koordinaten abfragen, damit wir sie konstant halten können.
	    m_y = transform.position.y;
    }
    
    /// <summary>
    /// In Disable für die Szene de-aktivieren wir auch unsere Action.
    /// </summary>
    private void OnDisable()
    {
	    PlayAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
	    var results = ctx.ReadValue<Vector2>();
	    var trans = new Vector3(results.x, 0.0f, results.y);
	    var newposition = transform.position + trans;
	    transform.position = Vector3.MoveTowards(
		    transform.position,
		    newposition,
		    Speed * Time.deltaTime);
    }

    /// <summary>
    /// Überprüfen, ob die Grenzen eingehalten werden.
    /// </summary>
	private void CheckBounds(){
		float x = transform.position.x;
		float z = transform.position.z;
		x = Mathf.Clamp(x, m_minx, m_maxx);
		z = Mathf.Clamp(z, m_minz, m_maxz);
		
		transform.position = new Vector3(x, m_y, z);
	}
}
