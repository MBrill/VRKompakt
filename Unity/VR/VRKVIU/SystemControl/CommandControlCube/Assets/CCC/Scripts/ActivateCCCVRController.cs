//========= 2023 - 2024 Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Aktivierung der CCC-Komponente mit Eingaben in
/// Vive Input Utility.
/// </summary>
/// <remarks>
/// Je nach ausgewählter Hand aktivieren wir den Controller über die HandRole.
/// den Collider der anderen Hand deaktivieren wir.
/// </remarks>
public enum Hand : ushort
{
    Left = 0,
    Right = 1
}
public class ActivateCCCVRController : ActivateCCC
{
    /// <summary>
    /// Linker oder rechter Controller?
    /// </summary>
    [Tooltip("Linker oder rechter controller?")]
    public Hand Hand = Hand.Left;
    
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist der Trigger des Controllers.
    ///  </remarks>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton TheButton = ControllerButton.Grip;

    /// <summary>
    /// Welcher Controller wird verwendet?
    /// </summary>
    /// <remarks>
    ///Default ist die rechte Hand.
    /// </remarks>
    [Tooltip("Welcher Controller (links/rechts) soll für das Highlight verwendet werden?")]
    private HandRole m_MainHand = HandRole.LeftHand;
    
    /// <summary>
    /// GameObject des Controllers, den wir verwenden möchten.
    /// </summary>
    private GameObject m_Controller;

    /// <summary>
    /// GameObjekct des Colliders des Controllers, den wir nicht verwenden.
    /// </summary>
    private GameObject m_ControllerCollider;
    
    private void Awake()
    {
        FindTheCCC();
        if (Hand == Hand.Left)
        {
            m_Controller = GameObject.Find("LeftHand");
            m_ControllerCollider = GameObject.Find("Right");
            m_MainHand = HandRole.LeftHand;
        }
        else
        {
            m_Controller = GameObject.Find("RightHand");
            m_ControllerCollider = GameObject.Find("Left");       
            m_MainHand = HandRole.RightHand;
        }
    }
    /// <summary>
    /// Registrieren der Listener für den gewünschten Button
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(m_MainHand,
            TheButton,
            ButtonEventType.Up,
            ToggleCCC);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDisable()
    {
        ViveInput.RemoveListenerEx(m_MainHand,
            TheButton,
            ButtonEventType.Up,
            ToggleCCC);
    }
    
    
    /// <summary>
    /// Farbwechsel, wird in den Listernern registriert
    /// </summary>
    private void ToggleCCC()
    {
        Show = !Show;
        if (Show)
        {
            TheCCC.SetActive(true);
            TheCCC.transform.SetPositionAndRotation(
                m_Controller.transform.position,
                m_Controller.transform.rotation);
            m_ControllerCollider.SetActive(false);
        }
        else
        {
            TheCCC.SetActive(false);
            TheCCC.transform.parent = null;
            m_ControllerCollider.SetActive(true);
        }
    }
}
