//========= 2023 - 2024 --  Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Implementierung eines einfachen Portals
/// </summary>
public class SimplePortal : Portal
{
    /// <summary>
    /// Wir berechnen den Abstand als euklidischen Abstand
    /// zwischen aktueller Positoin und der Position des Portals.
    /// </summary>
    /// <returns>Abstand zum Portal</returns>
    protected override float ComputeDistance()
    {
        return 0.0f;
    }
}
