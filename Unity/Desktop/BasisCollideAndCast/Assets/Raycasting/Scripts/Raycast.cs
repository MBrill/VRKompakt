using System;
using UnityEngine;


/// <summary>
///   Raycast in Richtung einer der Koordinatenachsen des lokalen
/// Koordinatensystems eines Objekts.
/// </summary>
/// <remarks>
/// Wir geben bei einem Treffer den Namen des getroffenen
/// Objekts aus, und auch die Koordinaten des Schnittpunkts.
/// </remarks>
public class Raycast : RaycastBase
{
    /// <summary>
    /// Dieses Prefab wird an einem berechneten Schnittpunkt dargestellt.
    /// </summary>
    [Tooltip("Prefab für die Visualisierung des Schnittpunkts")]
    public GameObject HitVis;

    /// <summary>
    /// Instanziieren des Prefabs für die Visualisierung des Schnittpunkts.
    /// </summary>
    private void Start()
    {
        HitVis = Instantiate(HitVis,
            new Vector3(0.0f, 0.0f, 0.0f),
                          Quaternion.identity);
        HitVis.SetActive(true);
        HitVis.GetComponent<MeshRenderer>().enabled = false;
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
        RaycastHit hitInfo;
        var ax = transform.TransformDirection(m_axis[(int) Dir]);
        
        if (m_cast && Physics.Raycast(transform.position,
               ax,
               out hitInfo,
               MaxLength)
            )
        {
            if (RayLogs)
            {
                Debug.Log(m_Log[(int) Dir]);
                Debug.Log("Getroffen wurde das Objekt " + hitInfo.collider);
                Debug.Log("Der Abstand zu diesem Objekt ist "
                          + hitInfo.distance
                          + " Meter");
            }
            // Prefab um Schnittpunkt visualisieren
            HitVis.GetComponent<MeshRenderer>().enabled = true;
            HitVis.transform.position = hitInfo.point;
        }
        else
        {
            // HitVis ausblenden
            HitVis.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
