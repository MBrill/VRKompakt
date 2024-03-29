﻿//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Vector3.MoveTowards und Transform.LookAt.
/// </summary>
/// <remarks>
/// Version mit Protokollausgaben mit Log4Net.
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
    /// Bewegung in Update
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: Überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void Update ()
    {
        if (!IsFollowing) return;
        transform.position = Vector3.MoveTowards(transform.position,
            PlayerTransform.position,
            Speed * Time.deltaTime);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(PlayerTransform);
        
        // Protokollausgaben der Positionen, falls die Verfolgung aktiviert ist
        var time = System.DateTime.Now;
        object[] args = {
            time,
            gameObject.name, 
            gameObject.transform.position.x,
            gameObject.transform.position.y,           
            gameObject.transform.position.z,            
        };
        Logger.InfoFormat("{0:mm::ss}; {1}; {2:F}; {3:F}, {4:F}", args);
        args = new object[] {
            time,
            PlayerTransform.name, 
            PlayerTransform.position.x,
            PlayerTransform.position.y,           
            PlayerTransform.position.z,            
        };
        Logger.InfoFormat("{0:mm::ss}; {1}; {2:F}; {3:F}, {4:F}", args);
        args = new object[] {
            time,
            "Distanz", 
            (PlayerTransform.position - gameObject.transform.position).magnitude    
        };
        Logger.InfoFormat("{0:mm::ss}; {1:G}; {2:F}", args);
    }
    
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(FollowTheTarget));
}
