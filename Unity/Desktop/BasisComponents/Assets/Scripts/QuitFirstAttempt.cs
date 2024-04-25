using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Die Anwendung mit dem ESC-Button beenden.
/// <remarks>
/// Wir verwenden das neue InputSystem und fragen
/// als ersten Ansatz die Taste konkret ab.
/// </remarks>
/// </summary>
public class QuitFirstAttempt : MonoBehaviour
{
    //// <summary>
    /// Die Taste mit dem Input-System abfragen.
    /// </summary>
    /// <remarks>
    /// In dieser Version verwenden wir keine Actions, sondern
    /// direkt die Taste. 
    /// </remarks>
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
