//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Vector3.MoveTowards und Transform.LookAt.
/// </summary>
/// <remarks>
/// Protokollausgaben mit Log4Net.
/// </remarks>
public class FollowTheTarget : MonoBehaviour
{
    /// <summary>
    /// Position und Orientierung des verfolgten Objekts
    /// </summary>
    [Tooltip("Das verfolgte Objekt")]
    public Transform PlayerTransform;

    /// <summary>
    /// Wir können das Verfolgen an- und ausschalten.
    /// </summary>
    public bool IsFollowing = false;

    /// <summary>
    /// Geschwindigkeit des Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit")]
    [Range(1.0F, 20.0F)]
    public float Speed = 10.0F;

    /// <summary>
    /// Instanz einesLog4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(FollowTheTarget));

    /// <summary>
    /// Bewegung in Update
    /// </summary>
    private void Update ()
    {
        if (!IsFollowing) return;
        transform.position = Vector3.MoveTowards(transform.position,
            PlayerTransform.position,
            Speed * Time.deltaTime);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(PlayerTransform);
        
        object[] args = {gameObject.name, 
            gameObject.transform.position.x,
            gameObject.transform.position.y,           
            gameObject.transform.position.z,            
        };
        Logger.InfoFormat("{0}; {1}; {2}; {3}", args);
    }

    /// <summary>
    /// Callback für die Action Following im Input Asset
    /// </summary>
    private void OnFollowing()
    {
        IsFollowing = !IsFollowing;
    }
}
