//========= 2021 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

/// <summary>
/// Fly als Locomotion in einer VR-Anwendung, mit zwei Objekten f�r
/// die Definition der Bewegungsrichtung.
/// </summary>
/// <remarks>
/// Fly bedeutet, dass wir die Bewegungsrichtung in allen drei
/// Koordinatenachsen ver�ndern k�nnen.
/// 
/// Als Bewegungsrichtung verwenden wir den Differenzvektor
/// zweier Objekte, typischer Weise die Controller. M�glich ist
/// auch den Kopf als einer der Objekte zu verwenden.
/// </remarks>
public class DifferenceFly : DifferenceLocomotion
{
        /// <summary>
        /// Bewegungsrichtung als Differenz der forward-Vektoren
        /// der beiden definierenden Objekte setzen.
        /// </summary>
        /// <remarks>
        /// Implementierung stimmt aktuell mit InitializeDirection
        /// in der Basisklasse �berein.
        /// </remarks>
        protected override void UpdateDirection()
        {
            m_Direction = EndObject.transform.position - StartObject.transform.position;
            m_Direction.Normalize();
        }
}
