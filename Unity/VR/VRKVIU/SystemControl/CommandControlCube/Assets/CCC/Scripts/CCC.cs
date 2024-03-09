//========= 2023 -2024  Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;

/// <summary>
/// Controller für das Prefab Command Control Cube.
/// </summary>
public class CCC : MonoBehaviour
{
    /// <summary>
    /// Button für das Press-Event
    /// </summary>
    [Tooltip("Welcher Controller-Button soll für das Auslösen der Events verwendet werden?")]
    public ColliderButtonEventData.InputButton selectButton = 
        ColliderButtonEventData.InputButton.Trigger;

    /// <summary>
    /// GameObjects für die drei Schichten
    /// </summary>
    protected GameObject m_Layer0, m_Layer1, m_Layer2;

    void Awake()
    {
        m_Layer0 = GameObject.Find(("Schicht0"));
        m_Layer1 = GameObject.Find(("Schicht1"));
        m_Layer2 = GameObject.Find(("Schicht2"));
    }

    protected void m_SetDefaultShows()
    {
        m_Layer0.GetComponent<LayerEventManager>().Show = false;
        m_Layer1.GetComponent<LayerEventManager>().Show = true;
        m_Layer2.GetComponent<LayerEventManager>().Show = false;       
    }

    protected void m_SetNoShows()
    {
        m_Layer0.GetComponent<LayerEventManager>().Show = false;
        m_Layer1.GetComponent<LayerEventManager>().Show = false;
        m_Layer2.GetComponent<LayerEventManager>().Show = false;            
    }
    
    /// <summary>
    /// Instanz eines Log4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(CCC));
}
