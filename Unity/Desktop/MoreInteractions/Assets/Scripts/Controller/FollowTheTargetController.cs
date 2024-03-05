//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Controller-Klasse für FollowTheTarget
/// </summary>
public class FollowTheTargetController : FollowTheTarget
{
    /// <summary>
    /// Callback für die Action Following im Input Asset
    /// </summary>
    private void OnFollowing()
    {
        IsFollowing = !IsFollowing;
    }
}
