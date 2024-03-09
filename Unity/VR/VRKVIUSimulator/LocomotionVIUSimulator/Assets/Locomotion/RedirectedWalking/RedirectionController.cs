//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Abstrakte Basisklasse f�r einenRedirection Controller bei RDW,.
/// </summary>
public abstract class RedirectionController : MonoBehaviour
{
    [Header("Redirected Walking")]
    /// <summary>
    /// Welches GameObject erh�lt die Tracker-Daten?
    /// </summary>
    /// <remarks>
    /// Dieses Objekt k�nnten wir aus der Hierarchie auslesen.
    /// Aber so ist es transparenter.
    /// </remarks>
    [Tooltip("Objekt miit den Trackerdaten)")]
    public Transform TrackedObject;
    
    /// <summary>
    /// Update aufrufen und die Skalierung ausf�hren,
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
