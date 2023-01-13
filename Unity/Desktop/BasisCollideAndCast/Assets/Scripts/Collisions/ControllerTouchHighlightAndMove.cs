using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bewegen eines Objekts nah Berührung und einem Tastendruck.
/// Wird die Taste wieder losgelassen, bleibt das Objekt am aktuellen
/// Ort stehen.
/// </summary>
/// <remarks>
/// In diese Version verändern wir die Farbe des gesteuerten Objekts
/// nicht, da wir dieses Script für ein Controller-Prefab einsetzen.
/// </remarks>
public class ControllerTouchHighlightAndMove : MonoBehaviour
{
    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des berührten Objekts bei TriggerStay")]
    public Material Stay;
    
    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des Trigger-Objekts während Kollision")]
    public Material TriggerExit;

    /// <summary>
    /// Auslösender Bewegung, falls Kollision vorliegt und Tastendruck
    /// </summary>
    public InputAction MoveAction;
    
    /// <summary>
    /// Soll eine Bewegung ausgeführt werden?
    /// </summary>
    private bool m_move = false;
    
    /// <summary>
    /// Material des berührten Objekts für die Rekonstruktion.
    /// </summary>
    private Material Original;

    private GameObject touchedObject = null;
    
    private bool m_touched = false;
    
    /// <summary>
    /// MeshRenderer des berührten Objekts
    /// </summary>
    private MeshRenderer otherRenderer;

    /// <summary>
    /// Callback registrieren für den Tastendruck.
    /// </summary>
    private void Awake()
    {
        MoveAction.started += OnPress;
        MoveAction.canceled += OnRelease;
    }

    /// <summary>
    /// In Enable für die Szene aktivieren wir  unsere Action.
    /// </summary>
    private void OnEnable()
    {
        MoveAction.Enable();
    }
   
    /// <summary>
    /// In Disable für die Szene de-aktivieren wir  unsere Action.
    /// </summary>
    private void OnDisable()
    {
        MoveAction.Disable();
    }
    
    /// <summary>
    /// Callback für die  Action CastAction.
    ///<summary>
    private void OnPress(InputAction.CallbackContext ctx)
    {
        if (m_touched)
             m_move = ctx.ReadValueAsButton();
        else
            m_move = false;

    }

    /// <summary>
    /// Callback für die  Action CastAction.
    ///<summary>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        if (m_touched)
        {
            m_move = ctx.ReadValueAsButton();
            if (otherRenderer.material != Stay)
                otherRenderer.material = Original;
            touchedObject.transform.SetParent(null);
        }
        else
            m_move = false;

    }
    
    /// <summary>
    /// Speichern des Materials des berührten Objekts.
    /// </summary>
    /// <remarks>
    /// Man sieht keine Farbänderung, da wir sofort Stay aufrufen.
    /// Deshalb werden hier nur Materilien für die Rekonstruktion
    /// gespeichert!
    /// </remarks>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    void OnTriggerEnter(Collider otherObject)
    {
        m_touched = true;
        touchedObject = otherObject.gameObject;
        otherRenderer = touchedObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        Original = otherRenderer.material as Material;
    }
    
    /// <summary>
    /// Das GameObject ist mit einem weiteren Objekt in der Szene kollidiert.
    /// Der Trigger-Event  hat auch im Frame vorher stattgefunden.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    void OnTriggerStay(Collider otherObject)
    {
        otherRenderer.material= Stay;        
       // Falls Tastendruck, dann führen wir ein parent aus und bewegen das Objekt mit!
        if (m_move)
            touchedObject.transform.SetParent(transform, true);
        else
            touchedObject.transform.SetParent(null);
    }
    
    /// <summary>
    /// Trigger-Event ist beendet. Materialien rekonstruieren.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    void OnTriggerExit(Collider otherObject)
    {
        touchedObject = otherObject.gameObject;
        otherRenderer = touchedObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        otherRenderer.material = Original;
        touchedObject.transform.SetParent(null);
        touchedObject = null;
        m_touched = false;
    }
}
