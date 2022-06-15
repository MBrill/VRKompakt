using System.IO;
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
        //= log4net.LogManager.GetLogger(typeof(LogTheTime));
        = log4net.LogManager.GetLogger("foo");
    private  void Awaket()
    {
        Debug.Log("In Awake!");
        log4net.Config.XmlConfigurator.ConfigureAndWatch(
            new FileInfo($"{Application.dataPath}/Resources/Log4NetConfig.xml")
        );
        Debug.Log("Awake für LogTheTime");
    }
    
    /// <summary>
    /// Start-Funktion mit Log-Ausgaben der Zeit.
    /// </summary>
    private void Start()
    {
        Logger.Debug(">> " + gameObject.name + "." +nameof(LogTheTime) + ".Start");
        System.DateTime time = System.DateTime.Now;
        Logger.Info("Datum und Zeit:  " + time);
        Logger.Debug("<< " + gameObject.name + "." +nameof(LogTheTime) + ".Start");
    }
}
