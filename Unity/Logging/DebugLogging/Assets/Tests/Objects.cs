using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Objects
{

    public Objects()
    {
        var m_Accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(m_Accuracy);
        m_ExpectedPosition = new Vector3(2.0f, 0.5f, 2.0f);
    }
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Floor = GameObject.Find("Boden");
        m_Cube = GameObject.Find("ScalingCube");
    }
    
    /// <summary>
    /// Test, ob die Ebene als Boden existiert
    /// </summary>
    [Test]
    public void FloorTest()
    {
        NUnit.Framework.Assert.NotNull(m_Floor);
    }

    /// <summary>
    /// Test, obderürfel mit dem Namen ScalingCube existiert
    /// </summary>
    [Test]
    public void CubeTest()
    {
        NUnit.Framework.Assert.NotNull(m_Cube);
    }

    [Test]
    public void CubePosition()
    {
        NUnit.Framework.Assert.That(m_Cube.transform.position,
            Is.EqualTo(m_ExpectedPosition).Using(m_Comparer));
    }
    
    private GameObject m_Floor;
    private GameObject m_Cube;
    private Vector3 m_ExpectedPosition;
    private Vector3EqualityComparer m_Comparer;
}
