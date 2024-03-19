//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  World-in-Miniature
/// </summary>
/// <remarks>
/// Erzeugen einer Miniaturansicht.
///
/// Das Modell kann mit Hilfe einer Action ein-
/// und ausgeblendet werden.
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
    /// Offset zum Pivot-Punkt des Objekts, das diese Komponente besitzt.
    /// </summary>
    [Tooltip("Offset des Modells zum Wurzelobjekt")]
    public Vector3 Offset = Vector3.zero;

    /// <summary>
    /// Objekte aus der Scene, die in der Miniaturwelt enthalten sein sollen.
    /// </summary>
    /// <remarks>
    /// Im Inspector verwenden wir +/- für das Hinzufügen oder
    /// Löschen von Objekten, die in der Miniaturwelt enthalten sein sollen.
    /// </remarks>
    [Tooltip("Welche Objekte sollen in der Miniatur enthalten sein?")]
    public List<GameObject> Objects;

    /// <summary>
    /// Soll die WIM angezeigt werden?
    /// </summary>
    [Tooltip("Ist das WiM sichtbar?")]
    public bool ShowTheWim = true;
    
    /// <summary>
    /// Dateiname für das Protokoll
    /// </summary>
    [Tooltip("Name der Protokoll-Datei")]
    public string fileName = "wimlog.csv";

    /// <summary>
    /// Logging aktivieren und de-aktivieren.
    /// </summary>
     [Tooltip("Sollen Protokoll-Ausgaben erstelltnwerde?")]
    public bool Logs = true;

    /// <summary>
    /// World-in-Miniature neu erstellen.
    /// </summary>
    public void Refresh()
    {
        if (ShowTheWim)
        {
            Destroy(m_OffsetObject);
            m_Create();
        }
    }

    /// <summary>
    /// Modell-Objekt und Szenenobjekt bewegen.
    /// </summary>
    /// <remarks>
    /// Im Modell-Objekt verändern wir localPosition,
    /// im Szenen-Objekt position.
    /// </remarks>
    /// <param name="model">Modell-Objekt</param>
    /// <param name="delta">Positionsveränderung</param>
    public void MoveModelAndObject(GameObject model,
        Vector3 delta)
    {
        var goname = WiMUtilities.ObjectNameFromModel(model.name);
        var go = GameObject.Find(goname);

        model.transform.localPosition += delta;
        go.transform.position += delta;
    }

    /// <summary>
    /// ÜBertragen der Orientierung des Modell-Objekts auf das
    /// zugehörige Szenen-Objekt.
    /// </summary>
    /// <remarks>
    /// Wir kopieren localRotation auf rotation.
    /// </remarks>
    /// <param name="model"></param>
    public void TransferModelOrientation(GameObject model)
    {
        var goname = WiMUtilities.ObjectNameFromModel(model.name);
        var go = GameObject.Find(goname);

        go.transform.rotation = model.transform.localRotation;
    }
    
    /// <summary>
    /// Modell-Objekt und zugehöriges szenen-Objekt löschen.
    /// </summary>
    /// <param name="model">Modell-Objekt, das gelöscht
    /// werden soll.</param>
    public void DeleteModelAndObject(GameObject model)
    {
        var goname = WiMUtilities.ObjectNameFromModel(model.name);
        var go = GameObject.Find(goname);
        
        Destroy(go);
        Destroy(model);
    }

    /// <summary>
    /// WiM ein- oder ausblenden.
    /// </summary>
    protected void ToggleShow()
    {
        ShowTheWim = !ShowTheWim;
        if (ShowTheWim)
            m_Create();
        else  
            Destroy(m_OffsetObject);
    }
    
    /// <summary>
    /// Offset-Objekt, der eigentliche Ursprung des Modellkoordinatensystems.
    /// </summary>
    protected GameObject m_OffsetObject;
    
    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    private CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
    

    
    /// <summary>
    /// Setzen des Maßstabs und Clone der Objekte.
    /// </summary>
    void  Start()
    {
        csvLogHandler = new CustomLogHandler(fileName);
        if (ShowTheWim)
            m_Create();
    }
    
    /// <summary>
    /// World-in-Miniature erzeugen.
    /// </summary>
    /// <remarks>
    /// Wir legen das Offset-Objekt an und erzeugen
    /// dann die Kopien der Objekte im Modell,
    /// als Kindknoten des Offset-Objekts.
    /// </remarks>
    protected void m_Create()
    {
        m_MakeOffset();
        m_CloneObjects();
    }
    
    /// <summary>
    /// Offset-Objekt erzeugen.
    /// </summary>
    /// <remarks>
    /// Das Offset-Objekt steht in der Hierarchie zwischen dem GameObject,
    /// dem wir diese Klasse als Komponente hinzufügen und den Objekten,
    /// die wir in die World-in-Miniature hinzufügen.
    ///
    /// Damit können die World-in-Miniature vom Pivot-Punkt des GameObjects
    /// wegbewegen.
    /// </remarks>
    protected void m_MakeOffset()
    {
        m_OffsetObject = new GameObject("Offset");
         m_OffsetObject.transform.SetParent(this.transform);
         m_OffsetObject.transform.localPosition = Offset;
         m_OffsetObject.transform.localScale = 
             new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
         m_OffsetObject.transform.localRotation = Quaternion.identity;
    }
    
    /// <summary>
    /// Clones der Objekte für das Modell.
    /// </summary>
    /// <remarks>
    /// /Falls ein Objekt in das Modell aufgenommen wird,
    /// das Kindknoten hat  werden auch diese GameObjekts
    /// umbenannt!
    /// </remarks>
    protected void m_CloneObjects()
    {
        foreach (var go in Objects)
        {
            object[] args = {go.name, 
                go.transform.position.x,
                go.transform.position.y,           
                go.transform.position.z,            
            };
            if (Logs)
                s_Logger.LogFormat(LogType.Warning, go,
                "{0:c};{1:G}; {2:G}; {3:G}", args);
            
            var clonedObject = Instantiate(go, m_OffsetObject.transform);
            clonedObject.name = WiMUtilities.BuildModelName(go.name);
            // Überprüfen, ob es Kindknoten gibt und diese auch umbenennen
            if (clonedObject.transform.childCount != 0)
            {
                List<GameObject> children = WiMUtilities.GetChildren(clonedObject);
                foreach (var gochild in children)
                {
                    gochild.name =  WiMUtilities.BuildModelName(gochild.name);
                }
            }
            
            args[0] = clonedObject.name;
            args[1] = clonedObject.transform.position.x;
            args[2] = clonedObject.transform.position.y;
            args[3] = clonedObject.transform.position.z;
            if (Logs)
                s_Logger.LogFormat(LogType.Warning, clonedObject,
                "{0:c};{1:G}; {2:G}; {3:G}", args);
        }
    }
}
