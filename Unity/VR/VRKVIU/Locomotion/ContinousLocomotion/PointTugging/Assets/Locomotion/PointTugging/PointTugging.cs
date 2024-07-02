//========= 2023 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// PointTugging als Locomotion in einer VR-Anwendung, mit zwei Objekten für
/// die Definition der Bewegungsrichtung und den Trigger.
/// </summary>
public class PointTugging : Locomotion
{
    /// <summary>
    /// Wie im Originalpaper definineren wir die Veränderung der
    /// Position als Differenz zwischen der Position des Punkts,
    /// den wir "gegrabbed" haben und der aktuellen Controller-Position.
    /// Dabei verändern wir, dem Paper folgend, nur die
    /// x- und z-Koordinaten.
    /// </summary>
    protected override void UpdateDirection()
    {
        m_Direction = m_GrabbedPosition - m_Controller.transform.position;
        m_Direction.y = 0.0f;
    }

    /// <summary>
    /// Die Bewegung durchführen. Wir verwenden keine Werte
    /// für die Geschwindigkeit, sondern direkt den Differenzvektor.
    /// </summary>
    protected override void Move()
    {
        transform.Translate(m_Direction);
    }
            
    /// <summary>
    /// Update aufrufen und die Bewegung ausführen.
    /// </summary>
    protected virtual void Update()
    {
        UpdateDirection();
        if (!Moving) return;
        Move();
    }
    
    /// <summary>
    /// GameObject im Rig tür den verwendeten Controller
    /// für das Ablesen der Positionsdaten
    /// </summary>
    protected GameObject m_Controller;

    /// <summary>
    /// Position des Punkts im Raum, den wir "gegrabbed" haben.
    /// </summary>
    protected Vector3 m_GrabbedPosition;
    
    /// <summary>
    /// Nicht benötigte abstrakte Funktion
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected override void UpdateSpeed()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Nicht benötigte abstrakte Funktion
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected override void UpdateOrientation()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Nicht benötigte abstrakte Funktion
    /// </summary>
    protected override void InitializeSpeed()
    {
        m_Speed = 1.0f;
    }
}
