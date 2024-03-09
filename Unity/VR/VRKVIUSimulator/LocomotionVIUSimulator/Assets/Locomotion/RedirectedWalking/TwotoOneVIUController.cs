//========= 2020 -  2023 - Copyright Manfred Brill. All rights reserved. ===========

using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
///  VIU-Interface für 2:1 Controller  in RDW
/// </summary>
public class TwotoOneVIUController : TwoToOneReset
{
    [Header("Vive Input Utility")]
    /// <summary>
    /// Welchen Controller verwenden wir für das Triggern der Fortbewegung?
    /// </summary>
    /// <remarks>
    /// Als Default verwenden wir den Controller in der rechten Hand,
    /// also "RightHand" im "ViveCameraRig".
    /// </remarks>
    [Tooltip("Rechter oder linker Controller für die Aktivierung?")]
    public HandRole Hand = HandRole.RightHand;

    /// <summary>
    /// Der verwendete Button, der die Bewegung auslöst, kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist "Trigger"
    /// </remarks>
    [Tooltip("Welchen Button verwenden wir als Trigger ?")]
    public ControllerButton ResetButton = ControllerButton.Trigger;

    ///<summary>
    /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
    /// Initialisierungen durchführen, die spezifisch für VR sind.
    /// </summary>
    /// <remarks>
    /// Die Callbacks für Beschleunigung und Abbremsen in der VIUregistrieren.
    /// </remarks>
    protected void OnEnable()
    {
        ViveInput.AddListenerEx(Hand, ResetButton, 
            ButtonEventType.Down,  
            m_Trigger);
    }
    
    /// <summary>
    /// Die Callbacks in der VIU wieder abhängen.
    /// </summary>
    protected void OnDisable()
    {
        ViveInput.RemoveListenerEx(Hand, ResetButton, 
            ButtonEventType.Down,  
            m_Trigger);
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
    protected void m_Trigger()
    {
        //if (ViveInput.GetPressUp(Hand, TurnButton))
            
            if (!Active)
            {
                Debug.Log("2:1");
                Active = true;
            }
            else
            {
                Debug.Log("2:1");
                Active = false;
            }
    }
}
