using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Eine einfache Version einer World in a Miniature.
/// </summary>
/// <remarks>
/// Die Objekte die im Modell auftauchen sollen
/// können interaktiv im Inspector hinzugefügt werden.
///
/// Keine weiterre Funktionalität.
/// </remarks>
public class WiM : MonoBehaviour
{
    [Tooltip("Maßstab für das Modell")]
    [Range(0.01f, 0.2f)] 
    /// <summary>
    /// Maßstab für die Miniaturansicht.
    /// </summary>
    /// <remarks>
    /// Default-Wert ist 0.1.
    /// </remarks>
    public float ScaleFactor = 0.1f;
    
    /// <summary>
    /// GameObject, auf dem die Miniaturwelt platziert werden soll.
    /// </summary>
    [Tooltip("Wo soll die Miniatur positioniert werden?")]
    public Transform Origin;

    /// <summary>
    /// Objekte aus der Szene, die in der Miniaturwelt enthalten sein sollen.
    /// </summary>
    /// <remarks>
    /// Im Inspector verwenden wir +/- für das Hinzufügen oder
    /// Löschen von Objekten, die in der Miniaturwelt enthalten sein sollen.
    /// </remarks>
    [Tooltip("Welche Objekte sollen in der Miniatur enthalten sein?")]
    public List<GameObject> RealObjects;

    /// <summary>
    /// Setzen des Maßstabs und Clone der Objekte.
    /// </summary>
    void  Start()
    {
        transform.localScale =
            new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
        cloneObjects();
        setOrigin();
    }

    /// <summary>
    /// Erstellung eines Clones der Objekte in der Liste RealObjects
    /// </summary>
    private void cloneObjects()
    {
        foreach (GameObject go in RealObjects)
        {
            var clonedObject = Instantiate(go, this.transform);
            clonedObject.name = go.name + "_Modell";
        }
    }

    /// <summary>
    ///  Setzen der Transformation des Wurzelknotens
    /// </summary>
    private void setOrigin()
    {
        transform.SetPositionAndRotation(Origin.transform.position, 
            Origin.transform.rotation);       
    }
}
