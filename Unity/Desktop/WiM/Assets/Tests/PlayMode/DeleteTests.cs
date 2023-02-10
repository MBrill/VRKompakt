using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/// <summary>
/// Tests f�r das L�schen von Modellen.
/// </summary>
public class DeleteTests
{
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
            "Assets/Scenes/BasisWiM.unity", 
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
    /// L�schen der Modelle und �berpr�fen,
    /// dass danach auch das Objekt "Kapsel" nicht mehr vorhanden ist.
    /// </summary>
   [UnityTest]
    public IEnumerator Delete([ValueSource("name")] string name)
    {
        var modelName = WiMUtilities.BuildModelName(name);
        var model = GameObject.Find(modelName);
        m_MiniWorld.GetComponent<WiM>().DeleteModelAndObject(model);
        yield return new WaitForFixedUpdate();
        // Testen, ob jetzt auch "Kapsel" nicht mehr existiert      
        model = GameObject.Find(modelName);
        var go = GameObject.Find(name);
        // Beide Objekte m�ssen gel�scht sein!
        NUnit.Framework.Assert.Null(model);
        NUnit.Framework.Assert.Null(go);

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
}
