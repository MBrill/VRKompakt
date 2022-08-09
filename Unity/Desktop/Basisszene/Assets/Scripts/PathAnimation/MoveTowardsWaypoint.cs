//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

// Namespace
namespace VRKL.MBU
{
    /// <summary>
    /// Bewegung eines Objekts mit Hilfe von Zielpunkten.
    /// 
    /// Diese Klasse setzt voraus, dass es eine Komponente
    /// wie InteractiveWaypoints gibt, die wir für die 
    /// Definition der Wegpunkte verwenden. 
    /// 
    /// Dies wird mit Hilfe von RequireComponent sicher gestellt.
    /// </summary>
    [RequireComponent(typeof(InteractiveWaypoints))]
    public class MoveTowardsWaypoint : MonoBehaviour
    {
        /// <summary>
        /// Ist das Objekt näher beim Ziel als distance,
        /// wird das nächste Ziel verwendet.
        /// </summary>
        [Range(1.0f, 10.0f)]
        [Tooltip("Bei welchem Abstand gilt das Ziel als erreicht?")]
        public float distance = 1.0f;
        /// <summary>
        /// Geschwindigkeit der Bewegung
        /// </summary>
        [Range(0.1f, 10.0f)]
        [Tooltip("Geschwindigkeit der Bewegung")]
        public float speed = 5.0f;

        /// <summary>
        /// Instanz der Klasse, die die Weg-Punkte enthält
        /// </summary>
        private InteractiveWaypoints waypoints;
        /// <summary>
        /// Instanz der Klasse WaypointManager.
        /// 
        /// Die Berechnung von Positionen und die Verwaltung
        /// der Zielpunkte erfolgt in dieser C#-Klasse.
        /// Sie ist *nicht* von MonoBehaviour abgeleitet!
        /// </summary>
        private WaypointManager manager = null;

        /// <summary>
        /// Komponente WayPointManager abfragen und speichern.
        /// Wir fragen auch das erste Ziel ab.
        /// </summary>
        private void Awake()
        {
            this.waypoints = GetComponent<InteractiveWaypoints>();
            this.manager = new WaypointManager(this.waypoints.waypoints, distance);
        }

        /// <summary>
        /// Wir verwenden FixedUpdate, da wir mit Time.deltaTime arbeiten.
        /// </summary>
        private void FixedUpdate()
        {
            // Objekt mit Hilfe von FollowerWithLogs bewegen
            transform.position = this.manager.Move(
                transform.position,
                speed * Time.fixedDeltaTime);
        }
    }
}