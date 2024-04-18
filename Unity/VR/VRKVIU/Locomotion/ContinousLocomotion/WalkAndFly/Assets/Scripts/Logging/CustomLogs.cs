using UnityEngine;

/// <summary>
/// Protokoll-Ausgaben in einer ASCII-Datei mit Hilfe von CustomLogHandler.
/// </summary>
public class CustomLogs : MonoBehaviour
{
    /// <summary>
    /// Dateiname für die Logs
    /// </summary>
    [Tooltip("Name der Log-Datei")]
    public string fileName = "loggingExample.csv";
    
    /// <summary>
    /// Ausgaben in der Start-Funktion.
    /// </summary>
    void Start()
    {
        csvLogHandler = new CustomLogHandler(fileName);
        
        object[] args = {gameObject.name, 
            gameObject.transform.position.x,
            gameObject.transform.position.y,           
            gameObject.transform.position.z,            
        };
        s_Logger.LogFormat(LogType.Warning, gameObject,
            "{0:c};{1:G}; {2:G}; {3:G}", args);
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
