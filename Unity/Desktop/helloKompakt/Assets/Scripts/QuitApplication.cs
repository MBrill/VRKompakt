/// <summary>
/// Erstes Beispiel einer C# Klasse in einer Unity-Anwendung
/// </summary>
using UnityEngine;
public class QuitApplication : MonoBehaviour {
   void Update () {
      if (Input.GetKeyUp(KeyCode.Escape)) {
          Application.Quit();
          #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
          #endif
      }
  }
}
