using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

/// <summary>
/// Test der Szene als Editor Test
/// </summary>
public class SceneTesting
{
    /// <summary>
    /// Default-Konstruktor f�r die Testklasse
    /// </summary>
    /// <remarks>
    /// Als Genauigkeit f�r den Vergleich verwenden wir 0.001,
    /// als Skalierungsfaktor f�r das Flugzeug erwarten wir 0.3.
    /// </remarks>
    public SceneTesting()
    {
        m_Accuracy = 0.001f;
        m_ExpectedPlaneScale = 0.3f;
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
    [Test]
    public void FollowerExists()
    {
        NUnit.Framework.Assert.NotNull(m_Follower);
    }
    
    /// <summary>
    /// Test ob es das GameObject mit dem Namen "Player"
    /// in der Szene gibt.
    /// </summary>
    [Test]
    public void PlayerExists()
    {
        NUnit.Framework.Assert.NotNull(m_Player);
    }
    
    /// <summary>
    /// Test ob der Player der Klasse FollowTheTarget
    /// hinzugef�gt wurde
    /// </summary>
    [Test]
    public void FollowerHasPlayer()
    {
        var comp = 
            m_Follower.GetComponent<FollowTheTarget>().playerTransform;
        NUnit.Framework.Assert.NotNull(comp);
    }
    
    /// <summary>
    /// Test ob wir dem Objekt Follower die Klasse SimpleAirPlane hinzugef�gt haben
    /// </summary>
    [Test]
    public void FollowerHasAirPlane()
    {
        var comp = 
            m_Follower.GetComponent<SimpleAirPlane>();
        NUnit.Framework.Assert.NotNull(comp);
    }
    
    /// <summary>
    /// Test ob der Skalierungsfaktor f�r den Airplane korrekt gesetzt ist.
    /// </summary>
    [Test]
    public void AirPlaneScaleIsCorrect()
    {
        var factor = 
            m_Follower.GetComponent<SimpleAirPlane>().ScalingFactor;
        NUnit.Framework.Assert.AreEqual(
            m_ExpectedPlaneScale,
            factor,
            m_Accuracy
            );
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
