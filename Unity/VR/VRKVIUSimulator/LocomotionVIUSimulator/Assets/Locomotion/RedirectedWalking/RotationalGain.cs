//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// RotationalGain Controller 
/// </summary>
public class RotationalGain : RedirectionController
{
    [Header("Rotational Gain")]
    
    /// <summary>
    /// Wert für den Translations Gain.
    /// </summary>
    /// <remarks>
    /// Die Grenzen für den Range stammen aus den Veröffentlichungen
    /// zum Thema Detection Thresholds.
    /// Bei der oberen Grenze lassen wir auch 2.0 zu, damit
    /// wir die Resetter implementieren können.
    /// </remarks>
    [Tooltip("Faktor für die Redirection")] 
    [Range(0.7f, 2.0f)]
    public float Gain = 1.0f;

    protected void Awake()
    {
        m_LastValue = TrackedObject.localRotation.eulerAngles.y;
    }
    
    /// <summary>
    /// Die Redirection anwenden.
    /// </summary>
    protected override void Redirect()
    {
        // Eulerwinkel werden in Grad verwaltet!
        var diff = TrackedObject.localRotation.eulerAngles.y - m_LastValue;

        if (Mathf.Abs(diff) > Mathf.Epsilon)
        {
            gameObject.transform.RotateAround(
                TrackedObject.position,
                Vector3.up,
                (Gain - 1.0f) * diff);
        }
        m_LastValue = TrackedObject.localRotation.eulerAngles.y;
    }

    /// <summary>
    /// Speicher für den Vorgänger-Wert des tegtrackten Objekts.
    /// </summary>
    private float m_LastValue;
}
