using UnityEngine;

/// <summary>
/// Veränderung des Materials eines GameObjects abhängig davon,
/// welcher Trigger-Event aktuell ausgelöst wurde.
/// </summary>
/// <remarks>
/// In diese Version verändern wir die Farbe des gesteuerten Objekts
/// nicht, da wir dieses Script für ein Controller-Prefab einsetzen.
/// </remarks>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ControllerTouchHighlighter : MonoBehaviour
{
    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des berührten Objekts bei TriggerStay")]
    public Material Stay;
    
    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des Trigger-Objekts während Kollision")]
    public Material TriggerExit;

    /// <summary>
    /// Material des berührten Objekts für die Rekonstruktion.
    /// </summary>
    private Material Original;

    /// <summary>
    /// MeshRenderer des berührten Objekts
    /// </summary>
    private MeshRenderer otherRenderer;
    
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
    }
    
    /// <summary>
    /// Trigger-Event ist beendet. Materialien rekonstruieren.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    void OnTriggerExit(Collider otherObject)
    {
        otherRenderer.material = Original;
    }
}
