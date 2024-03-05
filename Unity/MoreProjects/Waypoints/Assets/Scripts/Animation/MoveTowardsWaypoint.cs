//========= 2020 - 2024 - Copyright Manfred Brill. All rights reserved. ===========

using System.ComponentModel.Design.Serialization;
using UnityEngine;

/// <summary>
/// Bewegung eines Objekts mit Hilfe von Zielpunkten.
/// 
/// Diese Klasse setzt voraus, dass es eine Komponente
/// wie Waypoints gibt, die wir für die 
/// Definition der Wegpunkte verwenden. 
/// 
/// Dies wird mit Hilfe von RequireComponent sicher gestellt.
/// </summary>
[RequireComponent(typeof(Waypoints))]
public class MoveTowardsWaypoint : MonoBehaviour
{
             /// <summary>
            /// Sollen die Wegpunkte zyklisch oder nur einmal durchlaufen werden?
          /// </summary>
             [Tooltip("Zyklisches Durchlaufen der Wegpunkte oder einmaliges Durchlaufen?")]
          public bool Cyclic = true;
          
        /// <summary>
        /// Ist das Objekt näher beim Ziel als Distance,
        /// wird das nächste Ziel verwendet.
        /// </summary>
        [Range(0.1f, 10.0f)]
        [Tooltip("Bei welchem Abstand gilt ein Wegpunkt als erreicht?")]
        public float Distance = 0.5f;
        
        /// <summary>
        /// Geschwindigkeit der Bewegung
        /// </summary>
        [Range(0.1f, 10.0f)]
        [Tooltip("Geschwindigkeit der Bewegung")]
        public float Speed = 0.5f;

        /// <summary>
        ///  Sollen die Wegpunkte abgefahren werden?
        /// </summary>
        public bool Run = false;

        /// <summary>
        /// Instanz der Klasse, die die Weg-Punkte enthält
        /// </summary>
        protected Waypoints waypoints;
        
        /// <summary>
        /// Instanz der Klasse WaypointManager.
        /// 
        /// Die Berechnung von Positionen und die Verwaltung
        /// der Zielpunkte erfolgt in dieser C#-Klasse.
        /// Sie ist *nicht* von MonoBehaviour abgeleitet!
        /// </summary>
        protected WaypointManager manager = null;

        /// <summary>
        /// Komponente WayPointManager abfragen und speichern.
        /// Wir fragen das erste Ziel ab und orientieren das Objekt.
        /// </summary>
        private void Start()
        {
            this.waypoints = GetComponent<Waypoints>();
            this.manager = new WaypointManager(this.waypoints.waypoints, 
                                                                     Distance,
                                                                     Cyclic);

            transform.LookAt(manager.GetWaypoint());
        }
        
        /// <summary>
        /// Wir verwenden FixedUpdate, da wir mit Time.deltaTime arbeiten.
        /// </summary>
        private void FixedUpdate()
        {
            if (!Run) return;
            transform.position = this.manager.Move(
                    transform.position,
                    Speed * Time.fixedDeltaTime);           
            transform.LookAt(manager.GetWaypoint());
        }
}