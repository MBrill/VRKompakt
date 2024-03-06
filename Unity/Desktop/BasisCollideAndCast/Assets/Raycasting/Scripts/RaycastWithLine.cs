//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
///   Raycast in Richtung einer der Koordinatenachsen des lokalen
/// Koordinatensystems eines Objekts.
/// </summary>
/// <remarks>
/// Wir geben bei einem Treffer den Namen des getroffenen
/// Objekts aus, und auch die Koordinaten des Schnittpunkts.
/// </remarks>
public class RaycastWithLine : RaycastBase
{
    /// <summary>
    /// Dieses Prefab wird an einem berechneten Schnittpunkt dargestellt.
    /// </summary>
    [Tooltip("Prefab für die Visualisierung des Schnittpunkts")]
    public GameObject HitVis;
    
    /// <summary>
    /// Instanz eines LineRenderers.
    /// </summary>
    /// <remarks>
    /// Wir benötigen eine LineRenderer-Komponente im Inspektor!
    /// </remarks>
    protected LineRenderer lr;
    
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
        lr = gameObject.AddComponent<LineRenderer>();
        // Position des Objekts nutzen für erste Position
        var ax = transform.TransformDirection(m_axis[(int) Dir]);
        Vector3[] points = new Vector3[2];
        points[0] = transform.position;
        points[1] = transform.position + MaxLength * ax;
        lr.useWorldSpace = true;
        lr.positionCount = points.Length;
        lr.SetPositions(points);
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.enabled = m_cast;
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
    void FixedUpdate()
    {
        // Ist Raycasting aktiv zeigen wir den Strahl, mit Endpunkt
        // bei dem Parameterwert MaxDist. 
        // Treffen wir ein Objekt setzen wir den Endpunkt auf
        // den Schnittpunkt.
        if (m_cast)
        {
            lr.enabled = true;
            // Prefab für das Ende des Strahls  visualisieren

            var ax = transform.TransformDirection(m_axis[(int) Dir]);
            Vector3[] points = new Vector3[2];
            points[0] = transform.position;
            // Zweiter Punkt ist abhängig davon, ob wir einen Schnittpunkt
            // erhalten oder nicht.
            RaycastHit hitInfo;
            if (Physics.Raycast(
                transform.position,
                ax,
                out hitInfo,
                MaxLength))
            {
                HitVis.transform.position = hitInfo.point;
                HitVis.GetComponent<MeshRenderer>().enabled = true;
                
                if (RayLogs)
                {
                    Debug.Log(m_Log[(int) Dir]);
                    Debug.Log("Getroffen wurde das Objekt " + hitInfo.collider);
                    Debug.Log("Der Abstand zu diesem Objekt ist "
                              + hitInfo.distance
                              + " Meter");
                }
            }
            else
            {
                HitVis.transform.position = transform.position + MaxLength * ax;
                HitVis.GetComponent<MeshRenderer>().enabled = false;
            }
            points[1] = HitVis.transform.position;
            lr.SetPositions(points);
        }
        else
        {
            lr.enabled = false;
            // HitVisausblenden
            HitVis.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
