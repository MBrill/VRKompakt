//========= 2023 - 2024  - Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Eingabe-Controller f�r die Visualisierung einer Parameterkurve mit Hilfe
/// einer Instanz einer Klasse, die von PathAnimation abgeleitet ist.
/// </summary>
[RequireComponent(typeof(PathAnimation))]
public class PathAnimationController : MonoBehaviour
{
    /// <summary>
    /// Input Asset f�r die Entscheidung, ob wir die Parameterkurve durchlaufen oder nicht.
    /// </summary>
    public InputAction RunAction;
    
    /// <summary>
    /// Input Asset f�r die Entscheidung, ob wir die Parameterkurve mit
    /// Hilfe eines Polygonzugs visualisieren oder nicht.
    /// </summary>
    public InputAction ShowAction;
    
    /// <summary>
    /// Komponente WayPointManager abfragen und speichern.
    /// Wir fragen auch das erste Ziel ab.
    /// </summary>
    private void Awake()
    {         
        RunAction.performed += OnRun;
        ShowAction.performed += OnShow;
        
        m_Path = GetComponent<PathAnimation>();
    }
        
    /// <summary>
    /// Callback f�r das Schalten der Bewegung
    /// </summary>
    /// <remarks>
    /// Falls wir am letzten Punkt angekommen sind sorgen wir daf�r,
    /// dasswir beim Schalten der Bewegung wieder an den Anfangspunkt
    /// gesetzt werden!
    /// </remarks>
    private void OnRun(InputAction.CallbackContext ctx)
    {
        m_Path.Run = !m_Path.Run;
        m_Path.ResetCurve();
    }
     
    /// <summary>
    // Callback f�r die Visualisierung der Parameterkurve
    /// </summary>
    private void OnShow(InputAction.CallbackContext ctx)
    {
        m_Path.ShowTheCurve = !m_Path.ShowTheCurve;
    }
    
    /// <summary>
    /// In Enable f�r die Szene aktivieren wir auch unsere Action.
    /// </summary>
    private void OnEnable()
    {
        RunAction.Enable();
        ShowAction.Enable();
    }
        
    /// <summary>
    /// In Disable f�r die Szene de-aktivieren wir auch unsere Action.
    /// </summary>
    private void OnDisable()
    {
        RunAction.Disable();
        ShowAction.Disable();
    }

    /// <summary>
    /// Instanz der Parameterkurve.
    /// </summary>
    private PathAnimation m_Path;
}
