//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// FreezeBackup Controller 
/// </summary>
///  <remarks>
/// Für den interaktiven Einsatz gibt es einen VIU-Controller!
/// </remarks>
public class FreezeBackup : RedirectionController
{
    [Header("Freeze-Backup")]
    
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
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            m_FreezeValue - TrackedObject.localPosition.z);
    }

    /// <summary>
    /// Wir "frieren" den z-Wert ein zu dem Zeitpunkt,
    /// als dieser Controller aktiviert wird.
    /// </summary>
    /// <remarks>
    /// Wir frieren die Weltkoordinaten des getrackten Objekts
    /// ein, damit wir mehr als einmal den Controller durchführen können.
    protected void Freeze()
    {
        m_FreezeValue = TrackedObject.position.z;
    }

    /// <summary>
    /// Speicher für den Wert von z zu dem Zeitpunkt,
    /// an dem der Controller aktiviert wurde.
    /// </summary>
    private float m_FreezeValue;
}
