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
    /// Falls im value aus dem Input System isPressed
    /// True ist stoppen wir die Anwendung.
    /// </summary>
    /// <param name="value"></param>
    private void OnQuit(InputValue value)
    {
        m_stop = value.isPressed;
    }
    
    //// <summary>
    /// Bool'sche Variable m_stop abfragen und reagieren.
    /// </summary>
    void Update()
    {
        if (m_stop)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    private bool m_stop = false;
}
