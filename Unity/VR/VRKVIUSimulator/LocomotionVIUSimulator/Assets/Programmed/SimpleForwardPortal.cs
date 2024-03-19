//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

/// <summary>
///Ein einfaches Portal in der Szene Hallway.
/// <remarks>
/// Wird ein z-Wert, den wir einstellen können, überschritten
/// verwenden wir eine Instanz der komponente LineEaseInEaseOut
/// und bewegen uns eine einstellbare Distanz in positive z-Richtung.
/// </remarks>
[RequireComponent(typeof(LineEaseInEaseOut))]
public class SimpleForwardPortal : MonoBehaviour
{
    [Header("SimpleForwardPortal")]
    /// <summary>
    /// Anfangspunkt
    /// </summary>
    [Tooltip("z-Koordinaten des Portals")]
    [Range(1.0f, 27.0f)]
    public float PortalPosition = 6.0f;

    /// <summary>
    /// Länge der Linie bis zum Endpunkt der Pfadanimation
    /// </summary>
    [Tooltip("Endpunkt der Pfadanimation")]
    [Range(1.0f, 24.0f)]
    public float DestinationPosition = 6.0f;

    /// <summary>
    /// Prefab für die Visualisierung des Start- und Endpunkt des Portals
    /// </summary>
     [Tooltip("Prefab für die Visualisierung des Portals")]
    public GameObject PrefabForVisualization;

    /// <summary>
    /// y-Koordinate für die Sichthöhe.
    /// </summary>
    [Tooltip("Sichthöhe für die Platzierung des Prefabs")]
    [Range(1.0f, 2.0f)]
    public float PlayerHeight = 1.75f;
    
    /// <summary>
    /// Objekt, dessen Position die Transition auslöst.
    /// </summary>
    /// <remarks>
    /// Camera im Rig ist die Default-Wahl.
    /// </remarks>
    [Tooltip("GameObject wie Camera im Rig")]
    public GameObject Pivot;
    
    /// <summary>
    ///  Auslösen des Übergangs
    /// </summary>
    private void Trigger()
    {
        var pos = Pivot.transform.position;
        var deltaPos = new Vector3(0.0f, 0.0f, DestinationPosition);

        if (pos.z >= PortalPosition && !m_Moving)
        {
            m_Moving = true;
            m_Line.p1 = pos;
            m_Line.p2 = m_Line.p1 + deltaPos;
        }
    }

    /// <summary>
    /// Instanzieren des Prefabs, damit wir wissen wo das Portal liegt
    /// und wo der endpunkt ist.
    /// </summary>
    void Awake()
    {
        var angles = new Vector3(90.0f, 0.0f, 0.0f);
        var orientation = new Quaternion();
        
        orientation.eulerAngles = angles;
        Instantiate(PrefabForVisualization,
            new Vector3(0.0f, PlayerHeight, PortalPosition),
            orientation);
        Instantiate(PrefabForVisualization,
            new Vector3(0.0f, PlayerHeight, PortalPosition+DestinationPosition),
            orientation);
    }
    
    /// <summary>
    /// Vorbereiten des Portals
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        m_Line = gameObject.GetComponent<LineEaseInEaseOut>();
        m_Line.p1.z = PortalPosition;
        m_Line.p2.z = PortalPosition + DestinationPosition;
        m_Line.Periodic = false;
        m_Line.enabled = false;
    }

    /// <summary>
    /// Wir überprüfen in Trigger, ob die Bewegung ausgelöst werden  soll.
    /// falls ja machen wir die Komponente LineEaseInEaseOut aktiv.
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        Trigger();
        if (!m_Moving) return;
        m_Line.enabled = true;

    }

    private Vector3 m_Start = Vector3.zero;
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
