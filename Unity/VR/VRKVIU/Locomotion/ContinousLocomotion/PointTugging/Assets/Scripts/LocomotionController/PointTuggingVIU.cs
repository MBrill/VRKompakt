//========= 2023 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
///  VIU-Interface für PointTugging.
/// </summary>
public class PointTuggingVIU : PointTugging
{
    /// <summary>
    /// Welcher m_Controller wird verwendet?
    /// </summary>
    /// <remarks>
    ///Default ist die rechte Hand.
    /// </remarks>
    [Tooltip("Welcher Controller (links/rechts) wird verwendet?")]
    public HandRole MainHand = HandRole.RightHand;
    
    /// <summary>
    /// Der verwendete Button, der die Bewegung auslöst,
    /// kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist "Trigger"
    /// </remarks>
    [Tooltip("Welchen Button verwenden wir als Trigger der Fortbewegung?")]
    public ControllerButton moveButton = ControllerButton.Trigger;

    /// <summary>
    /// GameObjects für die m_Controller suchen und zuweisen.
    /// </summary>
    private void Start()
    {
        if (MainHand == HandRole.RightHand)
            m_Controller = GameObject.Find("RightHand");
        if (MainHand == HandRole.LeftHand)
            m_Controller = GameObject.Find("LeftHand");
    }
    
    ///<summary>
    /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
    /// Initialisierungen durchführen, die spezifisch für VR sind.
    /// </summary>
    /// <remarks>
    /// Die Callbacks für Beschleunigung und Abbremsen in  VIU registrieren.
    /// </remarks>
    protected void OnEnable()
    {
        ViveInput.AddListenerEx(MainHand,
            moveButton, 
            ButtonEventType.Down,  
            m_ButtonDown);
        
        // m_Controller loslassen beendet die Fortbewegung
        ViveInput.AddListenerEx(MainHand,
            moveButton, 
            ButtonEventType.Up,
            m_ButtonUp);
    }
    
    /// <summary>
    /// Die Callbacks in der VIU wieder abhängen.
    /// </summary>
    protected void OnDisable()
    {
        ViveInput.RemoveListenerEx(MainHand,
            moveButton,  
            ButtonEventType.Down,  
            m_ButtonDown);
        
        ViveInput.RemoveListenerEx(MainHand, 
            moveButton, 
            ButtonEventType.Up, 
            m_ButtonUp);
    }

    /// <summary>
    /// Wird der Button das erste Mal gedrückt,
    /// dann setzen wir aktivieren wir die Bewegung und
    /// speichern die aktuelle Controller-Position.
    /// </summary>
    private void m_ButtonDown()
    {
        if (!ViveInput.GetPressDown(MainHand, moveButton)) 
            return;
        
        if (!Moving)
        {
            m_GrabbedPosition = m_Controller.transform.position;
            Moving = true;
        }
    }
    
    /// <summary>
    /// Wird der Button losgelassent,
    /// danndeaktivieren wir die Bewegung.
    /// </summary>
    private void m_ButtonUp()
    {
        if (!ViveInput.GetPressUp(MainHand, moveButton)) 
            return;
        
        Moving = false;
    }
}
