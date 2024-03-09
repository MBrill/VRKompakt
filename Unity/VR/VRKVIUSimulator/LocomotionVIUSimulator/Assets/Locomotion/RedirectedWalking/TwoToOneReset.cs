//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// FreezeTurn Controller 
/// </summary>
///  <remarks>
/// Für den interaktiven Einsatz gibt es einen VIU-Controller!
/// </remarks>
public class TwoToOneReset : RedirectionController
{
    [Header("2:1 Reset")]
    
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
        // Eulerwinkel werden in Grad verwaltet!
        // Das entspricht einem RotationalController mit gain = 2.
        gameObject.transform.RotateAround(
                TrackedObject.position,
                Vector3.up,
                TrackedObject.localRotation.eulerAngles.y - m_LastValue);
        m_LastValue = TrackedObject.localRotation.eulerAngles.y;
    }

    /// <summary>
    /// Speicher für den Vorgänger-Wert des tegtrackten Objekts.
    /// </summary>
    private float m_LastValue;
}
