//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Protokollieren der Fortbewegung.
/// </summary>
/// <remarks>
/// Wir gehen davon aus, dass dies eine Komponente des Rigs ist.
/// Als weiteres GameObject verbinden wir die Camera des Rigs,
/// damit k�nnen wir die realen Positionen, z.B. bei Scaled Walking
/// und RDW protokollieren.
/// </remarks>
public class LocomotionLogger : MonoBehaviour
{
    [Header("Protokollieren von virtuellen und realen Positionen")]
    /// <summary>
    /// Camera oder anderes Objekt, mit dem wir die reale Positionen
    /// im Arbeitsbereich protokollieren k�nnen.
    /// </summary>
    [Tooltip("GameObject f�r die reale Position")]
    public GameObject PlayAreaPosition;
    
    /// <summary>
    /// Protokollieren aktivieren und de-aktivieren
    /// </summary>
    [Tooltip("Protokollieren?")]
    public bool Logs = false;
    
    /// <summary>
    /// Dateiname f�r die Logs
    /// </summary>
    [Tooltip("Name der Protokoll-Datei")]
    public string FileName = "locomotion.csv";
    
    /// <summary>
    /// Initialisierung
    ///
    /// Wir stellen den LogHander ein und
    /// erzeugen anschlie�end Log-Ausgaben in LateUpdate.
    void Awake()
    {
        if (!Logs)
            Debug.unityLogger.logEnabled = false;
        
        csvLogHandler = new CustomLogHandler(FileName);
        // Header
        object[] args = {"Zeit",
            "Kopf.x",
            "Kopf.y",
            "Kopf.z",
            "Real.x",
            "Real.y",
            "Real.z"
        };
        s_Logger.LogFormat(LogType.Log, gameObject,
            "{0:G};{1:G};{2:G};{3:G}; {4:G}; {5:G}; {6:G}", args);
        
        deviceIndex = role.GetDeviceIndex();
    }

    /// <summary>
    /// CSV-Daten protokollieren
    /// </summary>
    void Update()
    {
        var pos = VivePose.GetPose(role).pos;
        var headPos =  PlayAreaPosition.transform.position;
        object[] args = {Time.time,
            PlayAreaPosition.transform.position.x,
            PlayAreaPosition.transform.position.y,
            PlayAreaPosition.transform.position.z,
            PlayAreaPosition.transform.localPosition.x,
            PlayAreaPosition.transform.localPosition.y,
            PlayAreaPosition.transform.localPosition.z,
            /*pos.x,
            pos.y,
            pos.z*/
        };
        s_Logger.LogFormat(LogType.Log, gameObject,
            "{0:G};{1:G};{2:G}; {3:G}; {4:G}; {5:G}; {6:G}", args);
    }

    /// <summary>
    /// Schlie�en der Protokolldatei
    /// </summary>
    private void OnDisable()
    {
        csvLogHandler.CloseTheLog();
    }
    
    /// <summary>
    /// ViveRole f�r den Kopf
    /// </summary>
    private ViveRoleProperty role = ViveRoleProperty.New(BodyRole.Head);
    /// <summary>
    /// Ger�te-Index f�r das Ansprechen �ber diese Schnittstelle
    /// </summary>
    private uint deviceIndex;

    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    private CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
}
