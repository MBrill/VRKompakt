//========= 2023 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Eingabe Controller mit dem Input System f�r die Klasse
/// MoveTowardsWaypoint.
/// </summary>
[RequireComponent(typeof(MoveTowardsWaypoint))]
public class MoveToWaypointController : MoveTowardsWaypoint
{       
        /// <summary>
        /// Input Asset f�r die Steuerung der Variable Run in der Basisklasse
        /// </summary>
        [Tooltip("Input Action f�r das Schalten der Bewegung")]
        public InputAction RunAction;

        /// <summary>
        /// Komponente WayPointManager abfragen und speichern.
        /// </summary>
        private void Awake()
        {         
            RunAction.performed += OnRun;
        }
        
        /// <summary>
        ///Callback f�r das Schalten der Bewegung
        /// </summary>
        private void OnRun(InputAction.CallbackContext ctx)
        {
            // Falls wir bei azyklischem Verhalten am Endpunkt angelangt sind,
            // schalten wir Run auf false.
            // Damit k�nnen wir dann wieder von vorne beginnen.
            Run = !Run;
           if (Cyclic) return;

           if (!manager.ReachedLastWayPoint) return;
           manager.ResetWaypoints();
           transform.position = manager.GetWaypoint();
           transform.LookAt(manager.GetFollowupWaypoint());
           manager.ReachedLastWayPoint = false;
        }
        
        /// <summary>
        /// In Enable f�r die Szene aktivieren wir auch unsere Action.
        /// </summary>
        private void OnEnable()
        {
            RunAction.Enable();
        }
        
        /// <summary>
        /// In Disable f�r die Szene de-aktivieren wir auch unsere Action.
        /// </summary>
        private void OnDisable()
        {
            RunAction.Disable();
        }
}
