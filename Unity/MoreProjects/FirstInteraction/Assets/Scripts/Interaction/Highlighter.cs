//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

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
    protected Material myMaterial;
    
    /// <summary>
    /// Variablen für die Farben des Highlight-Materials
    /// und die Originalfarbe.
    /// </summary>
    protected Color originalColor, highlightColor;

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
}
