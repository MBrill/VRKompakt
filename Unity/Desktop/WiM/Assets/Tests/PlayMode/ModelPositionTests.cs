using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class ModelPositionTests
{
    /// <summary>
    ///  Konstruktor
    /// </summary>
    public  ModelPositionTests()
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
            "Assets/Scenes/TestsWiM.unity", 
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
        m_miniWorldPos = m_MiniWorld.transform.position;
        m_MiniWorldOrientation = m_MiniWorld.transform.rotation;
        m_Offset = GameObject.Find("Offset");
        m_OffsetPos = m_Offset.transform.localPosition;
    }
    
    /// <remarks>
    /// Für welche GameObjects sollen die Tests durchgeführt werden?
    /// </remarks>
    static string[] name = new string[] {"Kapsel", 
        "ScalingCube",
        "Flugzeugmodell",
        "Zylinderlinks1",
        "KugelLinksVorneKlein2"
    };
    
    // Testen, ob es die drei Modelle und MiniWorld gibt
    [UnityTest]
    public IEnumerator ModelObjectPairsExist([ValueSource("name")] string name)
    {
        var obj = GameObject.Find(name);
        var model = GameObject.Find(WiMUtilities.BuildModelName(name));
        
        NUnit.Framework.Assert.NotNull(obj);
        NUnit.Framework.Assert.NotNull(model);

        yield return null;
    }

    /// <summary>
    /// Testen, ob es die drei Modelle nach einem Refresh gibt
    /// </summary>
    [UnityTest]
    public IEnumerator ModelObjectPairsExistAfterRefresh([ValueSource("name")] string name)
    {
        var obj = GameObject.Find(name);
        var model = GameObject.Find(WiMUtilities.BuildModelName(name));
        
        NUnit.Framework.Assert.NotNull(obj);

        m_MiniWorld.GetComponent<WiM>().Refresh();
        yield return new WaitForFixedUpdate();
        
        NUnit.Framework.Assert.NotNull(model);
        yield return null;
    }

    /// <summary>
    /// Test, ob die Modellkoordinaten des Würfels korrekt sind.
    /// </summary>
    [UnityTest]
    public IEnumerator ModelPosition([ValueSource("name")] string name)
    {
        var obj = GameObject.Find(name).transform;
        var objectPos = obj.position;
        var model = GameObject.Find(WiMUtilities.BuildModelName(name));
        var modelPos = model.transform.position;
        var computerModelPos = WiMUtilities.WorldToModel(
            m_Scale,
            objectPos,
            m_miniWorldPos,
            m_MiniWorldOrientation,
            m_OffsetPos
        );
        
        NUnit.Framework.Assert.That(computerModelPos,
            Is.EqualTo(modelPos).Using(m_Comparer));
        yield return null;
    }
    
    /// <summary>
    /// Test, ob die Weltkoordinaten des Würfels korrekt aus den
    /// Modellkoordinaten berechnet werden.
    /// </summary>
    [UnityTest]
    public IEnumerator PositionFromModel([ValueSource("name")] string name)
    {
        var go = GameObject.Find(name);
        var goPos = go.transform.position;
        var modelGo = GameObject.Find(WiMUtilities.BuildModelName(name));
        var modelPos = modelGo.transform.position;
        var quat = m_MiniWorld.transform.rotation;
        var pos = WiMUtilities.ModelToWorld(
            m_Scale,
            modelPos,
            m_miniWorldPos,
            m_MiniWorldOrientation,
            m_OffsetPos
        );

        NUnit.Framework.Assert.That(pos,
            Is.EqualTo(goPos).Using(m_Comparer));
        yield return null;
    }

    /// <summary>
    /// GameObjects für die Tests
    /// </summary>
    private GameObject m_MiniWorld,
        m_Offset;

    /// <summary>
    /// Vektoren  für die Tests
    /// </summary>
    private Vector3 m_miniWorldPos,
        m_OffsetPos;

    /// <summary>
    /// Orientierung wiM
    /// </summary>
    private Quaternion m_MiniWorldOrientation;
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
