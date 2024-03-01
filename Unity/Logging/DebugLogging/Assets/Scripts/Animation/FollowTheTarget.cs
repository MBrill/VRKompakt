//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Vector3.MoveTowards und Transform.LookAt.
/// </summary>
public class FollowTheTarget : MonoBehaviour
{
    /// <summary>
    /// Position und Orientierung des verfolgten Objekts
    /// </summary>
    [Tooltip("Das verfolgte Objekt")]
    public Transform PlayerTransform;

    /// <summary>
    /// Wir können das Verfolgen an- und ausschalten.
    /// </summary>
    public bool IsFollowing = false;

    /// <summary>
    /// Geschwindigkeit des Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit")]
    [Range(1.0F, 20.0F)]
    public float Speed = 10.0F;

    /// <summary>
    /// Dateiname für die Logs
    /// </summary>
    [Tooltip("Name der Log-Datei")]
    public string fileName = "follower.csv";

    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    private CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
    
    /// <summary>
    /// Initialisierung
    ///
    /// Wir stellen den LogHander ein und
    /// erzeugen anschließend Log-Ausgaben.
    private void Awake()
    {
        csvLogHandler = new CustomLogHandler(fileName);
    }
    
    /// <summary>
    /// Bewegung in Update
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: Überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void Update ()
    {
        if (!IsFollowing) return;
        transform.position = Vector3.MoveTowards(transform.position,
            PlayerTransform.position,
            Speed * Time.deltaTime);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(PlayerTransform);
        
        object[] args = {gameObject.name, 
            gameObject.transform.position.x,
            gameObject.transform.position.y,           
            gameObject.transform.position.z,            
        };
        s_Logger.LogFormat(LogType.Log, gameObject,
            "{0:c};{1:G}; {2:G}; {3:G}", args);
    }

    /// <summary>
    /// Callback für die Action Following im Input Asset
    /// </summary>
    private void OnFollowing()
    {
        IsFollowing = !IsFollowing;
    }
    
    /// <summary>
    /// Schließen der Protokolldatei
    /// </summary>
    private void OnDisable()
    {
        csvLogHandler.CloseTheLog();
    }
}
