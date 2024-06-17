//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
///   XRaycast - Objekte, die ein gesuchtes Objekt verdecken und dabei
/// vom Strahl getroffen werden erhalten eine Transparenz, so lange der
/// Strahl angezeigt wird.
/// </summary>
/// <remarks>
/// Diese Klasse ist analog zu RaycastWithLine implementiert.
/// Wir verwenden statt Raycast die Unity-Funktion RaycastAll.
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
    /// In ndiesem Dictionary speichern wi die Originalwerte
    /// von Alpha für die aktuell transparent dargestellten Objekte
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
    ///  Raycasting wird in FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
        // Alpha-Werte zurücksetzen
        m_ReconstructAlphaValues();
        
        if (m_cast)
        {
            lr.enabled = true;
            // Prefab für das Ende des Strahls  visualisieren
            var points = new Vector3[2];
            points[0] = transform.position;
            // Zweiter Punkt ist abhängig davon, ob wir einen Schnittpunkt
            // erhalten oder nicht.

            var hits = Physics.RaycastAll(transform.position,
                transform.forward,
                MaxLength);
            
            Debug.Log("Es gibt " + hits.Length + " getroffene Objekte");
            foreach (var hit in hits)
            {
                Debug.Log("Objekt: " + hit.collider.name + "Distanz" + hit.distance);
                var rend = hit.transform.GetComponent<Renderer>();

                // Alle getroffenen Objekte, die nicht auswählbar sind
                // mit Transparenz darstellen.
                if (rend && hit.collider.tag != SelectTag)
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
            if (hits.Length > 0)
            {
                // Auswählbares Objekt vorher aus Liste filtern ...
                
                HitVis.transform.position = hits[0].point;
                HitVis.GetComponent<MeshRenderer>().enabled = true;
                
                if (RayLogs)
                {
                    Debug.Log("Getroffen wurde das Objekt " + hits[0].collider);
                    Debug.Log("Der Abstand zu diesem Objekt ist "
                              + hits[0].distance
                              + " Meter");
                }
            }
            else
            {
                HitVis.transform.position = transform.position + MaxLength * transform.forward;
                HitVis.GetComponent<MeshRenderer>().enabled = false;
            }
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
    /// Alle Alpha-Werte in Dictionary rekonstruieren, falls welche geändert wurden
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
}
