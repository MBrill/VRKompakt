//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// TranslationalGain Controller 
/// </summary>
public class TranslationalGain : RedirectionController
{
   [Header("Translational Gain")]
    
    /// <summary>
    /// Wert für den Translations Gain.
    /// </summary>
    /// <remarks>
    /// Die Grenzen für den Range stammen aus den Veröffentlichungen
    /// zum Thema Detection Thresholds.
    /// </remarks>
    [Tooltip("Faktor für die Redirection")] 
    [Range(0.8f, 1.3f)]
    public float Gain = 1.0f;

    protected void Awake()
    {
        m_LastValue = TrackedObject.localPosition.z;
    }
    
    /// <summary>
    /// Die Redirection anwenden.
    /// </summary>
    protected override void Redirect()
    {
        var diff = TrackedObject.localPosition.z - m_LastValue;

        if (Mathf.Abs(diff) > Mathf.Epsilon)
        {
            var redirection = new Vector3(0.0f,
                0.0f,
                (Gain - 1.0f) * diff);
            gameObject.transform.position += redirection;
        }
        m_LastValue = TrackedObject.localPosition.z;
    }

    /// <summary>
    /// Speicher für den Vorgänger-Wert des tegtrackten Objekts.
    /// </summary>
    private float m_LastValue;
}
