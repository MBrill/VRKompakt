using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Die Anwendung mit dem ESC-Button beenden.
/// </summary>
/// <remarks>
/// Wir verwenden das neue InputSystem
/// und eine Component PlayerInput.
/// Dort stellen wir ein, dass wir das Asset mit den
/// Actions und den Bindings verwenden.
/// Wir verwenden "Send Messages" und implementieren
/// in dieser Klasse die Funktion "OnQuit".
/// </remarks>
public class QuitApplication : MonoBehaviour
{
    /// <summary>
    /// Wir folgen der Dokumentation von Unity
    /// und dem Abschnitt "Embedding Actions in MonoBehaviours".
    ///
    /// Im Inspektor  erzeugen ir eine Composite-Action,
    /// die als Ergebnis einen Vector2D erzeugt. 
    /// </summary>
    public InputAction QuitAction;
    
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
        QuitAction.performed += OnQuit;
    }
    
        /// <summary>
        /// In Enable für die Szene aktivieren wir auch unsere Action.
        /// </summary>
        private void OnEnable()
        {
    	    QuitAction.Enable();
        }
        
        /// <summary>
        /// In Disable für die Szene de-aktivieren wir auch unsere Action.
        /// </summary>
        private void OnDisable()
        {
            QuitAction.Disable();
        }

        /// <summary>
        /// Diese Funktion ist als Callback für die InputAction QuitAction
        /// registriert und wir aufgerufen, wenn der im Inspector
        /// definierte Button verwendet wird.
        /// </summary>
        private void OnQuit(InputAction.CallbackContext ctx)
        {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
}
