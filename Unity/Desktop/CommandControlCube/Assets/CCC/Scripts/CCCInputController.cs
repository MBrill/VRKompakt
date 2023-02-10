using UnityEngine;
using UnityEngine.InputSystem;

public class CCCInputController : CCC
{
    /// <summary>
    /// Action für das Ein- und Ausblendendes CCC
    /// </summary>
    [Tooltip("Input Action für das Umschalten der Sichtbarkeit")]
    public InputAction ShowAction;
    
    /// <summary>
    /// Registrieren der Callbacks für ShowAction
    /// </summary>
    protected override void Awake()
    {
        ShowAction.performed += OnShow;
        base.Awake();
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir die Action.
    /// </summary>
    void OnEnable()
    {
        ShowAction.Enable();
    }
    
    /// <summary>
    /// In Disable für die Szene deaktivieren wir die Action.
    /// </summary>
    void OnDisable()
    {
        ShowAction.Disable();
    }
  
        
    /// <summary>
    /// Callback für das Togglen der Anzeige der WiM
    /// </summary>
    /// <param name="ctx"></param>
    private void OnShow(InputAction.CallbackContext ctx)
    {
        //var result = ctx.ReadValueAsButton();
        //if (result)
            ToggleShow();
    }
    
    /// <summary>
    /// CCC ein- oder ausblenden.
    /// </summary>
    protected void ToggleShow()
    {
        Debug.Log("In toggleShow)");
        isCCCVisible = !isCCCVisible;
        Show();
    }
}
