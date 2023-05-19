//========= 2023 - Copyright Manfred Brill. All rights reserved. ===========
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/// <summary>
///  World-in-Miniature
/// </summary>
/// <remarks>
/// Erzeugen einer Miniaturansicht auf der Basis von Tags.
/// </remarks>
public class TaggedWiM : MonoBehaviour
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
    /// Tag der Objekte, die in die Miniaturansicht kommen sollen.
    /// </summary>
    [Tooltip("Welcher Layer soll für die Miniaturansicht verwendet werden?")]
    public string TagName = "MiniWorld";
    
    /// <summary>
    /// Soll die WIM angezeigt werden?
    /// </summary>
    public bool ShowTheWim = true;
    
    /// <summary>
    /// Action für das Ein- und Ausblenden der Miniatur-Darstellung
    /// </summary>
    public InputAction ShowAction;

    /// <summary>
    /// Offset-Objekt, die Wurzel der Hierarchie für die World-in-Miniature
    /// </summary>
    private GameObject m_OffsetObject;
    
    /// <summary>
    /// Registrieren der Callbacks für ShowAction
    /// </summary>
    private void Awake()
    {
        ShowAction.performed += OnShow;
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir die Action.
    /// </summary>
    private void OnEnable()
    {
        ShowAction.Enable();
    }
    
    /// <summary>
    /// In Disable für die Szene deaktivieren wir die Action.
    /// </summary>
    private void OnDisable()
    {
        ShowAction.Disable();
    }
    
    /// <summary>
    /// Setzen des Maßstabs und Clone der Objekte.
    /// </summary>
    void  Start()
    {
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
    /// dem wir diese Klasse als Komponente hinzufügen und den Objekten,
    /// die wir in die World-in-Miniature hinzufügen.
    ///
    /// Damit können die World-in-Miniature vom Pivot-Punkt des GameObjects
    /// wegbewegen.
    /// </remarks>
    /// <returns></returns>
    private void m_MakeOffset()
    {
         m_OffsetObject = new GameObject("Offset");
         m_OffsetObject.transform.SetParent(this.transform);
         m_OffsetObject.transform.localPosition = Offset;
         m_OffsetObject.transform.localScale = new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
    }
    
    private void m_CloneObjects()
    {
        var scobjs = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects( scobjs );
        
        foreach (GameObject go in scobjs)
        {
            if (go.tag == TagName)
            {
                var clonedObject = Instantiate(go, m_OffsetObject.transform);
                clonedObject.name = go.name + "_Modell";
            }
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
