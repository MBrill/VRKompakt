using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHighlighter : MonoBehaviour
{
    /// <summary>
    /// Die Farbe dieses Materials wird für die geänderte Farbe verwendet.
    /// </summary>
    [Tooltip("Material für das Highlight")]
    public Material HighlightMaterial;
    
    
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

    void Update()
    {
        if (!m_status)
            myMaterial.color = highlightColor;
        else
            myMaterial.color = originalColor; 
    }
    /// <summary>
    /// Farbwechsel, wird in den Listernern registriert
    /// </summary>
    public void ChangeColor()
    {
        Debug.Log("In Changecolor");
        m_status = !m_status;
    }
    
    /// <summary>
    /// Soll das Objekt hervorgehoben werden oder nicht?
    /// </summary>
    private bool m_status = true;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enthält
    /// </summary>
    private Material myMaterial;
    /// <summary>
    /// Wir fragen die Materialien ab und speichern die Farben als Instanzen
    /// der Klasse Color ab.
    /// </summary>
    private Color originalColor, highlightColor;
}
