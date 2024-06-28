//========= 2023 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
///  VIU-Interface f�r PointTugging.
/// </summary>
/// <remarks>
/// Welcher Controller den Start- und welcher den Endpunkt
/// definiert wird dadurch entschieden, auf welchem wir
/// den einstellbaren Button gedr�ckt halten.
///
/// Dies triggert auch  die Fortbewegung.
/// </remarks>
public class PointTuggingVIU : PointTugging
{
    [Header("Vive Input Utility")]
    /// <summary>
    /// Der verwendete Button, der die Bewegung ausl�st,
    /// kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist "Trigger"
    /// </remarks>
    [Tooltip("Welchen Button verwenden wir als Trigger der Fortbewegung?")]
    public ControllerButton moveButton = ControllerButton.Trigger;

    /// <summary>
    /// GameObjects f�r die Controller suchen und zuweisen.
    /// </summary>
    private void Start()
    {
        RightHand = GameObject.Find("Righthand");
        if (!RightHand)
            Debug.LogError("Rechter Controller nicht gefunden!");
        LeftHand = GameObject.Find("Leftthand");
        if (!LeftHand)
            Debug.LogError("Linker Controller nicht gefunden!");
    }
    
    ///<summary>
    /// Richtung, Geschwindigkeit aus der Basisklasse initialisieren und weitere
    /// Initialisierungen durchf�hren, die spezifisch f�r VR sind.
    /// </summary>
    /// <remarks>
    /// Die Callbacks f�r Beschleunigung und Abbremsen in  VIU registrieren.
    /// </remarks>
    protected void OnEnable()
    {
        ViveInput.AddListenerEx(HandRole.RightHand,
            moveButton, 
            ButtonEventType.Down,  
            m_Velocity.Decrease);
        ViveInput.AddListenerEx(HandRole.LeftHand, 
            moveButton, 
            ButtonEventType.Down,
            m_Velocity.Increase);
    }
    
    /// <summary>
    /// Die Callbacks in der VIU wieder abh�ngen.
    /// </summary>
    protected void OnDisable()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand,
            moveButton,  
            ButtonEventType.Down,  
            m_Velocity.Decrease);
        ViveInput.RemoveListenerEx(HandRole.LeftHand, 
            moveButton, 
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
    /// gedr�ckt ist.
    /// </remarks>
    protected override void Trigger()
    {
        if (ViveInput.GetPress(HandRole.LeftHand, moveButton))
        {
            Moving = true;
            StartObject = RightHand;
            EndObject = LeftHand;
        }
        if (ViveInput.GetPress(HandRole.RightHand, moveButton))
        {
            Moving = true;
            StartObject = LeftHand;
            EndObject = RightHand;
        }
    }

    /// <summary>
    /// GameObject im Rig t�r den rechten Controller
    /// </summary>
    private GameObject RightHand;
    
    /// <summary>
    /// GameObject im Rig t�r den linken Controller
    /// </summary>
    private GameObject LeftHand;
}
