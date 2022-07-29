using UnityEngine;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechecks in x und z-Koordinaten. Die y-Koordinate 
/// des bewegten Objekts wird abgefragt und unverändert übergeben.
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Grenzender Bewegung in x und z
    /// </summary>
     [Tooltip("Grenzen des Bewegungsraums")]
	public float MIN_X = -10,
	                   MAX_X = 10,
	                   MIN_Z = -5,
	                   MAX_Z = 5;

    /// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt.
    /// </summary>
    private float m_y;
    /// <summary>
    /// Geschwindigkeit der Bewegung
    /// </summary>
	private const float m_speed = 20.0F;
    /// <summary>
    /// Instanz einesLog4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
	    = log4net.LogManager.GetLogger(typeof(PlayerControl));
    /// <summary>
    /// Initialisierung
    /// 
    /// Wir fragen die y-Koordinate des GameObjects ab,
    /// die von uns nicht verändert wird.
    /// Wir benötigen diesen Wert für die Translation,
    /// mit der wir die Bewegung durchführen.
    ///
    /// Zusätzlich stellen wir den LogHander ein und
    /// erzeugen anschließend Log-Ausgaben in FixedUpdate.
    private void Awake()
    {
		m_y = transform.position.y;
    }

    /// <summary>
    /// Bewegung in FixedUpdate
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void FixedUpdate ()
    {
		KeyboardMovement();
		CheckBounds();
		
		object[] args = {gameObject.name, 
			gameObject.transform.position.x,
			gameObject.transform.position.y,           
			gameObject.transform.position.z,            
		};
		Logger.InfoFormat("{0}; {1}; {2}; {3}", args);
    }
	
    /// <summary>
    /// Abfragen der Achsen Horizontal und Vertical (das sind zum Beispiel
    /// die Cursortasten) und Translation an Hand dieser Eingaben.
    /// </summary>
	private void KeyboardMovement(){
		float dx = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
		float dz = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
		transform.Translate( new Vector3(dx, m_y, dz) );		
	}
	
    /// <summary>
    /// Überprüfen, ob die Grenzen eingehalten werden.
    /// </summary>
	private void CheckBounds(){
		float x = transform.position.x;
		float z = transform.position.z;
		x = Mathf.Clamp(x, MIN_X, MAX_X);
		z = Mathf.Clamp(z, MIN_Z, MAX_Z);
		
		transform.position = new Vector3(x, m_y, z);
	}
}
