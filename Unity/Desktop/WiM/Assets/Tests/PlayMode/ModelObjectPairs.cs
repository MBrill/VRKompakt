using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ModelObjectPairs
{
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
    /// Setup f�r die Szene - wir suchen die Objekte �ber die Namen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_MiniWorld = GameObject.Find("MiniWorld");
    }
    
    /// <summary>
    /// Liste von Objekten f�r die die Tests durchgef�hrt werden sollen.
    /// </summary>
    static string[] name = new string[] {"Kapsel", 
        "ScalingCube",
        "Flugzeugmodell",
        "Zylinderlinks1",
        "KugelLinksVorneKlein2"
    };
    
/// <summary>
/// Test, ob es f�r die Objekte in der Szene die Modell-Objekte gibt
/// </summary>
/// <param name="name">Name des Szenen-Objekts</param>
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
    /// Mit der Refresh-Funktion in WiM erzeugen wir das Modell neu.
    /// In diesem Test �berpr�fen wir das Ergebnis dieser Funktion.
    /// </summary>
    /// <param name="name">Name des Szenen-Objekts</param>
    [UnityTest]
    public IEnumerator ModelObjectPairsExistAfterRefresh([ValueSource("name")] string name)
    {
        var obj = GameObject.Find(name);
        NUnit.Framework.Assert.NotNull(obj);
        m_MiniWorld.GetComponent<WiM>().Refresh();
        yield return new WaitForFixedUpdate();
        var model = GameObject.Find(WiMUtilities.BuildModelName(name));        
        NUnit.Framework.Assert.NotNull(model);
        yield return null;
    }

    /// <summary>
    /// GameObjects f�r die Tests
    /// </summary>
    private GameObject m_MiniWorld;
}
