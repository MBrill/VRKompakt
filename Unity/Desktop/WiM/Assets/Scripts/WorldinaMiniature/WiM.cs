using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [Tooltip("Ma�stab f�r das Modell")]
    [Range(0.01f, 0.2f)] 
    /// <summary>
    /// Ma�stab f�r die Miniaturansicht.
    /// </summary>
    /// <remarks>
    /// Default-Wert ist 0.1.
    /// </remarks>
    public float ScaleFactor = 0.1f;
    
    /// <summary>
    /// Offset zum Pivot-Punkt des Objekts, das diese Komponente besitzt.
    /// </summary>
    public Vector3 Offset = Vector3.zero;

    /// <summary>
    /// Objekte aus der Scene, die in der Miniaturwelt enthalten sein sollen.
    /// </summary>
    /// <remarks>
    /// Im Inspector verwenden wir +/- f�r das Hinzuf�gen oder
    /// L�schen von Objekten, die in der Miniaturwelt enthalten sein sollen.
    /// </remarks>
    [Tooltip("Welche Objekte sollen in der Miniatur enthalten sein?")]
    public List<GameObject> Objects;

    /// <summary>
    /// Soll die WIM angezeigt werden?
    /// </summary>
    public bool ShowTheWim = true;
    
    /// <summary>
    /// Action f�r das Ein- und Ausblenden der Miniatur-Darstellung
    /// </summary>
    public InputAction ShowAction;

    /// <summary>
    /// Dateiname f�r das Protokoll
    /// </summary>
    [Tooltip("Name der LProtokoll-Datei")]
    public string fileName = "wimlog.csv";
    
    /// <summary>
    /// Offset-Objekt, die Wurzel der Hierarchie f�r die World-in-Miniature
    /// </summary>
    private GameObject m_OffsetObject;
    
        
    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    private CustomLogHandler csvLogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
    
    /// <summary>
    /// Registrieren der Callbacks f�r ShowAction
    /// </summary>
    private void Awake()
    {
        ShowAction.performed += OnShow;
    }
    
    /// <summary>
    /// In Enable f�r die Szene aktivieren wir die Action.
    /// </summary>
    private void OnEnable()
    {
        ShowAction.Enable();
    }
    
    /// <summary>
    /// In Disable f�r die Szene deaktivieren wir die Action.
    /// </summary>
    private void OnDisable()
    {
        ShowAction.Disable();
    }
    
    /// <summary>
    /// Setzen des Ma�stabs und Clone der Objekte.
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
    private void m_Create()
    {
        m_MakeOffset();
        m_CloneObjects();
    }
    
    /// <summary>
    /// Offset-Objekt erzeugen.
    /// </summary>
    /// <remarks>
    /// Das Offset-Objekt steht in der Hierarchie zwischen dem GameObject,
    /// dem wir diese Klasse als Komponente hinzuf�gen und den Objekten,
    /// die wir in die World-in-Miniature hinzuf�gen.
    ///
    /// Damit k�nnen die World-in-Miniature vom Pivot-Punkt des GameObjects
    /// wegbewegen.
    /// </remarks>
    /// <returns></returns>
    private void m_MakeOffset()
    {
         m_OffsetObject = new GameObject("Offset");
         m_OffsetObject.transform.SetParent(this.transform);
         m_OffsetObject.transform.localPosition = Offset;
         m_OffsetObject.transform.localScale = 
             new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
    }
    
    private void m_CloneObjects()
    {
        foreach (var go in Objects)
        {
            object[] args = {go.name, 
                go.transform.position.x,
                go.transform.position.y,           
                go.transform.position.z,            
            };
            s_Logger.LogFormat(LogType.Warning, go,
                "{0:c};{1:G}; {2:G}; {3:G}", args);
            var clonedObject = Instantiate(go, m_OffsetObject.transform);
            clonedObject.name = go.name + "_Modell";
            args[0] = clonedObject.name;
            args[1] = clonedObject.transform.position.x;
            args[2] = clonedObject.transform.position.y;
            args[3] = clonedObject.transform.position.z;
            s_Logger.LogFormat(LogType.Warning, clonedObject,
                "{0:c};{1:G}; {2:G}; {3:G}", args);
        }
    }
    
    private void OnShow(InputAction.CallbackContext ctx)
    {
        var result = ctx.ReadValueAsButton();
        if (result)
            ShowTheWim = !ShowTheWim;
        
        if (ShowTheWim)
            m_Create();
        else  
            Destroy(m_OffsetObject);
    }
}
