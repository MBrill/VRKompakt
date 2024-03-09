//========= 2020 - 2023 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

    /// <summary>
    /// Abstrakte Basisklasse für die Fortbewegung  in VR.
    /// </summary>
    public abstract class Locomotion : MonoBehaviour
    {
        /// <summary>
        /// Festlegen der Bewegungsrichtung.
        /// </summary>
        /// <remarks>
        /// Bewegungsrichtung als normierte Vector3-Instanz.
        /// Wenn diese Funktion nicht überschrieben wird verwenden
        /// wir forward des GameObjects, an dem die Komponente
        /// hängt.
        /// </remarks>
        protected virtual void InitializeDirection()
        {
            m_Direction = transform.forward;
        }

        /// <summary>
        /// Initialisierung der Orientierung,. Wird in Awake aufgerufen.
        /// </summary>
        /// <remarks>
        ///  Wir überschreiben diese
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
        ///  Wir überschreiben diese
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
        /// Die Bewegung durchführen. Wir gehen davon aus,
        /// dass vorher in der Update-Funktion überprüft wird,
        /// ob die logische Varialble m_moving true ist.
        /// </summary>
        /// <remarks>
        /// Die Bewegung wird durchgeführt, wenn eine in dieser Klasse
        /// deklarierte logische Variable true ist.
        /// 
        /// Wir bewegen uns in Richtung des Vektors m_Direction,
        /// er typischer Weise auf forward des GameObjects gesetzt wird.
        ///
        /// Wir orientieren das Objekt mit Hilfe der Eulerwinkel in m_Orientation
        /// und führen anschließend eine Translation in Richtung m_Direction durch.
        /// <remarks>
        protected virtual void Move()
        {
            transform.eulerAngles = m_Orientation;
                transform.Translate(m_Speed * Time.deltaTime * m_Direction);
        }

        /// <summary>
        /// Bewegung kann durch einen Trigger ausgelöst worden.
        /// <remarks>
        /// Ob die Bewegung mit Hilfe eines gedrückten Buttons erfolgt
        /// oder durch zwei Button-Clicks ausgelöst und beendet
        /// wird müssen die  davon abgeleiteten Klassen entscheiden!
        /// </remarks>
        /// </summary>
        private bool m_moving;
        protected bool Moving
        {
            get => m_moving;
            set => m_moving = value;
        }
        
        /// <summary>
        /// Normierter Richtungsvektor für die Fortbewegung.
        /// </summary>
        /// <remarks>
        /// In den VR-Varianten wird die Richtung direkt
        /// aus dem forward-Vektor des Orientierungsobjekts
        /// gesetzt.
        /// </remarks>
        protected Vector3 m_Direction;

        /// <summary>
        /// Vektor mit den Eulerwinkeln für die Kamera
        /// </summary>
        protected Vector3 m_Orientation;

        /// <summary>
        /// Betrag der Geschwindigkeit für die Bewegung
        /// <remarks>
        /// Einheit dieser Variable ist m/s.
        /// </remarks>
        /// </summary>
        protected float m_Speed;
        
        /// <summary>
        /// Klasse für die Verwaltung der Bahngeschwindigkeit.
        /// </summary>
        protected ScalarBlend m_Velocity;
    }

