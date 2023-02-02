using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

/// <summary>
/// Tests für das Löschen von Modellen.
/// </summary>

public class DeleteTest
{
    public DeleteTest()
    {
        m_name = "Kapsel_Modell";
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
            "Assets/Scenes/BasisWiM.unity", 
            LoadSceneMode.Single);
    }
    /// <summary>
    /// Setup für die Szene - wir suchendasModell über die Namen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_MiniWorld = GameObject.Find("MiniWorld");
        m_ModelCapsule = GameObject.Find(m_name);
    }
    
    /// <summary>
    /// Löschen des Modells "Kapsel_Modell" und überprüfen,
    /// dass danach auch das Objekt "Kapsel" nicht mehr vorhanden ist.
    /// </summary>
    [UnityTest]
    public IEnumerator DeleteCapsule()
    {
        m_MiniWorld.GetComponent<WiM>().DeleteObjectFromModelName(
            m_name);
        yield return new WaitForFixedUpdate();
        // Testen, ob jetzt auch "Kapsel" nicht mehr existiert      
        var go = GameObject.Find("Kapsel");
        NUnit.Framework.Assert.Null(go);

        yield return null;
    }

    /// <summary>
    /// Name des gelöschten Objekts in der Szene
    /// </summary>
    /// <remarks>
    /// Wird im Konstruktor gesetzt, auf "Kapsel_Modell".
    /// </remarks>
    private string m_name; 
    /// <summary>
    /// Position des Root-Objekts von WiM
    /// </summary>
    private GameObject m_MiniWorld;

    /// <summary>
    /// GameObject des Modells
    /// </summary>
    /// <returns></returns>
    private GameObject m_ModelCapsule;
}
