using UnityEngine;

/// <summary>
/// Beispiel f�r das Filtern von Log-Ausgaben 
/// mit Hilfe von filterLogType im Interface ILogger..
/// </summary>
public class FilterLogs : MonoBehaviour
{
    [Header("Einstellungen f�r das Logging")]
    /// <summary>
    /// Schalter f�r die Aktivierung von Log-Ausgaben
    /// </summary>
    [Tooltip("Logging-Ausgaben an oder aus?")]
    public bool Logs = false;
 
    /// <summary>
    /// Einstellbarer Tag f�r die Ausgaben
    /// </summary>
    [Tooltip("Text, der den Ausgaben vorangestellt wird")]
    public string myTag = "Information";

    /// <summary>
    /// Einstellung f�r die Stufe, ab der Logs ausgegeben werden sollen.
    /// </summary>
    [Tooltip("Welche Stufe von Logs soll ausgegeben werden?")]
    public LogType logLevel = LogType.Warning;
    
    /// <summary>
    /// Start-Funktion mit Log-Ausgaben
    /// </summary>
    void Start()
    {
        if (Logs)
        {
            Debug.unityLogger.logEnabled = true;
            s_Logger.filterLogType = logLevel;
        }
        else
            Debug.unityLogger.logEnabled = false;

        s_Logger.Log(myTag, ">> " + gameObject.name + 
                            "." + nameof(FilterLogs)+".Start");
        object[] args = {myTag,
                               gameObject.name, 
                               gameObject.transform.position};
        s_Logger.LogFormat(logLevel, "{0}: Position von {1} ist {2}",  args);
        s_Logger.Log(myTag, "<< " + gameObject.name + 
                                  "." + nameof(FilterLogs)+".Start");
    }
    
    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
}
