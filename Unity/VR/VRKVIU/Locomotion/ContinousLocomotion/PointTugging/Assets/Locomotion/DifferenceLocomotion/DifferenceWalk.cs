//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========

/// <summary>
/// Walk als Fortbewegung in einer VR-Anwendung,
/// mit zwei Objekten für
/// die Definition der Bewegungsrichtung  und den Trigger.
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
public class DifferenceWalk : DifferenceLocomotion
{
    /// <summary>
        /// Bewegungsrichtung aus dem Differenzvektor bilden.
        /// Wir ignorieren die y-Koordinate.
        /// </summary>
        protected override void UpdateDirection()
        {
            m_Direction = EndObject.transform.position - StartObject.transform.position;
            m_Direction.y = 0.0f;
            m_Direction.Normalize();
        }
}
