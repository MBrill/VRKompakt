//========= 2022- 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Ver�nderung des Materials eines GameObjects abh�ngig davon,
/// welcher Trigger-Event aktuell ausgel�st wurde.
/// </summary>
/// <remarks>
/// In diese Version ver�ndern wir die Farbe des gesteuerten Objekts
/// nicht, da wir dieses Script f�r ein Controller-Prefab einsetzen.
/// </remarks>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TouchHighlighter : MonoBehaviour
{
    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des ber�hrten Objekts w�hrend der Kollision")]
    public Material TouchedMaterial;

    /// <summary>
    /// Material des ber�hrten Objekts f�r die Rekonstruktion.
    /// </summary>
    private Material otherOriginal;

    /// <summary>
    /// Speichern des Materials des ber�hrten Objekts.
    /// </summary>
    /// <remarks>
    /// Man sieht keine Farb�nderung, da wir sofort TouchedMaterial aufrufen.
    /// Deshalb werden hier nur Materilien f�r die Rekonstruktion
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
