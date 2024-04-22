//========= 2021 2024 - Copyright Manfred Brill. All rights reserved. ===========

/// <summary>
 /// Fly  als Locomotion mit der steering metaphor.
 /// </summary>
 /// <remarks>
 /// Fly bedeutet, dass wir die Bewegungsrichtung in allen drei
 /// Koordinatenachsen verändern können.
 ///
 /// Wir verwenden einen Trigger-Button. So lange dieser Button
 /// gedrückt ist wird die Bewegung ausgeführt.
 /// 
 /// Als Bewegungsrichtung verwenden wir die Orientierung
 /// eines GameObjects, typischer Weise eines der Controllert.
 ///
 /// Die Bahneschwindigkeit wird mit Buttons auf einem Controller
 /// verändert.
 /// </remarks>
 public class Fly : JoystickLocomotion
 {
        /// <summary>
        /// Bewegungsrichtung auf den forward-Vektor
        /// des Orientierungsobjekts setzen.
        /// </summary>
        protected override void UpdateDirection()
        {
            m_Direction = OrientationObject.transform.forward;
        }
        
        /// <summary>
        /// Update der Orientierung des GameObjects,
        /// das die Bewegungsrichtung definiert..
        /// </summary>
        /// <remarks>
        /// Für die Verarbeitung der Orientierung verwenden wir
        /// die Eulerwinke der x- und y-Achse.
        ///
        /// Wird aktuell nicht verwendet, da wir die Bewegungsrichtung
        /// direkt aus dem forward-Vektor des Orientierungsobjekts
        /// ablesen.
        /// </remarks>
        protected override void UpdateOrientation()
        {
            m_Orientation.x = OrientationObject.transform.eulerAngles.x;
            m_Orientation.y = OrientationObject.transform.eulerAngles.y;
        }
 }
