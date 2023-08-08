using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Auswählen einer gefundenen Ebene mit Hilfe eines
/// Toluch-Events auf dem Bildschirm.
/// </summary>
/// <remarks>
/// Wir setzen voraus, dass es eine Komponente ARRycastmanager gibt.
/// Dies stellen wir mit Hilfe von RequireComponent sicher.
/// Das gilt auch für die Komponente ARPlaneManager.
///
/// Wir verwenden das neue Input System!
/// </remarks>
[RequireComponent(typeof(ARPlaneManager))]
[RequireComponent(typeof(ARRaycastManager))]
public class TapToSelect : MonoBehaviour
{
    /// <summary>
    ///  Das Prefab, das bei einem Schnitt auf der Ebene
    /// visualisiert wird.
    /// </summary>
    [Tooltip("Prefab für die Visualisierung")]
    public GameObject PrefabObject;
    /// <summary>
    /// Instanz der Ebene, die wir ausgewählt haben.
    /// </summary>
    public ARPlane ThePlane
    {
        get { return m_ThePlane; }
        set { m_ThePlane = value; }
    }
    
    /// <summary>
    /// Instanz der Ebene, die wir ausgewählt haben.
    /// </summary>
    private ARPlane m_ThePlane;

    /// <summary>
    /// Haben wir eine Ebene ausgewählt?
    /// </summary>
    private bool m_PlaneSelected = false;
    
    /// <summary>
    /// Instanz des Prefabs, das wir darstellen
    /// </summary>
    private GameObject m_SpawnedObject;
    
    /// <summary>
    ///  Position des Touch-Events in Bildschirmkoordinaten
    /// </summary>
    private Vector2 m_TouchPosition;

    /// <summary>
    /// Instanz des Klasse ARRayCastManager aus AR Foundation
    /// </summary>
    private ARPlaneManager m_PlaneManager;
    
    /// <summary>
    /// Instanz des Klasse ARRayCastManager aus AR Foundation
    /// </summary>
    private ARRaycastManager m_CastManager;

    /// <summary>
    ///  Liste mit Schnittpunkten
    /// </summary>
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    /// <summary>
    ///  Verbindung zur Komponente ARRaycastManager herstellen.
    /// </summary>
    private void Awake()
    {
        m_CastManager = GetComponent<ARRaycastManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        EnhancedTouchSupport.Enable();
    }

    /// <summary>
    /// Auf Touch-Event prüfen und reagieren.
    /// </summary>
    private void Update()
    {
        // Touch Events durchgehen, den ersten mit Status
        // isInProgress  verwenden, falls wir noch keine Ebene
        // ausgewählt haben.
        if (!m_PlaneSelected)
        {
            for (var index = 0; index < Touch.activeTouches.Count; index++)
            {
                var touch = Touch.activeTouches[index];
                if (!touch.isInProgress) continue;
                m_TouchPosition = touch.screenPosition;
                // Raycast ausführen
                if (!m_CastManager.Raycast(m_TouchPosition,
                    m_Hits,
                    TrackableType.PlaneWithinPolygon)) continue;
                // Sicher stellen, dass wir eine Ebene getroffen haben
                if ((m_Hits[0].hitType & TrackableType.Planes) == 0) continue;
                var hitPose = m_Hits[0].pose;
                // Beim ersten Touch-Event das Prefab
                // instantiieren. Anschließend wird das Objekt
                // an die neue Hit-Position verschoben.
                if (m_SpawnedObject == null)
                    m_SpawnedObject = Instantiate(PrefabObject,
                        hitPose.position,
                        hitPose.rotation);
                m_SelectPlane(m_PlaneManager.GetPlane(m_Hits[0].trackableId));
            }
        }
    }
    
    private void m_SelectPlane(ARPlane plane)
    {
        // Die Ebene abfragen
        var arPlane = plane.GetComponent<ARPlane>();
        if (arPlane != null)
            m_PlaneSelected = true;
        
        // Alle getrackten Ebenen durchgehen und alle bis
        // auf die ausgewählte de-aktivieren.
        foreach (var p in m_PlaneManager.trackables)
        {
            if (p != arPlane)
                p.gameObject.SetActive(false);
        }

        ThePlane = arPlane;
        m_PlaneManager.enabled = false;
    }
    

}
