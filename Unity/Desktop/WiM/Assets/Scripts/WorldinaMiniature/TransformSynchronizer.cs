using UnityEngine;

/// <summary>
/// Synchronisation zwischen den Transformationen zweier Gameobjects.
/// </summary>
public class TransformSynchronizer : MonoBehaviour
{
    /// <summary>
    /// Das Objekt, dessen Positon und Rotation synchronisiert wird.
    /// </summary>
    public Transform Target;

    /// <summary>
    ///IAktivieren bzw. De-Aktivieren der Synchronisation
    /// </summary>
    public bool UpdateTarget = true;
    
    /// <summary>
    /// Synchronisation
    /// </summary>
    void LateUpdate() 
    { 
        if (UpdateTarget)
        {
            transform.localPosition = Target.transform.localPosition;
            transform.localRotation = Target.transform.localRotation;
        }
    }
}
