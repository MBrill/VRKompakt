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
        public InputAction RunAction;

        /// <summary>
        /// Komponente WayPointManager abfragen und speichern.
        /// Wir fragen auch das erste Ziel ab.
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
            Run = !Run;
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
