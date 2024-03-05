using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ActionTests  : InputTestFixture
{
    /// <summary>
    /// Default-Konstruktor für die Testklasse
    /// </summary>
    /// <remarks>
    /// Als Genauigkeit für den Vergleich verwenden wir 0.001.
    /// </remarks>
    public ActionTests() => m_Accuracy = 0.001f;
    
    /// <summary>
    /// Setup, wir fügen die virtuelle Tastatur hinzu
    /// </summary>
    public override void Setup()
    {
        base.Setup();
       
    }
    
    /// <summary>
    /// Laden der Szene.
    /// </summary>
    /// <remarks>
    /// Die Szene muss in den Build Settings stehen!
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
    /// Testen, ob wir mit "P" oder einem anderen Binding die Eigenschaft
    /// IsFollowing auf true setzen können.
    /// </summary>
    [UnityTest]
    public IEnumerator ToggleFollowerMoveWithKeyboard()
    {
        yield return null;
        keyboard = InputSystem.AddDevice<Keyboard>();

        var follow =
            m_Follower.GetComponent<FollowTheTargetController>();
        var action = follow.FollowAction;
        // Verfolger abfragen und in der Komponente 
        // FollowTheTarget die Eigensdchaft IsFollowing
        // auf false setzen.

        Debug.Log(follow.IsFollowing);
        var expectedState = follow.IsFollowing;
        Debug.Log(expectedState);
        Press(keyboard.pKey);
        yield return new WaitForSeconds(1.0f);
        Debug.Log(follow.IsFollowing);
        
        yield return new WaitForSeconds(1.0f);
        //Release(keyboard.pKey);    
        //Debug.Log(follow.IsFollowing);
        //yield return new WaitForSeconds(1.0f);
        
        NUnit.Framework.Assert.AreEqual(!expectedState, follow.IsFollowing);
    }
    
    /// <summary>
    /// Test auf das Binding für die Bewegung nach links ("d")
    /// </summary>
    [UnityTest]
    public IEnumerator IsPlayerMovingToTheLeft()
    {
        yield return null;
        var xPlayer = m_Target.transform.position.x;

        Press(keyboard.aKey);
        yield return new WaitForSeconds(1.0f);
        Release(keyboard.aKey);
        yield return new WaitForSeconds(1.0f);
        var xPlayer2 = m_Target.transform.position.x;
        
        NUnit.Framework.Assert.That(xPlayer2, 
            Is.LessThan(xPlayer));
    }
    
    /// <summary>
    /// Gameobject für das verfolgte Objekt
    /// </summary>
    private GameObject m_Target;
    /// <summary>
    /// GameObject für den Verfolger
    /// </summary>
    private GameObject m_Follower;
    /// <summary>
    /// Instanz der Tastatur
    /// </summary>
    private Keyboard keyboard;
    /// <summary>
    /// Genauigkeit für den Vergleich von float-Werten
    /// </summary>
    private readonly float m_Accuracy;
}
