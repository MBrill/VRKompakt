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
        var time = System.DateTime.Now;
        
        if (IsFollowing)
        {
            object[] args =
            {
                time,
                gameObject.name,
                "Start Verfolgung"
            };
            Logger.InfoFormat("{0:mm::ss}; {1:G}; {2:G}", args);
        }
        else
        {
            object[] args =
            {
                time,
                gameObject.name,
                "Stopp Verfolgung"
            };
            Logger.InfoFormat("{0:mm::ss}; {1:G}; {2:G}", args);          
        }

    }
    
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(FollowTheTargetController));
}
