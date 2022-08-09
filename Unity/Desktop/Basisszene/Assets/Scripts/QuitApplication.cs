//========= 2020 - 2022 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

namespace VRKL.MBU
{
    /// <summary>
    /// Die Anwendung mit dem Cancel-Button beenden.
    /// <remarks>
    /// Wir verwenden die Klasse Input und die Buttons, die
    /// im Input-Manager definiert sind. Diese Belegungen erhalten 
    /// wir mit Edit -> Project Settings -> input Manager.
    /// 
    /// Dort werden logische Namen wie "Submit, Cancel oder Jump
    /// und die Buttons dafür definiert. Der Vorteil dieser Methode
    /// ist, dass wir nicht nur physikalisch vorhandene Tasten, sondern
    /// auch Joystick-Buttons verwenden können wenn sie vorhanden sind.
    /// 
    /// Default ist "Cancel", was im Normalfall auf der Tastatus
	/// der Escape-Taste entspricht.
    /// </remarks>
    /// </summary>
    public class QuitApplication : MonoBehaviour
    {
        /// <summary>
        /// Die Taste mit dem Input-Manager abfragen.
        /// </summary>
        private void Update()
        {

            if (Input.GetButton("Cancel"))
            {
                Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            }
        }
    }
}

/*! \mainpage
* Eine mit doxygen erstellte HTML-Dokumentation des Unity-Packages MBU.
*
* Beispiele für die Verwendung der Klassen und Assets finden Sie im Verzeichnis
* Examples.
 */
