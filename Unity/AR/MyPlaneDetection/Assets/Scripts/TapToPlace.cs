using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



/// <summary>
/// Platzieren eines Prefabs auf einer erzeugten Ebene mit Hilfe
/// des Raycastings in AR Foundation.
/// </summary>
/// <remarks>
/// Wir setzen voraus, dass es eine Komponente ARRycastmanager gibt.
/// Dies stellen wir mit Hilfe von RequireComponent sicher.
///
/// Wir verwenden das neue Input System!
/// </remarks>
[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
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
    private GameObject m_SpawnedObjet;

    /// <summary>
    ///  Position des Touch-Events in Bildschirmkoordinaten
    /// </summary>
    private Vector2 m_TouchPosition;

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
        EnhancedTouchSupport.Enable();
    }

    /// <summary>
    /// Auf Touch-Event prüfen und reagieren.
    /// </summary>
    private void Update()
    {
        // Touch Events durchgehen, den ersten mit Status
        // began verwenden.
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.isInProgress)
            {
                m_TouchPosition = touch.screenPosition;
                // Raycast ausführen
                if (m_CastManager.Raycast(m_TouchPosition,
                    m_Hits,
                    TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = m_Hits[0].pose;
                    // Beim ersten Touch-Event das Prefab
                    // instantiieren. Anschließend wird das Objekt
                    // an die neue Hit-Position verschoben.
                    if (m_SpawnedObjet == null)
                        m_SpawnedObjet = Instantiate(PrefabObject,
                            hitPose.position,
                            hitPose.rotation);
                    else
                        m_SpawnedObjet.transform.position = hitPose.position;
                }
            }
        }
    }
}
