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
public class QuitApplicationInputAsset : MonoBehaviour
{

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