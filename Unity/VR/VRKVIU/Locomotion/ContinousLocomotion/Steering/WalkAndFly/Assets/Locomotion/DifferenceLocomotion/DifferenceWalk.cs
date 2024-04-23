//========= 2021 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Walk als Fortbewegung in einer VR-Anwendung,
/// mit zwei Objekten f�r
/// die Definition der Bewegungsrichtung.
/// </summary>
/// <remarks>
/// Walk bedeutet, dass wir die Bewegungsrichtung inx und z-
/// Koordinatenachsen ver�ndern k�nnen.
///
/// Wir verwenden einen Trigger-Button. So lange dieser Button
/// gedr�ckt ist wird die Bewegung ausgef�hrt.
/// 
/// Als Bewegungsrichtung verwenden wir den Differenzvektor
/// zweier Objekte, typischer Weise die Controller. M�glich ist
/// auch den Kopf als einer der Objekte zu verwenden.
///
/// Die Geschwindigkeit wird mit Buttons auf einem Controller
/// ver�ndert.
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
