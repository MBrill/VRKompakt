//========= 2023 - -2024 Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.ColliderEvent;

/// <summary>
// /Interface und Implementierung des Command Control Cube
/// auf Basis von Vive Input Utility.
/// </summary>
/// <remarks>
/// Die von dieser Klaasse abgeleiteten Versionen verwenden entweder
/// das Input System und Unity XR oder Vive Input Utility.
/// </remarks>
public class ViuCCC : MonoBehaviour
{ 
    /// <summary>
    /// GameObject, das die Prefabs enthält
    /// </summary>
     [Tooltip("Objekt mit CCC-Prefabs")] 
    public GameObject TheCCC;
    
    
    /// <summary>
    /// Linker oder rechter Controller?
    /// </summary>
    public HandRole CCCHand = HandRole.LeftHand;
    
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist der Trigger des Controllers.
    ///  </remarks>
     [Tooltip("Welcher Button auf dem Controller wird für das Einblenden eingesetzt?")]
    public ControllerButton ActivationButton = ControllerButton.Trigger;
    
    /// <summary>
    /// Button für das Press-Event
    /// </summary>
    [Tooltip("Welcher Controller-Button wird für das Auslösen der Events verwendet?")]
    public ColliderButtonEventData.InputButton SelectButton = 
        ColliderButtonEventData.InputButton.PadOrStick;

    /// <summary>
    /// CCC anzeigen oder nicht?
    /// </summary>
    [Tooltip("CCC beim Start anzeigen?")]
    public bool Show = false;
    
    /// <summary>
    /// GameObject des Controllers, den wir verwenden möchten.
    /// </summary>
    private GameObject m_Controller;
    
    /// <summary>
    /// GameObjekct des Colliders des Controllers, den wir nicht verwenden.
    /// </summary>
    /// <remarks>
    /// Wir benögiten dieses Objekt, da wir den Collider dieses Controllers
    /// deaktivieren.
    /// </remarks>
    private GameObject m_ControllerCollider;
    
    /// <summary>
    /// CCC-Objekt finden und alles initialisieren
    /// </summary>
    private void Start()
    {
        Logger.Debug(">>> ViuCCC.Start");
        FindTheCCC();
        if (!TheCCC)
        {
            Logger.Fatal("CCC Objekt wurde nicht gefunden!");
            return;
        }
        
        if (CCCHand == HandRole.LeftHand) 
        {
            Logger.Debug("Verwendeter Controller: Links!");
            m_Controller = GameObject.Find("LeftHand");
            if (!m_Controller)
            {
                Logger.Fatal("Linker Controller nicht gefunden!");
                return;            
            }
            m_ControllerCollider = GameObject.Find("Right");
        }
        else
        {
            Logger.Debug("Verwendeter Controller: Rechts!");
            m_Controller = GameObject.Find("RightHand");
            if (!m_Controller)
            {
                Logger.Fatal("Rechter Controller nicht gefunden!");
                return;            
            }
            m_ControllerCollider = GameObject.Find("Left");       
        }
        
        Logger.Debug("<<< ViuCCC.Start");
        TheCCC.transform.SetPositionAndRotation(
            m_Controller.transform.position,
            m_Controller.transform.rotation);
        
        TheCCC.SetActive(Show);
    }
    
    /// <summary>
    /// Registrieren der Listener für den gewünschten Button
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(CCCHand,
            ActivationButton,
            ButtonEventType.Up,
            ToggleCCC);
    }
    
    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDisable()
    {
        ViveInput.RemoveListenerEx(CCCHand,
            ActivationButton,
            ButtonEventType.Up,
            ToggleCCC);
    }
    
    /// <summary>
    ///Callback für das Aktivieren und Deaktivieren des CCC Prefabs
    /// </summary>
    private void ToggleCCC()
    {
        Logger.Debug(">>> ToggleCCC");
        Show = !Show;

        if (Show)
        {   
            TheCCC.SetActive(Show);
            TheCCC.transform.SetPositionAndRotation(
                m_Controller.transform.position,
                m_Controller.transform.rotation);
            
            m_ControllerCollider.SetActive(false);
        }
        else
        {
            TheCCC.SetActive(Show);
            Logger.Debug("CCC Objekt wird ausgeblendet!");
            m_ControllerCollider.SetActive(true);
        }
        Logger.Debug("<<< ToggleCCC");
    }
    
    /// <summary>
    /// Verbindung zu GameObject  in der Szene herstellen,
    /// das die CCC-Prefabs enthält
    /// </summary>
    protected void FindTheCCC()
    {
        TheCCC = GameObject.Find(TheCCC.name);
        if (!TheCCC)
        {
            Logger.Fatal("CCC-Objekt wurde nicht gefunden!!");
            return;
        }
    }
    
    /// <summary>
    /// Instanz eines Log4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(ViuCCC));
}
