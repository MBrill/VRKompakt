/// <summary>
/// Erstes Beispiel einer C# Klasse in einer Unity-Anwendung
/// </summary>
using UnityEngine;

public class QuitApplication : MonoBehaviour {

    /// <summary>
    /// Taste, mit der die Anwendung beendet wird.
    /// 
    /// Häufig wird hierfür ESC verwendet. Dies ist
    /// aber bei der Verwendung der Vive Input Utility
    /// keine gute Wahl, da diese Taste schon im Simulator
    /// für das Pausieren der Simulation besetzt ist!
    /// </summary>
    public KeyCode quitKey = KeyCode.Backspace;

    void Update () {
      if (Input.GetKeyUp(quitKey)) {
          Application.Quit();
          #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
          #endif
      }
  }
}
