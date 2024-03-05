//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller f�r Player2D f�r die Verwendung eines
/// Input Asset.
/// </summary>
/// <remarks>
/// Version mit Protokollausgaben mit Log4Net.
/// </remarks>

public class PlayerControl2D : Player2D
{
	/// <summary>
	/// Callback f�r die Composite Action Move.
	///<summary>
    private void OnMove(InputValue value)
    {
	    var results = value.Get<Vector2>();
	    m_Delta = new Vector3(results.x, 0.0f, results.y);
	    m_Moving = m_Delta.magnitude > 0.0f;
	    


		    object[] args = {gameObject.name, 
		    m_Moving,
		    gameObject.transform.position.y,           
		    gameObject.transform.position.z,            
	    };
	    if (m_Moving)
	         Logger.Info("Verfolger wurde durch Tastendruck aktiviert");
	    // Her noch die Uhrzeit mit ausgeben.
    }
	
	private static readonly log4net.ILog Logger 
		= log4net.LogManager.GetLogger(typeof(FollowTheTarget));
}