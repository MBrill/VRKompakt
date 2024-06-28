//========= 2023 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
///  VIU-Interface für PointTugging.
/// </summary>
/// <remarks>
/// Welcher Controller den Start- und welcher den Endpunkt
/// definiert wird dadurch entschieden, auf welchem wir
/// den einstellbaren Button gedrückt halten.
///
/// Dies triggert auch  die Fortbewegung.
/// </remarks>
public class PointTuggingVIU : PointTugging
{
    [Header("Vive Input Utility")]
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
    /// GameObjects für die Controller suchen und zuweisen.
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
    /// Initialisierungen durchführen, die spezifisch für VR sind.
    /// </summary>
    /// <remarks>
    /// Die Callbacks für Beschleunigung und Abbremsen in  VIU registrieren.
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
    /// Die Callbacks in der VIU wieder abhängen.
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
    /// gedrückt ist.
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
    /// GameObject im Rig tür den rechten Controller
    /// </summary>
    private GameObject RightHand;
    
    /// <summary>
    /// GameObject im Rig tür den linken Controller
    /// </summary>
    private GameObject LeftHand;
}
