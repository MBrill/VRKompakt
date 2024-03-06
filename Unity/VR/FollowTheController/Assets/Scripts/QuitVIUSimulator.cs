//========= 2022 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Den VIU-Simulator auf dem Desktop mit einem Button beenden.
/// </summary>
/// <remarks>
/// Ziel dieser Klasse ist insbesondere, ein Build des Simulators, das
/// Full-Screen läuft sinnvoll ohne Einsatz des Task-Managers zu beenden.
/// 
/// Wir verwenden die Klasse Input und die Buttons, die
/// im Input-Manager definiert sind. Wir verwenden aktuell noch nicht
/// das neue Input-System.
/// 
/// Wir finden logische Namen wie Submit, Cancel, Fire<x>
/// oder Jump und die Belegung dafür definiert. Der Vorteil dieser Methode
/// ist, dass wir nicht nur physikalisch vorhandene Tasten, sondern
/// auch Joystick-Buttons verwenden können falls sie vorhanden sind.
///
///  Wir müssen etwas anderes als
/// den ESC-Button verwenden, da dieser im VIU-Simulator bereits
/// für das Pausieren der Anwendung eingesetzt wird. Anschließend
/// kann dann im Inspektor der Play-Button deaktiviert werden.
/// 
/// Die Fire<x>-Buttons scheiden aus, da ´die Maustasten in der
/// Simulatoin der Controller eingesetzt werden.
 ///
 /// Deshalb fällt die Entscheidung aktuell für "Jump". Auf dem Keyboard
 /// entspricht dies der Pause-Taste!
 /// </remarks>
public class QuitVIUSimulator : MonoBehaviour
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
