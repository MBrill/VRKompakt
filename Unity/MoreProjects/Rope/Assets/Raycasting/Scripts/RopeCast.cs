//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
///   Ein Seilzug für die Untersuchung eines weit entfernten Objekts.
/// Wir führen einen Raycast durch und holen ein Objekt auf Tastendruck
/// entlang des Strahls zum Objekt, von dem der Raycast ausgeht.
/// </summary>
/// <remarks>
///  Wir verwenden RopeAnimation und SlowInSlowOut für die Bewegung
/// des Objekts.
/// </remarks>
public class RopeCast : RaycastBase
{
    /// <summary>
    /// Dieses Prefab wird an einem berechneten Schnittpunkt dargestellt.
    /// </summary>
    [Tooltip("Prefab für die Visualisierung des Schnittpunkts")]
    public GameObject HitVis;
    
    /// <summary>
    /// Soll der Seilzug aktiviert werden oder nicht?
    /// </summary>
    protected bool m_rope = false;

    protected LineSlowInSlowOut m_ropeLine;
    
    /// <summary>
    /// Komponente für die Bewegung entlang des Seilzugs
    /// </summary>
    //protected LineSlowInSlowOut m_ropeLine;
    /// <summary>
    /// Instanz eines LineRenderers für den Strahl, falls Raycast
    /// durchgeführt wird.
    /// </summary>
    private LineRenderer m_lr;
    
    /// <summary>
    /// Anlegen des Prefabs für die Schnitpunkt-Visualisierung
    /// und Initialisieren des lineRenderers.
    /// </summary>
    private void Start()
    {
        HitVis = Instantiate(HitVis,
            new Vector3(0.0f, 0.0f, 0.0f),
            Quaternion.identity);
        HitVis.SetActive(true);
        HitVis.GetComponent<MeshRenderer>().enabled = false;
        
        // LineRenderer Komponente erzeugen
        m_lr = gameObject.AddComponent<LineRenderer>();
        // Position des Objekts nutzen für erste Position
        var ax = transform.TransformDirection(m_axis[(int) Dir]);
        var points = new Vector3[2];
        points[0] = transform.position;
        points[1] = transform.position + MaxLength * ax;
        m_lr.useWorldSpace = true;
        m_lr.positionCount = points.Length;
        m_lr.SetPositions(points);
        m_lr.material = new Material(Shader.Find("Sprites/Default"));
        m_lr.startColor = Color.green;
        m_lr.endColor = Color.green;
        m_lr.startWidth = 0.01f;
        m_lr.endWidth = 0.01f;
        m_lr.enabled = m_cast;
    }
    
    /// <summary>
    ///  Raycasting wird in FixedUpdate ausgeführt!
    /// </summary>
    /// <remarks>
    /// Wir führen den Raycast auf Tastendruck aus, sonst wird
    /// die Konsole mit den immer gleichen Meldungen überschwemmt.
    ///
    /// Wir protokollieren einige Ergebnisse der Schnittberechnung
    /// und stellen am berechneten Schnittpunkt ein kleines Prefab dar.
    /// </remarks>
    protected void FixedUpdate()
    {
        GameObject ropeObject;
        var points = new Vector3[2];
        
        // Ist Raycasting aktiv zeigen wir den Strahl, mit Endpunkt
        // bei dem Parameterwert MaxDist. 
        // Treffen wir ein Objekt setzen wir den Endpunkt auf
        // den Schnittpunkt.
        if (m_cast)
        {
            m_lr.enabled = true;
            // Prefab für das Ende des Strahls  visualisieren

            var ax = transform.TransformDirection(m_axis[(int) Dir]);
            points[0] = transform.position;
            // Zweiter Punkt ist abhängig davon, ob wir einen Schnittpunkt
            // erhalten oder nicht.
            RaycastHit hitInfo;
            if (Physics.Raycast(
                points[0],
                ax,
                out hitInfo,
                MaxLength))
            {
                HitVis.transform.position = hitInfo.point;
                HitVis.GetComponent<MeshRenderer>().enabled = true;
                
                if (hitInfo.collider != null)
                {
                        ropeObject = hitInfo.collider.gameObject;
                        Debug.Log(ropeObject.name);
                        m_ropeLine = ropeObject.AddComponent<LineSlowInSlowOut>();
                        m_ropeLine.Run = false;
                        m_ropeLine.p1 = hitInfo.point;
                        m_ropeLine.p2 = transform.position;
                }
                if (m_rope)
                    m_ropeLine.Run = true;
            }
            else
            {
                HitVis.transform.position = transform.position + MaxLength * ax;
                HitVis.GetComponent<MeshRenderer>().enabled = false;
            }
            points[1] = HitVis.transform.position;
            m_lr.SetPositions(points);
        }
        else
        {
            m_lr.enabled = false;
            // HitVisausblenden
            HitVis.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
