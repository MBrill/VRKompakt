using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using UnityEngine.SceneManagement;


/// <summary>
/// Testklasse f�r die Szene und ihre Elemente
/// </summary>
public class FirstSceneTest
{
    /// <summary>
    /// Default-Konstruktor f�r die Testklasse
    /// </summary>
    /// <remarks>
    ///Wir besetzen die erwartete Position mit (11.5, 1, 1),
    /// und instanzieren eine Instanz eines Vector3-Vergleichers.
    /// Als Genauigkeit f�r den Vergleich verwenden wir 0.001.
    /// </remarks>
    public FirstSceneTest()
    {
        m_ExpectedPos= new Vector3(1.5f, 1.0f, 1.0f);
        // Genauigkeit f�r den Vergleich der Positionen      
        const float accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(accuracy);    
    }

    /// <summary>
	/// Die beiden GameObjects zuweisen
	/// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Floor = GameObject.Find("Floor");
        m_Cube = GameObject.Find("ScalingCube");
    }
	
    /// <summary>
    /// Test ob es das GameObject mit dem Namen "floor"
    /// in der Szene gibt.
    /// </summary>
    [Test]
    public void FloorTest()
    {
        NUnit.Framework.Assert.NotNull(m_Floor);
    }
    
    /// <summary>
    /// Test ob es das GameObject mit dem Namen "ScalingCube"
    /// in der Szene gibt.
    /// </summary>
    [Test]
    public void CubeTest()
    {
        NUnit.Framework.Assert.NotNull(m_Cube);
    }
    
    /// <summary>
    /// Test der Position des Objekts ScalingCube
    /// </summary>
    [Test]
    public void CubePosition()
    {
        NUnit.Framework.Assert.That(m_Cube.transform.position, 
            Is.EqualTo(m_ExpectedPos).Using(m_Comparer));
    }
    
    /// <summary>
    /// Instanz der Vergleichsfunktion f�r Vector3 in Unity
    /// </summary>
    private readonly Vector3EqualityComparer m_Comparer;
    /// <summary>
    /// Erwartete Position des W�rfels mit dem Namen ScalingCube
    /// </summary>
    private readonly Vector3 m_ExpectedPos;
    /// <summary>
    /// Gameobject f�r den Boden
    /// </summary>
    private GameObject m_Floor;
     /// <summary>
     /// GameObject f�r den W�rfel "Scalingcube"
     /// </summary>
    private GameObject m_Cube;
}
