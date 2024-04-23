//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für Joystick-based Locomotion, die
/// steerig metaphor..
 /// </summary>
public abstract class JoystickLocomotion : Locomotion
{
    [Header("Locomotion")]
    /// <summary>
    /// Welches GameObject verwenden wir für die Definition der Richtung?
    /// </summary>
    /// <remarks>
    /// Sinnvoll ist einer der beiden Controller, aber auch andere
    /// GameObjects (wie der Kopf oder ein Vive Tracker) können
    /// sinnvoll eingesetzt  werden.
    /// </remarks>
    [Tooltip("GameObject, das die Bewegungsrichtung definiert")]
    public GameObject OrientationObject;
    
        /// <summary>
        /// Geschwindigkeit für die Bewegung der Kamera in km/h
        /// </summary>
        [Tooltip("Geschwindigkeit")]
        [Range(0.1f, 20.0f)]
        public float InitialSpeed = 5.0f; 
        
        /// <summary>
        /// Maximal mögliche Geschwindigkeit in km/h.
        /// </summary>
        [Tooltip("Maximal mögliche Geschwindigkeit")]
        [Range(0.001f, 20.0f)]
        public float MaximumSpeed = 10.0f;

        /// <summary>
        /// Delta für das Verändern der Geschwindigkeit in km/h.
        /// </summary>
        [Tooltip("Delta für die Veränderung der Bahngeschwindigkeit")]
        [Range(0.001f, 2.0f)]
        public float DeltaSpeed = 0.2f;
        
        /// <summary>
        /// Update aufrufen und die Bewegung ausführen.
        /// </summary>
        /// <remarks>
        ///Wir verwenden den forward-Vektor des
        /// Orientierungsobjekts als Bewegungsrichtung.
        /// </remarks>
        protected virtual void Update()
        {
            UpdateDirection();
            UpdateSpeed();
            Trigger();
            if (!Moving) return;
            Move();
        }

        /// <summary>
        /// Bewegungsrichtung auf den forward-Vektor
        /// des Orientierungsobjekts setzen.
        /// </summary>
        protected override void InitializeDirection()
        {
            m_Direction = OrientationObject.transform.forward;
        }
        
        /// <summary>
        /// Die Bewegung durchführen.
        /// </summary>
        /// <remarks>
        /// Die Bewegung wird durchgeführt, wenn eine in dieser Klasse
        /// deklarierte logische Variable true ist.
        /// <remarks>
        protected override void Move()
        {
            transform.Translate(m_Speed * Time.deltaTime * m_Direction);
        }
        
        /// <summary>
        /// Berechnung der Geschwindigkeit der Fortbewegung
        /// </summary>
        /// <remarks>
        /// Wir rechnen die km/h aus dem Interface durch Division
        /// mit 3.6f in m/s um.
        /// </remarks>
        protected override void UpdateSpeed()
        {
            m_Speed = m_Velocity.Value/3.6f;
        }
        
        /// <summary>
        /// Geschwindigkeit initialiseren. Wir überschreiben diese
        /// Funktion in den abgeleiteten Klassen und rufen
        /// diese Funktion in Locomotion::Awake auf.
        /// </summary>
        protected override void InitializeSpeed()
        {
            m_Velocity = new LinearBlend(InitialSpeed, DeltaSpeed, 
                                                                      0.0f, MaximumSpeed);
            m_Speed = m_Velocity.Value/3.6f;
        }

        /// <summary>
        /// Die abgeleiteten Klassen entscheiden, wann die Locomotion
        /// getriggert werden. Da dies vom verwendeten SKD abhängt
        /// finden wir die Realisierung in den Controller-Klassen.
        /// </summary>
        protected virtual void Trigger() { }
}
