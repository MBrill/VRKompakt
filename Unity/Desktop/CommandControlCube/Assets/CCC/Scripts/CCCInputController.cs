using UnityEngine;
using UnityEngine.InputSystem;

public class CCCInputController : CCC
{
    /// <summary>
    /// Action f�r das Ein- und Ausblendendes CCC
    /// </summary>
    [Tooltip("Input Action f�r das Umschalten der Sichtbarkeit")]
    public InputAction ShowAction;
    
    /// <summary>
    /// Registrieren der Callbacks f�r ShowAction
    /// </summary>
    protected override void Awake()
    {
        ShowAction.performed += OnShow;
        base.Awake();
    }
    
    /// <summary>
    /// In Enable f�r die Szene aktivieren wir die Action.
    /// </summary>
    void OnEnable()
    {
        ShowAction.Enable();
    }
    
    /// <summary>
    /// In Disable f�r die Szene deaktivieren wir die Action.
    /// </summary>
    void OnDisable()
    {
        ShowAction.Disable();
    }
  
        
    /// <summary>
    /// Callback f�r das Togglen der Anzeige der WiM
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
