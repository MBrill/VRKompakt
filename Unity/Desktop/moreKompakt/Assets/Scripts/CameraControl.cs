using UnityEngine;
//using System.Collections;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe von Cursortasten 
/// innerhalb eines Intervalls in z-Richtung. 
/// Die x- und y-Koordinaten 
/// des bewegten Objekts werden abgefragt, aber nicht verändert.
/// </summary>
public class CameraControl : MonoBehaviour {

    /// <summary>
    /// Intervall, innerhalb dessen wir das 
    /// GameObject bewegen.
    /// 
    /// public-Variable einer Klasse, die von
    /// MonoBehaviour abgeleitet wird erscheinen
    /// im Inspector des GameObjects und können
    /// interaktiv verändert werden.
    /// </summary>
	public float minZ = -3.0f,
                 maxZ =  1.5f;
    /// <summary>
    /// x- und y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt.
    /// </summary>
    private float x, y;
    /// <summary>
    /// Konstante Geschwindigkeit der Bewegung.
    /// Die Einheit dieser Größe ist m/s.
    /// </summary>
	public float speed = 1.0f;
    /// <summary>
    /// x- und y-Position des gesteuerten Objekts abfragen.
    /// Die Position des GameObjects, dem diese Klasse
    /// zugeordnet ist können wir mit transform.position
    /// abfragen.
    /// </summary>
    private void Awake()
    {
        x = transform.position.x;
        y = transform.position.y;
	}

    /// <summary>
    /// Events abfragen, Variablen verändern und
    /// alles bereit machen für das Rendern des
    /// nächsten Frames.
    /// </summary>
    private void Update ()
    {
        // Keyboard-Events abfragen und verarbeiten
        KeyboardMovement();
        // Sicherstellen, dass wir den definieren Bereich
        // nicht verlassen
		CheckBounds();
	}
	
    /// <summary>
    /// Abfragen der Vertical-Achse.
    /// Das können die Cursortasten sein, aber auch eine Achse
    /// eines Joysticks, je nach Konfiguration in Unity. 
    /// Wir fragen den Wert ab, multiplizieren ihn mit unserer
    /// Geschwindigkeit und der Zeit, die seit dem letzten
    /// Frame vergangen ist.
    /// 
    /// Update wird nicht in äquidistanten Zeitintervallen,
    /// sondern abhängig von der Frame-Rate, aufgerufen.
    /// 
    /// Time.deltaTime wird in Sekunden angegeben.
    /// </summary>
	private void KeyboardMovement() {
		float dz = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		transform.Translate( new Vector3(x, y, dz) );		
	}
	
    /// <summary>
    /// Überprüfen, ob die Grenzen des Intervalls eingehalten werden.
    /// Wir führen ein Clamp durch, dazu verwenden wir die Funktion
    /// Clamp in Mathf.
    /// </summary>
	private void CheckBounds() {
        // Abfragen der z-Position des GameObjects
		float z = Mathf.Clamp(transform.position.z, minZ, maxZ);		
		transform.position = new Vector3(x, y, z);
	}
}
