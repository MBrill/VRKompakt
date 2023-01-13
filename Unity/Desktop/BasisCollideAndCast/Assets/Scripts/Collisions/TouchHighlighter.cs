using UnityEngine;

/// <summary>
/// Veränderung des Materials eines GameObjects abhängig davon,
/// welcher Trigger-Event aktuell ausgelöst wurde.
/// </summary>
/// <remarks>
/// Wir verwenden vier verschiedene Materialien:
/// - das Original-Material des Objekts
/// - ein Material nach TriggerEnter
/// - ein Material bei TriggerExit
/// - ein Material bei TriggerExit
/// </remarks>
public class TouchHighlighter : MonoBehaviour
{
    /// <summary>
    /// Farbe bei TriggerExit
    /// </summary>
    [Tooltip("Material bei TriggerExit")]
    public Material Stay;
    
    /// <summary>
    /// Farbe bei TriggerExit
    /// </summary>
    [Tooltip("Material des Trigger-Objekts während Kollision")]
    public Material TriggerExit;

    /// <summary>
    /// Variable, die das Original-Material des Objekts enthält,
    /// das den Trigger auslöst.
    /// </summary>
    private Material myMaterial;

    /// <summary>
    /// Material des berührten Objekts für die Rekonstruktion.
    /// </summary>
    private Material Original;

    /// <summary>
    /// MeshRenderer des berührten Objekts
    /// </summary>
    private MeshRenderer otherRenderer;
    
    /// <summary>
    /// MeshRenderer des Trigger-Objekts
    /// </summary>
    private MeshRenderer rend;
    
    /// <summary>
    /// Wir fragen den Renderer und das Material des Trigger-Objekts  ab
    /// </summary>
    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        rend = GetComponent(typeof(MeshRenderer)) as MeshRenderer;
    }
    
    /// <summary>
    /// Speichern des Materials des berührten Objekts.
    /// </summary>
    /// <remarks>
    /// Man sieht keine Farbänderung, da wir sofort Stay aufrufen.
    /// Deshalb werden hier nur Materilien für die Rekonstruktion
    /// gespeichert!
    /// </remarks>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    void OnTriggerEnter(Collider otherObject)
    {
        otherRenderer = otherObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        Original = otherRenderer.material as Material;
    }
    
    /// <summary>
    /// Das GameObject ist mit einem weiteren Objekt in der Szene kollidiert.
    /// Der Trigger-Event  hat auch im Frame vorher stattgefunden.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    void OnTriggerStay(Collider otherObject)
    {
        otherRenderer.material= Stay;
        rend.material = TriggerExit;
    }
    
    /// <summary>
    /// Trigger-Event ist beendet. Materialien rekonstruieren.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    void OnTriggerExit(Collider otherObject)
    {
        otherRenderer.material = Original;
        rend.material = myMaterial;
    }
}
