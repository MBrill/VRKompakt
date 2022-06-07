using System;
using UnityEngine;

/// <summary>
/// Beispiel f�r das Logging mit der Klasse Debug in Unity
/// </summary>
public class LogTheTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Logs) Debug.Log(">> Start");
        DateTime time = DateTime.Now;
        
        // Ausgaben mit Debug.Log
        if (Logs)
        {
            Debug.LogWarning("** Datum und Zeit:  " + time);
            Debug.Log("** Stunde: " + time.Hour);
            Debug.Log("** Minute: " + time.Minute);
            Debug.Log("** Sekunde: " + time.Second);
        }

        if (Logs) Debug.Log("<< Start");
    }

    /// <summary>
    /// Schalter f�r die Aktivierung von Log-Ausgaben
    /// </summary>
    public bool Logs = false;
}
