//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Einen Seilzug mit einer von RopeAnimaiton
/// abgeleiteten Klasse mit Input Actions für das Auslösen
/// </summary>
public class RopeLineController : Line
{
    /// <summary>
    /// Aktivieren des Seilzugs
    /// </summary>
    public InputAction RopeShowAction;
    
    /// <summary>
    /// Aktivieren des Seilzugs
    /// </summary>
    public InputAction RopeAction;

    /// <summary>
    /// In Enable für die Szene aktivieren wir  unsere Action.
    /// </summary>
    /// <remarks>
    /// Start und Awake werden in den Basisklassen schon ausgeführt,
    /// deshalb die Registrierung der Callbacks in OnEnable().
    /// </remarks>
    private void OnEnable()
    {
        RopeAction.started += OnRope;

        RopeShowAction.started += OnRopeShow;
        
        RopeAction.Enable();
        RopeShowAction.Enable();
    }

    /// <summary>
    /// Callback bei Button Press für die  Action RopeAction.
    ///<summary>
    private void OnRope(InputAction.CallbackContext ctx)
    {
        Run = !Run;
    }

    /// <summary>
    /// Callback bei Button Press für die  Action RopeShowAction.
    ///<summary>
    private void OnRopeShow(InputAction.CallbackContext ctx)
    {
        ShowTheLine = !ShowTheLine;
    }

    /// <summary>
    /// In Disable für die Szene de-deaktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
        RopeAction.Disable();
        RopeShowAction.Disable();
    }
}