//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
///Walking-in-Place mit einem Trigger-Objekt
/// </summary>
/// <remarks>
/// Wir beobachten die y-Koordinaten eine getrackten Objekts
/// und entscheiden damit,
/// ob wir uns fortbewegen m�chten. Keine weiteren Strategien.
///
/// Implementiert LLVM WiP.
///
/// Wir m�ssen hier noch Moving Average und weitere
/// Signalverarbeitung einbauen f�r die Steuerung der
/// Geschwindigkeit der Fortbewegung.
/// </remarks>
public class OneTriggerChangingSpeedWiP : InPlaceLocomotion
{
    /// <summary>
    /// Welchen Arm verwenden wir f�r das Triggern der Fortbewegung?
    /// </summary>
    [Header("WiP")]
    [Tooltip("Welches Objekt wird f�r die Fortbewegung bewegt?")]
    public GameObject TriggerObject;
    
    
    /// <summary>
    /// Delta f�r das Ver�ndern der Geschwindigkeit.
    /// </summary>
    public float DeltaSpeed = 0.2f;
    /// <summary>
    /// Maximale Geschwindigkeit f�r die Bewegung der Kamera in km/h.
    /// </summary>
    /// <remarks>
    /// Vorerst protected gesetzt. Mittelfristig werden wir
    /// die Geschwindigkeit aus der Bewegung auf der Stelle
    /// herauslesen.
    /// </remarks>
    ///         [Tooltip("Maximale Geschwindigkeit  in km/h")]
    public float MaximumSpeed = 10.0f;

    /// <summary>
    /// Walk wird so lange durchgef�hrt wie das Trigger-Objekt  bewegt wird.
    /// Das entscheiden wir auf Grund der Geschwindigkeit dieser
    /// Ver�nderung, die wir
    /// mit Hilfe von numerischem Differenzieren sch�tzen.
    /// </summary>
    /// <remarks>
    ///Wir vergleichen die Signalgeschwindigkeiten. Werden sie gr��er
    /// vergr��ern wir die Geschwindigkeit, wird sie kleiner
    /// bremen wir ab.
    /// </remarks>
    protected override void Trigger()
    {
        float position = 0.0f,
            signalVelocity = 0.0f;

        // Numerisches Differenzieren
        position = TriggerObject.transform.position.y;
        signalVelocity = Mathf.Abs((position - m_LastValue) / Time.deltaTime);
        
        Moving = signalVelocity > Threshold;
        if (Moving)
        {
            if (signalVelocity > m_LastSignalVelocity)
                m_Velocity.Increase();
            else
                m_Velocity.Decrease();
            Debug.Log(m_LastSignalVelocity);
            Debug.Log(m_Speed);
        }

        m_LastValue = position; 
        m_LastSignalVelocity = signalVelocity;
    }

    /// <summary>
    /// Speicher f�r den letzten Wert des Signales
    /// </summary>
    private float m_LastValue = 1.6f;

    /// <summary>
    /// Speicher f�r die Signalgeschwindigkeit im letzten Frame.
    /// </summary>
    private float m_LastSignalVelocity = 1f;
}
