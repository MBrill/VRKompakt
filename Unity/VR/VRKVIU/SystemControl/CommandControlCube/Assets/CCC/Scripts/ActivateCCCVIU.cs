//========= 2023 - 2024 Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Aktivierung der CCC-Komponente mit Eingaben in
/// Vive Input Utility.
/// </summary>
/// <remarks>
/// Je nach ausgew�hlter CCCHand aktivieren wir den Controller
/// �ber die HandRole.
/// den Collider der anderen CCCHand deaktivieren wir.
/// </remarks>
public enum Hand : ushort
{
    Left = 0,
    Right = 1
}
public class ActivateCCCVIU : ActivateCCC
{
    /// <summary>
    /// Linker oder rechter Controller?
    /// </summary>
    public HandRole CCCHand = HandRole.LeftHand;
    
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist der Trigger des Controllers.
    ///  </remarks>
    [Tooltip("Welcher Button auf dem Controller wird f�r das einblenden eingesetzt?")]
    public ControllerButton TheButton = ControllerButton.Trigger;
    
    /// <summary>
    /// GameObject des Controllers, den wir verwenden m�chten.
    /// </summary>
    private GameObject m_Controller;

    /// <summary>
    /// GameObjekct des Colliders des Controllers, den wir nicht verwenden.
    /// </summary>
    /// <remarks>
    /// Wir ben�giten dieses Objekt, da wir den Collider dieses Controllers
    /// deaktivieren.
    /// </remarks>
    private GameObject m_ControllerCollider;
    
    private void Awake()
    {
        FindTheCCC();
        if (!TheCCC) return;
        TheCCC.SetActive(Show);
        
        if (CCCHand == HandRole.LeftHand) 
        {
            m_Controller = GameObject.Find("LeftHand");
            m_ControllerCollider = GameObject.Find("Right");
        }
        else
        {
            m_Controller = GameObject.Find("RightHand");
            m_ControllerCollider = GameObject.Find("Left");       
        }
        
        Position = m_Controller;
    }
    
    /// <summary>
    /// Registrieren der Listener f�r den gew�nschten Button
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(CCCHand,
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
        ViveInput.RemoveListenerEx(CCCHand,
            TheButton,
            ButtonEventType.Up,
            ToggleCCC);
    }
    
    
    /// <summary>
    ///Callback f�r das Aktivieren und Deaktivieren des CCC Prefabs
    /// </summary>
    private void ToggleCCC()
    {
        Show = !Show;
        TheCCC.SetActive(Show);
        if (Show)
        {   
                TheCCC.transform.position = Position.transform.position;
                m_ControllerCollider.SetActive(false);
        }
        else
            m_ControllerCollider.SetActive(true);
    }
}
