//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// RaycasWithLine  mit Input Actions für das Auslösen
/// </summary>
public class RopeCastController : RopeCast
{
    /// <summary>
    /// Auslösen eines Ray-Casts mit Tastendruck
    /// </summary>
    public InputAction CastAction;
    
    /// <summary>
    /// Falls Rayasting ausgeführt wird aktivieren wir den Seilzug
    /// </summary>
    public InputAction RopeAction;
    
    /// <summary>
    /// Callback registrieren für den Tastendruck.
    /// </summary>
    private void Awake()
    {
        CastAction.started += OnPress;
        CastAction.canceled += OnRelease;
        RopeAction.started += OnRopeStarted;
        RopeAction.canceled += OnRopeCanceled;
    }

    /// <summary>
    /// In Enable für die Szene aktivieren wir  unsere Action.
    /// </summary>
    private void OnEnable()
    {
        CastAction.Enable();
        RopeAction.Enable();
    }
    
    /// <summary>
    /// Callback bei Button Press  für die  Action CastAction.
    ///<summary>
    private void OnPress(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
    }

    /// <summary>
    /// Callback bei Button Press für die  Action RopeAction.
    ///<summary>
    /// <remarks>
    /// Wir verwenden den Seilzug nur, falls Raycasting ausgeführt wird!
    /// </remarks>
    private void OnRopeStarted(InputAction.CallbackContext ctx)
    {
        if (!m_cast) return;
        m_rope = true;
    }
    
    /// <summary>
    /// Callback bei Cancel  für die  Action CastAction.
    ///<summary>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
    }
    
    /// <summary>
    /// Callback bei Cancel  für die  Action RopeAction.
    ///<summary>
    private void OnRopeCanceled(InputAction.CallbackContext ctx)
    {
        m_rope = false;
    }
    
    /// <summary>
    /// In Disable für die Szene de-deaktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
        CastAction.Disable();
        RopeAction.Disable();
    }
}