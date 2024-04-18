//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse f�r Locomotion-Verfahren,
/// die Scaled Walking einsetzen.
/// </summary>
/// <remarks>
/// Wir gehen davon aus, dass wir Real Walking durchf�hren k�nnen.
/// Ist die Ganggeschwindigkeit gr��er als ein Schwellwert l�sen
/// wir Scaled Walking aus.
/// </remarks>
public abstract class ScaledWalking : Locomotion
{
    [Header("Scaled Walking")]
    /// <summary>
    /// Welches GameObject verwenden wir f�r das Ablesen der Blickrichtungg?
    /// </summary>
    /// <remarks>
    /// Sinnvoll ist der Kopf.
    /// </remarks>
    [Tooltip("Objekt, das wir f�r die Blickrichtung einsetzen")]
    public GameObject OrientationObject;
    
    /// <summary>
    /// Ist die gesch�tzte Bewegungsgeschwindigkeit gr��er als
    /// dieser Wert wird Scaled Walking ausgel�st.
    /// </summary>
    [Tooltip("Schwellwert f�r das Ausl�sen der Skalierung in km/h")] 
    [Range(0.01f, 5.0f)]
    public float Threshold = 1.0f;
    
    /// <summary>
    /// Geschwindigkeit bei der die maximale Skalierung erreicht
    /// werden soll. Muss gr��er sein als Threshold!
    /// </summary>
    /// <remarks>
    /// Default die ist drei-mal der Wert von Threshold.
    /// </remarks>
    [Tooltip("Ab welcher Geschwindigkeit in km/h soll die maximale Skalierung erreicht sein?")] 
    [Range(0.01f, 6.0f)]
    public float ScalingThreshold = 3.0f;
    
    /// <summary>
    ///Maximaler Wert f�r die Skalierung.
    /// </summary>
    /// <remarks>
    ///Passend zu den Siebenmeilenstiefeln w�hlen wir
    /// als Default die 7.
    /// </remarks>
    [Tooltip("Maximale Skalierung der Geschwindigkeit")] 
    [Range(2.0f, 10.0f)]
    public float MaximumScale = 7.0f;

    /// <summary>
    /// Aktivieren der Protokollierung der internen
    /// Berechnungen des Verfahrens.
    /// </summary>
    [Header("Logging")] 
    [Tooltip("Protokollieren?")]
    public bool Logs = false;
    
    /// <summary>
    /// Dateiname f�r die Logs
    /// </summary>      
    [Tooltip("Name der Protokoll-Datei")]
    public string fileName = "sevenleagueboots.csv";
    
    /// <summary>
    /// Update aufrufen und die Skalierung ausf�hren,
    /// falls sie aktiv ist,
    /// </summary>
    /// <remarks>
    /// Wir setzen die Bewegungsrichtung als Konvexkombination
    /// zwischen der Blickrichtung und einer Prognose f�r die Richtung
    /// in der Funktion UpdateDirection.
    /// </remarks>
    protected virtual void Update()
    {
        Trigger();
        if (!Moving) return;
        Move();
    } 
    
    /// <summary>
    /// Die abgeleiteten Klassen entscheiden, wann die Locomotion
    /// getriggert werden.
    /// </summary>
    protected abstract void Trigger();
    
    /// <summary>
    /// Die Bewegung durchf�hren.
    /// </summary>
    /// <remarks>
    /// Die Funktion geht davon aus, dass die �berpr�fung
    /// der Variable Moving vor dem Aufruf bereits durchgef�hrt wurde!
    /// <remarks>
    protected override void Move()
    {
        // Wir berechnen die nichtlineare manipulation und alles
        // andere in Trigger.
        // Das Ergebnis ist ein nicht-nomrierter Vektor
        // m_Direction f�r die neue Position, der hier f�r
        // die Transfoation eingesetzt wird.
        transform.Translate(m_Direction);
    }
    
    /// <summary>
    /// Geschwindigkeit initialiseren.
    /// </summary>
    protected override void InitializeSpeed()
    {
        // Wir haben noch keine Geschwindigkeit, deshalb verwenden 
        // wir den Default-Konstruktor und setzen m_Speed auf 0.0.
        m_Velocity = new HermiteBlend();
        // m_Speed sollten wir nicht ben�tigen, wir setztn das auf 0.
        m_Speed = 0.0f;
    }
    
    /// <summary>
    /// Zu Beginn verwenden wir die Blickrichtung als
    /// Bewegungsrichtung, bis der Trigger
    /// ausl�st uund die Konvexkombination gebildert wird.
    /// </summary>
    /// <remarks>
    /// Wir f�hren ein Walk durch, deshalb setzen wir die y-Koordinate
    /// der Richtung auf 0.
    /// </remarks>
    protected override void InitializeDirection()
    {
        m_Direction = OrientationObject.transform.forward;
        m_Direction.y = 0.0f;
        m_Direction.Normalize();
    }

    /// <summary>
    /// �berschreibbare nichtlineare Manipulation.
    /// </summary>
    /// <param name="t">Skalarer Wert, der manipuliert wird.</param>
    /// <returns>Manipulierter Wert</returns>
    protected virtual float NonlinearScaling(float t)
    {
        return MaximumScale * Mathf.SmoothStep(0.0f,
            1.0f,
            (t - 1.0f) / (ScalingThreshold - 1.0f)
            ) + 1.0f;
    }
    
    /// <summary>
    /// Schlie�en der Protokolldatei
    /// </summary>
    private void OnDisable()
    {
        csvLogHandler.CloseTheLog();
    }
    
    /// <summary>
    /// Wird aktuell nicht verwendet, da wir die Bewegungsrichtung
    /// als Konvexkombination aus Blickrichtung und einer Prognose
    /// bestimmen.
    /// </summary>
    protected override void UpdateOrientation()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Verwenden wir in dieser Technik aktuell nicht!
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected override void UpdateSpeed()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    protected CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    protected static readonly ILogger s_Logger = Debug.unityLogger;
}
