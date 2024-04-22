//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;

/// <summary>
/// Walk mit VIU. Die Fortbewegung  startet mit einem Tastendruck
/// und kann durch den gleichen Tastendruck wieder beendet werden.
/// </summary>
public class WalkVIUSteady : WalkVIU
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
