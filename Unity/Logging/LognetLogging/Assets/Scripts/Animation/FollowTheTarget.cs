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
    /// Instanz einesLog4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
	    = log4net.LogManager.GetLogger(typeof(FollowTheTarget));
    
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

        var source = transform.position;
		var target = playerTransform.position;

        // Neue Position berechnen
		transform.position = Vector3.MoveTowards(source, target, stepSize);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(playerTransform);

        object[] args = {gameObject.name, 
	        gameObject.transform.position.x,
	        gameObject.transform.position.y,           
	        gameObject.transform.position.z,            
        };
        Logger.InfoFormat("{0}; {1}; {2}; {3}", args);
    }
}
