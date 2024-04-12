using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using UnityEngine.SceneManagement;

/// <summary>
/// Tests für die LineRenderer-Komponente, die im PlayMode angelegt wird.
/// </summary>
public class LineRendererTests
{
    public LineRendererTests()
    {
        const float m_Accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(m_Accuracy);
        m_ExpectedP1 = new Vector3(0.6f, 0.65f, 0.6f);
    }
    
    /// <summary>
    /// Laden der Szene.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene(
            "Assets/Scenes/RopeLine.unity", 
            LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Verbinden der getesteten Objekte
    /// </summary>
    /// <returns></returns>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Kapsel = GameObject.Find("Kapsel");
        m_Controller = GameObject.Find("XRControllerRight");
        var distance = 
            m_Kapsel.GetComponent<RopeLineController>().DistanceToTarget;
        var dir = m_Controller.transform.position - m_Kapsel.transform.position;

        m_ExpectedP2 = m_ExpectedP1 + (1.0f - distance) * dir;
    }
    
    /// <summary>
    /// Test, ob der das Objekt Kaösel nach Start der Anwendung
    /// eine Komponente vom Typ LineRenderer besitzt
    /// </summary>
    [Test]
    public void LineRenderer()
    {
        NUnit.Framework.Assert.NotNull( m_Kapsel.GetComponent(typeof(LineRenderer)));
    }
    
    /// <summary>
    /// Test, ob der Startpunkt der Linie korrekt ist
    /// </summary>
    [UnityTest]
    public IEnumerator LinePoint1()
    {
        yield return null;
        
        var lrComp = m_Kapsel.GetComponent(typeof(LineRenderer))
            as LineRenderer;
        var p1 = lrComp.GetPosition(0);

        NUnit.Framework.Assert.That(p1,
            Is.EqualTo(m_ExpectedP1).Using(m_Comparer));
    }
    
    /// <summary>
    /// Test, ob der Endpunkt der Linie korrekt ist
    /// </summary>
    [UnityTest]
    public IEnumerator LinePoint2()
    {
        yield return null;
        
        var lrComp = m_Kapsel.GetComponent(typeof(LineRenderer))
            as LineRenderer;
        var p1 = lrComp.GetPosition(1);

        NUnit.Framework.Assert.That(p1,
            Is.EqualTo(m_ExpectedP2).Using(m_Comparer));
    }
    
    private GameObject m_Kapsel;
    private GameObject m_Controller;
    
    private Vector3 m_ExpectedP1, m_ExpectedP2;
    private readonly Vector3EqualityComparer m_Comparer;
}
