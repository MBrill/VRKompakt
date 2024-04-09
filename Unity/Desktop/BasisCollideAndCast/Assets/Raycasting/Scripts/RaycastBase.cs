//========= 2022- 2024 - Copyright Manfred Brill. All rights reserved. ===========
using System;
using UnityEngine;

/// <summary>
/// Basisklasse für die Implementierungen eines Raycasts auf der
/// Basis des Raycasters in Unity.
/// </summary>
public class RaycastBase : MonoBehaviour
{
    /// <summary>
    /// Welche Achse des lokalen Koordinatensystems  verwenden wir
    /// als Richtung des Raycasts
    /// </summary>
    [Tooltip("Richtungsvektor")]
    public Directions Dir = Directions.Forward;
    
    /// <summary>
    /// Maximale Länge des Strahls
    /// </summary>
    [Tooltip("Maximale Länge des Strahls")]
    [Range(1.0f, 10.0f)]
    public float MaxLength = 2.0f;
    
    /// <summary>
    /// Sollen Informationen über das Raycasting protokolliert werden?
    /// </summary>
    public bool RayLogs = false;
    
    /// <summary>
    /// Soll der Ray-Cast ausgeführt werden?
    /// </summary>
    protected bool m_cast = false;
    
    /// <summary>
    /// Feld mit den sechs lokalen Koordinatenachsenals Richtungen für den Cast
    /// </summary>
    /// <remarks>
    /// Wie man im Editor sieht sind die lokalen Achsten des Controller-prefabs
    /// verdreht. Deshalb müssen wir right/left und forware/back tauschen!
    protected readonly Vector3[] m_axis =
    {
        Vector3.left,
        Vector3.right,
        Vector3.up,
        Vector3.down,
        Vector3.back,
        Vector3.forward
    };
    
    
    /// <summary>
    /// Feld mit Ausgaben zu den Koordinatenachsesn
    /// </summary>
    protected readonly String[] m_Log =
    {
        "Es gibt ein Objekt rechts von mir!",
        "Es gibt ein Objekt links von mir!",
        "Es gibt ein Objekt oberhalb von mir!",
        "Es gibt ein Objekt unterhalb von mir!",
        "Es gibt ein Objekt vor mir!",
        "Es gibt ein Objekt hinter mir!",
    };
}
