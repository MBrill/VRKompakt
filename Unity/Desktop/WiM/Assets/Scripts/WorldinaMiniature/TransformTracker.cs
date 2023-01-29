using UnityEngine;

/// <summary>
/// "Synchronisiert" den Transform von diesem gameObject mit dem vom target-Transform.
/// </summary>
public class TransformTracker : MonoBehaviour
{
    /// <summary>
    /// Das Objekt, von welchem die Positon und Rotation getrackt wird.
    /// </summary>
    [Tooltip("Welches Transform soll getrackt werden?")]
    public Transform target;

    /// <summary>
    /// Ob die Position und Rotation des target Objekts aktualisiert werden soll, falls diese Objekt bewegt wird.
    /// </summary>
    [Tooltip("Soll das target Transform aktualisiert werden, wenn sich dieses Objekt bewegt?")]
    public bool updateTarget = true;
    
    /// <summary>
    /// Solange dieses Objekt nicht bewegt wird, wird die lokale Position und
    /// Rotation auf die vom target Objekt aktualisiert.
    /// Sonst wird, falls updateTarget true ist, die lokale Position
    /// und Rotation des target Objekts aktualisiert.
    /// </summary>
    void Update()
    { 
        if (updateTarget)
        {
            target.transform.localPosition = transform.localPosition;
            target.transform.localRotation = transform.localRotation;
        }
    }
}
