using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Highlighting mit Hilfe eines Input Action Assets.
/// </summary>
/// <remarks>
/// Wir fangen mit "Q"  das Quit ab und beenden
/// damit die Anwendung.
/// </remarks>
public class Highlighter : MonoBehaviour
{
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
    /// Variablen für die Farben des Highlight-Materials
    /// und die Originalfarbe.
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
    }

    /// <summary>
    /// Callback für die Action Highlight
    /// </summary>
    /// <remarks>
    /// Damit value.isPressed beim Loslassenden Wert false
    /// zurückgibt definieren wir die Action nicht als Button,
    /// sondern als Passthrough!
    /// </remarks>
    private void OnHighlight(InputValue value) => myMaterial.color = value.isPressed ? highlightColor : originalColor;

    /// <summary>
    /// Diese Funktion ist als Callback für die InputAction QuitAction
    /// registriert und wir aufgerufen, wenn der im Inspector
    /// definierte Button verwendet wird.
    /// </summary>
    private void OnQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
