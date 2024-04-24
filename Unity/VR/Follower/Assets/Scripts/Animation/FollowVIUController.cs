//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;
public class FollowVIUController : FollowTheTarget
{
    /// <summary>
    /// Linker oder rechter  Controller
    /// </summary>
    [Tooltip("Linker oder rechter Controller?")]
    public HandRole Hand = HandRole.RightHand;
    
    /// <summary>
    /// Button auf dem Controller, mit dem wir die Verfolgung toggeln.
    /// </summary>
    /// <remarks>
    /// Default ist der Trigger des Controllers. Der Controller
    /// wird verfolgt, so lange der Button gedrückt wird.
    /// </remarks>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton TheButton = ControllerButton.Trigger;
    
    private void m_Go()
    {
        IsFollowing = true;
    }
    
    private void m_Stop()
    {
        IsFollowing = false;
    }
    
    /// <summary>
    /// Listener für den Controller registrieren
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(Hand,
            TheButton,
            ButtonEventType.Down,
            m_Go);

        ViveInput.AddListenerEx(Hand,
            TheButton,
            ButtonEventType.Up,
            m_Stop);
    }
    
    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(Hand,
            TheButton,
            ButtonEventType.Down,
            m_Go);

        ViveInput.RemoveListenerEx(Hand,
            TheButton,
            ButtonEventType.Up,
            m_Stop);
    }

}
