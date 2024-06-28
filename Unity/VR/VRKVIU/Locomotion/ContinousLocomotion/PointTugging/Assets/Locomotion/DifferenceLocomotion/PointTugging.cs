//========= 2023 - 2024 - Copyright Manfred Brill. All rights reserved. ===========

/// <summary>
/// PointTugging als Locomotion in einer VR-Anwendung, mit zwei Objekten für
/// die Definition der Bewegungsrichtung und den Trigger.
/// </summary>
/// <remarks>
/// Als Bewegungsrichtung verwenden wir den Differenzvektor
/// zweier Objekte, typischer Weise die Controller.
///
/// Die Bewegungsrichtung lesen wir aus dem Differenzvektoir
/// der beiden Controller ab. Dabei zeigt die Richtung
/// auf die Position des Controllers, an dem ein Button bewegt wird.
/// </remarks>
public class PointTugging : DifferenceLocomotion
{
    /// <summary>
    /// Bewegungsrichtung als Differenz der Positionen
    /// der beiden definierenden Objekte setzen.
    /// </summary>
    protected override void UpdateDirection()
    {
        m_Direction = EndObject.transform.position - StartObject.transform.position;
    }
    
    /// <summary>
    /// Die Bewegung durchführen. Wir verwenden keine Werte
    /// für die Geschwindigkeit, sondern direkt den Differenzvektor.
    /// </summary>
    protected override void Move()
    {
        transform.Translate(m_Direction);
    }
}
