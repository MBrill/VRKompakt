using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Maus 
/// innerhalb eines Recthecks in x und z-Koordinaten.
/// Die y-Koordinate des bewegten Objekts wird abgefragt
/// und unverändert übergeben.
/// </summary>
public class PlayerControl : MonoBehaviour
{
	/// <summary>
	/// Multiplikator für die Bewegungsgeschwindigkeit
	/// </summary>
	public float Speed = 20.0f;
    /// <summary>
    /// Grenzender Bewegung in x und z
    /// </summary>
     [Tooltip("Grenzen des Bewegungsraums")]
	public float MIN_X = -10,
	                   MAX_X = 10,
	                   MIN_Z = -5,
	                   MAX_Z = 5;
    
    /// Initialisierung
    /// 
    /// Wir fragen die y-Koordinate des GameObjects ab,
    /// die von uns nicht verändert wird.
    /// Wir benötigen diesen Wert für die Translation,
    /// mit der wir die Bewegung durchführen.
    private void Awake()
    {
		m_y = transform.position.y;
		m_Moving = false;
		
		mouse = Mouse.current;
		if (mouse == null)
		{
			Debug.Log("Keine Maus gefunden!");
			return;
		}

		keyboard = Keyboard.current;
		if (keyboard == null)
		{
			Debug.Log("Keine Tastatur gefunden!");
			return;
		}
    }

    /// <summary>
    /// Bewegung in FixedUpdate
    /// </summary>
    /// <remarks
    /// Erster Schritt: Maus abfragen und bewegen.
    /// Zweiter Schritt: überprüfen, ob wir im zulässigen Bereich sind.
    /// </remarks>
    private void FixedUpdate ()
    {
		Movement();
		CheckBounds();
    }
	
    /// <summary>
    /// Abfragen der Maus mit dem neuen InputSystem.
    /// Dazu müssen wir die using-Anweisung wie oben machen
    /// und auch das InputSystem dem Assembly hinzufügen.
    /// </summary>
	private void Movement()
    {
	    float horizontal, vertical, dx, dz;
	    
	    //if (mouse.leftButton.isPressed)
	    //
	    keyboard[KeyCode.D].wasPressedThisFrame
	    horizontal = keyboard.dKey.ReadValue()
	                 - keyboard.aKey.ReadValue(); 
		    vertical = keyboard.wKey.ReadValue()
	                 - keyboard.sKey.ReadValue();
		    dx = horizontal * Speed * Time.deltaTime;
		    dz = vertical * Speed * Time.deltaTime;
		    transform.Translate(new Vector3(dx, m_y, dz));
	   /* }
	    else
	    {
		    Debug.Log("Nichts");
	    }*/
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
    
    /// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt.
    /// </summary>
    private float m_y;
    /// <summary>
    /// Soll Bewegung durchgeführt werden
    /// </summary>
    private bool m_Moving;
    /// <summary>
    /// Instanz der Maus im InputSystem
    /// </summary>
    private Mouse mouse;
    /// <summary>
    /// Instanz der Tastatur im InputSystem
    /// </summary>
    private Keyboard keyboard;
}
