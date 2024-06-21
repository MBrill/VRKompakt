//========= 2021 - 2024 --  Copyright Manfred Brill. All rights reserved. ===========
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

/// <summary>
///Ein einfaches Portal in der Szene Hallway.
/// <remarks>
/// Wird ein z-Wert, den wir einstellen können, überschritten
/// verwenden wir eine Instanz der komponente LineEaseInEaseOut
/// und bewegen unszum Endpunkt des Portals, der ebenfalls durch
/// eine einstellbare z-Koordiknaten gegeben ist..
/// </remarks>
[RequireComponent(typeof(LineEaseInEaseOut))]
public class SimpleForwardPortal : MonoBehaviour
{
    [Header("Triggerobjekct")]
    /// <summary>
    /// Objekt, dessen Position die Transition auslöst.
    /// </summary>
    /// <remarks>
    /// Camera im Rig ist die Default-Wahl.
    /// </remarks>
    [Tooltip("GameObject, das dias Portal triggert")]
    public GameObject Pivot;
    
    [Header("Positionsangaben")]
    /// <summary>
    /// Anfangspunkt
    /// </summary>
    [Tooltip("z-Position des Anfangspunkts des Portals")]
    [Range(0.0f, 27.0f)]
    public float PortalPosition = -24.0f;

    /// <summary>
    /// Länge der Linie bis zum Endpunkt der Pfadanimation
    /// </summary>
    [Tooltip("z-Position des Zielpunkts des Portals")]
    [Range(0.0f, 27.0f)]
    public float DestinationPosition = 18.0f;

    [Header("Visualisierung des Portals")]
    /// <summary>
    /// Höhe für die Darstellung ders Prefabs des Portals
    /// </summary>
     [Tooltip(" Höhe für die Darstellung ders Prefabs des Portals")]
    public float PrefabHeight =  1.75f;
    
    /// <summary>
    /// Prefab für die Visualisierung des Start- und Endpunkt des Portals
    /// </summary>
    [Tooltip("Prefab für die Visualisierung des Portals")]
    public GameObject PrefabForVisualization;
    
    /// <summary>
    ///  Auslösen des Übergangs
    /// </summary>
    private void Trigger()
    {
        var pos = Pivot.transform.position;
        var targetPosition = new Vector3(0.0f, pos.y, DestinationPosition);

        if (pos.z >= DestinationPosition && !m_Moving)
        {
            m_Moving = true;
            m_Line.p1 = pos;
            m_Line.p2 = targetPosition;
            m_Line.enabled = true;
        }
    }
    
    /// <summary>
    /// Vorbereiten des Portals
    /// </summary>
    /// <remarks>
    ///  Instanzieren des Prefabs, damit wir wissen wo das Portal liegt
    /// und wo der Endpunkt ist.
    /// 
    /// Linie für die Animation zum Endpunkt vorbereiten und
    /// die Höhe des Nutzers abfragen, um die Prefabs für die
    /// Visualisierung des Portals in Sichthöre anzuzeigen.
    ///
    ///
    /// Für die Sichthöre setzen wir duie y-Koordinate des Pivots
    /// ein, der im Allgemeinen mit der Kamera übereinstimmt.
    /// </remarks>
    private void Start()
    {
        m_Line = gameObject.GetComponent<LineEaseInEaseOut>();
        m_Line.p1 = new Vector3(0.0f, Pivot.transform.position.y, PortalPosition);
        m_Line.p2 = new Vector3(0.0f, Pivot.transform.position.y, DestinationPosition);
        m_Line.Periodic = false;
        m_Line.enabled = false;
        ;
        var angles = new Vector3(90.0f, 0.0f, 0.0f);
        var orientation = new Quaternion
        {
            eulerAngles = angles
        };

        var startPoint = 
            Instantiate(PrefabForVisualization,
            new Vector3(0.0f, 
                PrefabHeight, 
                PortalPosition),
            orientation);
        
        var endPoint = Instantiate(PrefabForVisualization,
            new Vector3(0.0f, 
                PrefabHeight, 
                DestinationPosition),
            orientation);
    }

    /// <summary>
    /// Wir überprüfen in Trigger, ob die Bewegung ausgelöst werden  soll.
    /// falls ja machen wir die Komponente LineEaseInEaseOut aktiv.
    /// </summary>
    // Update is called once per frame
    private void FixedUpdate()
    {
        Trigger();
        if (!m_Moving) return;

    }

    /// <summary>
    /// Variable für den Anfangspunkt der Linie, auf der wir uns bewegen
    /// </summary>
    private Vector3 m_Start = Vector3.zero;
    /// <summary>
    /// Variable für den Endpunkt der Linie, auf der wir uns bewegen
    /// </summary>
    private Vector3 m_End = Vector3.zero;
    
    /// <summary>
    /// Logische Variable für den Zusdtand der Fortbewegung.
    /// </summary>
    /// <remarks>
    /// Wir in Trigger verändert.
    /// </remarks>
    private bool m_Moving = false;
    
    /// <summary>
    /// Instanz der Komponente LineEaseInEaseOut.
    /// </summary>
    /// <remarks>
    /// Dass es diese Komponenten gibt wird mit
    /// RequireComponent sicher gestellt.
    /// </remarks>
    private LineEaseInEaseOut m_Line;
}
