//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller-Klasse für FollowTheTarget
/// </summary>
public class FollowTheTargetController : FollowTheTarget
{
    /// <summary>
    /// Wir erzeugen eine Button-Action.
    /// Das Binding verwendet  aktuell das Keyboard
    ///  und die Taste P.
    /// </summary>
    public InputAction FollowAction;
    
    /// <summary>
    /// Eine Action hat verschiedene Zustände, für
    /// die wir Callbacks regristieren können.
    /// Wir könnten wie in der Unity-Dokumentation
    /// teilweise gezeigt das hier gleich mit implementieren.
    /// Hier entscheiden wir uns dafür, die Funktion
    /// OnPress zu registrieren, die wir implementieren
    /// und die den Wert von IsFollowing toggelt.
    /// </summary>
    private void Awake()
    {
        FollowAction.performed += OnPress;
        FollowAction.canceled += OnRelease;
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir auch unsere Action.
    /// </summary>
    private void OnEnable()
    {
        FollowAction.Enable();
    }
    
    /// <summary>
    /// In Disable für die Szene de-aktivieren wir auch unsere Action.
    /// </summary>
    private void OnDisable()
    {
        FollowAction.Disable();
    }

    private void OnPress(InputAction.CallbackContext ctx)
    {
        IsFollowing = true;
    }
    
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        IsFollowing = false;
    }  
}
