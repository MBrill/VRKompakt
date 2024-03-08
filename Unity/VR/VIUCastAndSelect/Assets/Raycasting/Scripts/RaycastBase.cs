using System;
using UnityEngine;

/// <summary>
/// Basisklasse f�r Raycasting.
/// </summary>
/// <remarks>
/// Wir verwenden in dieser Klasse immer den Vektor
/// transform.forward als Richtung f�r den Raycast.
///
/// Das bedeutet, dass der Strahl in Richtung der z-Achse
/// des lokalen Koordinatensystems zeigt!
/// </remarks>
public class RaycastBase : MonoBehaviour
{
    /// <summary>
    /// Maximale L�nge des Strahls
    /// </summary>
    /// <remarks>
    /// F�r den einsatz in der Basiss-Szene STrahll�nge auf 4.0 setzen.
    /// Damit ist gew�hrleistet, dass wir eigentlich i mmer etwas treffen.
    /// </remarks>
    [Tooltip("Maximale L�nge des Strahls")]
    [Range(1.0f, 10.0f)]
    public float MaxLength = 4.0f;
    
    /// <summary>
    /// Sollen Informationen �ber das Raycasting protokolliert werden?
    /// </summary>
    public bool RayLogs = false;
    
    
    /// <summary>
    /// Raycast toggeln
    /// </summary>
    protected void m_castTheRay()
    {
        m_cast = !m_cast;
    }
    
    /// <summary>
    /// Soll der Ray-Cast ausgef�hrt werden?
    /// </summary>
    protected bool m_cast = false;
}
