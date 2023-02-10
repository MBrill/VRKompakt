using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

/// <summary>
/// Tests f�r das Bewegen eines Modell-Objekts.
/// </summary>
public class MoveTests
{
    /// <summary>
    ///  Konstruktor
    /// </summary>
    public  MoveTests()
    {
        m_Accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(m_Accuracy );
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
            "Assets/Scenes/TestsWiM.unity", 
            LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Setup f�r die Szene - wir suchendasModell �ber die Namen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_MiniWorld = GameObject.Find("MiniWorld");
    }

    /// <summary>
    /// Namen f�r die parametrisierten Tests
    /// </summary>

    static string[] name = new string[] {"Kapsel", 
        "ScalingCube",
        "ZylinderRechtsHinten"
    };
    
    /// <summary>
    ///Bewegen des Modell-objekts und �berpr�fen,
    /// dass auch das Modell-Objekt korrekt bewegt wurde.
    /// </summary>
    /// <param name="name">Name des Szenen-Objekts</param>
    [UnityTest]
    public IEnumerator Move([ValueSource("name")] string name)
    {
        var modelName = WiMUtilities.BuildModelName(name);
        var model = GameObject.Find(modelName);

        var delta = new Vector3(0.1f, 0.2f, 0.3f);
        // Den erwarteten Wert f�r beide Positionen bilden
        var newPos = model.transform.localPosition + delta;
            
        m_MiniWorld.GetComponent<WiM>().MoveModelAndObject(
            model,
            delta);
        yield return new WaitForFixedUpdate();
        // Testen, ob die Positionen korrekt sind.
        var go = GameObject.Find(name);
        
        // localPosition des Modelles und position des Szenen-Objekts
        // �berpr�fen!
        NUnit.Framework.Assert.That(go.transform.position,
            Is.EqualTo(newPos).Using(m_Comparer));
        NUnit.Framework.Assert.That(model.transform.localPosition,
            Is.EqualTo(newPos).Using(m_Comparer));
        NUnit.Framework.Assert.That(go.transform.position,
            Is.EqualTo(model.transform.localPosition).Using(m_Comparer));

        yield return null;
    }
    
    
    /// <summary>
    /// Position des Root-Objekts von WiM
    /// </summary>
    private GameObject m_MiniWorld;

    /// <summary>
    /// GameObject des Modells
    /// </summary>
    /// <returns></returns>
    private GameObject m_Model;
    
    /// <summary>
    /// Genauigkeit f�r den Verbleich von float und Vector3
    /// </summary>
    private float m_Accuracy;

    /// <summary>
    /// Vergleichsklasse f�r Unity-Klasse Vector3
    /// </summary>
    private Vector3EqualityComparer m_Comparer;
}
