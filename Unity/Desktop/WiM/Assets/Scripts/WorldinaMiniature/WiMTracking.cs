using System.Collections.Generic;
using UnityEngine;

public class WiMTracking : MonoBehaviour
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
    /// Objekte aus der Scene, die in der Miniaturwelt enthalten sein sollen.
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
        CloneRealWorld();
    }

    /// <summary>
    ///  Ausrichten der Miniaturwelt am GameObject, falls sich dieses bewegt.
    /// </summary>
    void Update()
    {
        AlignMiniWorldToOrigin();
    }

    /// <summary>
    ///Ausrichten der Miniaturwelt, falls das Gameobject, auf dem sie
    /// dargestellt wird sich verädnert.
    /// </summary>
    private void AlignMiniWorldToOrigin()
    {
        var position = Origin.transform.position + Origin.transform.rotation * OriginOffset;
        transform.SetPositionAndRotation(position, Origin.transform.rotation);
    }

    /// <summary>
    /// Erstellung eines Clones der Objekte in der Liste Objects
    /// </summary>
    private void CloneRealWorld()
    {
        foreach (GameObject realObject in RealObjects)
        {
            GameObject clonedObject = Instantiate(realObject, this.transform);
            clonedObject.name = realObject.name + "_Modell";
            // Überprüfen, ob das Objekt eine Component vom Typ Playercontrol hat
            var component = clonedObject.GetComponent<PlayerControl>();
            if (component != null)
            {
                Destroy(component);
            }

            TransformSync(realObject, clonedObject);
            TransformSync(clonedObject, realObject);
        }
    }

    /// <summary>
    /// Erzeugen einer Component vom Typ TransformSynchronizer.
    /// </summary>
    /// <remarks>
    /// Damit können wir das Objekt in der Welb bewegen und damit auch
    /// das im Modell.
    /// </remarks>
    private void TransformSync(GameObject realObject, GameObject clonedObject)
    {
        Debug.Assert(realObject.transform.childCount == clonedObject.transform.childCount, "Not a clone of realObject");

        // Falls es keine Kinder gibt direkt einen Tracker hinzufügen
        if (clonedObject.transform.childCount == 0)
        {
            var transformTracker = clonedObject.AddComponent<TransformSynchronizer>();
            transformTracker.Target = realObject.transform;
        }
        // Wir traversieren die Hierarchie, falls es eine gibt.
        for (var i = 0; i < clonedObject.transform.childCount; i++)
        {
            var transformTracker = clonedObject.transform.GetChild(i).gameObject.AddComponent<TransformSynchronizer>();
            transformTracker.Target = realObject.transform.GetChild(i);

            TransformSync(realObject.transform.GetChild(i).gameObject, clonedObject.transform.GetChild(i).gameObject);
        }
    }
}
