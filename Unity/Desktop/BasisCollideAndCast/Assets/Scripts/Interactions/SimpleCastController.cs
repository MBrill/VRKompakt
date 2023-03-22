using UnityEngine.InputSystem;

/// <summary>
/// SimpleCast mit Input Actions für das Auslösen
/// </summary>
public class SimpleCastController : SimpleCast
{
    /// <summary>
    /// Auslösen eines Ray-Casts mit Tastendruck
    /// </summary>
    public InputAction CastAction;
    
     
    /// <summary>
    /// Callback registrieren für den Tastendruck.
    /// </summary>
    private void Awake()
    {
        CastAction.started += OnPress;
        CastAction.canceled += OnRelease;
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir  unsere Action.
    /// </summary>
    private void OnEnable()
    {
        CastAction.Enable();
    }
    
    /// <summary>
    /// Callback für die  Action CastAction.
    ///<summary>
    private void OnPress(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
    }
    
    /// <summary>
    /// Callback für die  Action CastAction.
    ///<summary>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
    }
    
    /// <summary>
    /// In Disable für die Szene de-deaktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
        CastAction.Disable();
    }
    
    
}
