//========= 2020 -  2023 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;


/// <summary>
/// Abstrakte Basisklasse f�r dien kontinuierliche Fortbewegung
/// in immersiven Anwendungen auf der Basis von VIU.
/// </summary>
/// <remarks>
/// Diese Klasse ist von VRKL.MBU.Locomotion abgeleitet.
/// Dort sind bereits abstrakte Funktionen f�r die Fortbewegung
/// vorgesehen, die wir in den abgeleiteten Klassen einsetzen.
/// In der Basisklasse ist eine Variable ReverseButton vorgesehen,
/// die aber in der VR-Version nicht ver�ndert wird. Das kann man noch tun,
/// dann k�nnen wir einen R�ckw�rtsgang realisieren. Ob der wirklich
/// gebraucht wird sehen wir dann noch.
///
/// In dieser Klasse kommen Ger�te und Einstellungen f�r den
/// Inspektor dazu.
///
/// Mit RequireComponent wird sicher gestellt, dass das GameObject, dem
/// wir diese Klasse hinzuf�gen einen CameraRig der Vive Input Utility
/// enth�lt.
/// </remarks>
public abstract class TouchpadVRocomotion : Locomotion
{
       [Header("Devices")]
        /// <summary>
        /// Welchen Controller verwenden wir f�r das Triggernund Steuern  der Fortbewegung?
        /// </summary>
        /// <remarks>
        /// Als Default verwenden wir den Controller in der rechten Hand,
        /// also "RightHand" im "ViveCameraRig".
        /// </remarks>
        [Tooltip("Rechter oder linker Controller f�r die Steuerung?")]
        public HandRole moveHand = HandRole.RightHand;

        /// <summary>
        /// Der verwendete Button, der die Bewegung ausl�st, kann im Editor mit Hilfe
        /// eines Pull-Downs eingestellt werden.
        /// </summary>
        /// <remarks>
        /// Default ist "PadTouch" - wir reagieren darauf, dass das Touchpad ber�hrt wird.
        /// </remarks>
        [Tooltip("Welchen Button verwenden wir als Trigger der Fortbewegung?")]
        public ControllerButton moveButton = ControllerButton.PadTouch;

        [Header("Anfangsgeschwindigkeit")]
        /// <summary>
        /// Geschwindigkeit f�r die Bewegung der Kamera in km/h
        /// </summary>
        [Tooltip("Geschwindigkeit")]
        [Range(0.1f, 20.0f)]
        public float initialSpeed = 5.0f; 
        
        /// <summary>
        /// Maximal m�gliche Geschwindigkeit
        /// </summary>
        [Tooltip("Maximal m�gliche Bahngeschwindigkeit")]
        [Range(0.001f, 20.0f)]
        public float vMax = 10.0f;

        /// <summary>
        /// Delta f�r das Ver�ndern der Geschwindigkeit
        /// </summary>
        [Tooltip("Delta f�r die Ver�nderung der Bahngeschwindigkeit")]
        [Range(0.001f, 2.0f)]
        public float vDelta = 0.2f;

        ///<summary>
        /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
        /// Initialisierungen durchf�hren, die spezifisch f�r VR sind.
        /// </summary>
        /// <remarks>
        /// Die Callbacks f�r Beschleunigung und Abbremsen in der VIUregistrieren.
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
        /// Die Callbacks in der VIU wieder abh�ngen.
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
        /// Update aufrufen und die Bewegung ausf�hren.
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
        /// Geschwindigkeit initialiseren. Wir �berschreiben diese
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
