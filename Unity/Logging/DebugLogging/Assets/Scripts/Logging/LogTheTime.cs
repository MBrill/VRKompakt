using System;
using UnityEngine;

/// <summary>
/// Beispiel für das Logging mit der Klasse Debug in Unity
/// </summary>
public class LogTheTime : MonoBehaviour
{
    /// <summary>
    /// Schalter für die Aktivierung von Log-Ausgaben
    /// </summary>
    public bool Logs = false;
    
    /// <summary>
    /// Start-Funktion mit Log-Ausgaben
    /// </summary>
    void Start()
    {
        if (Logs)
        {
            Debug.unityLogger.logEnabled = true;
            Debug.Log(">> " + gameObject.name + ".Start");
        }
        else
            Debug.unityLogger.logEnabled = false;
        
        DateTime time = DateTime.Now;
        
        Debug.LogWarning("** Datum und Zeit:  " + time);
        Debug.Log("** Stunde: " + time.Hour);
        object[] args = {time.Hour, time.Minute, time.Second};
        Debug.LogFormat("Zeitausgabe : Stunden: {0}, Minuten {1}, Sekunden {2}",
                args);
        Debug.LogFormat("{0}; {1}; {2}", args);

        Debug.Log("<< " + gameObject.name + ".Start");
    }
}
