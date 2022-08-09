//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

// Namespace festlegen
namespace VRKL.MBU
{
    /// <summary>
    /// Abstrakte Basisklasse f�r die Bewegung eines GameObjects entlang einer Kurve
    /// </summary>
    public abstract class PathAnimation : MonoBehaviour
    {
        /// <summary>
		///Wir n�hern die Kurve mit Hilfe von Waypoints an.
		/// </summary)
		[Range(4, 1024)]
        [Tooltip("Anzahl der Waypoints")]
        public int NumberOfPoints = 64;
        [Tooltip("Periodischer Verlauf")]
        public bool Periodic = true;

        /// <summary>
        /// Die Zielpunkte berechnen und damit eine neue Instanz von WaypointManager erzeugen.
        /// Es wird direkt das erste Ziel abgefragt, da wir diese Variable
        /// in FixedUpdate an die Instanz des WaypointManagers �bergeben, um die
        /// Position zu ver�ndern.
        /// </summary>
        protected virtual void Awake()
        {
            ComputePath();
            var dist = ComputeDistance();

            this.manager = new WaypointManager(waypoints, dist, Periodic);
            // Den ersten Zielpunkt setzen
            transform.position = manager.GetWaypoint();
            // Orientierung setzen
            // Wichtig: wir k�nnten hier auch einen up-Vektor �bergeben.
            // Der Defaultwert daf�r ist der up-Vektor des WKS, also die y-Achse.
            transform.LookAt(ComputeFirstLookAt());
        }


        /// <summary>
        /// Wir verwenden FixedUpdate, da wir mit Time.fixedDeltaTime arbeiten.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            // Objekt mit Hilfe von FollowerWithLogs bewegen
            transform.position = this.manager.Move(
                                 transform.position,
                                 velocities[manager.Current] * Time.fixedDeltaTime
                );
            transform.LookAt(manager.GetFollowupWaypoint());
        }

        /// <summary>
        /// Abstrakte Funktion ComputePath. Muss von den abgeleiteten Klassen
        /// implementiert werden und enth�lt die Parameterdarstellung der
        /// Kurve.
        /// </summary>
        protected abstract void ComputePath();

        /// <summary>
        /// Berechnung der ersten Lookat-Punkts. 
        /// Damit k�nnen wir das gesteuerte Objekt ausrichten.
        /// 
        /// Als Default wird hier forward verwendet. Abgeleitete Klassen
        /// k�nnen f�r den ersten Punkt die Tangente berechnen und hier setzen.
        /// </summary>
        /// <returns>Punkt, der LookAt �bergeben werden kann</returns>
        protected virtual Vector3 ComputeFirstLookAt()
        {
            return Vector3.forward;
        }

        /// <summary>
        /// Berechne den minimalen Abstand f�r das Durchlaufen der Wegpunkte.
        ///
        /// Wir berechnen die Feinheit des Polygonzugs und verwenden 50% davon
        /// als minimalen Abstand im Waypoint-Manager.
        /// </summary>
        /// <returns></returns>
        protected float ComputeDistance()
        {
            var dist = 100.0f;
            float next;
            for (var i = 0; i < waypoints.Length - 1; i++)
            {
                next = Vector3.Distance(waypoints[i + 1],waypoints[i]);
                if (dist > next)
                    dist = next;
            }
            return dist/2.0f;
        }
        
        /// <summary>
        /// Instanz der Klasse WaypointManager
        /// 
        /// Die Berechnung von Positionen und die Verwaltung
        /// der Zielpunkte erfolgt in dieser C#-Klasse.
        /// Sie ist *nicht* von MonoBehaviour abgeleitet!
        /// </summary>
        private WaypointManager manager = null;
        /// <summary>
        /// Array mit Instanzen von Vector3 f�r die Waypoints
        /// </summary>
        protected Vector3[] waypoints;       
        /// <summary>
        /// Array mit Instanzen von Vector3 f�r die Waypoints
        /// </summary>
        protected float[] velocities;
    }
}
