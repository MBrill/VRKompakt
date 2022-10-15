using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechecks in x und z-Koordinaten. Die y-Koordinate 
/// des bewegten Objekts wird abgefragt und unverändert übergeben.
/// </summary>
public class PlayerControl2D : MonoBehaviour
{

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
    /// Geschwindigkeit der Bewegung
    /// </summary>
	private const float m_speed = 20.0F;

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
    /// Bewegung in FixedUpdate
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void Update ()
    {
	    float dx = 0.0f, 
		    dz = 0.0f;

	    if (playerInput.actions["Up"].WasReleasedThisFrame())
	    {
		    dz = 1.0f * m_speed * Time.deltaTime;
		    Debug.Log("Up hat funktioniert");
	    }



	    Vector3 trans = new Vector3(dx, 0.0f, dz);
	    transform.Translate( trans );		
	    //Debug.Log(transform.position);
		//KeyboardMovement();
		//CheckBounds();
    }
	

	/*private void KeyboardMovement()
    {
	    float dx = 0.0f, dz = 0.0f;
	    var moveDirection = moveAction.ReadValue<Vector2>();

	    if (moveDirection.x != 0)
	    {
		    Debug.Log("x changed");
		    Debug.Log(moveDirection.x);
	    }

	    if (moveDirection.y != 0)
		    Debug.Log(moveDirection.y);

	    if (moveDirection != Vector2.zero)
	    {
		    dx = moveDirection.x * m_speed * Time.deltaTime;
		    Debug.Log(dx);
		    dz = moveDirection.y * m_speed * Time.deltaTime;
		    Debug.Log(dz);
	    }

	    //transform.position.x += dx;
	   // transform.position.z += dz;
	    transform.Translate( new Vector3(dx, 0.0f, dz) );		
		//Debug.Log(transform.position);
    }*/
	
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
