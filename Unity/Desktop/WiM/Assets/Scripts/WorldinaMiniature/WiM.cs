using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logik für eine 3D Miniaturansicht.
/// </summary>
public class WiM : MonoBehaviour
{
    [Tooltip("Maßstab für das Modell")]
    [Range(0.01f, 0.2f)] 
    public float scaleFactor = 0.1f;
    
    [Tooltip("Wo soll die Miniatur positioniert werden? z. B. Tisch oder eine Platte")]
    public Transform origin;

    [Tooltip("Offset vom Ursprung, z.B. (0,1,0) um 1m über dem Ursprung zu positionieren")]
    public Vector3 originOffset;

    /// <summary>
    /// Objekte aus der Scene, welche in der Miniatur enthalten sein sollen.
    /// </summary>
    [Tooltip("Welche Objekte sollen in der Miniatur enthalten sein?")]
    public List<GameObject> realObjects;

    /// <summary>
    /// Anfangs werden die in realObjects festgelegten Objekte geklont.
    /// </summary>
    void Start()
    {
        transform.localScale =
            new Vector3(scaleFactor, scaleFactor, scaleFactor);
        CloneRealWorld();
    }

    /// <summary>
    /// Richtet immer wieder die Miniatur am Ursprung aus. Nützlich,
    /// wenn sichz.B. der Ursprung bewegt.
    /// </summary>
    void Update()
    {
        AlignMiniWorldToOrigin();
    }

    /// <summary>
    /// Setzt die Position von diesem Transform an die Position von origin + originOffset
    /// </summary>
    private void AlignMiniWorldToOrigin()
    {
        var position = origin.transform.position + origin.transform.rotation * originOffset;
        transform.SetPositionAndRotation(position, origin.transform.rotation);
    }

    /// <summary>
    /// Klont alle Objekte in realObjects und fügt den Klons eine TransformTracker-Komponente hinzu, damit die
    /// Position synchron zum "realen"-Objekt ist
    /// </summary>
    private void CloneRealWorld()
    {
        foreach (GameObject realObject in realObjects)
        {
            GameObject clonedObject = Instantiate(realObject, this.transform);
            //TrackTransform(realObject, clonedObject);
        }
    }

    /// <summary>
    /// Fügt rekursiv dem clonedObject und seinen Kindern eine
    /// TransformTracker-Komponente hinzu, welche den Transform vom 
    /// realObject trackt und die lokale Position
    /// des clonedObject entsprechend anpasst.
    /// </summary>
    private void TrackTransform(GameObject realObject, GameObject clonedObject)
    {
        Debug.Assert(realObject.transform.childCount == clonedObject.transform.childCount, "Not a clone of realObject");

        for (var i = 0; i < clonedObject.transform.childCount; i++)
        {
            var transformTracker = clonedObject.transform.GetChild(i).gameObject.AddComponent<TransformTracker>();
            transformTracker.target = realObject.transform.GetChild(i);

            TrackTransform(realObject.transform.GetChild(i).gameObject, clonedObject.transform.GetChild(i).gameObject);
        }
    }
}
