//========= 2023 - 2024 --  Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Ausl�sen eines Portals mit Hilte eines Controller Buttons
///VIU-Version
/// </summary>
public class TriggerPortal : SimplePortal
{
    /// <summary>
    /// Welcher Controller wird verwendet?
    /// </summary>
    /// <remarks>
    ///Default ist die rechte Hand.
    /// </remarks>
    [Tooltip("Welcher Controller (links/rechts) soll f�r das Highlight verwendet werden?")]
    public HandRole MainHand = HandRole.RightHand;
    
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist der Trigger des Controllers.
    ///  </remarks>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton TheButton = ControllerButton.Trigger;

    /// <summary>
    /// Registrieren der Listener f�r den gew�nschten Button
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(MainHand,
                                TheButton,
                                ButtonEventType.Down,
                                m_TriggerPortal);

        ViveInput.AddListenerEx(MainHand,
                                TheButton,
                                ButtonEventType.Up,
                                m_TriggerPortal);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDisable()
    {
        ViveInput.RemoveListenerEx(MainHand,
                                   TheButton,
                                   ButtonEventType.Down,
                                   m_TriggerPortal);

        ViveInput.RemoveListenerEx(MainHand,
                                   TheButton,
                                   ButtonEventType.Up,
                                   m_TriggerPortal);
        
    }
    
    /// <summary>
    /// Triggern des Positionswechels, der in der Basisklasse eingestellt ist
    /// </summary>
    private void m_TriggerPortal()
    { 
         Active = !Active;
         transform.position = TargetPosition.position;
    }
}