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
    [Header("Protokollieren")]
    [Tooltip("Anzeige des Vektors, der für die Verfolgung berechnet wird")] 
	public bool showRay = false;
    
    /// <summary>
    /// Dateiname für die Logs
    /// </summary>
    [Tooltip("Name der Log-Datei")]
    public string fileName = "MoveAndLog.csv";
    
    /// <summary>
    /// Initialisierung
    ///
    /// Wir stellen den LogHander ein und
    /// erzeugen anschließend Log-Ausgaben in LateUpdate.
    private void Awake()
    {
	    csvLogHandler = new CustomLogHandler(fileName);
    }
    
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

        object[] args = {gameObject.name, 
	        gameObject.transform.position.x,
	        gameObject.transform.position.y,           
	        gameObject.transform.position.z,            
        };
        s_Logger.LogFormat(LogType.Log, gameObject,
	        "{0:c};{1:G}; {2:G}; {3:G}", args);
        
        if (showRay)
        {
	        // Länge des Strahls: die halbe Distanz zwischen verfolgtem Objekt und Verfolger
	        var dist = 0.5F * Vector3.Distance(playerTransform.position, source);
	        Debug.DrawRay(transform.position,
		        dist * transform.TransformDirection(Vector3.forward),
		        Color.red);
        }
    }
    
    /// <summary>
    /// Schließen der Protokolldatei
    /// </summary>
    private void OnDisable()
    {
	    csvLogHandler.CloseTheLog();
    }
    
    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    private CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
}
