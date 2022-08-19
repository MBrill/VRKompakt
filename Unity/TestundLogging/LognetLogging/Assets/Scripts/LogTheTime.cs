using UnityEngine;

/// <summary>
/// Beispiel für das Logging mit log4net in Unity
/// </summary>
public class LogTheTime : MonoBehaviour
{
    /// <summary>
    /// Instanz eines Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(LogTheTime));
    
    
    /// <summary>
    /// Start-Funktion mit Log-Ausgaben der Zeit.
    /// </summary>
    private void Start()
    {
        Logger.Debug(">> " + gameObject.name + "." + nameof(LogTheTime) + ".Start");
        var time = System.DateTime.Now;
        Logger.WarnFormat("Datum und Zeit: {0:D}", time);
        Logger.Debug("<< " + gameObject.name + "." + nameof(LogTheTime) + ".Start");
    }
}
