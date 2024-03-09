using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Highlighter für ein GameObject,
/// abhängig von einem Tastendruck auf einem Controller
/// als erstes Beispiel für eine Interaktion mit VIU.
/// </summary>
public class Highlighter : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    [Tooltip("Material für das Highlight")]
    public Material HighlightMaterial;

    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// 
    /// Default ist der Trigger des Controllers.
    /// </summary>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton TheButton = ControllerButton.Trigger;

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
    ///Die Listerner registrieren.
    /// </summary>
    /// <remarks>
    ///In dieser Version registrieren wir beide Controller.
    /// </remarks>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(HandRole.RightHand,
                                TheButton,
                                ButtonEventType.Down,
                                changeColor);

        ViveInput.AddListenerEx(HandRole.RightHand,
                                TheButton,
                                ButtonEventType.Up,
                                changeColor);

        ViveInput.AddListenerEx(HandRole.LeftHand,
                                TheButton,
                                ButtonEventType.Down,
                                changeColor);

        ViveInput.AddListenerEx(HandRole.LeftHand,
                                TheButton,
                                ButtonEventType.Up,
                                changeColor);
    }

    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand,
                                   TheButton,
                                   ButtonEventType.Down,
                                   changeColor);

        ViveInput.RemoveListenerEx(HandRole.RightHand,
                                   TheButton,
                                   ButtonEventType.Up,
                                   changeColor);

        ViveInput.RemoveListenerEx(HandRole.LeftHand,
                                   TheButton,
                                   ButtonEventType.Down,
                                   changeColor);

        ViveInput.RemoveListenerEx(HandRole.LeftHand,
                                   TheButton,
                                   ButtonEventType.Up,
                                   changeColor);
    }
    
    /// <summary>
    /// Farbwechsel, wird in den Listernern registriert
    /// </summary>
    private void changeColor()
    { 
        if (!m_status)
            myMaterial.color = highlightColor;
        else
            myMaterial.color = originalColor; 
        
         m_status = !m_status;
    }
}
