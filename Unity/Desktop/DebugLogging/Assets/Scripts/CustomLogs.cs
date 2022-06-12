using UnityEngine;

public class CustomLogs : MonoBehaviour
{
    [Header("Einstellungen für das Logging")]
    /// <summary>
    /// Einstellbarer Tag für die Ausgaben
    /// </summary>
    [Tooltip("Text, der den Ausgaben vorangestellt wird")] 
    public string myTag = "INFO";
    
    void Start()
    {
        csvLogHandler = new CustomLogHandler();
        
        object[] args = {gameObject.name, 
            gameObject.transform.position.x,
            gameObject.transform.position.y,           
            gameObject.transform.position.z,            
        };
        s_Logger.LogFormat(LogType.Warning, gameObject,
            "{0:c};{1}; {2}; {3}", args);
    }
    
    private CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
}
