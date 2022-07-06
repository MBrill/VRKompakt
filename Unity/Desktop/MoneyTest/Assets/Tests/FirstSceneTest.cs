using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using UnityEngine.SceneManagement;


public class FirstSceneTest
{
    /// <summary>
    /// Default-Konstruktor für die Testklasse
    /// </summary>
    /// <remarks>
    ///Wir besetzen die erwartete Position mi t(11.5, 1, 1),
    /// und instanzieren eine Instanz eines Vector3-Vergleichers.
    /// Als Genauigkeit für den Vergleich verwenden wir 0.001.
    /// </remarks>
    public FirstSceneTest()
    {
        m_ExpectedPos= new Vector3(1.5f, 1.0f, 1.0f);
        // Genauigkeit für den Vergleich der Positionen      
        var accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(accuracy);    
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
            "Assets/Scenes/myFirstScene.unity", 
            LoadSceneMode.Single);
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
    [UnityTest]
    public IEnumerator FloorTest()
    {
        NUnit.Framework.Assert.NotNull(m_Floor);
        yield return null;
    }
    
    /// <summary>
    /// Test ob es das GameObject mit dem Namen "ScalingCube"
    /// in der Szene gibt.
    /// </summary>
    [UnityTest]
    public IEnumerator CubeTest()
    {
        NUnit.Framework.Assert.NotNull(m_Cube);
        yield return null;
    }
    
    /// <summary>
    /// Test der Position des Objekts ScalingCube
    /// </summary>
    [UnityTest]
    public IEnumerator CubePosition()
    {
        NUnit.Framework.Assert.That(m_Cube.transform.position, 
            Is.EqualTo(m_ExpectedPos).Using(m_Comparer));
        yield return null;
    }
    
    /// <summary>
    /// Instanz der Vergleichsfunktion für Vector3 in Unity
    /// </summary>
    private readonly Vector3EqualityComparer m_Comparer;
    /// <summary>
    /// Erwartete Position des Würfels mit dem Namen ScalingCube
    /// </summary>
    private readonly Vector3 m_ExpectedPos;
    /// <summary>
    /// Gameobject für den Boden
    /// </summary>
    private GameObject m_Floor;
     /// <summary>
     /// GameObject für den Würfel "Scalingcube"
     /// </summary>
    private GameObject m_Cube;
}
