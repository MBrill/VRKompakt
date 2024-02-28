using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

/// <summary>
/// Test, ob die Verfolgung ausgeführt wird, falls wir das Attribut
/// <Running> auf true setzen im Verfolger.
/// </summary>
public class PlayerIsRunning
{
    /// <summary>
    /// Laden der Szene.
    /// </summary>
    /// <remarks>
    ///Die Szene muss in den Build Settings enthalten sein!
    /// </remarks>
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene(
            "Assets/Scenes/Basis.unity", 
            LoadSceneMode.Single);
    }

    /// <summary>
    /// Die beiden GameObjects zuweisen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Follower = GameObject.Find("Flugzeugmodell");
        m_Target = GameObject.Find("Kapsel");
    }
    
    /// <summary>
    /// Test ob der Follower sich dem Ziel  nähert
    /// </summary>
    [UnityTest]
    public IEnumerator FollowerMoves()
    {
        yield return null;
        
        // Verfolger abfragen und in der Komponente 
        // FollowTheTarget die Eigensdchaft IsFollowing
        // auf false setzen.
        var running = 
            m_Follower.GetComponent<FollowTheTarget>().IsFollowing;
        // Verfolgung deaktivieren
        running = false;
        // Distanz zwischen Ziel und Verfolger berechnen
        var posPlayer = m_Target.transform.position;
        var posFollower = m_Follower.transform.position;
        var distance = (posPlayer - posFollower).magnitude;
        // Jetzt die Verfolgung aktivieren
        running = true;
        
        // FixedUpdate abwarten und dann Positionen neu abfragen
        yield return new WaitForSeconds(1.0f);
        posPlayer = m_Target.transform.position;
        posFollower = m_Follower.transform.position;
        var distance2 =(posPlayer - posFollower).magnitude;
        
        // Jetzt sollte die Distanz zwischen den Objekten kleiner
        // geworden sein.
        NUnit.Framework.Assert.That(distance2, 
            Is.LessThan(distance));
    }
    
    /// <summary>
    /// Gameobject für das Ziel der Verfolgung
    /// </summary>
    private GameObject m_Target;
    /// <summary>
    /// GameObject für den Verfolger
    /// </summary>
    private GameObject m_Follower;  
}
