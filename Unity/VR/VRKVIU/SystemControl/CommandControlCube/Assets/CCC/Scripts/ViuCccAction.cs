//========= 2023 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller f�r das ein- und ausblenden der CCC mit dem Keyboard.
/// Diese Klasse realisiert die Interaktionen auf der Basis des Input Systems.
/// </summary>
public class ViuCccAction : ViuCCC
{
    /// <summary>
    /// Action f�r das Ein- und Ausblendendes CCC
    /// </summary>
    /// <remarks>
    /// Diese Klasse wird als Komponente einem anderen GameObject
    /// hinzugef�gt.
    ///
    /// Damit wird sicher gestellt, dass wir CCC in der
    /// Szene erst bei Bedarf einblenden k�nnen.
    /// </remarks>
    [Tooltip("Input Action f�r Ein- und Ausblenden")]
    public InputAction ShowAction;
    
    /// <summary>
    /// Verbindung zu CCC in der Szene aufnehmen und InputManager konfigurieren.
    /// </summary>
    private void Awake()
    {
        FindTheCCC();
        if (!TheCCC) return;
        ShowAction.canceled += OnRelease;
        if (Show)
            TheCCC.SetActive(true);
    }

    /// <summary>
    /// In Enable f�r die Szene aktivieren wir die  Action.
    /// </summary>
    private void OnEnable()
    {
        ShowAction.Enable();
    }
    
    /// <summary>
    /// In Disable f�r die Szenede aktivieren wir die  Action.
    /// </summary>
    private void DisEnable()
    {
        ShowAction.Disable();
    }
    
    /// <summary>
    /// Ein- und Ausblenden des CCC Prefabs.
    ///<summary>
    /// <remarks>
    /// Beim Einblenden des CCC verwenden wir die Position
    /// des ausgew�hlten Objekts f�r die Position des Prefabs.
    /// </remarks>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        Show = !Show;
        
        TheCCC.SetActive(Show);
        //if (Show) 
            //TheCCC.transform.position = PlacementObject.transform.position;
    }
}
