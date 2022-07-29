using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

/// <summary>
/// Test der Szene
/// </summary>
public class SceneTestingInPlaymode
{
    /// <summary>
    /// Default-Konstruktor f�r die Testklasse
    /// </summary>
    /// <remarks>
    /// Als Genauigkeit f�r den Vergleich verwenden wir 0.001,
    /// als Skalierungsfaktor f�r das Flugzeug erwarten wir 0.3.
    /// </remarks>
    public SceneTestingInPlaymode()
    {
        m_Accuracy = 0.001f;
        m_ExpectedPlaneScale = 0.3f;
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
    /// Test ob es das GameObject mit dem Namen "Follower"
    /// in der Szene gibt.
    /// </summary>
    [UnityTest]
    public IEnumerator FollowerExists()
    {
        NUnit.Framework.Assert.NotNull(m_Follower);
        yield return null;
    }
    
    /// <summary>
    /// Test ob es das GameObject mit dem Namen "Player"
    /// in der Szene gibt.
    /// </summary>
    [UnityTest]
    public IEnumerator PlayerExists()
    {
        NUnit.Framework.Assert.NotNull(m_Player);
        yield return null;
    }
    
    /// <summary>
    /// Test ob der Player der Klasse FollowTheTarget
    /// hinzugef�gt wurde
    /// </summary>
    [UnityTest]
    public IEnumerator FollowerHasPlayer()
    {
        var comp = 
            m_Follower.GetComponent<FollowTheTarget>().playerTransform;
        NUnit.Framework.Assert.NotNull(comp);
        yield return null;
    }
    
    /// <summary>
    /// Test ob wir dem Objekt Follower die Klasse SimpleAirPlane hinzugef�gt haben
    /// </summary>
    [UnityTest]
    public IEnumerator FollowerHasAirPlane()
    {
        var comp = 
            m_Follower.GetComponent<SimpleAirPlane>();
        NUnit.Framework.Assert.NotNull(comp);
        yield return null;
    }
    
    /// <summary>
    /// Test ob der Skalierungsfaktor f�r den Airplane korrekt gesetzt ist.
    /// </summary>
    [UnityTest]
    public IEnumerator AirPlaneScaleIsCorrect()
    {
        var factor = 
            m_Follower.GetComponent<SimpleAirPlane>().ScalingFactor;
        NUnit.Framework.Assert.AreEqual(
            m_ExpectedPlaneScale,
            factor,
            m_Accuracy
            );
        yield return null;
    }
    
    /// <summary>
    /// Gameobject f�r den Player
    /// </summary>
    private GameObject m_Player;
    /// <summary>
    /// GameObject f�r den Follower
    /// </summary>
    private GameObject m_Follower;
    /// <summary>
    /// Erwarteter Wert f�r die Gr��e des Flugzeugmodells
    /// </summary>
    private readonly float m_ExpectedPlaneScale;
    /// <summary>
    /// Genauigkeit f�r den Vergleich von float-Werten
    /// </summary>
    private readonly float m_Accuracy;
}
