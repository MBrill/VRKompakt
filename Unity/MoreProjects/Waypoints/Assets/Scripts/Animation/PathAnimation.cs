//========= 2020 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
 /// Abstrakte Basisklasse f�r die Bewegung eines Objekts  entlang
 /// einer Parameterkurve
 /// </summary>
public abstract class PathAnimation : MonoBehaviour
{
        /// <summary>
		/// Wir n�hern die Kurve mit Hilfe eines Polygonzugs  an.
		/// Mit diesem Polygonzug erzeugen wir dann Wegpunkte und
		/// verenden die Klasse WaypointManager f�r die Bewegung.
		/// </summary)
		[Range(4, 1024)]
        [Tooltip("Anzahl der Waypoints")]
        public int NumberOfPoints = 65;

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
        /// Reset der Visualisierung, falls wir beim Durchlaufen den letzten Punkt
        /// erreicht haben.
        /// </summary>
        public void ResetCurve()
        {
            if (!this.manager.ReachedLastWayPoint) return;
            this.manager.ResetWaypoints();
            transform.position = this.manager.GetWaypoint();
            transform.LookAt(this.manager.GetFollowupWaypoint());
            this.manager.ReachedLastWayPoint = false;
        }
        
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
            
            this.manager = new WaypointManager(waypoints, dist, false);
            
            // Den ersten Zielpunkt setzen
            transform.position = manager.GetWaypoint();
            // Orientierung setzen
            transform.LookAt(ComputeFirstLookAt());
            
            // LineRenderer Komponente erzeugen
            m_Line = gameObject.AddComponent<LineRenderer>();
            m_Line.useWorldSpace = true;
            m_Line.positionCount = waypoints.Length;
            m_Line.SetPositions(waypoints);
            m_Line.material = new Material(Shader.Find("Sprites/Default"));
            m_Line.startColor = Color.green;
            m_Line.endColor = Color.green;
            m_Line.startWidth = 0.01f;
            m_Line.endWidth = 0.01f;
            m_Line.enabled = ShowTheCurve;
        }
        
        /// <summary>
        /// Wir verwenden FixedUpdate, da wir mit Time.fixedDeltaTime arbeiten.
        /// </summary>
        protected virtual void FixedUpdate()
        {
            m_Line.enabled = ShowTheCurve;
            
            if (!Run) return;

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
            return waypoints[1];
        }

        /// <summary>
        /// Berechne den minimalen Abstand f�r das Durchlaufen der Wegpunkte.
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
        /// Array mit Instanzen von Vector3 f�r die Wegpunkte
        /// </summary>
        protected Vector3[] waypoints;

        /// <summary>
        /// Array mit den Bahngeschwindigkeiten an den Wegpunkten
        /// </summary>
        protected float[] velocities;

        /// <summary>
        /// Instanz eines LineRenderers f�r die Visualisierung der Kurve
        /// </summary>
        /// <remarks>
        /// Wir ben�tigen eine LineRenderer-Komponente im Inspektor!
        /// </remarks>
        private LineRenderer m_Line;
        
        /// <summary>
        /// Instanz der Klasse WaypointManager
        /// 
        /// Die Berechnung von Positionen und die Verwaltung
        /// der Zielpunkte erfolgt in dieser C#-Klasse.
        /// Sie ist *nicht* von MonoBehaviour abgeleitet!
        /// </summary>
        private WaypointManager manager = null;
}
