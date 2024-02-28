using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Existenz der Objekte überprüfen und einige Komponenten
/// checken.
/// </summary>
public class Objects
{
   /// <summary>
    /// Default-Konstruktor
    /// </summary>
    /// <remarks>
    /// Als Genauigkeit für den Vergleich verwenden wir 0.001,
    /// als Skalierungsfaktor für das Flugzeug erwarten wir 1.0.
    /// </remarks>
    public Objects()
    {
        m_Accuracy = 0.001f;
        m_ExpectedPlaneScale = 1.0f;
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
    /// Test ob es das GameObject mit dem Namen "Flugzeugmodell"
    /// in der Szene gibt.
    /// </summary>
    [Test]
    public void FollowerExists()
    {
        NUnit.Framework.Assert.NotNull(m_Follower);
    }
    
    /// <summary>
    /// Test ob es das GameObject mit dem Namen "Kapsel"
    /// in der Szene gibt.
    /// </summary>
    [Test]
    public void TargetExists()
    {
        NUnit.Framework.Assert.NotNull(m_Target);
    }
    
    /// <summary>
    /// Test ob das verfolgte Objekte die Komponente  FollowTheTarget besitzt
    /// </summary>
    [Test]
    public void FollowerHasPlayer()
    {
        var comp = 
            m_Follower.GetComponent<FollowTheTarget>().PlayerTransform;
        NUnit.Framework.Assert.NotNull(comp);
    }

    /// <summary>
    /// Test ob wir als verfolgtes Objekt "Kapsel" eingestellt haben
    /// </summary>
    [Test]
    public void PlayerIsKapsel()
    {
        const string expectedPlayer = "Kapsel";
        var comp = 
            m_Follower.GetComponent<FollowTheTarget>().PlayerTransform;
        ;NUnit.Framework.Assert.AreEqual(expectedPlayer, comp.name);
    }
    
    /// <summary>
    /// Test ob das verfolgte Objekt die Komponente PlayerControl2D besitzt.
    /// </summary>
    [Test]
    public void TargetHasControl()
    {
        var comp =
            m_Target.GetComponent<PlayerControl2D>();
        NUnit.Framework.Assert.NotNull(comp);
    }
    
    /// <summary>
    /// Test ob das verfolgte Objekt die Komponente PlayerControl2D besitzt.
    /// </summary>
    [Test]
    public void TargetControlHasBounds()
    {
        const string expectedBounds = "Boden";
        var comp =
            m_Target.GetComponent<PlayerControl2D>().Bounds;
        ;NUnit.Framework.Assert.AreEqual(expectedBounds, comp.name);
    }
    
    /// <summary>
    /// Test ob der Skalierungsfaktor für das Flugzeugmodell  korrekt gesetzt ist.
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
    /// Gameobject für den Player
    /// </summary>
    private GameObject m_Target;
    /// <summary>
    /// GameObject für den Follower
    /// </summary>
    private GameObject m_Follower;
    /// <summary>
    /// Erwarteter Wert für die Größe des Flugzeugmodells
    /// </summary>
    private readonly float m_ExpectedPlaneScale;
    /// <summary>
    /// Genauigkeit für den Vergleich von float-Werten
    /// </summary>
    private readonly float m_Accuracy;
}
