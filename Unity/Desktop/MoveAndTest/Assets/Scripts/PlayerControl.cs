using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechtecks in x und z-Koordinaten. Die y-Koordinate 
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
    /// Initialisierung
    /// 
    /// Wir fragen die y-Koordinate des GameObjects ab,
    /// die von uns nicht verändert wird.
    /// Wir benötigen diesen Wert für die Translation,
    /// mit der wir die Bewegung durchführen.
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
    }
	
    /// <summary>
    /// Abfragen der Cursor-Tasten mit dem neuen InputSystem.
    /// Dazu müssen wir die using-Anweisung wie oben machen
    /// und auch das InputSystem dem Assembly hinzufügen.
    /// </summary>
	private void KeyboardMovement()
    {
	    Keyboard keyboard = Keyboard.current;

	    if (keyboard == null)
	    {
		    return;
	    }
	    
	    // Neues Input-System
	    // Mit ReadValue können wir lesen, ob gedrückt (1) oder nicht (0)
	    float forward = keyboard.upArrowKey.ReadValue()
	                    - keyboard.downArrowKey.ReadValue();
	    float horizontal = keyboard.rightArrowKey.ReadValue()
	                       - keyboard.leftArrowKey.ReadValue();
	    
		float dx = horizontal * m_speed * Time.deltaTime;
		float dz = forward * m_speed * Time.deltaTime;
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
