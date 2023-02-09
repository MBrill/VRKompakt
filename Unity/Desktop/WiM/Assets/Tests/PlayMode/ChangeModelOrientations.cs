using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class ChangeModelOrientations
{
   /// <summary>
    ///  Konstruktor
    /// </summary>
    public  ChangeModelOrientations()
    {
        m_Accuracy = 0.001f;
        m_Comparer = new QuaternionEqualityComparer(m_Accuracy );
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

    private static string[] name = new string[] {"Kapsel", 
        "ScalingCube",
        "ZylinderRechtsHinten"
    };
    
    /// <summary>
    /// Ver�ndern der Orientierung des Modell-Objekts
    /// und tsicher stellen, dass auch das Szenen-Objekt
    /// ver�dnert wurde.
    /// </summary>
    /// <param name="name">Name des Szenen-Objekts</param>
    [UnityTest]
    public IEnumerator Orientations([ValueSource("name")] string name)
    {
        var modelName = WiMUtilities.BuildModelName(name);
        var model = GameObject.Find(modelName);

        var delta = Quaternion.AngleAxis(30.0f, Vector3.up);
        // Orientierung Modell-Objekt ver�ndern
        model.transform.localRotation = delta;
            
        m_MiniWorld.GetComponent<WiM>().TransferModelOrientation(model);
        yield return new WaitForFixedUpdate();
        // Testen, ob die Positionen korrekt sind.
        var go = GameObject.Find(name);
        NUnit.Framework.Assert.That(go.transform.rotation,
            Is.EqualTo(model.transform.localRotation).Using(m_Comparer));

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
    private QuaternionEqualityComparer m_Comparer;
}
