//========= 2020 - 2023 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

    /// <summary>
    /// Abstrakte Basisklasse f�r die Fortbewegung  in VR.
    /// </summary>
    public abstract class Locomotion : MonoBehaviour
    {
        /// <summary>
        /// Festlegen der Bewegungsrichtung.
        /// </summary>
        /// <remarks>
        /// Bewegungsrichtung als normierte Vector3-Instanz.
        /// Wenn diese Funktion nicht �berschrieben wird verwenden
        /// wir forward des GameObjects, an dem die Komponente
        /// h�ngt.
        /// </remarks>
        protected virtual void InitializeDirection()
        {
            m_Direction = transform.forward;
        }

        /// <summary>
        /// Initialisierung der Orientierung,. Wird in Awake aufgerufen.
        /// </summary>
        /// <remarks>
        ///  Wir �berschreiben diese
        /// Funktion in den abgeleiteten Klassen.
        /// </remarks>
        protected virtual void InitializeOrientation()
        {
            m_Orientation = new Vector3(0.0f, 0.0f, 0.0f);
        }
        
        /// <summary>
        /// Update  der Bewegungsrichtung.
        /// </summary>
        protected virtual void UpdateDirection()
        {
            m_Direction = transform.forward;
        }
        
        /// <summary>
        /// Berechnung der Geschwindigkeit der Fortbewegung
        /// </summary>
        protected abstract void UpdateSpeed();

        /// <summary>
        /// Update der Orientierungl.
        /// </summary>
        protected abstract void UpdateOrientation();

        /// <summary>
        /// Geschwindigkeit initialiseren. Wird in AWake aufgerufen..
        /// </summary>
        /// <remarks>
        ///  Wir �berschreiben diese
        /// Funktion in den abgeleiteten Klassen.
        /// </remarks>
        protected abstract void InitializeSpeed();

        /// <summary>
        /// Initialisieren
        /// </summary>
        protected virtual void Awake()
        {
            // Bewegungsrichtung, Orientierung und Bahngeschwindigkeit initialisieren
            InitializeDirection();
            InitializeOrientation();
            InitializeSpeed();
        }

        /// <summary>
        /// Die Bewegung durchf�hren. Wir gehen davon aus,
        /// dass vorher in der Update-Funktion �berpr�ft wird,
        /// ob die logische Varialble m_moving true ist.
        /// </summary>
        /// <remarks>
        /// Die Bewegung wird durchgef�hrt, wenn eine in dieser Klasse
        /// deklarierte logische Variable true ist.
        /// 
        /// Wir bewegen uns in Richtung des Vektors m_Direction,
        /// er typischer Weise auf forward des GameObjects gesetzt wird.
        ///
        /// Wir orientieren das Objekt mit Hilfe der Eulerwinkel in m_Orientation
        /// und f�hren anschlie�end eine Translation in Richtung m_Direction durch.
        /// <remarks>
        protected virtual void Move()
        {
            transform.eulerAngles = m_Orientation;
                transform.Translate(m_Speed * Time.deltaTime * m_Direction);
        }

        /// <summary>
        /// Bewegung kann durch einen Trigger ausgel�st worden.
        /// <remarks>
        /// Ob die Bewegung mit Hilfe eines gedr�ckten Buttons erfolgt
        /// oder durch zwei Button-Clicks ausgel�st und beendet
        /// wird m�ssen die  davon abgeleiteten Klassen entscheiden!
        /// </remarks>
        /// </summary>
        private bool m_moving;
        protected bool Moving
        {
            get => m_moving;
            set => m_moving = value;
        }
        
        /// <summary>
        /// Normierter Richtungsvektor f�r die Fortbewegung.
        /// </summary>
        /// <remarks>
        /// In den VR-Varianten wird die Richtung direkt
        /// aus dem forward-Vektor des Orientierungsobjekts
        /// gesetzt.
        /// </remarks>
        protected Vector3 m_Direction;

        /// <summary>
        /// Vektor mit den Eulerwinkeln f�r die Kamera
        /// </summary>
        protected Vector3 m_Orientation;

        /// <summary>
        /// Betrag der Geschwindigkeit f�r die Bewegung
        /// <remarks>
        /// Einheit dieser Variable ist m/s.
        /// </remarks>
        /// </summary>
        protected float m_Speed;
        
        /// <summary>
        /// Klasse f�r die Verwaltung der Bahngeschwindigkeit.
        /// </summary>
        protected ScalarBlend m_Velocity;
    }

