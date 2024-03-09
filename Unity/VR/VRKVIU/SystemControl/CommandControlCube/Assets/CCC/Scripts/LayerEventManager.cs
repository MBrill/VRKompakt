//========= 2023 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.ColliderEvent;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Hover-Events aus VIU für das Ein- und Ausblenden der Schichten in
/// einer CCC-Komponente.
/// </summary>
public class LayerEventManager : MonoBehaviour,
    IColliderEventHoverEnterHandler,
    IColliderEventHoverExitHandler
{
    /// <summary>
    /// Soll die Schicht angezeigt werden oder nicht?
    /// </summary>
    [Tooltip("Anzeige der Schicht")]
    public bool Show = false;
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        Logger.Debug(">>> LayerEventManager.OnColliderEventHoverEnter");
        object[] args = {m_goName, 
            "HoverEnter",            
        };
        Logger.DebugFormat("{0}; {1};", args);
        // Wenn wir in die Schicht eintreten  wird die Schicht sichtbar
        m_Show.Invoke();
        Logger.Debug("<<< LayerEventManager.OnColliderEventHoverEnter");
    }

    public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
    {
        Logger.Debug(">>> LayerEventManager.OnColliderEventHoverExit");
        Logger.Debug("HoverEnter Event");
        // Wenn wir die Schicht verlassen  wird die Schicht unsichtbar
        m_NoShow.Invoke();
        Logger.Debug("<<< LayerEventManager.OnColliderEventHoverExit");
    }

    /// <summary>
    /// Initialisieren
    /// </summary>
    void Awake()
    {
        m_goName = gameObject.name;
        m_DetermineCubeRenderers();
        
        m_Show.AddListener(ShowTheLayer);
        m_NoShow.AddListener(NoShowTheLayer);
    }

    void Start()
    {
        if (Show)
            ShowTheLayer();
        else
            NoShowTheLayer();
    }
    
    void Update()
    {
        if (Show)
            ShowTheLayer();
        else
            NoShowTheLayer();
    }

    /// <summary>
    /// Unity-Event mit einem Callback,der in der Lage ist
    /// einen der Layer einzublenden.
    /// </summary>
    private UnityEvent m_Show = new UnityEvent();
    
    /// <summary>
    /// Unity-Event mit einem Callback,der in der Lage ist
    /// einen der Layer auublenden.
    /// </summary>
    private UnityEvent m_NoShow = new UnityEvent();
    
    /// <summary>
    /// Callback für das Einblendene einer Schicht.
    /// </summary>
    public void ShowTheLayer()
    {
        Logger.Debug(">>> ShowTheLayer");
        object[] args = {"Show the Layer",            
            m_goName
        };
        Logger.DebugFormat("{0}; {1};", args);
        Show = true;
        for (var i = 0; i < cubeRenderers.Length; i++)
            cubeRenderers[i].enabled = true;
        Logger.Debug("<<< ShowTheLayer");
    }
    
    /// <summary>
    /// Callback für das Ausblenden einer Schicht.
    /// </summary>
    public  void NoShowTheLayer()
    {
        Logger.Debug(">>> NoShowTheLayer");
        object[] args = {"No Show the Layer",           
            m_goName
        };
        Logger.DebugFormat("{0}; {1};", args);
        Show = false;
        for (var i = 0; i < cubeRenderers.Length; i++)
            cubeRenderers[i].enabled = false;
        Logger.Debug("<<< ShowTheLayer");
    }
    
    /// <summary>
    /// Wir bestimmen den Index der Schicht aus dem Namen
    /// </summary>
    /// <returns></returns>
    private uint m_DetermineLayerIndex()
    {
        uint i=99;
        switch (m_goName)
        {
            case "Schicht0": 
                    i=0;
                break;
            case "Schicht1": 
                i = 1;
                break;
            case "Schicht2": 
                i=2;
                break; ;
        }

        return i;
    }
    /// <summary>
    /// Name des GameObjects, das durch dieses Prefab definiert wird.
    /// </summary>
    private string m_goName;

    /// <summary>
    /// Index der Schicht: 0, 1 oder 2.
    /// </summary>
    private uint m_LayerIndex;

    private void m_DetermineCubeRenderers()
    {
        switch (m_goName)
        {
            case "Schicht0":
                cubeRenderers[0] = m_getRenderer("CC10"); 
                cubeRenderers[1] = m_getRenderer("CC20");
                cubeRenderers[2] = m_getRenderer("CC30");
                cubeRenderers[3] = m_getRenderer("CC40");
                cubeRenderers[4] = m_getRenderer("CC50");
                cubeRenderers[5] = m_getRenderer("CC60");
                cubeRenderers[6] = m_getRenderer("CC70");
                cubeRenderers[7] = m_getRenderer("CC80");
                cubeRenderers[8] = m_getRenderer("CC90");
                break;
            case "Schicht1":
                cubeRenderers[0] = m_getRenderer("CC11"); 
                cubeRenderers[1] = m_getRenderer("CC21");
                cubeRenderers[2] = m_getRenderer("CC31");
                cubeRenderers[3] = m_getRenderer("CC41");
                cubeRenderers[4] = m_getRenderer("CC51");
                cubeRenderers[5] = m_getRenderer("CC61");
                cubeRenderers[6] = m_getRenderer("CC71");
                cubeRenderers[7] = m_getRenderer("CC81");
                // Mittlere Schicht hat keinen innneren Cube
                cubeRenderers[8] = m_getRenderer("CC81");
                break;
            case "Schicht2": 
                cubeRenderers[0] = m_getRenderer("CC12"); 
                cubeRenderers[1] = m_getRenderer("CC22");
                cubeRenderers[2] = m_getRenderer("CC32");
                cubeRenderers[3] = m_getRenderer("CC42");
                cubeRenderers[4] = m_getRenderer("CC52");
                cubeRenderers[5] = m_getRenderer("CC62");
                cubeRenderers[6] = m_getRenderer("CC72");
                cubeRenderers[7] = m_getRenderer("CC82");
                cubeRenderers[8] = m_getRenderer("CC92");
                break; ;          
        }
    }
    
    private Renderer[] cubeRenderers = new Renderer[9];

    private Renderer m_getRenderer(string name)
    {
        return GameObject.Find(name).GetComponent<Renderer>() as Renderer;
    }
    /// <summary>
    /// Instanz eines Log4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(LayerEventManager));
}
