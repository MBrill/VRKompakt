using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Nach dem Erkennen einer Ebene führen wir einen Raycast durch.
/// Dabei verwneden wir die Bildschirmmitte als Ausgangspunkt des
/// Strahls und visualisieren am Schnittpunkt ein Prefab.
/// </summary>
/// <remarks>
/// Wir setzen voraus, dass es eine Komponente ARRycastmanager gibt.
/// Dies stellen wir mit Hilfe von RequireComponent sicher.
/// </remarks>
[RequireComponent(typeof(ARRaycastManager))]
public class AimAndCast : MonoBehaviour
{
    /// <summary>
    ///  Das Prefab, das bei einem Schnitt auf der Ebene
    /// visualisiert wird.
    /// </summary>
    [Tooltip("Prefab für die Visualisierung")]
    public GameObject PrefabObject;

    /// <summary>
    /// Instanz des Prefabs, das wir darstellen
    /// </summary>
    private GameObject m_SpawnedObject;
    
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
    }

    /// <summary>
    /// Auf Touch-Event prüfen und reagieren.
    /// </summary>
    private void Update()
    {
        var position = new Vector2(0.5f*Screen.width, 
            0.5f*Screen.height);
        var trackableType = TrackableType.PlaneWithinPolygon;
        if (m_CastManager.Raycast(
            position,
                    m_Hits,
                   trackableType))
        {
                    var hitPose = m_Hits[0].pose;
                    // Zu Beginn das Prefab
                    // instantiieren. Anschließend wird das Objekt
                    // an die neue Hit-Position verschoben.
                    if (m_SpawnedObject == null)
                        m_SpawnedObject = Instantiate(PrefabObject,
                            hitPose.position,
                            hitPose.rotation);
                    else
                        m_SpawnedObject.transform.position = hitPose.position;
        }
    }
}
