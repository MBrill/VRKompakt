//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///   XRaycast - Objekte, die ein gesuchtes Objekt verdecken und dabei
/// vom Strahl getroffen werden erhalten eine Transparenz, so lange der
/// Strahl angezeigt wird.
/// </summary>
/// <remarks>
/// Wir verwenden die Unity-Funktion RaycastAll.
/// </remarks>
public class XRayCast : RaycastBase
{
    /// <summary>
    /// Wir wählen nur Objekte aus, die den hier eingestellten Tag
    /// besitzen. Alle anderen werden transparent dargestellt.
    /// </summary>
    [Tooltip("Tag für die auswählbaren Objekte")]
    public String SelectTag = "Selectable";

    /// <summary>
    /// Alpha-Wert für die Darstellung der Objekte, die zwar
    /// vom Strahl getroffen, aber den "falschen" Tag haben
    /// </summary>
    [Tooltip("Transparenzwert für nicht auswählbare Objekte")]
    [Range(0.0f, 1.0f)]
    public float Transparency = 0.6f;
    
    /// <summary>
    /// Dieses Prefab wird an einem berechneten Schnittpunkt dargestellt.
    /// </summary>
    [Tooltip("Prefab für die Visualisierung des Schnittpunkts")]
    public GameObject HitVis;
    
    
    /// <summary>
    /// In diesem Dictionary speichern wir die Originalwerte
    /// vder Transparenz für die aktuell transparent dargestellten Objekte
    /// zur Wiederherstellung des Materials
    /// </summary>
    private static Dictionary<string, float> m_TransparentObjects =
        new Dictionary<string, float>();
    
    /// <summary>
    /// Instanz eines LineRenderers.
    /// </summary>
    /// <remarks>
    /// Wir benötigen eine LineRenderer-Komponente im Inspektor!
    /// </remarks>
    private LineRenderer lr;
    
    /// <summary>
    /// Anlegen des Prefabs für die Schnitpunkt-visualisierung
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
        // Position des Objekts nutzen für erste Positionen
        Vector3[] points = new Vector3[2];
        points[0] = transform.position;
        points[1] = transform.position + MaxLength * transform.forward;
        lr.useWorldSpace = true;
        lr.positionCount = points.Length;
        lr.SetPositions(points);
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        if (m_cast)
            lr.enabled = true;
        else
        {
            lr.enabled = false;
        }
    }
    
    /// <summary>
    ///  Raycasting
    /// </summary>
    private void FixedUpdate()
    {
        // Alpha-Werte zurücksetzen, Dictionary ist anschließend leer.
        m_ReconstructAlphaValues();
        var theHit = -1;
        
        if (m_cast)
        {
            // Strahl visualisieren
            lr.enabled = true;
            var points = new Vector3[2];
            points[0] = transform.position;
            // Erst einmal den ganzen Strahl zeigen
            points[1] = transform.position + MaxLength * transform.forward;
            lr.SetPositions(points);
            
            var hits = Physics.RaycastAll(transform.position,
                transform.forward,
                MaxLength);

            // Liste daraus erstellen und nach Abstand sortieren
            m_FinalHits = hits.ToList();
            m_FinalHits = new List<RaycastHit>(m_FinalHits.OrderBy(o => o.distance));
            // Array durchlaufen und nach  auswählbaren Objekten suchen
            theHit = m_FinalHits.FindIndex(m_IsSelectable);
            // Alle Elemente in der Liste nach dem auswählbaren Objekt entfernen
            if (theHit >= 0)
                m_FinalHits.RemoveRange(theHit+1, m_FinalHits.Count-1-theHit);
            
            // Das letzte Element in der Liste ist das auswählbare Objekt
            //Alle davor transparent darstellen
            for (int i=0; i< m_FinalHits.Count-1; i++)
            {
                var hit = m_FinalHits[i];
                var rend = hit.transform.GetComponent<Renderer>();
                if (rend)
                {
                    rend.material.shader = Shader.Find("Transparent/Diffuse");
                    var tempColor = rend.material.color;
                    // Alpha-Werte speichern für die Rekonstruktion,
                    // falls das Objekt noch nichtt enthalten ist
                    if (!m_TransparentObjects.ContainsKey(hit.collider.name))
                        m_TransparentObjects.Add(hit.collider.name, tempColor.a);
                    // Temporärer Alpha-Wert setzen
                    tempColor.a = Transparency;
                    rend.material.color = tempColor;
                }
            }
            
            if (m_FinalHits.Count > 0)
            {
                // Auswählbares Objekt hat Index 0
                
                HitVis.transform.position = m_FinalHits.Last().point;
                HitVis.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                HitVis.transform.position = transform.position + MaxLength * transform.forward;
                HitVis.GetComponent<MeshRenderer>().enabled = false;
            }
            
            // Strahl am auswählbaren Objekt enden lassen
            points[1] = HitVis.transform.position;
            lr.SetPositions(points);
        }
        else 
        {
            lr.enabled = false;
            // HitVis ausblenden
            HitVis.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// Alle Alpha-Werte im Dictionary rekonstruieren, falls welche geändert wurden
    /// und anschließend das Dictionary zurücksetzen.
    private void m_ReconstructAlphaValues()
    {
        if (m_TransparentObjects.Count == 0) return;
        // Einträge durchlaufen und Alpha-Werte auf Original setzen
        foreach( var kvp in m_TransparentObjects )
        {
            var element = GameObject.Find(kvp.Key);
            var rend = element.transform.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Transparent/Diffuse");
            var tempColor = rend.material.color;
            tempColor.a = kvp.Value;
            rend.material.color = tempColor;
        }
        m_TransparentObjects.Clear();
    }

    /// <summary>
    /// Testen, ob ein Objekt "hinter" dem am nächsten gelegenen
    /// auswählbaren Objekt liegt.
    /// </summary>
    /// <param name="distance">Aktueller Abstand</param>
    /// <param name="minDistance">Abstand des auswählbaren Objekts</param>
    /// <returns>true, falls aktuelles Objekt hinter dem auswählbaren Objekt liegt</returns>
    private static bool m_IsToFarAway(float distance, float minDistance)
    {
        return distance > minDistance;
    }

    /// <summary>
    /// Prädikat für die Entscheidung, ob ein getroffenes Objekt auswählbar ist
    /// </summary>
    /// <param name="hit"></param>
    /// <returns>True, falls das Objekt auswählbar ist</returns>
    private bool m_IsSelectable(RaycastHit hit)
    {
        return hit.collider.tag == SelectTag;
    }
    /// <summary>
    /// Liste der Treffer, die wir nach RaycastAll und Durchsehen noch haben
    /// </summary>
    private List<RaycastHit> m_FinalHits = new List<RaycastHit>();
}
