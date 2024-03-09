//========= 2020 -  2023 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;


/// <summary>
/// Abstrakte Basisklasse für dien kontinuierliche Fortbewegung
/// in immersiven Anwendungen auf der Basis von VIU.
/// </summary>
/// <remarks>
/// Diese Klasse ist von VRKL.MBU.Locomotion abgeleitet.
/// Dort sind bereits abstrakte Funktionen für die Fortbewegung
/// vorgesehen, die wir in den abgeleiteten Klassen einsetzen.
/// In der Basisklasse ist eine Variable ReverseButton vorgesehen,
/// die aber in der VR-Version nicht verändert wird. Das kann man noch tun,
/// dann können wir einen Rückwärtsgang realisieren. Ob der wirklich
/// gebraucht wird sehen wir dann noch.
///
/// In dieser Klasse kommen Geräte und Einstellungen für den
/// Inspektor dazu.
///
/// Mit RequireComponent wird sicher gestellt, dass das GameObject, dem
/// wir diese Klasse hinzufügen einen CameraRig der Vive Input Utility
/// enthält.
/// </remarks>
public abstract class TouchpadVRocomotion : Locomotion
{
       [Header("Devices")]
        /// <summary>
        /// Welchen Controller verwenden wir für das Triggernund Steuern  der Fortbewegung?
        /// </summary>
        /// <remarks>
        /// Als Default verwenden wir den Controller in der rechten Hand,
        /// also "RightHand" im "ViveCameraRig".
        /// </remarks>
        [Tooltip("Rechter oder linker Controller für die Steuerung?")]
        public HandRole moveHand = HandRole.RightHand;

        /// <summary>
        /// Der verwendete Button, der die Bewegung auslöst, kann im Editor mit Hilfe
        /// eines Pull-Downs eingestellt werden.
        /// </summary>
        /// <remarks>
        /// Default ist "PadTouch" - wir reagieren darauf, dass das Touchpad berührt wird.
        /// </remarks>
        [Tooltip("Welchen Button verwenden wir als Trigger der Fortbewegung?")]
        public ControllerButton moveButton = ControllerButton.PadTouch;

        [Header("Anfangsgeschwindigkeit")]
        /// <summary>
        /// Geschwindigkeit für die Bewegung der Kamera in km/h
        /// </summary>
        [Tooltip("Geschwindigkeit")]
        [Range(0.1f, 20.0f)]
        public float initialSpeed = 5.0f; 
        
        /// <summary>
        /// Maximal mögliche Geschwindigkeit
        /// </summary>
        [Tooltip("Maximal mögliche Bahngeschwindigkeit")]
        [Range(0.001f, 20.0f)]
        public float vMax = 10.0f;

        /// <summary>
        /// Delta für das Verändern der Geschwindigkeit
        /// </summary>
        [Tooltip("Delta für die Veränderung der Bahngeschwindigkeit")]
        [Range(0.001f, 2.0f)]
        public float vDelta = 0.2f;

        ///<summary>
        /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
        /// Initialisierungen durchführen, die spezifisch für VR sind.
        /// </summary>
        /// <remarks>
        /// Die Callbacks für Beschleunigung und Abbremsen in der VIUregistrieren.
        /// </remarks>
        protected override void Awake()
        {
            base.Awake();
            
            ViveInput.AddListenerEx(moveHand, 
                                                 moveButton, 
                                                 ButtonEventType.Down,  
                                                 m_Velocity.Decrease);
            ViveInput.AddListenerEx(moveHand, moveButton, 
                                                 ButtonEventType.Down,
                                                 m_Velocity.Increase);
        }

        /// <summary>
        /// Die Callbacks in der VIU wieder abhängen.
        /// </summary>
        protected void OnDestroy()
        {
             ViveInput.RemoveListenerEx(moveHand, moveButton, 
                                                         ButtonEventType.Down,  
                                                         m_Velocity.Decrease);
            ViveInput.RemoveListenerEx(moveHand, moveButton, 
                                                        ButtonEventType.Down, 
                                                        m_Velocity.Increase);
        }
        
        /// <summary>
        /// Update aufrufen und die Bewegung ausführen.
        /// </summary>
        /// <remarks>
        ///Wir verwenden den forward-Vektor des
        /// Orientierungsobjekts als Bewegungsrichtung.
        ///
        /// Deshalb verwenden wir hier nicht die Funktion
        /// UpdateOrientation, sondern setzen die Bewegungsrichtung
        /// direkt.
        /// </remarks>
        protected virtual void Update()
        {
            UpdateDirection();
            UpdateSpeed();
            
            if (ViveInput.GetPress(moveHand, moveButton))
                Move();
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
            m_Velocity = new LinearBlend(initialSpeed, vDelta, 
                                                                      0.0f, vMax);
            m_Speed = m_Velocity.Value;
        }
}
