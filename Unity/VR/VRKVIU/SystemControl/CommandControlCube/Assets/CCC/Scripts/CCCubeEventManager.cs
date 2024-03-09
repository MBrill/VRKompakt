//========= 2023 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.ColliderEvent;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handling der Events HoverEnter, HoverExit,
/// PressEnter und PressExit für einen einzelnen Cube in CCC.
/// </summary>
/// <remarks>
/// Voraussetzung: Das Prefab ViveColliders ist in der Szene enthalten.
/// Der Contoller-Button der für die Press-Events eingesetzt
/// wird wird im übergeordneten GameObject CCCalt gesetzt
/// und hier abgefragt.
/// </remarks>
public class CCCubeEventManager : MonoBehaviour, 
    IColliderEventHoverEnterHandler,
    IColliderEventHoverExitHandler,
    IColliderEventPressEnterHandler,
    IColliderEventPressExitHandler
{
    /// <summary>
    /// Event, der im Inspektor dem Würfel zugeordnet werden kann.
    /// </summary>
    /// <remarks>
    /// Event wird aufgerufen, wenn wir in PressExit gehen.
    /// </remarks>
    [Tooltip("Event für diesen Button")]
    public UnityEvent MyEvent = new UnityEvent();
    
    /// <summary>
   ///  Funktion, die bei HoverEnter aufgerufen wird
   /// </summary>
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        Logger.Debug(">>> CCCubeEventManager.OnColliderEventHoverEnter");
        Logger.Debug("HoverEnter Event");
        Logger.Debug("<<< CCCubeEventManager.OnColliderEventHoverEnter");
    }

   /// <summary>
   ///  Funktion, die bei HoverExit aufgerufen wird
   /// </summary> 
   public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
    {
        Logger.Debug(">>> CCCubeEventManager.OnColliderEventHoverExit");
        Logger.Debug("HoverExit Event");
        Logger.Debug("<<< CCCubeEventManager.OnColliderEventHoverExit");
    }
    
   /// <summary>
   ///  Funktion, die bei PressEnter aufgerufen wird
   /// </summary> 
   public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        Logger.Debug(">>> CCCubeEventManager.OnColliderEventPressEnter");
        Logger.Debug("PressEnter Event");
        Logger.Debug("<<< CCCubeEventManager.OnColliderEventPressEnter");
    }

   /// <summary>
   ///  Funktion, die bei PressExit aufgerufen wird
   /// </summary> 
   public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {

        Logger.Debug(">>> CCCubeEventManager.OnColliderEventPressExit");
        object[] args = {gameObject.name, 
            "Event ausgelöst!",            
        };
        
        Logger.InfoFormat("{0}; {1};", args);
        Logger.Debug("<<< CCCubeEventManager.OnColliderEventPressExit");
        MyEvent.Invoke();
        m_LogEvent.Invoke();
    }

   /// <summary>
   /// Initialisieren
   /// </summary>
   void Awake()
   {
       // Callbacks registrieren
       MyEvent.AddListener(m_Logging);
       m_LogEvent.AddListener(m_Logging);
       
       // Im übergeordneten GameObject CCC nachsehen, welchen
       // Button wir eingestellt haben.
       if (transform.parent == null) return;
       var layer = GameObject.Find(transform.parent.name);
       var rootObject = GameObject.Find(layer.transform.parent.name);
       m_triggerButton = rootObject.GetComponent<CCC>().selectButton;
   }

   /// <summary>
   /// Name des GameObjects, das durch dieses Prefab definiert wird.
   /// </summary>
   private string m_goName;
   
   /// <summary>
   /// Button, der in CCC eingestellt ist.
   /// </summary>
   private ColliderButtonEventData.InputButton m_triggerButton = 
       ColliderButtonEventData.InputButton.Trigger;
   
   /// <summary>
   /// Unity-Event mit einem Callback, der mit Hilfe von Log4net
   /// die Events protokolliert. Ob die Protokollierung auf der Konsole
   /// oder eine Datei durchgeführt wird entscheiden wir in einer
   /// Konfigurationsdatei.
   /// </summary>
   protected UnityEvent m_LogEvent = new UnityEvent();
   
   protected virtual  void m_Logging()
   {
       Logger.Debug(">>> CCCubeEventManager.m_Logging");
       Logger.Debug("Event ausgelöst!");
       Logger.Debug("<<< CCCubeEventManager.m_Logging");
   }
   
   /// <summary>
   /// Instanz eines Log4Net Loggers
   /// </summary>
   private static readonly log4net.ILog Logger 
       = log4net.LogManager.GetLogger(typeof(CCCubeEventManager));
}
