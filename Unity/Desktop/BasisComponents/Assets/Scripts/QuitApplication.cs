using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{

    private void OnQuit(InputValue value)
    {
        m_stop = value.isPressed;
    }
    //// <summary>
    /// Die Taste mit dem Input-Manager abfragen.
    /// </summary>
    /// <remarks>
    /// In dieser Version verwenden wir keine Actions, sondern
    /// direkt die Taste. Dafür bräuchten wir diese Klasse nicht.
    /// </remarks>
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
