//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine.InputSystem;

/// <summary>
/// RaycasWith Line  mit Input Actions f�r das Ausl�sen
/// </summary>
public class RaycastWithLineController : RaycastWithLine
{
    /// <summary>
    /// Ausl�sen eines Ray-Casts mit Tastendruck
    /// </summary>
    public InputAction CastAction;
    
    /// <summary>
    /// Callback registrieren f�r den Tastendruck.
    /// </summary>
    private void Awake()
    {
        CastAction.started += OnPress;
        CastAction.canceled += OnRelease;
    }

    /// <summary>
    /// In Enable f�r die Szene aktivieren wir  unsere Action.
    /// </summary>
    private void OnEnable()
    {
        CastAction.Enable();
    }
    
    /// <summary>
    /// Callback f�r die  Action CastAction.
    ///<summary>
    private void OnPress(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
    }

    /// <summary>
    /// Callback f�r die  Action CastAction.
    ///<summary>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
        lr.enabled = false;
    }
    
    
    /// <summary>
    /// In Disable f�r die Szene de-deaktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
        CastAction.Disable();
    }
}
