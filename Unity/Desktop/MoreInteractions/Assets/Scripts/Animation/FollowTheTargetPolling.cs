using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Transform.MoveTowards und Transform.LookAt.
/// </summary>
/// 
public class FollowTheTargetPolling : MonoBehaviour
{
    /// <summary>
    /// Position und Orientierung des verfolgten Objekts
    /// </summary>
    [Tooltip("Das verfolgte Objekt")]
    public Transform playerTransform;

    /// <summary>
    /// Wir können das Verfolgen an- und ausschalten.
    /// </summary>
    public bool IsFollowing = false;
    
    /// <summary>
    /// Geschwindigkeit des Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit")]
    [Range(1.0F, 20.0F)]
    public float speed = 10.0F;
    
    
    /// <summary>
    /// Bewegung in Update
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: Überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void Update ()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
            IsFollowing = !IsFollowing;
        
        if (!IsFollowing) return;
        transform.position = Vector3.MoveTowards(transform.position,
            playerTransform.position,
            speed * Time.deltaTime);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(playerTransform);
    }
}
