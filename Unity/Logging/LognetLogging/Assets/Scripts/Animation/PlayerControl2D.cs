//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller für Player2D für die Verwendung eines
/// Input Asset.
/// </summary>

public class PlayerControl2D : Player2D
{
	/// <summary>
	/// Callback für die Composite Action Move.
	///<summary>
    private void OnMove(InputValue value)
    {
	    var results = value.Get<Vector2>();
	    m_Delta = new Vector3(results.x, 0.0f, results.y);
	    m_Moving = m_Delta.magnitude > 0.0f;

    }
}