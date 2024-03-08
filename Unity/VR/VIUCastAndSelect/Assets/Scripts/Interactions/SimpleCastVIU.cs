//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// SimpleCast mit Input Actions für das Auslösen
/// </summary>
public class SimpleCastVIU : SimpleCast
{
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
    /// Feststellen, an welchem Controller das Script angehängt ist.
    /// </summary>
    protected void Awake()
    {
        if (gameObject.name == "LeftHand")
        {
            m_CastHand = HandRole.LeftHand;
        }
        else
        {
            m_CastHand = HandRole.RightHand;
        }
    }
    
    /// <summary>
    /// Welcher Controller wird verwendet?
    /// </summary>
    /// <remarks>
    ///Default ist die rechte Hand.
    /// </remarks>
    private HandRole m_CastHand = HandRole.RightHand;
    
    /// <summary>
    /// Registrieren der Listerner für den gewünschten Button
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(m_CastHand,
            TheButton,
            ButtonEventType.Down,
            m_castTheRay);

        ViveInput.AddListenerEx(m_CastHand,
            TheButton,
            ButtonEventType.Up,
            m_castTheRay);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDisable()
    {
        ViveInput.RemoveListenerEx(m_CastHand,
            TheButton,
            ButtonEventType.Down,
            m_castTheRay);

        ViveInput.RemoveListenerEx(m_CastHand,
            TheButton,
            ButtonEventType.Up,
            m_castTheRay);
        
    }
}
