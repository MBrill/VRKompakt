using UnityEngine;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Transform.MoveTowards und Transform.LookAt.
/// </summary>
/// 
public class FollowTheTarget : MonoBehaviour
{
    /// <summary>
    /// Position und Orientierung des verfolgten Objekts
    /// </summary>
    [Tooltip("Das verfolgte Objekt")]
    public Transform playerTransform;
    /// <summary>
    /// Geschwindigkeit des Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit")]
    [Range(1.0F, 20.0F)]
    public float speed = 10.0F;
    /// <summary>
    /// Soll der Vektor zwischen Ziel und dem aktuellen Objekt angezeigt werden?
    /// </summary>
    [Tooltip("Anzeige des Vektors, der für die Verfolgung berechnet wird")] 
	public bool showRay = false;
    
    /// <summary>
    /// Bewegung in LateUpdate
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: Überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    /// <remarks>
    /// Wir geben die forward-Richtung des gesteuerten Objekts
    /// mit Hilfe von Debug.dDrawRay aus. Darauf achten, dass die
    /// Ausgabe der Gizmos im Player aktiviert ist!
    /// </remarks>
    private void LateUpdate ()
    {
        // Schrittweite
		float stepSize = speed * Time.deltaTime;

        Vector3 source = transform.position;
		Vector3 target = playerTransform.position;

        // Neue Position berechnen
		transform.position = Vector3.MoveTowards(source, target, stepSize);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(playerTransform);
        
        if (showRay)
        {
	        // Länge des Strahls: die halbe Distanz zwischen verfolgtem Objekt und Verfolger
	        var dist = 0.5F * Vector3.Distance(playerTransform.position, source);
	        Debug.DrawRay(transform.position,
		        dist * transform.TransformDirection(Vector3.forward),
		        Color.red);
        }
    }
}
