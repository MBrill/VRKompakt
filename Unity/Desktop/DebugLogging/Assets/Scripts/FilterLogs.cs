using UnityEngine;

/// <summary>
/// Beispiel für das Filtern von Log-Ausgaben der
/// Klasse Debug mit Hilfe von filterLogType.
/// </summary>
public class FilterLogs : MonoBehaviour
{
    [Header("Einstellungen für das Logging")]
    /// <summary>
    /// Schalter für die Aktivierung von Log-Ausgaben
    /// </summary>
    [Tooltip("Logging-Ausgaben an oder aus?")]
    public bool Logs = false;
 
    /// <summary>
    /// Einstellbarer Tag für die Ausgaben
    /// </summary>
    [Tooltip("Text, der den Ausgaben vorangestellt wird")]
    public string myTag = "INFO";

    /// <summary>
    /// Einstellung für die Stufe, ab der Logs ausgegeben werden sollen.
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
            s_Logger.Log(myTag, ">> " + gameObject.name + 
                         "." + nameof(FilterLogs)+".Start");

        }
        else
            Debug.unityLogger.logEnabled = false;

        object[] args = {gameObject.name, 
                   gameObject.transform.position};
        s_Logger.LogFormat(LogType.Warning, "Position von {0}: {1}",  args);

        s_Logger.Log(myTag, "<< " + gameObject.name + 
                  "." + nameof(FilterLogs)+".Start");
    }


    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
}
