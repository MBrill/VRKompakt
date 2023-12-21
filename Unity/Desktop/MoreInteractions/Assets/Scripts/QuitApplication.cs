using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Die Anwendung mit dem ESC-Button beenden.
/// </summary>
/// <remarks>
/// Wir verwenden eine InputAction, die in dieser Klasse
/// integriert ist. Das Binding f�r die Tasten kann im Inspector
/// ver�ndert werden.
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
    /// Eine Action hat verschiedene Zust�nde, f�r
    /// die wir Callbacks regristieren k�nnen.
    /// Wir k�nnten wie in der Unity-Dokumentation
    /// teilweise gezeigt das hier gleich mit implementieren.
    /// Hier entscheiden wir uns daf�r, die Funktion
    /// OnPress zu registrieren, die wir implementieren
    /// und die den Wert von IsFollowing toggelt.
    /// </summary>
    private void Awake()
    {
        QuitAction.performed += ctx => OnQuit();
    }
    
        /// <summary>
        /// In Enable f�r die Szene aktivieren wir auch unsere Action.
        /// </summary>
        private void OnEnable()
        {
    	    QuitAction.Enable();
        }
        
        /// <summary>
        /// In Disable f�r die Szene de-aktivieren wir auch unsere Action.
        /// </summary>
        private void OnDisable()
        {
            QuitAction.Disable();
        }

        /// <summary>
        /// Diese Funktion ist als Callback f�r die InputAction QuitAction
        /// registriert und wir aufgerufen, wenn der im Inspector
        /// definierte Button verwendet wird.
        /// </summary>
        private void OnQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}