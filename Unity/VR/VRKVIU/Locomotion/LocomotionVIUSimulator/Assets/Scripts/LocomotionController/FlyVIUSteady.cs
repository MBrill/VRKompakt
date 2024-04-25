//========= 2021 - 20243 Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;

/// <summary>
/// Fly mit VIU. Die Fortbewegung  startet mit einem Tastendruck
/// und kann durch den gleichen Tastendruck wieder beendet werden.
/// </summary>
public class FlyVIUSteady : FlyVIU
{
    /// <summary>
    /// Fly wird so lange durchgeführt bis der Trigger-Button
    /// erneut  gedrückt wird.
    /// </summary>
    protected override void Trigger()
    {
        if (ViveInput.GetPressUp(moveHand, moveButton))
            Moving = !Moving;
    }
}
