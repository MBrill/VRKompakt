using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestTheMoves : InputTestFixture
{
    public override void Setup()
    {
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();
    }
    /// <summary>
    /// Laden der Szene.
    /// </summary>
    /// <remarks>
    ///Die Szene muss in den Build Settings stehen.
    /// </remarks>
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene(
            "Assets/Scenes/moveAndTest.unity", 
            LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Die beiden GameObjects zuweisen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Follower = GameObject.Find("Follower");
        m_Player = GameObject.Find("Player");
    }
    
    /// <summary>
    /// Test ob der Follower sich dem Player nähert
    /// </summary>
    [UnityTest]
    public IEnumerator FollowerMoves()
    {
        yield return null;
        
        // Distanz zwischen Player und Follower berechnen
        var posPlayer = m_Player.transform.position;
        var posFollower = m_Follower.transform.position;
        var distance = (posPlayer - posFollower).magnitude;

        // FixedUpdate abwarten und dann Positionen neu abfragen
        yield return new WaitForFixedUpdate();
        posPlayer = m_Player.transform.position;
        posFollower = m_Follower.transform.position;
        var distance2 =(posPlayer - posFollower).magnitude;
        
        // Jetzt sollte die Distanz zwischen den Objekten kleiner
        // geworden sein.
        NUnit.Framework.Assert.That(distance2, 
                                                     Is.LessThan(distance));
    }
    
    /// <summary>
    /// Test auf die linke Cursor-Taste
    /// </summary>
    [UnityTest]
    public IEnumerator IsPlayerMovingToTheLeft()
    {
        yield return null;
        var xPlayer = m_Player.transform.position.x;

        Press(keyboard.leftArrowKey);
        yield return new WaitForSeconds(0.1f);
        Release(keyboard.leftArrowKey);
        yield return new WaitForSeconds(0.1f);
        var xPlayer2 = m_Player.transform.position.x;
        
        NUnit.Framework.Assert.That(xPlayer2, 
                                         Is.LessThan(xPlayer));
    }
    
    /// <summary>
    /// Test auf die rechte Cursor-Taste
    /// </summary>
    [UnityTest]
    public IEnumerator IsPlayerMovingToTheRight()
    {
        yield return null;
        var xPlayer = m_Player.transform.position.x;

        Press(keyboard.rightArrowKey);
        yield return new WaitForSeconds(0.1f);
        Release(keyboard.rightArrowKey);
        yield return new WaitForSeconds(0.1f);
        var xPlayer2 = m_Player.transform.position.x;
        
        NUnit.Framework.Assert.That(xPlayer2, 
            Is.GreaterThan(xPlayer));
    }
    
    /// <summary>
    /// Test auf die Cursor-Taste nach oben
    /// </summary>
    [UnityTest]
    public IEnumerator IsPlayerMovingUp()
    {
        yield return null;
        var xPlayer = m_Player.transform.position.z;

        Press(keyboard.upArrowKey);
        yield return new WaitForSeconds(0.1f);
        Release(keyboard.upArrowKey);
        yield return new WaitForSeconds(0.1f);
        var xPlayer2 = m_Player.transform.position.z;
        
        Debug.Log(xPlayer2);
        NUnit.Framework.Assert.That(xPlayer2, 
            Is.GreaterThan(xPlayer));
    }
    
    /// <summary>
    /// Test auf die Cursor-Taste nach unten
    /// </summary>
    [UnityTest]
    public IEnumerator IsPlayerMovingDown()
    {
        yield return null;
        var xPlayer = m_Player.transform.position.z;

        Press(keyboard.downArrowKey);
        yield return new WaitForSeconds(0.1f);
        Release(keyboard.downArrowKey);
        yield return new WaitForSeconds(0.1f);
        var xPlayer2 = m_Player.transform.position.z;
        
        Debug.Log(xPlayer2);
        NUnit.Framework.Assert.That(xPlayer2, 
            Is.LessThan(xPlayer));
    }
    
    /// <summary>
    /// Gameobject für den Player
    /// </summary>
    private GameObject m_Player;
    /// <summary>
    /// GameObject für den Follower
    /// </summary>
    private GameObject m_Follower;

    private Keyboard keyboard;
}
