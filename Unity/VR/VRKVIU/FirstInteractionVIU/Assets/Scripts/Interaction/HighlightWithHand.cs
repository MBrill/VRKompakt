using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Highlighter f�r ein GameObject,
/// abh�ngig von einem Tastendruck auf einem Controller
/// als erstes Beispiel f�r eine Interaktion mit VIU.
/// <remarks>
/// In dieser Version kann ausgew�hlt werden, ob wir den rechten
/// oder den linken Controller verwenden.
/// </remarks>
/// </summary>
public class HighlightWithHand : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird f�r die ge�nderte Farbe verwendet.
    /// </summary>
    [Tooltip("Material f�r das Highlight")]
    public Material HighlightMaterial;

    /// <summary>
    /// Welcher Controller wird verwendet?
    /// </summary>
    /// <remarks>
    ///Default ist die rechte Hand.
    /// </remarks>
    [Tooltip("Welcher Controller (links/rechts) soll f�r das Highlight verwendet werden?")]
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
    ///  Logische Variable, mit der wir �berpr�fen k�nnen, ob
    /// aktuell die Taste gedr�ckt gehalten wird.
    /// </summary>
    private bool m_status = false;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enth�lt
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
    /// Registrieren der Listerner f�r den gew�nschten Button
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
    /// Farbwechsel, wird in den Listernern registriert
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