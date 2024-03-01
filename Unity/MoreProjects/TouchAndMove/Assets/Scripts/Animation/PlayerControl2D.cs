//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller-Klasse mit einer InputAction für die Bewegung eines
/// Objekts in x- und z-Richtung in der Klasse Player2D.
/// </summary>
public class PlayerControl2D : Player2D
{
	/// <summary>
	/// InputAsset, die im Inspektor definiert werden kann.
	/// </summary>
	/// <remarks>
	/// Im Inspektor  erzeugen wir eine Composite-Action,
	/// die als Ergebnis einen Vector2D erzeugt. 
	/// </remarks>
	public InputAction PlayAction;

/// <summary>
    /// Registrieren des Callbacks
    /// </summary>
    private void Awake()
    {
	    PlayAction.performed += OnMove;
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir auch unsere Action.
    /// </summary>
    private void OnEnable()
    {
	    PlayAction.Enable();
    }
    
    /// <summary>
    /// In Disable für die Szene de-aktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
	    PlayAction.Disable();
    }

    /// <summary>
	/// Callback für die Composite Action PlayAction.
	///<summary>
    private void OnMove(InputAction.CallbackContext ctx)
    {
	    // Wir führen eine Bewegung durch solange Taste gedrückt wird.
	    // Bei Press erhalten wir True, bei Release erhalten wir False.
	    m_Moving = ctx.control.IsPressed();
	    var results = ctx.ReadValue<Vector2>();
	    m_Delta = new Vector3(results.x, 0.0f, results.y);
    }
}