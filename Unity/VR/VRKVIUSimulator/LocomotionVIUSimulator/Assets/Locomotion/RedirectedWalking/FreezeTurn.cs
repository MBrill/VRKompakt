//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// FreezeTurn Controller 
/// </summary>
///  <remarks>
/// Für den interaktiven Einsatz gibt es einen VIU-Controller!
/// </remarks>
public class FreezeTurn : RedirectionController
{
    [Header("Freeze-Turn")]
    
    /// <summary>
    ///  Freeze aktivieren/de-aktivieren
    /// </summary>
    public bool Active = false;
    /// <summary>
    ///So lange der Controller aktiv ist gehen wir davon aus,
    /// dass die Anwender "nach hinten" gehen, also Backup
    /// ausführen. Das bedeutet, dass wir kleinere z-Werte als
    /// vorher erhalten.
    ///
    /// Wir kompensieren diese y-Werte so, dass sich die Ansicht
    /// nicht verändert, mit dem entsprechenden Betrag des aktuellen z-Werts.
    /// </summary>
    protected override void Redirect()
    {
        if (!Active) return;
        gameObject.transform.RotateAround(
            TrackedObject.position,
            Vector3.up,
            m_FreezeValue-TrackedObject.rotation.eulerAngles.y);
    }

    /// <summary>
    /// Wir "frieren" den y-Eulerwinkels  ein zu dem Zeitpunkt,
    /// als dieser Controller aktiviert wird.
    /// </summary>
    protected void Freeze()
    {
        m_FreezeValue = TrackedObject.rotation.eulerAngles.y;
    }

    /// <summary>
    /// Speicher für den Wert von z zu dem Zeitpunkt,
    /// an dem der Controller aktiviert wurde.
    /// </summary>
    private float m_FreezeValue;
}
