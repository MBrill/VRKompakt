//========= 2023 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller für das ein- und ausblenden der CCC mit dem Keyboard.
/// Diese Klasse realisiert die Interaktionen auf der Basis des Input Systems.
/// </summary>
public class ActivateCCCKeyboardController : ActivateCCC
{
    /// <summary>
    /// Action für das Ein- und Ausblendendes CCC
    /// </summary>
    /// <remarks>
    /// Diese Klasse wird als Komponente einem anderen GameObject
    /// hinzugefügt.
    ///
    /// Damit wird sicher gestellt, dass wir CCC in der
    /// Szene erst bei Bedarf einblenden können.
    /// </remarks>
    [Tooltip("Input Action für das Umschalten der Sichtbarkeit")]
    public InputAction ShowAction;
    
    /// <summary>
    /// Verbindung zu CCC in der Szene aufnehmen und InputManager konfigurieren.
    /// </summary>
    private void Awake()
    {
        FindTheCCC();
        if (TheCCC)
        {
            ShowAction.canceled += OnRelease;
            if (Show)
            {
                TheCCC.SetActive(true);
            }
        }
    }

    /// <summary>
    /// In Enable für die Szene aktivieren wir die  Action.
    /// </summary>
    private void OnEnable()
    {
        ShowAction.Enable();
    }
    
    /// <summary>
    /// In Disable für die Szenede aktivieren wir die  Action.
    /// </summary>
    private void DisEnable()
    {
        ShowAction.Disable();
    }
    
    /// <summary>
    /// Callback für die  Action ShowAction.
    ///<summary>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        Show = !Show;
        if (Show)
            TheCCC.SetActive(true);
        else
            TheCCC.SetActive(false);
    }
}
