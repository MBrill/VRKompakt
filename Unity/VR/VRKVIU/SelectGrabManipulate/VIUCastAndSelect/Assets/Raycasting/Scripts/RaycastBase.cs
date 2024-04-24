//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Basisklasse für Raycasting.
/// </summary>
/// <remarks>
/// Wir verwenden in dieser Klasse immer den Vektor
/// transform.forward als Richtung für den Raycast.
///
/// Das bedeutet, dass der Strahl in Richtung der z-Achse
/// des lokalen Koordinatensystems zeigt!
/// </remarks>
public class RaycastBase : MonoBehaviour
{
    /// <summary>
    /// Maximale Länge des Strahls
    /// </summary>
    /// <remarks>
    /// Für den einsatz in der Basiss-Szene STrahllänge auf 4.0 setzen.
    /// Damit ist gewährleistet, dass wir eigentlich i mmer etwas treffen.
    /// </remarks>
    [Tooltip("Maximale Länge des Strahls")]
    [Range(1.0f, 10.0f)]
    public float MaxLength = 4.0f;
    
    /// <summary>
    /// Sollen Informationen über das Raycasting protokolliert werden?
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
    /// Soll der Ray-Cast ausgeführt werden?
    /// </summary>
    protected bool m_cast = false;
}
