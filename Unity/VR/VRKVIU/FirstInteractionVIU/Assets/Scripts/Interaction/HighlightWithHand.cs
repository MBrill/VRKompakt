//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Highlighter für ein GameObject,
/// abhängig von einem Tastendruck auf einem Controller
/// als erstes Beispiel für eine Interaktion mit VIU.
/// <remarks>
/// In dieser Version kann ausgewählt werden, ob wir den rechten
/// oder den linken Controller verwenden.
/// </remarks>
/// </summary>
public class HighlightWithHand : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    [Tooltip("Material für das Highlight")]
    public Material HighlightMaterial;

    /// <summary>
    /// Welcher Controller wird verwendet?
    /// </summary>
    /// <remarks>
    ///Default ist die rechte Hand.
    /// </remarks>
    [Tooltip("Welcher Controller (links/rechts) soll für das Highlight verwendet werden?")]
    public HandRole MainHand = HandRole.RightHand;
    
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
    /// Logische Variable, mit der wir überprüfen können, ob
    /// aktuell die Taste gedrückt gehalten wird.
    /// </summary>
    private bool m_status = false;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enthält
    /// </summary>
    private Material myMaterial;
    
    /// <summary>
    /// Wir fragen die Materialien ab und speichern die Farben als Instanzen
    /// der Klasse Color ab.
    /// </summary>
    private Color originalColor, highlightColor;

    /// <summary>
    /// Wir fragen das Material und die Farbe ab und setzen
    /// die Highlight-Farbe aus dem zugewiesenen Material.
    /// </summary>
    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;
        highlightColor = HighlightMaterial.color;
    }

    /// <summary>
    /// Registrieren der Listener für den gewünschten Button
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(MainHand,
                                TheButton,
                                ButtonEventType.Down,
                                m_ChangeColor);

        ViveInput.AddListenerEx(MainHand,
                                TheButton,
                                ButtonEventType.Up,
                                m_ChangeColor);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDisable()
    {
        ViveInput.RemoveListenerEx(MainHand,
                                   TheButton,
                                   ButtonEventType.Down,
                                   m_ChangeColor);

        ViveInput.RemoveListenerEx(MainHand,
                                   TheButton,
                                   ButtonEventType.Up,
                                   m_ChangeColor);
        
    }
    
    /// <summary>
    /// Farbwechsel, wird im Listener registriert
    /// </summary>
    private void m_ChangeColor()
    { 
        if (!m_status)
            myMaterial.color = highlightColor;
        else
            myMaterial.color = originalColor; 
        
         m_status = !m_status;
    }
}