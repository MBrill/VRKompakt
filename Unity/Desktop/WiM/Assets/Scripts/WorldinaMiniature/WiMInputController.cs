using UnityEngine;
using UnityEngine.InputSystem;

public class WiMInputController : WiM
{
    /// <summary>
    /// Action f�r das Ein- und Ausblenden der Miniatur-Darstellung
    /// </summary>
    [Tooltip("Input Action f�r das Umschalten der Sichtbarkeit")]
    public InputAction ShowAction;
    
    /// <summary>
    /// Registrieren der Callbacks f�r ShowAction
    /// </summary>
    void Awake()
    {
        ShowAction.performed += OnShow;
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
        var result = ctx.ReadValueAsButton();
        if (result)
            ShowTheWim = !ShowTheWim;
        
        if (ShowTheWim)
            m_Create();
        else  
            Destroy(m_OffsetObject);
    }
}
