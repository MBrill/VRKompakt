//========= 202 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Beenden einer VIU-Anwendung mit Hilfe des Keyboards
/// </summary>
public class QuitVIU : MonoBehaviour
{ 
    /// <summary>
    /// Die Taste mit dem Input-Manager abfragen.
    /// </summary>
    private void Update()
    {
        if (VIUSettings.activateSimulatorModule && Input.GetButton(STOP_BUTTON))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    /// <summary>
    /// Button für das Beenden des Simulators
    /// </summary>
    private const string STOP_BUTTON = "Jump";
}
