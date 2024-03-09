//========= 2020 -  2023 - Copyright Manfred Brill. All rights reserved. ===========

using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
///  VIU-Interface f�rFreezeBackup in RDW
/// </summary>
public class FreezeTurnVIUController : FreezeTurn
{
   [Header("Vive Input Utility")]
    /// <summary>
    /// Welchen Controller verwenden wir f�r das Triggern der Fortbewegung?
    /// </summary>
    /// <remarks>
    /// Als Default verwenden wir den Controller in der rechten Hand,
    /// also "RightHand" im "ViveCameraRig".
    /// </remarks>
    [Tooltip("Rechter oder linker Controller f�r die Aktivierung?")]
    public HandRole Hand = HandRole.RightHand;

    /// <summary>
    /// Der verwendete Button, der die Bewegung ausl�st, kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist "Trigger"
    /// </remarks>
    [Tooltip("Welchen Button verwenden wir als Trigger ?")]
    public ControllerButton TurnButton = ControllerButton.Trigger;

    ///<summary>
    /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
    /// Initialisierungen durchf�hren, die spezifisch f�r VR sind.
    /// </summary>
    /// <remarks>
    /// Die Callbacks f�r Beschleunigung und Abbremsen in der VIUregistrieren.
    /// </remarks>
    protected void OnEnable()
    {
        ViveInput.AddListenerEx(Hand, TurnButton, 
            ButtonEventType.Down,  
            m_Trigger);
    }
    
    /// <summary>
    /// Die Callbacks in der VIU wieder abh�ngen.
    /// </summary>
    protected void OnDisable()
    {
        ViveInput.RemoveListenerEx(Hand, TurnButton, 
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
    /// gedr�ckt ist.
    /// </remarks>
    protected void m_Trigger()
    {
        //if (ViveInput.GetPressUp(Hand, TurnButton))
            
            if (!Active)
            {
                Debug.Log("Freeze");
                Active = true;
                Freeze();
            }
            else
            {
                Debug.Log("Unfreeze");
                Active = false;
            }
    }
}
