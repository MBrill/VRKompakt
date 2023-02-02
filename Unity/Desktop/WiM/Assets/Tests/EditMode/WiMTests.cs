using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

/// <summary>
/// Tests für das Objekt MiniWorld und die Komponenten WiM.
/// </summary>
public class WiMTests
{
    /// <summary>
    ///  Konstruktor
    /// </summary>
    public WiMTests()
    {
        const float accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(accuracy);
        expectedMiniWorld = new Vector3(0.25f, 1.3f, -1.0f);
    }
    
    /// <summary>
    /// Setup für die Szene - wir suchen das Objekt über den Namen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_MiniWorld = GameObject.Find("MiniWorld");
    }
    
    /// <summary>
    /// Test, ob das Root-Objekt für WiM existiert..
    /// </summary>
    [Test]
    public void MiniWorldExists()
    {
        NUnit.Framework.Assert.NotNull(m_MiniWorld);
    }

    /// <summary>
    /// Test der Position des Objekts MiniWorld
    /// </summary>
    [Test]
    public void MiniWorldPosition()
    {
        NUnit.Framework.Assert.That(m_MiniWorld.transform.position,
            Is.EqualTo(expectedMiniWorld).Using(m_Comparer));
    }
    
    [Test]
    public void MiniWorldHasWiM()
    {
        var comp = m_MiniWorld.GetComponent<WiM>();
        NUnit.Framework.Assert.NotNull(comp);
    }
    
    [Test]
    public void WiMHasObjectList()
    {
        var comp = m_MiniWorld.GetComponent<WiM>().Objects;
        NUnit.Framework.Assert.NotNull(comp);
    }
    
    /// <summary>
    /// Überprüfen, ob alle Objekte korrekt in der Liste Objects enthalten sind.
    /// </summary>
    [Test]
    public void ObjectsList()
    {
        var objectsList = m_MiniWorld.GetComponent<WiM>().Objects;
        var namesList = objectsList.Select(go => go.name).ToList();
        List<string> expectedObjects = new List<string>
        {
            "ScalingCube",
            "Kapsel",
            "Zylinder",
            "BodenUndWände",
            "Flugzeugmodell",
            "KugelnLinks",
            "KastenUmKernbereich"
        };
        //NUnit.Framework.Assert.True(a);
        NUnit.Framework.CollectionAssert.AreEquivalent(
            expectedObjects,
            namesList);
    }
    
    /// <summary>
    /// GameObject MiniWorld
    /// </summary>
    private GameObject m_MiniWorld;

    /// <summary>
    /// Erwartete Positionen der drei GameObjects
    /// </summary>
    private readonly Vector3 expectedMiniWorld;

    /// <summary>
    /// Vergleichsklasse für Unity-Klasse Vector3
    /// </summary>
    private Vector3EqualityComparer m_Comparer;
}
