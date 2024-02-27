//========= 2022- 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bewegen eines Objekts nach Ber�hrung und einem Tastendruck.
/// Wird die Taste wieder losgelassen, bleibt das Objekt am aktuellen
/// Ort stehen.
/// </summary>
/// <remarks>
///Wir verwnden zwei Mateiaien:
/// - ein Material, das die Ber�hrung anzeigt
/// - ein Material, das anzeigt, dass das ber�hrte Objekt mitbewegt wird.
/// </remarks>
public class ControllerTouchHighlightAndMove : MonoBehaviour
{
    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des ber�hrten Objekts bei TriggerStay")]
    public Material HighlightMaterial;

    /// <summary>
    /// Farbe bei TriggerStay
    /// </summary>
    [Tooltip("Material des Trigger-Objekts w�hrend der Bewegung")]
    public Material MoveMaterial;

    /// <summary>
    /// Ausl�sender Bewegung, falls Kollision vorliegt und Tastendruck
    /// </summary>
    public InputAction MoveAction;
    
    /// <summary>
    /// Soll eine Bewegung ausgef�hrt werden?
    /// </summary>
    private bool m_move = false;
    
    /// <summary>
    /// Material des ber�hrten Objekts f�r die Rekonstruktion.
    /// </summary>
    private Material otherOriginal;

    /// <summary>
    /// Das aktuelle ber�hrte Objekt
    /// </summary>
    private GameObject touchedObject = null;
    
    /// <summary>
    /// Loische Variale, auf der wir speichern, ob wir
    /// aktuell ein Objekt ber�hrt haben
    /// </summary>
    private bool m_touched = false;
    
    /// <summary>
    /// MeshRenderer des ber�hrten Objekts
    /// </summary>
    private MeshRenderer otherRenderer;

    /// <summary>
    /// Callback registrieren f�r den Tastendruck.
    /// </summary>
    private void Awake()
    {
        MoveAction.started += OnPress;
        MoveAction.canceled += OnRelease;
    }

    /// <summary>
    /// In Enable f�r die Szene aktivieren wir  unsere Action.
    /// </summary>
    private void OnEnable()
    {
        MoveAction.Enable();
    }
   
    /// <summary>
    /// In Disable f�r die Szene de-aktivieren wir  unsere Action.
    /// </summary>
    private void OnDisable()
    {
        MoveAction.Disable();
    }
    
    /// <summary>
    /// Callback f�r die  Action CastAction.
    ///<summary>
    private void OnPress(InputAction.CallbackContext ctx)
    {
        if (m_touched)
             m_move = ctx.ReadValueAsButton();
        else
            m_move = false;

    }

    /// <summary>
    /// Callback f�r die  Action CastAction.
    ///<summary>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        if (m_touched)
        {
            m_move = ctx.ReadValueAsButton();
            if (otherRenderer.material != HighlightMaterial)
                otherRenderer.material = otherOriginal;
            touchedObject.transform.SetParent(null);
        }
        else
            m_move = false;
    }
    
    /// <summary>
    /// Speichern des Materials des ber�hrten Objekts.
    /// </summary>
    /// <remarks>
    /// Man sieht keine Farb�nderung, da wir sofort TouchedMaterial aufrufen.
    /// Deshalb werden hier nur Materilien f�r die Rekonstruktion
    /// gespeichert!
    /// </remarks>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    private void OnTriggerEnter(Collider otherObject)
    {
        m_touched = true;
        touchedObject = otherObject.gameObject;
        otherRenderer = touchedObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        otherOriginal = otherRenderer.material as Material;
    }
    
    /// <summary>
    /// Das GameObject ist mit einem weiteren Objekt in der Szene kollidiert.
    /// Der Trigger-Event  hat auch im Frame vorher stattgefunden.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    private void OnTriggerStay(Collider otherObject)
    {
        touchedObject = otherObject.gameObject;
        otherRenderer = touchedObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;

       // Falls Tastendruck, dann f�hren wir ein parent aus und bewegen das Objekt mit!
       if (m_move)
       {
           touchedObject.transform.SetParent(transform, true);
           otherRenderer.material = MoveMaterial;
       }
       else
       {
           touchedObject.transform.SetParent(null);
           otherRenderer.material= HighlightMaterial; 
       }
    }
    
    /// <summary>
    /// Trigger-Event ist beendet. Materialien rekonstruieren.
    /// </summary>
    /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
    private void OnTriggerExit(Collider otherObject)
    {
        touchedObject = otherObject.gameObject;
        otherRenderer = touchedObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        otherRenderer.material = otherOriginal;
        touchedObject.transform.SetParent(null);
        touchedObject = null;
        m_touched = false;
    }
}
