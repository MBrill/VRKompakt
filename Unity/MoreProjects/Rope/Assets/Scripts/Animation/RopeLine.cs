//========= 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
 /// Abstrakte Basisklasse für die Bewegung eines Objekts  entlang
 /// einer Parameterkurve
 /// </summary>
 /// <remarks>
 /// Im Gegensatz zur Pfadanimation wird hier kein LookAt eingesetzt,
 /// die bewegten Objekte sollen ihre ursprüngliche Orientierung beibehalten
 /// und nur die Position verändern.
 /// </remarks>
public class RopeLine : MonoBehaviour
{
    /// <summary>
    /// Ein Objekt, zu dem wir das ausgewählte Objekt hin bewegen.
    /// </summary>
    /// <remarks>
    /// Bei Rabbit-ouf-the-Hole ist dies einer der Controller oder ein anderes
    /// getracktes Objekt.
    /// </remarks>
    [Tooltip("Das Zielobjekt")]
    public Transform TargetObject;

    /// <summary>
    /// Geschwindigkeit für die Bewegung des ausgewählten Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit der Bewegung")]
    [Range(1.0F, 20.0F)]
    public float Speed = 5.0F;
    
    /// <summary>
        /// Soll  die Bewegung ausgeführt  werden?
        /// </summary>
        [Tooltip("Bewegung ausführen")] 
        public bool IsRunning = false;
       
        /// <summary>
        /// Soll die Linie  dargestellt werden?
        /// </summary>
        [Tooltip("Visualisierung der Verbindungslinie")] 
        public bool ShowTheLine = false;

        /// <summary>
        /// Wir stoppen das bewegte Objekt vor dem Pivot-Point
        /// des Zielobjekts, sonst wird ein Teil des Zielobjekts,
        /// zum Beispiel ein Controller, teilweise verdeckt.
        /// </summary>
        [Tooltip("Abstand zum Zielobjekt")] 
        [Range(0.0f, 1.0f)]
        public float DistanceToTarget = 0.1f;

        /// <summary>
        /// Originalposition des ausgewählten Objekts vor Durchführung
        /// der Interaktion.
        /// </summary>
        ///         /// <remarks>
        /// Wir speichern diese Position, denn nach Ende der Interaktion
        /// soll das ausgewählte Objekt wieder an seine ursprünglichen
        /// Position zurückkehren.
        /// </remarks>
        private Vector3 m_originalPosition;

        /// <summary>
        /// Instanz eines LineRenderers für die Visualisierung der Kurve
        /// </summary>
        /// <remarks>
        /// Wir benötigen eine LineRenderer-Komponente im Inspektor!
        /// </remarks>
        private LineRenderer m_Line;

        /// <summary>
        /// Logische Variable die angibt, ob sich das ausgewählte Objekt
        /// zum Zielobjekt bewegt oder ob die Bewegung das Objekt
        /// zu seiner ursprünglichen Position zurücktransportiert.
        ///
        /// true bedeutet Bewegung zum Zielobjekt, false zurück.
        /// </summary>
        private bool m_PhaseOne = true;
        
        /// <summary>
        /// Abstand zwischen ausgewähltem Objekt und Zielobjekt,
        /// bei dem die Bewegung in Richtung des Zielobjekts gestoppt wird.
        /// </summary>
        private const float MArriveDistance = 0.1f;
        
        /// <summary>
        /// Anfangs- und Endpunkt der Linie berechnen für die
        /// Visualisierung mit einem LineRenderer, den wir hier anlegen.
        /// </summary>
        protected virtual void Awake()
        {
            // Ursprüngliche Position speichern
            m_originalPosition = transform.position;
            
            m_originalPosition = transform.position;

            // LineRenderer Komponente für die Visualisierung erzeugen
            m_Line = gameObject.AddComponent<LineRenderer>();
            m_Line.useWorldSpace = true;
            m_Line.positionCount =2;
            m_Line.SetPosition(0, m_originalPosition);
            m_Line.SetPosition(1, m_Endpoint());
            m_Line.material = new Material(Shader.Find("Sprites/Default"));
            m_Line.startColor = Color.green;
            m_Line.endColor = Color.green;
            m_Line.startWidth = 0.01f;
            m_Line.endWidth = 0.01f;
            m_Line.enabled = ShowTheLine;
        }
        
        /// <summary>
        /// Bewegung in Update
        /// </summary>
        private void Update ()
        {
            // Linie visualisieren oder nicht,Endpunkt aktualisieren
            m_Line.enabled = ShowTheLine;
            
            m_Line.SetPosition(1, m_Endpoint());

            if (!IsRunning) return;
            // Bewegung durchführen
            transform.position = Vector3.MoveTowards(transform.position,
                m_Endpoint(),
                Speed * Time.deltaTime);
            
            // Überprüfen, ob der Endpunkt bereits erreicht wurde.
            // In diesem Fall stoppen wir die Bewegung, und vertauschen
            // Anfangs- und Endpunkt. Aktivieren wir dann wieder die Bewegung,
            // bewegt sich das ausgewählte Objekt wieder zu seiner Originaposition
            // zurück.
            if (Vector3.Distance(
                transform.position, m_Endpoint()) >= MArriveDistance)
                return;
       
            // Aktueller Endpunkt erreicht
            IsRunning = false;
            m_PhaseOne = !m_PhaseOne;
        }

        /// <summary>
        /// Wir berechnen einen Endpunkt für die Linie,
        /// die kurz vor dem Zielobjekt liegt.
        ///
        /// Bewegt sich das Zielobjekt, verändert sich auch
        /// der Endpunkt der Linie.
        /// </summary>
        private Vector3 m_Endpoint()
        {
            if (m_PhaseOne)
            {
                var dir = TargetObject.transform.position - m_originalPosition;
                return m_originalPosition + (1.0f - DistanceToTarget) * dir;
            }
            else
            {
                return m_originalPosition;
            }
        }
}
