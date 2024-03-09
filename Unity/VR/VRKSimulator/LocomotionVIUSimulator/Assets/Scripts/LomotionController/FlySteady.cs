//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using HTC.UnityPlugin.Vive;

/// <summary>
/// Fly VIU.Die Fortbewegung  startet mit einem Tastendruck
/// und kann durch den gleichen Tastendruck wieder beendet werden.
/// </summary>
public class FlySteady : FlyVIUController
{
    /// <summary>
    /// Walk wird so lange durchgef�hrt bis der Trigger-Button
    /// wieder gedr�ckt wird.
    /// </summary>
    protected override void Trigger()
    {
        if (ViveInput.GetPressUp(moveHand, moveButton))
            Moving = !Moving;
    }
}
