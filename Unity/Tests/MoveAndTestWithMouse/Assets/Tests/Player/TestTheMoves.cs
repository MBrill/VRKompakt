using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

/// <summary>
/// Test ob die Mausbewegungen korrekt auf das
/// gesteuerte Objekt übertragen werden.
/// </summary>
public class TestTheMoves : InputTestFixture
{
    /// <summary>
    /// Implementieren der Funktion Setup und
    /// die Maus und Tastatur hinzufügen.
    /// </summary>
    public override void Setup()
    {
        base.Setup();
        mouse = InputSystem.AddDevice<Mouse>();
        keyboard = InputSystem.AddDevice<Keyboard>();
    }
    
    /// <summary>
    /// Laden der Szene "moveAndTest"..
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
    /// Das GameObjects zuweisen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Player = GameObject.Find("Player");
    }
    
    /// <summary>
    /// Test auf eine Bewegung nach links.
    /// </summary>
    /// <remarks>
    /// Linke Maustaste muss gedrückt sein und die Taste A.
    /// </remarks>
    [UnityTest]
    public IEnumerator IsPlayerMovingToTheLeft()
    {
        yield return null;
        var xPlayer = m_Player.transform.position.x;

        Press(mouse.leftButton);        
        Press(keyboard.aKey);
        yield return new WaitForSeconds(0.1f);  
        
        Release(mouse.leftButton);
        yield return new WaitForSeconds(0.1f);  
        
        var xPlayer2 = m_Player.transform.position.x;
        
        NUnit.Framework.Assert.That(xPlayer, 
                                         Is.LessThan(xPlayer2));
    }
    
    /*/// <summary>
    /// Test auf die rechte Cursor-Taste
    /// </summary>
    [UnityTest]
    public IEnumerator IsPlayerMovingToTheRight()
    {
        yield return null;
        var xPlayer = m_Player.transform.position.x;

        Press(mouse.rightArrowKey);
        yield return new WaitForSeconds(0.1f);
        Release(mouse.rightArrowKey);
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

        Press(mouse.upArrowKey);
        yield return new WaitForSeconds(0.1f);
        Release(mouse.upArrowKey);
        yield return new WaitForSeconds(0.1f);
        var xPlayer2 = m_Player.transform.position.z;
        
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

        Press(mouse.downArrowKey);
        yield return new WaitForSeconds(0.1f);
        Release(mouse.downArrowKey);
        yield return new WaitForSeconds(0.1f);
        var xPlayer2 = m_Player.transform.position.z;
        
        NUnit.Framework.Assert.That(xPlayer2, 
            Is.LessThan(xPlayer));
    }*/
    
    /// <summary>
    /// Gameobject für den Player
    /// </summary>
    private GameObject m_Player;
    /// <summary>
    /// GameObject für den Follower
    /// </summary>
    private GameObject m_Follower;

    private Mouse mouse;

    private Keyboard keyboard;
}
