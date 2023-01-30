using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  World in a Miniature, die wir in der Szene bewegen können.
/// </summary>
/// <remarks>
/// Im Unterschied zur Basisklasse WiM wird die Funktion
/// AlignMiniWorldToOrigin nicht nur in Start, sondern auch
/// in Update aufgerufen.
///
/// Damit stellen wir sicher, dass die Miniaturwelt
/// korrekt platziert bleibt, falls sich das GameObject,
/// auf dem die Miniaturwelt liegt, bewegt!
/// </remarks>
public class WiMMovable : MonoBehaviour
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
    /// Objekte aus der Scene, die in der Miniaturwelt enthalten sein sollen.
    /// </summary>
    /// <remarks>
    /// Im Inspector verwenden wir +/- für das Hinzufügen oder
    /// Löschen von Objekten, die in der Miniaturwelt enthalten sein sollen.
    /// </remarks>
    [Tooltip("Welche Objekte sollen in der Miniatur enthalten sein?")]
    public List<GameObject> RealObjects;

    /// <summary>
    /// Soll die WIM angezeigt werden?
    /// </summary>
    public bool ShowTheWIM = true;
    
    /// <summary>
    /// Action für das Ein- und Ausblenden der Miniatur-Darstellung
    /// </summary>
    public InputAction ShowAction;
    
    
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
    /// In Dis für die Szene deaktivieren wir die Action.
    /// </summary>
    private void OnEDisable()
    {
        ShowAction.Disable();
    }
    /// <summary>
    /// Setzen des Maßstabs und Clone der Objekte.
    /// </summary>
    void  Start()
    {
        transform.localScale =
            new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
        cloneObjects();
    }

    /// <summary>
    /// Ausrichten der Miniaturwelt, falls sich das Gameobjekt Origin bewegt hat.
    /// </summary>
    void Update()
    {
        setOrigin();
    }
    
    /// <summary>
    /// Erstellung eines Clones der Objekte in der Liste RealObjects
    /// </summary>
    private void cloneObjects()
    {
        foreach (GameObject realObject in RealObjects)
        {
            GameObject clonedObject = Instantiate(realObject, this.transform);
            clonedObject.name = realObject.name + "_Modell";
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
    
    private void OnShow(InputAction.CallbackContext ctx)
    {
        var result = ctx.ReadValueAsButton();
        if (result)
            ShowTheWIM = !ShowTheWIM;
        gameObject.SetActive(ShowTheWIM);
    }
}
