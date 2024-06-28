//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========

/// <summary>
/// Fly als Locomotion in einer VR-Anwendung, mit zwei Objekten für
/// die Definition der Bewegungsrichtung und den Trigger.
/// </summary>
/// <remarks>
/// Als Bewegungsrichtung verwenden wir den Differenzvektor
/// zweier Objekte, typischer Weise die Controller. Möglich ist
/// auch den Kopf als einer der Objekte zu verwenden.
///
/// Die Bewegung wird gestartet wenn die beiden Objekte einen
/// Mindestabstand überschreiten, der im Inspektor auf der
/// Variable Threshold eingestellt werden kann.
/// 
/// Die Geschwindigkeit wird mit Buttons auf einem Controller
/// verändert.
/// </remarks>
public class DifferenceFly : DifferenceLocomotion
{
        /// <summary>
        /// Bewegungsrichtung als Differenz de rPositionen
        /// der beiden definierenden Objekte setzen.
        /// </summary>
        /// <remarks>
        /// Implementierung stimmt aktuell mit InitializeDirection
        /// in der Basisklasse überein.
        /// </remarks>
        protected override void UpdateDirection()
        {
            m_Direction = EndObject.transform.position - StartObject.transform.position;
            m_Direction.Normalize();
        }
}
