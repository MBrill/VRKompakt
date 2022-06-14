using System;
using UnityEngine;
using log4net;

/// <summary>
/// Beispiel für das Logging mit log4net in Unity
/// </summary>
public class LogTheTime : MonoBehaviour
{
    /// <summary>
    /// Start-Funktion mit Log-Ausgaben
    /// </summary>
    void Start()
    {
        Logger.Info(">> " + gameObject.name + ".Start");
        DateTime time = DateTime.Now;
        Logger.Warn("** Datum und Zeit:  " + time);
        Logger.Info("<< " + gameObject.name + ".Start");
    }

    /// <summary>
    /// Instanz eines Loggers
    /// </summary>
    private static readonly log4net.ILog Logger = 
        log4net.LogManager.GetLogger(nameof(LogTheTime));
}
