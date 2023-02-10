using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasse für die Repräsentation einer World-in-Miniature
/// </summary>
public class WiMUtilities
{
    /// <summary>
    /// Namensvergabe für die Modelle
    /// </summary>
    /// <param name="name">Name des Objekts in der Szene</param>
    /// <returns>Name des Modells</returns>
    public static string BuildModelName(string name)
    {
        return name + "_Modell";
    }
    
    /// <summary>
    /// Objektname aus dem Namen des Modells erfragen
    /// </summary>
    /// <remarks>
    /// Die Namen der Objekte sollten keine Unterstriche enthalten,
    /// sonst ist das Ergebnis dieser Funktion falsch!
    /// </remarks>
    /// <param name="modelname">Name des Modells</param>
    /// <returns>Name des Objekts in der Szene</returns>
    public static string ObjectNameFromModel(string modelname)
    {
        string[] parts = modelname.Split("_");
        Debug.Log(parts);
        return parts[0];
    } 
    
    /// <summary>
    /// Umrechnung von Modell- in Weltkoordinaten 2.0
    /// mit Berücksichtigung der Rotation des Modellkoordinatensystems
    /// gegenüber des Weltkoordinatensystems
    /// </summary>
    /// <remarks>
    /// 1. Rückgängigmachen der Rotation des Root-Objekts
    /// 2. Zurückrechnen wir bisher.
    /// </remarks>
    /// <param name="scale">Skalierungsfaktor</param>
    /// <param name="mPos">Modellkoordinaten</param>
    /// <param name="rPosition"">Koordinaten des Wurzelobjekts der WiM</param>
    /// <param name="rRotation">Orientierung des Wurzelobjekts als Quaternion</param>
    /// <param name="off"">Offset-Vektor in lokalen Koordinaten</param>
    /// <returns>Weltkoordinaten</returns>
    public static Vector3 ModelToWorld(float scale,
        Vector3 mPos,
        Vector3 rPosition,
        Quaternion rRotation,
        Vector3 off)
    {
        Quaternion inverseRotation = Quaternion.Inverse(rRotation);
        var result = inverseRotation * (mPos - rPosition) - off;
        return (1.0f/scale)*result;
    }
    
    /// <summary>
    /// Umrechnung von Welt- in die Modellkoordinaten
    /// </summary>
    /// <remarks>
    /// Wir verwenden die Euler-Winkel des Root-Objects
    /// und bauen daraus das Quaternion für die Drehung,
    /// falls das Root-Objekt eine Rotation enthält.
    /// </remarks>
    /// <param name="scale">Skalierungsfaktor</param>
    /// <param name="obj">Weltposition</param>
    /// <param name="rPosition"">Koordinaten des Wurzelobjekts der WiM</param>
    /// <param name="rRotation">Orientierung des Wurzelobjekts als Quaternion</param>
    /// <param name="off"">Lokale Koordinaten des Offsets in WiM</param>
    /// <returns>Modellkoordinaten</returns>
    public static Vector3 WorldToModel(float scale, 
        Vector3 obj, 
        Vector3 rPosition, 
        Quaternion rRotation, 
        Vector3 off)
    {
        var result =  rPosition;
        //Basistransformation durchführen: Modellachsen
        var localX = rRotation * Vector3.right;
        var localY = rRotation * Vector3.up;
        var localZ = rRotation * Vector3.forward;
        // Verschieben in Richtung lokale Achsen
        // mit Offset und skalierter Objektposition.
        result += (off.x + scale * obj.x ) * localX;
        result += (off.y + scale * obj.y) * localY;
        return result + (off.z+ scale * obj.z) * localZ;
    }
    
    public static List<GameObject> GetChildren(GameObject go) 
    {
        List<GameObject> list = new List<GameObject>();
        return GetChldrenHelper(go, list);
    }
    
    /// <summary>
    /// Alle Kindknoten rekursiv traversieren und der Liste hinzufügen.
    /// </summary>
    /// <remarks>
    ///Code stammt aus Unity Answers
    /// https://answers.unity.com/questions/205391/how-to-get-list-of-child-game-objects.html
    /// </remarks>
    /// <param name="go">Aktuelles GameObject</param>
    /// <param name="list">Liste, in die eingefügt werden soll</param>
    /// <returns>Erweiterte Liste der Kindknoten</returns>
    private static List<GameObject> GetChldrenHelper(GameObject go, List<GameObject> list) 
    {
        if (go == null || go.transform.childCount == 0) {
            return list;
        }
        foreach (Transform t in go.transform) 
        {
            list.Add (t.gameObject);
            GetChldrenHelper (t.gameObject, list);
        }
        return list;
    }
}
