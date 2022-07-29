using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;


public class ConstructedScene
{
    /// <summary>
    /// Defaultkonstruktor
    /// </summary>
    public ConstructedScene()
    {
        m_ExpectedDistance12 = 5.0f;
        m_ExpectedDistance13 = 3.0f;
        m_ExpectedDistance23 = 4.0f;

        m_ExpectedVector1 = new Vector3(-1.0f, 0.0f, 0.0f);
        m_ExpectedVector2 = new Vector3(0.0f, -1.0f, 0.0f);
        
        m_Accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(m_Accuracy);
    }
    
    /// <summary>
    /// Wir erzeugen drei GameObjects, positionieren sie
    /// und orientieren zwei davon mit Hilfe von LookAt.
    /// </summary>
    [OneTimeSetUp]
    public void CreateScene()
    {
        objectOne = new GameObject("One");
        objectTwo = new GameObject("Two");
        objectThree = new GameObject("Three");
        
        objectOne.transform.position = new Vector3(1.5f, 0.0f, 0.0f);
        objectTwo.transform.position = new Vector3(-1.5f, 4.0f, 0.0f);
        objectThree.transform.position = new Vector3(-1.5f, 0.0f,0.0f);
        
        objectOne.transform.LookAt(objectThree.transform);
        objectTwo.transform.LookAt(objectThree.transform);
    }
    
    /// <summary>
    /// Test Abstand Objekte 1 und 2
    /// </summary>
    [Test]
    public void DistanceOneTwo()
    {
        var distance = 
            (objectTwo.transform.position - objectOne.transform.position).magnitude;
            
        NUnit.Framework.Assert.AreEqual(m_ExpectedDistance12,
            distance,
            m_Accuracy);
    }

    /// <summary>
    /// Test Abstand Objekte 2 und 3
    /// </summary>
    [Test]
    public void DistanceTwoThree()
    {
        var distance = 
            (objectTwo.transform.position - objectThree.transform.position).magnitude;
            
        NUnit.Framework.Assert.AreEqual(m_ExpectedDistance23,
            distance,
            m_Accuracy);     
    }
    
    /// <summary>
    /// Test Abstand Objekte 1 und 3
    /// </summary>
    [Test]
    public void DistanceOneThree()
    {
        var distance = 
            (objectThree.transform.position - objectOne.transform.position).magnitude;
            
        NUnit.Framework.Assert.AreEqual(m_ExpectedDistance13,
            distance,
            m_Accuracy);     
    }
    
    /// <summary>
    /// Test der Orientierung des Objekts 1
    /// </summary>
    [Test]
    public void OrientationOneIsCorrect()
    {
        var connectionVector =
            (objectThree.transform.position - objectOne.transform.position).normalized;
        
        NUnit.Framework.Assert.That(connectionVector,
            Is.EqualTo(m_ExpectedVector1).Using(m_Comparer));
    }
    
    /// <summary>
    /// Test der Orientierung des Objekts 2
    /// </summary>
    [Test]
    public void OrientationOTwoIsCorrect()
    {
        var connectionVector =
            (objectThree.transform.position - objectTwo.transform.position).normalized;
        
        NUnit.Framework.Assert.That(connectionVector,
            Is.EqualTo(m_ExpectedVector2).Using(m_Comparer));
    }
    
    /// <summary>
    /// GameObjects
    /// </summary>
    private GameObject objectOne,
                        objectTwo, 
                        objectThree;
    /// <summary>
    /// Korrekte Werte für die Abstände
    /// </summary>
    private readonly float m_ExpectedDistance12, 
                       m_ExpectedDistance13,
                       m_ExpectedDistance23;
    /// <summary>
    /// Korrektore Vektoren für die Orientierungen
    /// </summary>
    private readonly Vector3 m_ExpectedVector1,
                            m_ExpectedVector2;
    /// <summary>
    /// Genauigkeit für den Vergleich von float-Zahlen
    /// </summary>
    private readonly float m_Accuracy;
    /// <summary>
    /// Vergleich für Vector3
    /// </summary>
    private readonly Vector3EqualityComparer m_Comparer;
}
