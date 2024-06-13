//========= 2023 -2024  Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Basisklasse für Command Control Cube
/// </summary>
/// <remarks>
/// Wir initialisieren die drei Schichten und setzen die Sichtbarkeit.
/// </remarks>
public class CCC : MonoBehaviour
{

    /// <summary>
    /// GameObjects für die drei Schichten
    /// </summary>
    protected GameObject m_Layer0, m_Layer1, m_Layer2;

    /// <summary>
    /// Layer zuordnen
    /// </summary>
    private void Awake()
    {
        m_Layer0 = GameObject.Find(("Schicht0"));
        m_Layer1 = GameObject.Find(("Schicht1"));
        m_Layer2 = GameObject.Find(("Schicht2"));
    }
    
    /// <summary>
    /// Einstellen der Sichtbarkeiten aus Default-Werten
    /// </summary>
    protected void m_SetDefaultShows()
    {
        m_Layer0.GetComponent<LayerEventManager>().Show = false;
        m_Layer1.GetComponent<LayerEventManager>().Show = true;
        m_Layer2.GetComponent<LayerEventManager>().Show = false;       
    }

    /// <summary>
    /// Einstellen der No-Shows  aus Default-Werten
    /// </summary>
    protected void m_SetNoShows()
    {
        m_Layer0.GetComponent<LayerEventManager>().Show = false;
        m_Layer1.GetComponent<LayerEventManager>().Show = false;
        m_Layer2.GetComponent<LayerEventManager>().Show = false;            
    }
}
