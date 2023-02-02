using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

/// <summary>
/// Unit-Tests für die Root-Objects in der Basis-Szene
/// </summary>
/// <remarks>
/// Wir führen die Tests aus für die Objekte
/// -ScalingCube,
/// Flugzeugmodell,
/// Kapsel.
/// Die Tests werden im EditMode ausgeführt!
/// </remarks>
public class ObjectsTest
{
    /// <summary>
    ///  Konstruktor
    /// </summary>
    public ObjectsTest()
    {
        const float accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(accuracy);
        
        expectedCube = new Vector3(2.0f, 0.5f, 2.0f);
        expectedAirplane = new Vector3(-1.1f, 0.65f, -1.1f);
        expectedCapsule = new Vector3(0.6f, 0.65f, 0.6f);
    }
    
    /// <summary>
    /// Setup für die Szene - wir suchen die Objekts über die Namen
    /// </summary>
    /// <returns></returns>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Cube = GameObject.Find("ScalingCube");
        m_Airplane = GameObject.Find("Flugzeugmodell");
        m_Capsule = GameObject.Find("Kapsel");
    }
    
    /// <summary>
    /// Test, ob der Wrürfel mit dem Namen ScalingCube existiert
    /// </summary>
    [Test]
    public void CubeExists()
    {
        NUnit.Framework.Assert.NotNull(m_Cube);
    }

    /// <summary>
    /// Test der Würfelposition
    /// </summary>
    [Test]
    public void CubePosition()
    {
        NUnit.Framework.Assert.That(m_Cube.transform.position,
            Is.EqualTo(expectedCube).Using(m_Comparer));
    }

    /// <summary>
    /// Test, ob das Flugzeugmodell existiert
    /// </summary>
    [Test]
    public void AirplaneExists()
    {
        NUnit.Framework.Assert.NotNull(m_Cube);
    }
   
    /// <summary>
    /// Test der Position des Flugzeugmodells
    /// </summary>
    [Test]
    public void AirPlanePosition()
    {
        NUnit.Framework.Assert.That(m_Airplane.transform.position,
            Is.EqualTo(expectedAirplane).Using(m_Comparer));
    }
    
    /// <summary>
    /// Test, ob das Flugzeugmodell existiert
    /// </summary>
    [Test]
    public void CapsuleExists()
    {
        NUnit.Framework.Assert.NotNull(m_Capsule);
    }
   
    /// <summary>
    /// Test der Position des Flugzeugmodells
    /// </summary>
    [Test]
    public void CapsulePosition()
    {
        NUnit.Framework.Assert.That(m_Capsule.transform.position,
            Is.EqualTo(expectedCapsule).Using(m_Comparer));
    }
    
    /// <summary>
    /// GameObjects für die Tests
    /// </summary>
    private GameObject m_Cube,
        m_Airplane,
        m_Capsule;
    /// <summary>
    /// Erwartete Positionen der drei GameObjects
    /// </summary>
    private readonly Vector3 expectedCube,
        expectedAirplane,
        expectedCapsule;
    
    /// <summary>
    /// Vergleichsklasse für Unity-Klasse Vector3
    /// </summary>
    private Vector3EqualityComparer m_Comparer;
}
