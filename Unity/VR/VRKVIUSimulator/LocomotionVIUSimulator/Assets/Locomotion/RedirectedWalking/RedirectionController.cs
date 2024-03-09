//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse für einenRedirection Controller bei RDW,.
/// </summary>
public abstract class RedirectionController : MonoBehaviour
{
    [Header("Redirected Walking")]
    /// <summary>
    /// Welches GameObject erhält die Tracker-Daten?
    /// </summary>
    /// <remarks>
    /// Dieses Objekt könnten wir aus der Hierarchie auslesen.
    /// Aber so ist es transparenter.
    /// </remarks>
    [Tooltip("Objekt miit den Trackerdaten)")]
    public Transform TrackedObject;
    
    /// <summary>
    /// Update aufrufen und die Skalierung ausführen,
    /// falls sie aktiv ist,
    /// </summary>
    protected virtual void Update()
    {
        Redirect();
    }

    /// <summary>
    /// Die Redirection anwenden.
    /// </summary>
    protected abstract void Redirect();
}
