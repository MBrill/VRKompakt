using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Highlighter für ein GameObject,
/// abhängig von einem Tastendruck, der
/// mit Hilfe einer Action im Inspektor definiert ist
/// verändert sich die Farbe eines Objekts
/// zu einem Highlight oder wieder zur Originalfarbe.
/// </summary>
public class Highlighter : MonoBehaviour
{
    /// <summary>
    /// Input Actdion, die wir im Inspektor definieren.
    /// </summary>
    public InputAction HighlightAction;
    
    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    [Tooltip("Material für das Highlight")]
    public Material HighlightMaterial;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enthält.
    /// </summary>
    /// <remarks>
    /// Wir rekonstruieren die Originalfarbe mit Hilfe dieser Variable.
    /// </remarks>
    private Material myMaterial;
    
    /// <summary>
    /// Wir fragen die Materialien ab und speichern die Farben als Instanzen
    /// der Klasse Color ab.
    /// </summary>
    private Color originalColor, highlightColor;

    /// <summary>
    /// Wir fragen das Material und die Farbe ab und setzen
    /// die Highlight-Farbe aus dem zugewiesenen Material.
    ///
    /// Zusätzlich registrieren wir die beiden Callbacks für den Tastendruck
    /// und das Loslassen der Taste.
    /// </summary>
    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        originalColor = myMaterial.color;
        highlightColor = HighlightMaterial.color;
        
        HighlightAction.started += HighlightOn;
        HighlightAction.canceled += HighlightOff;
    }

    /// <summary>
    /// In Enable für die Szene aktivieren wir die Action.
    /// </summary>
    private void OnEnable()
    {
        HighlightAction.Enable();
    }
        
    /// <summary>
    /// In Disable für die Szene de-aktivieren wir die Action.
    /// </summary>
    private void OnDisable()
    {
        HighlightAction.Disable();
    }

    /// <summary>
    /// Callback für den Tastendruck
    /// </summary>
    private void HighlightOn(InputAction.CallbackContext ctx)
    {
        myMaterial.color = highlightColor;
    }
    
    /// <summary>
    /// Callback, falls die Taste wieder losgelassen wird.
    /// </summary>
    private void HighlightOff(InputAction.CallbackContext ctx)
    {
        myMaterial.color = originalColor; 
    }
}
