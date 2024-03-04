//========= 2020 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
 /// Abstrakte Basisklasse für die Bewegung eines Objekts  entlang
 /// einer Parameterkurve
 /// </summary>
public abstract class PathAnimation : MonoBehaviour
{
        /// <summary>
		///Wir nähern die Kurve mit Hilfe eines Polygonzugs  an.
		/// Mit diesem Polygonzug erzeugen wir dann Wegpunkte und
		/// verenden die Klasse WaypointManager für die Bewegung.
		/// </summary)
		[Range(4, 1024)]
        [Tooltip("Anzahl der Waypoints")]
        public int NumberOfPoints = 64;
       
        /// <summary>
        /// Soll die Kurve periodisch durchlaufen werden oder nur einmal?
        /// </summary>
        [Tooltip("Periodischer Verlauf oder einmaliges Durchlaufen")]
        public bool Periodic = true;

        /// <summary>
        /// Sollen die Parameterkurve  abgefahren werden?
        /// </summary>
        [Tooltip("Durchlaufen der Kurve")] 
        public bool Run = false;
       
        /// <summary>
        /// Soll die Parameterkurve durch einen Polygonzug dargestellt werden?
        /// </summary>
        [Tooltip("Visualisierung der Kurve")] 
        public bool ShowTheCurve = false;
       
        /// <summary>
        /// Die Zielpunkte berechnen und damit eine neue Instanz von WaypointManager erzeugen.
        /// Es wird direkt das erste Ziel abgefragt, da wir diese Variable
        /// in FixedUpdate an die Instanz des WaypointManagers übergeben, um die
        /// Position zu verändern.
        /// </summary>
        protected virtual void Awake()
        {
            ComputePath();
            var dist = ComputeDistance();
            
            this.manager = new WaypointManager(waypoints, dist, Periodic);
            // Den ersten Zielpunkt setzen
            transform.position = manager.GetWaypoint();
            // Orientierung setzen
            // Wichtig: wir könnten hier auch einen up-Vektor übergeben.
            // Der Defaultwert dafür ist der up-Vektor des WKS, also die y-Achse.
            transform.LookAt(ComputeFirstLookAt());
            
            // LineRenderer Komponente erzeugen
            lr = gameObject.AddComponent<LineRenderer>();
            lr.useWorldSpace = true;
            lr.positionCount = waypoints.Length;
            lr.SetPositions(waypoints);
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = Color.green;
            lr.endColor = Color.green;
            lr.startWidth = 0.01f;
            lr.endWidth = 0.01f;
            lr.enabled = ShowTheCurve;
        }
        
        /// <summary>
        /// Wir verwenden FixedUpdate, da wir mit Time.fixedDeltaTime arbeiten.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            lr.enabled = ShowTheCurve;

            if (!Run) return;
            // Objekt mit Hilfe von FollowerWithLogs bewegen
            transform.position = this.manager.Move(
                                 transform.position,
                                 velocities[manager.Current] * Time.fixedDeltaTime
                );
            transform.LookAt(manager.GetFollowupWaypoint());
        }

        /// <summary>
        /// Abstrakte Funktion ComputePath. Muss von den abgeleiteten Klassen
        /// implementiert werden und enthält die Parameterdarstellung der
        /// Kurve.
        /// </summary>
        protected abstract void ComputePath();

        /// <summary>
        /// Berechnung der ersten Lookat-Punkts. 
        /// Damit können wir das gesteuerte Objekt ausrichten.
        /// 
        /// Als Default wird hier forward verwendet. Abgeleitete Klassen
        /// können für den ersten Punkt die Tangente berechnen und hier setzen.
        /// </summary>
        /// <returns>Punkt, der LookAt übergeben werden kann</returns>
        protected virtual Vector3 ComputeFirstLookAt()
        {
            return Vector3.forward;
        }

        /// <summary>
        /// Berechne den minimalen Abstand für das Durchlaufen der Wegpunkte.
        ///
        /// Wir berechnen die Feinheit des Polygonzugs und verwenden 50% davon
        /// als minimalen Abstand im Waypoint-Manager.
        /// </summary>
        /// <returns></returns>
        private float ComputeDistance()
        {
            var dist = float.MaxValue;
            for (var i = 0; i < waypoints.Length - 1; i++)
            {
                var next = Vector3.Distance(waypoints[i + 1],waypoints[i]);
                if (dist > next) dist = next;
            }
            return 0.5f*dist;
        }
        
        /// <summary>
        /// Array mit Instanzen von Vector3 für die Wegpunkt
        /// </summary>
        protected Vector3[] waypoints;       
        /// <summary>
        /// Array mit Instanzen von Vector3 für die Geschwindigkeiten an den Wegpunkten
        /// </summary>
        protected float[] velocities;

        /// <summary>
        /// Instanz eines LineRenderers für die Visualisierung der Kurve
        /// </summary>
        /// <remarks>
        /// Wir benötigen eine LineRenderer-Komponente im Inspektor!
        /// </remarks>
        protected LineRenderer lr;
        
        /// <summary>
        /// Instanz der Klasse WaypointManager
        /// 
        /// Die Berechnung von Positionen und die Verwaltung
        /// der Zielpunkte erfolgt in dieser C#-Klasse.
        /// Sie ist *nicht* von MonoBehaviour abgeleitet!
        /// </summary>
        private WaypointManager manager = null;
}
