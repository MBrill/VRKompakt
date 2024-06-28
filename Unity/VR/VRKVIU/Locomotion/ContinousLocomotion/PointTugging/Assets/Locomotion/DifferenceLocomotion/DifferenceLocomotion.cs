//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für die Realisierung von Locomotion-Verfahren,
/// die die Differenz zweier Objekte
/// für die Definition der Bewegungsrichtung verwenden.
/// </summary>
/// <remarks>
/// Der Differenzvektor wird normiert!
/// </remarks>
public abstract class DifferenceLocomotion : Locomotion
{
         [Header("Difference Locomotion")]  
        /// <summary>
        /// GameObject, das den Startpunkt der Bewegungsrichtung definiert
        /// </summary>
        [Tooltip("Startpunkt der Bewegungsrichtung")]
        public GameObject StartObject;

        /// <summary>
        /// GameObject, das den Endpunkt der Bewegungsrichtung definiert
        /// </summary>
        [Tooltip(" Endpunkt der Bewegungsrichtung")]
        public GameObject EndObject;
        
        [Tooltip("Schwellwert für das Auslösen der Fortbewegung")]
        [Range(0.1f, 1.5f)]
        public float Threshold = 1.0f;
        
        /// <summary>
        /// Geschwindigkeit für die Bewegung der Kamera in km/h
        /// </summary>
        [Tooltip("Geschwindigkeit in km/h")]
        [Range(0.1f, 20.0f)]
        public float InitialSpeed = 5.0f; 
        
        /// <summary>
        /// Bewegungsrichtung als Differenz der forward-Vektoren
        /// der beiden definierenden Objekte setzen.
        /// </summary>
        protected override void InitializeDirection()
        {
            m_Direction = EndObject.transform.position-StartObject.transform.position;
            m_Direction.Normalize();
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
        /// <remarks>
        /// In der Basis-Version wird die Geschwindigkeit nicht verändert!
        /// </remarks>
        protected override void InitializeSpeed()
        {
            // Wir verändern die Geschwindigkeit nicht ...
            m_Velocity = new LinearBlend(InitialSpeed, 0.001f,
                0.0f, 2.0f * InitialSpeed);
            m_Speed = m_Velocity.Value/3.6f;
        }
        /// <summary>
        /// Auslösen der Bewegung.
        /// </summary>
        /// <remarks>
        /// Die Fortbewegung wird ausgelöst, falls der Abstand zwischen
        /// den beiden Objekten, mit denen wir die Richtung der Fortbewegung
        /// steuern größer als ein Schwellwert ist.
        /// </remarks>
        protected virtual void Trigger()
        {
            var distance = Vector3.Magnitude(EndObject.transform.position - StartObject.transform.position);
            Moving = distance > Threshold;
        }

    
        /// <summary>
        /// Update aufrufen und die Bewegung ausführen.
        /// </summary>
        /// <remarks>
        ///Wir verwenden den Differenz-Vektor zwischen
        /// den beiden Objekten als Bewegungsrichtung.
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
        /// Wird aktuell nicht verwendet, da wir die Bewegungsrichtung
        ///aus dem Differenzvektor bestimmenn.
        /// </summary>
        protected override void UpdateOrientation()
        {
            throw new System.NotImplementedException();
        }
}

