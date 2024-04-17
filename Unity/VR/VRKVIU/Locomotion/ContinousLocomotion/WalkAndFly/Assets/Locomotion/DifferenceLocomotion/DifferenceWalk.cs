//========= 2021 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Walk als Fortbewegung in einer VR-Anwendung,
/// mit zwei Objekten für
/// die Definition der Bewegungsrichtung.
/// </summary>
/// <remarks>
/// Walk bedeutet, dass wir die Bewegungsrichtung inx und z-
/// Koordinatenachsen verändern können.
///
/// Wir verwenden einen Trigger-Button. So lange dieser Button
/// gedrückt ist wird die Bewegung ausgeführt.
/// 
/// Als Bewegungsrichtung verwenden wir den Differenzvektor
/// zweier Objekte, typischer Weise die Controller. Möglich ist
/// auch den Kopf als einer der Objekte zu verwenden.
///
/// Die Geschwindigkeit wird mit Buttons auf einem Controller
/// verändert.
/// </remarks>
public class DifferenceWalk : DifferenceLocomotion
{
    /// <summary>
        /// Bewegungsrichtungaus dem Differenzvektor bilden.
        /// Wir ignorieren die y-Koordinate.
        /// </summary>
        protected override void UpdateDirection()
        {
            m_Direction = EndObject.transform.position - StartObject.transform.position;
            m_Direction.y = 0.0f;
            m_Direction.Normalize();
        }
}
