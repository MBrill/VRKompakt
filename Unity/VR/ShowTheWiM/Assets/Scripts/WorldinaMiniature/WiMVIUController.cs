//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Controller-Klasse als Interface im Inspektor für die Klassw WiM.
/// </summary>
public class WiMVIUController : WiM
{
    /// <summary>
    /// Verwenden wir die rechte oder die linke Hand=
    /// </summary>
    [Tooltip("Linker oder rechter Controller?")]
    public HandRole TheHand = HandRole.LeftHand;
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist der Trigger des Controllers.
    /// </summary>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton TheButton = ControllerButton.Trigger;
    
    /// <summary>
    ///Die Listerner registrieren.
    /// </summary>
    /// <remarks>
    ///In dieser Version registrieren wir beide Controller.
    /// </remarks>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(TheHand,
            TheButton,
            ButtonEventType.Down,
            ToggleShow);
    }
    
    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(TheHand,
            TheButton,
            ButtonEventType.Down,
            ToggleShow);
    }
}
