//========= 2020 -2023  Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

    /// <summary>
    /// Verwaltung von Weg-Punkten.
    /// </summary>
    public class WaypointManager
    {
        /// <summary>
        /// Default-Konstruktor
        /// 
        /// Minimaler Abstand zum Zielobjekt 1.0,
        /// Keine Zielpunkte.
        /// Periodisches Verhalten
        /// </summary>
        public WaypointManager()
        {
            ArriveDistance = 1.0f;
            m_currentIndex = 0;
            m_periodic = true;
            m_targetPositions = null;
        }

        /// <summary>
        /// Default-Konstruktor
        /// 
        /// Minimaler Abstand zum Zielobjekt 1.0,
        /// Keine Zielpunkte.
        /// Periodisches Verhalten
        /// </summary>
        /// <param name="dist">
        /// Abstand, ab dem ein Zielpunkt als erreicht eingestuft wird.
        /// </param>
        public WaypointManager(float dist)
        {
            ArriveDistance = dist;
            m_currentIndex = 0;
            m_targetPositions = null;
            m_periodic = true;
        }

        /// <summary>
        /// Konstruktor mit Hilfe von GameObjects
        /// </summary>
        /// <remarks>
        /// Abstand zum Wechsel ist 1.0,
        /// Periodisches Verhalten
        /// </remarks>
        public WaypointManager(GameObject[] points)
        {
            var numberOfPoints = points.Length;
            m_currentIndex = 0;
            ArriveDistance = 1.0f;
            m_periodic = true;
            if (numberOfPoints > 0)
            {
                m_targetPositions = new Vector3[numberOfPoints];
                for (var i = 0; i < numberOfPoints; i++)
                    m_targetPositions[i] = points[i].transform.position;
            }
            else
                Debug.LogError("Fehler im Konstruktor von WaypointManager -- keine Punkte übergeben");
        }

        /// <summary>
        /// Konstruktor mit Hilfe von GameObjects und Distanz.
        /// Periodisches Verhalten.
        /// </summary>
        /// <param name="points">Feld mit GameObjects, deren Position für
        /// die Zielpunkte verwendet wird
        /// </param>
        /// <param name="dist">Abstand, ab dem ein Zielpunkt als
        /// erreicht eingestuft wird.
        /// </param>
        public WaypointManager(GameObject[] points, float dist)
        {
            var numberOfPoints = points.Length;
            m_currentIndex = 0;
            ArriveDistance = dist;
            this.m_periodic = true;
            if (numberOfPoints > 0)
            {
                m_targetPositions = new Vector3[numberOfPoints];
                for (var i = 0; i < numberOfPoints; i++)
                    m_targetPositions[i] = points[i].transform.position;
            }
            else
                Debug.LogError("Fehler im Konstruktor von WaypointManager -- keine Punkte übergeben");
        }

        /// <summary>
        /// Konstruktor mit Hilfe von Vector3-Instanzen.
        /// Periodisches Verhalten.
        /// </summary>
        /// <param name="points">
        /// Feld mit Vector3-<Instanzen, die die Zielpunkte definieren.
        /// </param>
        public WaypointManager(Vector3[] points)
        {
            var numberOfPoints = points.Length;
            m_currentIndex = 0;
            ArriveDistance = 1.0f;
            m_periodic = true;
            if (numberOfPoints > 0)
            {
                m_targetPositions = new Vector3[numberOfPoints];
                for (var i = 0; i < numberOfPoints; i++)
                    m_targetPositions[i] = points[i];
            }
            else
                Debug.LogError("Fehler im Konstruktor von WaypointManager -- keine Punkte übergeben");
        }

        /// <summary>
        /// Konstruktor mit Hilfe von Vector3-Instanzen.
        /// Periodisches Verhalten.
        /// </summary>
        /// <param name="points">
        /// Feld mit Vector-Instanzen, die die Zielpunkte definieren.
        /// </param>
        /// <param name="dist">Abstand, ab dem ein Zielpunkt als
        /// erreicht eingestuft wird.
        /// </param>/// 
        public WaypointManager(Vector3[] points, float dist)
        {
            var numberOfPoints = points.Length;
            m_currentIndex = 0;
            ArriveDistance = dist;
            m_periodic = true;
            if (numberOfPoints > 0)
            {
                m_targetPositions = new Vector3[numberOfPoints];
                for (var i = 0; i < numberOfPoints; i++)
                    m_targetPositions[i] = points[i];
            }
            else
                Debug.LogError("Fehler im Konstruktor von WaypointManager -- keine Punkte übergeben");
        }

        /// <summary>
        /// Konstruktor mit Hilfe von Vector3-Instanzen.
        /// </summary>
        /// <param name="points">
        /// Feld mit Vector3-Instanzen, die die Zielpunkte definieren.
        /// </param>
        /// <param name="dist">Minimalabstand zum Umschalten</param>
        /// <param name="per">Periodisches Verhalten oder nicht?</param>
        public WaypointManager(Vector3[] points, float dist, bool per)
        {
            var numberOfPoints = points.Length;
            m_currentIndex = 0;
            ArriveDistance = dist;
            m_periodic = per;
            if (numberOfPoints > 0)
            {
                m_targetPositions = new Vector3[numberOfPoints];
                for (var i = 0; i < numberOfPoints; i++)
                    m_targetPositions[i] = points[i];
            }
            else
                Debug.LogError("Fehler im Konstruktor von WaypointManager -- keine Punkte übergeben");
        }

        /// <summary>
        /// Zurücksetzen der Waypoints, wir beginnen von vorne.
        /// </summary>
        public void ResetWaypoints()
        {
            m_currentIndex = 0;
        }

        /// <summary>
        /// Das nächste Ziel verwenden.
        /// </summary>
        public void NextWaypoint()
        {
            var nextIndex = m_currentIndex+1;
            
            if (m_periodic)
            {
                m_currentIndex = nextIndex % m_targetPositions.Length;
            }
            else {
                if (nextIndex > 0 && nextIndex < m_targetPositions.Length)
                {
                    m_currentIndex = nextIndex;
                }
            }   
        }

        /// <summary>
        /// Abfragen des aktuellen Zielpunkts.
        /// </summary>
        /// <returns>
        /// Zielpunkt als Instanz von Vector3
        /// </returns>
        public Vector3 GetWaypoint()
        {
            return m_targetPositions[m_currentIndex];
        }

        /// <summary>
        /// Abfragen des Nachfolgers des aktuellen Zielpunkts.
        /// 
        /// Damit können wir die Orientierung mit Hilfe von LookAt setzen.
        /// </summary>
        /// <returns>
        /// Nachfolger des aktuellen Zielpunkts als Instanz von Vector3
        /// </returns>
        public Vector3 GetFollowupWaypoint()
        {
            var nextIndex = m_currentIndex + 1;

            if (m_periodic)
            {
                return m_targetPositions[nextIndex % m_targetPositions.Length];
            }
            else
            {
                if (nextIndex >= 0 && m_targetPositions.Length > nextIndex) 
                    return m_targetPositions[nextIndex];
                else
                    return m_targetPositions[m_currentIndex];
            }
        }

        /// <summary>
        /// Setzen von Zielpunkten mit Hilfe von Punkten.
        /// Vorher vorhandene Zielpunkte werden überschrieben!
        /// </summary>
        /// <param name="points">
        /// Array mit Instanzen von Vector3 als Zielobjekte.
        /// </param>
        public void SetWaypoints(Vector3[] points)
        {
            var numberOfPoints = points.Length;
            if (numberOfPoints > 1)
            {
                m_targetPositions = new Vector3[numberOfPoints];
                for (var i = 0; i < numberOfPoints; i++)
                    m_targetPositions[i] = points[i];
            }
        }

        /// <summary>
        /// Setzen von Zielpunkten mit Hilfe von Punkten.
        /// Vorher vorhandene Zielpunkte werden überschrieben!
        /// </summary>
        /// <param name="points">
        /// Array mit Instanzen von GameObject als Zielobjekte.
        /// </param>
        public void SetWaypoints(GameObject[] points)
        {
            var numberOfPoints = points.Length;
            if (numberOfPoints > 1)
            {
                m_targetPositions = new Vector3[numberOfPoints];
                for (var i = 0; i < numberOfPoints; i++)
                    m_targetPositions[i] = points[i].transform.position;
            }
        }

        /// <summary>
        /// Berechnung einer Positionsveränderung in Richtung des aktuellen Zielpunkts.
        /// </summary>
        /// <remarks>
        /// Falls wir näher als arriveDistance an den aktuellen Zielpunkt kommen schaltet
        /// diese Funktion auf den nächsten Zielpunkt um!
        /// </remarks>
        /// <param name="distance">
        /// Wie groß soll der Schritt in Richtung des aktuellen Zielpunkts sein?
        /// </param>
        /// <param name="position">
        /// Die aktuelle Position, von der aus die Bewegung in Richtung des Zielpunkts erfolgen soll.
        /// </param>
        /// <returns>
        /// Neue Position, näher am Zielpunkt. Falls wir dabei einen Zielpunkt
        /// erreicht haben wird der  Zielpunkt gewechselt.
        /// </returns>
        public Vector3 Move(Vector3 position, float distance)
        {
            Vector3 newPosition = Vector3.MoveTowards(position, 
                m_targetPositions[m_currentIndex], 
                distance);

            // Überprüfen, ob wir schon nah genug am Ziel sind und den
            // Zielpunkt dann wechseln.
            if (Vector3.Distance(newPosition, m_targetPositions[m_currentIndex]) < ArriveDistance)
                NextWaypoint();

            return newPosition;
        }

        /// <summary>
        /// Minimaler Abstand zwischen einem Objekt und einem Zielpunkt.
        /// </summary>
        /// <remarks>
        /// Ist der Abstand zum aktuellen Zielpunkt kleiner als arriveDistance,
        /// dann verwenden wir den nächsten Zielpunkt.
        /// </remarks>
        public float ArriveDistance { get; set; }
        
        /// <summary>
        ///  Index des aktuellen Ziels
        /// </summary>
        public int Current => m_currentIndex;
        /// <summary>
        /// Index des aktuellen Ziels
        /// 
        /// Wir beginnen beim ersten Eintrag des Ziels.
        /// </summary>
        private int m_currentIndex;       
        /// <summary>
        /// Werden die Punkte periodisch durchlaufen?
        /// </summary>
        public bool Periodic => m_periodic;
        /// <summary>
        /// Werden die Punkte periodisch durchlaufen?
        /// </summary>
        private readonly bool m_periodic; 
        
        /// <summary>
        /// Position der Zielpunkte
        /// </summary>
        private Vector3[] m_targetPositions;
    }
