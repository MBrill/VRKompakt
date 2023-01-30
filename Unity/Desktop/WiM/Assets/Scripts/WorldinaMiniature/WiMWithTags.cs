using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Eine einfache Version einer World in a Miniature.
/// </summary>
/// <remarks>
/// Die Objekte die im Modell auftauchen sollen
/// können interaktiv im Inspector hinzugefügt werden.
///
/// Keine weiterre Funktionalität.
/// </remarks>

public class WiMWithTags : MonoBehaviour
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
    /// Offset bei der Platzierung.
    /// </summary>
    /// <remarks>
    /// Damit können wir die Miniaturwelt relativ zum GameObject, auf dem wir sie
    /// darstellen bewegen und die Objekte zum Beispiel schweben lassen.
    /// </remarks>
    [Tooltip("Offset vom Ursprung, z.B. (0,1,0) um 1m über dem Ursprung zu positionieren")]
    public Vector3 OriginOffset;

    /// <summary>
    /// Tag der Objekte, die in die Miniaturansicht kommen sollen.
    /// </summary>
    [Tooltip("Welcher Layer soll für die Miniaturansicht verwendet werden?")]
    public string TagName = "MiniWorld";

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
        var scobjs = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects( scobjs );
        
        foreach (GameObject obj in scobjs)
        {
            if (obj.tag == TagName)
            {
                var clonedObject = Instantiate(obj, this.transform);
                clonedObject.name = obj.name + "_Modell";
            }
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
