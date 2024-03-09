//========= 2020 -  2023 - Copyright Manfred Brill. All rights reserved. ===========

using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
///  VIU-Interface für Walk.
/// </summary>
public class WalkVIUConroller : Walk
{
    [Header("Vive Input Utility")]
    /// <summary>
    /// Welchen Controller verwenden wir für das Triggern der Fortbewegung?
    /// </summary>
    /// <remarks>
    /// Als Default verwenden wir den Controller in der rechten Hand,
    /// also "RightHand" im "ViveCameraRig".
    /// </remarks>
    [Tooltip("Rechter oder linker Controller für den Trigger?")]
    public HandRole moveHand = HandRole.RightHand;

    /// <summary>
    /// Der verwendete Button, der die Bewegung auslöst, kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist "Trigger"
    /// </remarks>
    [Tooltip("Welchen Button verwenden wir als Trigger der Fortbewegung?")]
    public ControllerButton moveButton = ControllerButton.Trigger;
    
    /// <summary>
    /// Button auf dem Controller für das Abbremsen der Fortbewegung.
    /// </summary>
    /// <remarks>
    /// Default ist "Pad"
    /// </remarks>
    [Tooltip("Button für das Verkleinern der Bahngeschwindigkeit")] 
    public ControllerButton DecButton = ControllerButton.Pad;

    /// <summary>
    /// Button auf dem Controller für das Beschleunigen der Fortbewegung.
    /// </summary>
    /// <remarks>
    /// Default ist "Grip"
    /// </remarks>
    [Tooltip("Button für das Vergrößern der Bahngeschwindigkeit")]
    public ControllerButton AccButton = ControllerButton.Grip;
    
    ///<summary>
    /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
    /// Initialisierungen durchführen, die spezifisch für VR sind.
    /// </summary>
    /// <remarks>
    /// Die Callbacks für Beschleunigung und Abbremsen in der VIUregistrieren.
    /// </remarks>
    protected void OnEnable()
    {
        ViveInput.AddListenerEx(moveHand, DecButton, 
            ButtonEventType.Down,  
            m_Velocity.Decrease);
        ViveInput.AddListenerEx(moveHand, AccButton, 
            ButtonEventType.Down,
            m_Velocity.Increase);
    }
    
    /// <summary>
    /// Die Callbacks in der VIU wieder abhängen.
    /// </summary>
    protected void OnDisable()
    {
        ViveInput.RemoveListenerEx(moveHand, DecButton, 
            ButtonEventType.Down,  
            m_Velocity.Decrease);
        ViveInput.RemoveListenerEx(moveHand, AccButton, 
            ButtonEventType.Down, 
            m_Velocity.Increase);
    }
    
    /// <summary>
    ///Die von JoystickLocomotion abgeleiteten Klassen entscheiden wie die Bewegung
    /// getriggert wird. Mit einem gehaltenen Button, zwischen zwei Button-
    /// Clicks oder mit Hilfe anderer Dinge wie Bewegungen und Gesten.
    /// </summary>
    /// <remarks>
    /// Als Default-Behaviour implementieren wir das bisher verwendete
    /// Verhalten - die Bewegung findet so lange statt, wie ein ebenfalls
    /// in dieser Klasse deklariertes Trigger-Device und ein Button darauf
    /// gedrückt ist.
    /// </remarks>
    protected override void Trigger()
    {
        Moving = ViveInput.GetPress(moveHand, moveButton);
    }
}
