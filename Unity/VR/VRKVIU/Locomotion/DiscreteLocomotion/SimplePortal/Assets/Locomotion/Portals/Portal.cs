//========= 2023 - 2024 --  Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
///Basisklasse f�r die Implementierung von Portalen.
/// </summary>
/// <remarks>
/// Diese Implementierung geht davon aus, dass die Zielposition
/// in der gleichen Szene wie die Position des Portals ist.
/// </remarks>
public abstract class Portal : MonoBehaviour
{
    [Header("Positionsangaben")]
    /// <summary>
    /// Anfangspunkt
    /// </summary>
    [Tooltip("Postion des Portals")]
    public Transform PortalPosition ;

    /// <summary>
    /// L�nge der Linie bis zum Endpunkt der Pfadanimation
    /// </summary>
    [Tooltip("Zielposition des Portals")]
    public Transform TargetPosition;

    [Header("Triggerobjekt")]
    /// <summary>
    /// Objekt, dessen Position die Transition ausl�st.
    /// </summary>
    /// <remarks>
    /// Camera im Rig ist die Default-Wahl.
    /// </remarks>
    [Tooltip("GameObject, das dias Portal triggert")]
    public GameObject Pivot;
    
    [Header("Visualisierungen")]
    /// <summary>
    /// Prefab f�r die Visualisierung des Start- und Endpunkt des Portals
    /// </summary>
    [Tooltip("Prefab f�r die Visualisierung des Portals")]
    public GameObject PortalPrefab;
    
    /// <summary>
    /// Prefab f�r die Visualisierung des Zielpunktt des Portals
    /// </summary>
    [Tooltip("Prefab f�r die Visualisierung des Zielpunkts")]
    public GameObject TargetPrefab;

    /// <summary>
    /// Funktion, die den Abstand zum Portal berrechnet.
    /// </summary>
    /// <returns>Abstand zum Portal</returns>
    protected abstract float ComputeDistance();
    
    /// <summary>
    /// GameObject f�r die Visualisierung des Portals
    /// </summary>
    protected GameObject PortalVis;
    /// <summary>
    /// GameObject f�r die Visualisierung des Ziels
    /// </summary>
    protected GameObject TargetVis;
    
    /// <summary>
    ///Logische Variable f�r die Aktivierung des Portals
    /// </summary>
    /// <remarks>
    /// Ist diese Variable true, dann wird das Portal ausgel�st
    /// und das Objekt, das diese Komponente besitzt
    /// wird an den Zielpunkt des Portals transportiert.
    /// </remarks>
    private bool m_Active;
    protected bool Active
    {
        get => m_Active;
        set => m_Active = value;
    }

    /// <summary>
    /// Prefabs f�r die Visualsieirung des Portals und des Ziels
    /// instantiieren.
    /// </summary>
    private void Awake()
    {
        PortalVis = Instantiate(PortalPrefab, PortalPosition);
        TargetVis = Instantiate(PortalPrefab, TargetPosition);

        Active = false;
    }
}
