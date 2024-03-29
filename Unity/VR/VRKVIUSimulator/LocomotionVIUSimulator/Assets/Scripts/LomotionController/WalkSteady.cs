//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using HTC.UnityPlugin.Vive;

/// <summary>
/// Walkmit VIU.Die Fortbewegung  startet mit einem Tastendruck
/// und kann durch den gleichen Tastendruck wieder beendet werden.
/// </summary>
public class WalkSteady : WalkVIUConroller
{
        /// <summary>
        /// Walk wird so lange durchgeführt bis der Trigger-Button
        /// wieder gedrückt wird.
        /// </summary>
        protected override void Trigger()
        {
            if (ViveInput.GetPressUp(moveHand, moveButton))
                Moving = !Moving;
        }
}
