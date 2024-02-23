//========= 2020 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

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
        /// Ist das Objekt näher beim Ziel als Distance,
        /// wird das nächste Ziel verwendet.
        /// </summary>
        [Range(0.1f, 10.0f)]
        [Tooltip("Bei welchem Abstand gilt das Ziel als erreicht?")]
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
        /// Eingbe Asset für die Steuerung der Variable Run
        /// </summary>
        public InputAction RunAction;
        
        /// <summary>
        /// Instanz der Klasse, die die Weg-Punkte enthält
        /// </summary>
        private Waypoints waypoints;
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
            this.waypoints = GetComponent<Waypoints>();
            this.manager = new WaypointManager(this.waypoints.waypoints, Distance);
            
            RunAction.performed += OnRun;
        }

        /// <summary>
        /// Wir verwenden FixedUpdate, da wir mit Time.deltaTime arbeiten.
        /// </summary>
        private void FixedUpdate()
        {
            if (Run)
            {
                transform.position = this.manager.Move(
                    transform.position,
                    Speed * Time.fixedDeltaTime);              
            }

        }
        
        /// <summary>
        ///Callback für das Schalten der Bewegung
        /// </summary>
        private void OnRun(InputAction.CallbackContext ctx)
        {
            Run = !Run;
        }
        
        /// <summary>
        /// In Enable für die Szene aktivieren wir auch unsere Action.
        /// </summary>
        private void OnEnable()
        {
            RunAction.Enable();
        }
        
        /// <summary>
        /// In Disable für die Szene de-aktivieren wir auch unsere Action.
        /// </summary>
        private void OnDisable()
        {
            RunAction.Disable();
        }
}