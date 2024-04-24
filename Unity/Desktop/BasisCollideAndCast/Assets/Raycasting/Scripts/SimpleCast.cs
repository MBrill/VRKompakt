//========= 2022- 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;


/// <summary>
///  Einfaches Beispiel eines Raycast in Richtung einer der Koordinatenachsen.
/// </summary>
/// <remarks>
/// In dieser Klasse gibt es keine Interaktionen!
/// Für Anwendung auf dem Desktop verwenden wir die von dieser Klasse
/// abgeleitete Klasse SimpleCastController, die das Input System
/// einsetzt!
/// </remarks>
public class SimpleCast : RaycastBase
{
    /// <summary>
    ///  Raycasting wird in FixedUpdate ausgeführt!
    /// </summary>
    /// <remarks>
    /// Wir führen den Raycast auf Tastendruck aus, sonst wird
    /// die Konsole mit den immer gleichen Meldungen überschwemmt.
    /// </remarks>
    void FixedUpdate()
    {
        var ax = transform.TransformDirection(m_axis[(int) Dir]);
        if (m_cast && Physics.Raycast(transform.position, 
            ax, 
            MaxLength))
                Debug.Log(m_Log[(int) Dir]);
    }
}
