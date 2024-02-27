//========= 2022- 2024 - Copyright Manfred Brill. All rights reserved. ===========
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
public class TouchHighlighter : MonoBehaviour
{
    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des berührten Objekts während der Kollision")]
    public Material TouchedMaterial;

    /// <summary>
    /// Material des berührten Objekts für die Rekonstruktion.
    /// </summary>
    private Material otherOriginal;

    /// <summary>
    /// Speichern des Materials des berührten Objekts.
    /// </summary>
    /// <remarks>
    /// Man sieht keine Farbänderung, da wir sofort TouchedMaterial aufrufen.
    /// Deshalb werden hier nur Materilien für die Rekonstruktion
    /// gespeichert!
    /// </remarks>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    private void OnTriggerEnter(Collider otherObject)
    {
        var otherRenderer = otherObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        otherOriginal = otherRenderer.material as Material;
    }
    
    /// <summary>
    /// Das GameObject ist mit einem weiteren Objekt in der Szene kollidiert.
    /// Der Trigger-Event  hat auch im Frame vorher stattgefunden.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    private void OnTriggerStay(Collider otherObject)
    {
        var otherRenderer = otherObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        otherRenderer.material= TouchedMaterial;
    }
    
    /// <summary>
    /// Trigger-Event ist beendet. Materialien rekonstruieren.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    private void OnTriggerExit(Collider otherObject)
    {
        var otherRenderer = otherObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        otherRenderer.material = otherOriginal;
    }
}
