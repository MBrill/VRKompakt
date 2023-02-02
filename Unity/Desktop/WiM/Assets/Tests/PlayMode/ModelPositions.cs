using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

/// <summary>
/// Test der Umrechnung von Welt- in Modell-Positionen
/// </summary>
public class ModelPositions
{
    /// <summary>
    ///  Konstruktor
    /// </summary>
    public ModelPositions()
    {
        m_Accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(m_Accuracy );
    }

    /// <summary>
    /// Laden der Szene.
    /// </summary>
    /// <remarks>
    ///Die Szene muss in den Build Settings angegeben sein..
    /// </remarks>
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene(
            "Assets/Scenes/BasisWiM.unity", 
            LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Setup für die Szene - wir suchen die Objekte über die Namen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_MiniWorld = GameObject.Find("MiniWorld");
        m_Scale =  m_MiniWorld.GetComponent<WiM>().ScaleFactor;
        m_Cube = GameObject.Find("ScalingCube");
        m_ModelCube = GameObject.Find("ScalingCube_Modell");
        m_Airplane = GameObject.Find("Flugzeugmodell");
        m_ModelAirplane = GameObject.Find("Flugzeugmodell_Modell");
        m_Capsule = GameObject.Find("Kapsel");
        m_ModelCapsule = GameObject.Find("Kapsel_Modell");
    }
    
    // Testen, ob es die drei Modelle und MiniWorld gibt
    [UnityTest]
    public IEnumerator ModelObjectPairsExist()
    {
        NUnit.Framework.Assert.NotNull(m_MiniWorld);
        NUnit.Framework.Assert.NotNull(m_Cube);
        NUnit.Framework.Assert.NotNull(m_ModelCube);
        NUnit.Framework.Assert.NotNull(m_Airplane);
        NUnit.Framework.Assert.NotNull(m_ModelAirplane);
        NUnit.Framework.Assert.NotNull(m_Capsule);
        NUnit.Framework.Assert.NotNull(m_ModelCapsule);
        yield return null;
    }

    /// <summary>
    /// Testen, ob es die drei Modelle nach einem Refresh gibt
    /// </summary>
    [UnityTest]
    public IEnumerator ModelObjectPairsExistAfterRefresh()
    {
        m_MiniWorld.GetComponent<WiM>().Refresh();
        
        yield return new WaitForFixedUpdate();
        
        NUnit.Framework.Assert.NotNull(m_MiniWorld);
        NUnit.Framework.Assert.NotNull(m_Cube);
        NUnit.Framework.Assert.NotNull(m_ModelCube);
        NUnit.Framework.Assert.NotNull(m_Airplane);
        NUnit.Framework.Assert.NotNull(m_ModelAirplane);
        NUnit.Framework.Assert.NotNull(m_Capsule);
        NUnit.Framework.Assert.NotNull(m_ModelCapsule);
        yield return null;
    }
    
    /// <summary>
    /// Test, ob die Modellkoordinaten des Würfels korrekt sind
    /// </summary>
    [UnityTest]
    public IEnumerator CubeModelPosition()
    {
        var func = new functionCaller(
            m_MiniWorld.GetComponent<WiM>().WorldToModel);
        var modelPos = func(
            m_Scale,
            m_Cube.transform.position,
            m_MiniWorld.transform.position
        );
        NUnit.Framework.Assert.That(m_ModelCube.transform.position,
            Is.EqualTo(modelPos).Using(m_Comparer));
        yield return null;
    }
    
    /// <summary>
    /// Test, ob die Weltlkoordinaten des Würfels korrekt aus den
    /// Modellkoordinaten berechnet werden.
    /// </summary>
    [UnityTest]
    public IEnumerator CubePositionFromModel()
    {
        var func = new functionCaller(
            m_MiniWorld.GetComponent<WiM>().ModelToWorld);
        var pos = func(
            m_Scale,
            m_ModelCube.transform.position,
            m_MiniWorld.transform.position
        );
        NUnit.Framework.Assert.That(m_Cube.transform.position,
            Is.EqualTo(pos).Using(m_Comparer));
        yield return null;
    }
    
    /// <summary>
    /// Test, ob die Modellkoordinaten des Flugzeugs korrekt sind
    /// </summary>
    [UnityTest]
    public IEnumerator AirplaneModelPosition()
    {
        var func = new functionCaller(
            m_MiniWorld.GetComponent<WiM>().WorldToModel);
        var modelPos = func(
            m_Scale,
            m_Airplane.transform.position,
            m_MiniWorld.transform.position
        );
        NUnit.Framework.Assert.That(m_ModelAirplane.transform.position,
            Is.EqualTo(modelPos).Using(m_Comparer));
        yield return null;
    }
    
    /// <summary>
    /// Test, ob die Weltlkoordinaten des Flugzeugs korrekt aus den
    /// Modellkoordinaten berechnet werden.
    /// </summary>
    [UnityTest]
    public IEnumerator AirplanePositionFromModel()
    {
        var func = new functionCaller(
            m_MiniWorld.GetComponent<WiM>().ModelToWorld);
        var pos = func(
            m_Scale,
            m_ModelAirplane.transform.position,
            m_MiniWorld.transform.position
        );
        NUnit.Framework.Assert.That(m_Airplane.transform.position,
            Is.EqualTo(pos).Using(m_Comparer));
        yield return null;
    }
    
    /// <summary>
    /// Test, ob die Modellkoordinaten der Kapsel  korrekt sind
    /// </summary>
    [UnityTest]
    public IEnumerator CapsuleModelPosition()
    {
        var func = new functionCaller(
            m_MiniWorld.GetComponent<WiM>().WorldToModel);
        var modelPos = func(
            m_Scale,
            m_Capsule.transform.position,
            m_MiniWorld.transform.position
        );
        NUnit.Framework.Assert.That(m_ModelCapsule.transform.position,
            Is.EqualTo(modelPos).Using(m_Comparer));
        yield return null;
    }

    /// <summary>
    /// Test, ob die Weltlkoordinaten des Flugzeugs korrekt aus den
    /// Modellkoordinaten berechnet werden.
    /// </summary>
    [UnityTest]
    public IEnumerator CapsulePositionFromModel()
    {
        var func = new functionCaller(
            m_MiniWorld.GetComponent<WiM>().ModelToWorld);
        var pos = func(
            m_Scale,
            m_ModelCapsule.transform.position,
            m_MiniWorld.transform.position
        );
        NUnit.Framework.Assert.That(m_Capsule.transform.position,
            Is.EqualTo(pos).Using(m_Comparer));
        yield return null;
    }
    
    /// <summary>
    /// Delegate für die Umrechnungsfunktionen
    /// </summary>
    private delegate Vector3 functionCaller (
        float s, Vector3 mp, Vector3 mrp);
        
    /// <summary>
    /// GameObjects für die Tests
    /// </summary>
    private GameObject m_MiniWorld,
        m_Cube,
        m_Airplane,
        m_Capsule,
        m_ModelCube,
        m_ModelAirplane,
        m_ModelCapsule;

    /// <summary>
    /// Skalierungsfaktor in der Klasse WiM
    /// </summary>
    private float m_Scale;
    
   /// <summary>
   /// Genauigkeit für den Verbleich von float und Vector3
   /// </summary>
      private float m_Accuracy;

    /// <summary>
    /// Vergleichsklasse für Unity-Klasse Vector3
    /// </summary>
    private Vector3EqualityComparer m_Comparer;
}
