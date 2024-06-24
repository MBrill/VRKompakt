//========= 2023 - 2024 --  Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Implementierung eines einfachen Portals
/// </summary>
public class SimplePortal : Portal
{
    [Header("Eigenschaften von SimplePortal")]
    /// <summary>
    /// Ist der Abstand zwischen dem Objekt mit dieser Komponente
    /// und dem Portal kleiner als diese Variable, dann aktivieren
    /// wir das Portal.
    /// </summary>
    [Range(0.05f, 2.0f)]
    [Tooltip("Abstand, ab dem das Portal aktiviert wird")]
    public float ActivationDistance = 0.5f;
    
    /// <summary>
    /// Das Material der Visualisierung, falls das Portal aktiv ist
    /// </summary>
    public Material m_ActiveMaterial;
    
    /// <summary>
    /// Wir berechnen den Abstand als euklidischen Abstand
    /// zwischen aktueller Positoin und der Position des Portals.
    /// </summary>
    /// <returns>Abstand zum Portal</returns>
    protected override float ComputeDistance()
    {
        var posP2 = new Vector2(Pivot.transform.position.x,
            Pivot.transform.position.z);
        return (m_Portal2Coords-posP2).sqrMagnitude;
    }
    
    /// <summary>
    /// Wir fragen das Material und die Farbe ab und setzen
    /// die Highlight-Farbe aus dem zugewiesenen Material.
    ///
    /// Wir besetzen das Quadrat der Aktivierungsdistanz.
    /// </summary>
    private void Start()
    {
        m_OriginalMaterial = TargetVis.GetComponent<Renderer>().material;

        m_Portal2Coords = new Vector2(
            PortalPosition.transform.position.x,
            PortalPosition.transform.position.z
            );

        var height = PortalVis.transform.localScale.y;
        var scaling =  new Vector3(ActivationDistance, height,
            ActivationDistance);
        PortalVis.transform.localScale = scaling;
        TargetVis.transform.localScale = scaling;

        m_DistanceSquared = ActivationDistance * ActivationDistance;
    }

    private void Update()
    {
        var dist = ComputeDistance();

        if (dist >= m_DistanceSquared)
        {
            if (Active)
            {
                Debug.Log("Zu weit weg und aktiv");
                m_DeactivatePortal();
            }
            else
            {
                if (dist <= 0.5)
                    Debug.Log(dist);
            }
        }
        else
        {
            if (!Active)
            {
                Debug.Log("Nah genug und aktiv");
            }
            else
            {
                Debug.Log("Nah genug und nicht aktiv");
                m_ActivatePortal();
            }
        }
        
        if (dist < m_DistanceSquared && Active)
        {

        }
    }
    
    /// <summary>
    /// Portal aktivieren
    /// </summary>
    private void m_ActivatePortal()
    {
        Active = true;
        TargetVis.GetComponent<Renderer>().material = m_ActiveMaterial;
        Debug.Log("Portal aktiviviert");
    }
    
    /// <summary>
    /// Portal deaktivieren
    /// </summary>
    private void m_DeactivatePortal()
    {
        Active = false;
        TargetVis.GetComponent<Renderer>().material = m_OriginalMaterial;
        Debug.Log("Portal deaktiviviert");
    }

    /// <summary>
    /// Die Koordinaten des Portals in der xz-Ebene
    /// </summary>
    private Vector2 m_Portal2Coords;
    
    /// <summary>
    /// Variable für das Quadrat der Aktivierungsdistanz, um die
    /// Wurzel beim berechnen des Abstands zu vermeiden
    /// </summary>
    private float m_DistanceSquared;

    /// <summary>
    /// Variable, auf der wir das originale Material der
    /// Portal-Visualisierung speichern, um es bei Bedarf
    /// zu rekonstruieren
    /// </summary>
    /// <remarks>
    /// Wir rekonstruieren die Originalfarbe mit Hilfe dieser Variable.
    /// </remarks>
    private Material m_OriginalMaterial;
}
