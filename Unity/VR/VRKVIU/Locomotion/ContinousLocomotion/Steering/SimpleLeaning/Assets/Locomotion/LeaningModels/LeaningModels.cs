//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für Locomotion-Verfahren,
/// die Leaning-Models einsetzen.
/// </summary>
public abstract class LeaningModels : Locomotion
{
       [Header("Leaning Model")]
        /// <summary>
        /// Welches GameObject verwenden wir für die Überwachung
        /// und Realisierung des Leaning Models?
        /// </summary>
        /// <remarks>
        /// Sinnvoll ist  der Kopf, oder ein anderes
        /// getracktes GameObject.
        /// </remarks>
        [Tooltip("Welches Objekt realisiert das Leaning Model")]
        public Transform LeaningObject;
        
        
        /// <summary>
        /// Wird dieser Wert überschritten lösen wir den
        /// Trigger aus und die Bewegung startet.
        /// </summary>
        [Tooltip("Schwellwert für das Auslösen der Bewegung")] 
        [Range(0.01f, 1.0f)]
        public float Threshold = 0.05f;

        [Header("Protokollierung der Berechnungen")]
        /// <summary>
        /// Aktivieren und De-Aktivieren Protokollieren
        /// </summary>      
        [Tooltip("Protollieren?")]
        public bool Logs = false;
        
        /// <summary>
        /// Dateiname für das Protokoll
        /// </summary>      
        [Tooltip("Name der Protokoll-Datei")]
        public string fileName = "leaning.csv";
    
        
        /// <summary>
        /// Initialisierung
        ///
        /// Wir stellen den LogHander ein und
        /// erzeugen anschließend Log-Ausgaben in LateUpdate.
        protected override void Awake()
        {
            csvLogHandler = new CustomLogHandler(fileName);
            if (!Logs)
                Debug.unityLogger.logEnabled = false;

            base.Awake();
        }
        
        /// <summary>
        /// Update aufrufen und die Bewegung ausführen,
        /// falls sie aktiv ist.
        /// </summary>
        /// <remarks>
        /// Wir überprüfen ob der Schwellwert überschritten wurde
        /// und bestimmen anschließend die Bewegungsrichtung
        /// und die Geschwindigkeit. 
        /// </remarks>
        protected virtual void Update()
        {
            Trigger();
            if (!Moving) return;
            UpdateDirection();
            UpdateSpeed();
            Move();
        }

        /// <summary>
        /// Die Bewegung durchführen.
        /// </summary>
        /// <remarks>
        /// Die Funktion geht davon aus, dass die Überprüfung
        /// der Variable Moving vor dem Aufruf bereits durchgeführt wurde!
        /// <remarks>
        protected override void Move()
        {
            transform.Translate(m_Speed * Time.deltaTime * m_Direction);
        }
        
        /// <summary>
        /// Geschwindigkeit initialiseren. Wir überschreiben diese
        /// Funktion in den abgeleiteten Klassen und rufen
        /// diese Funktion in Locomotion::Awake auf.
        /// </summary>
        protected override void InitializeSpeed()
        {
            // Wir verändern die Geschwindigkeit nicht ...
            m_Velocity = new LinearBlend(0.0f, 0.1f,
                0.0f, 2.0f );
            m_Speed = 0.0f;
        }
        
        /// <summary>
        /// Die abgeleiteten Klassen entscheiden, wann die Locomotion
        /// getriggert werden.
        /// </summary>
        protected abstract void Trigger();

        /// <summary>
        /// Bewegungsrichtung auf den forward-Vektor
        /// des Orientierungsobjekts setzen.
        /// </summary>
        /// <remarks>
        /// Wir führen ein Walk durch, deshalb setzen wir die y-Koordinate
        /// der Richtung auf 0.
        /// </remarks>
        protected override void InitializeDirection()
        {
            m_Direction = LeaningObject.transform.forward;
            m_Direction.y = 0.0f;
            m_Direction.Normalize();
        }

        /// <summary>
        /// Schließen der Protokolldatei
        /// </summary>
        private void OnDisable()
        {
            csvLogHandler.CloseTheLog();
        }
        
        /// <summary>
        /// Umrechnung von kartesischen Koordinaten
        /// in Polarkoordinaten.
        /// </summary>
        /// <param name="coordinates">2D Vektor in kartesischen Koordinaten</param>
        /// <returns>Polarkoordinaten</returns>
        protected static Vector2 m_Cartesian2Polar(Vector2 coordinates)
        {
            var returnVector = new Vector2();
            returnVector[0] = coordinates.magnitude;
            returnVector[1] = Mathf.Atan2(coordinates.y, coordinates.x);
            return returnVector;
        }
        
        /// <summary>
        /// Wird aktuell nicht verwendet, da wir die Bewegungsrichtung
        /// direkt aus dem forward-Vektor des Orientierungsobjekts
        /// ablesen.
        /// </summary>
        protected override void UpdateOrientation()
        {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Eigener LogHandler
        /// </summary>
        protected CustomLogHandler csvLogHandler;

        /// <summary>
        /// Instanz des Default-Loggers in Unity
        /// </summary>
        protected static readonly ILogger s_Logger = Debug.unityLogger;
}
