//========= 2024 - Copyright Manfred Brill. All rights reserved. ===========
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Objects
{

    public Objects()
    {
        const float m_Accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(m_Accuracy);
        m_ExpectedPosition = new Vector3(0.6f, 0.6f, 2.6f);
    }
    
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Kapsel = GameObject.Find("ScalingCube");
        m_Controller = GameObject.Find("XRControllerRight");
    }
    

    /// <summary>
    /// Test, ob der Würfel mit dem Namen ScalingCube existiert
    /// </summary>
    [Test]
    public void KapselTest()
    {
        NUnit.Framework.Assert.NotNull(m_Kapsel);
    }

    /// <summary>
    /// Test, ob der der Controller existiert
    /// </summary>
    [Test]
    public void ControllerTest()
    {
        NUnit.Framework.Assert.NotNull(m_Controller);
    }
    
    /// <summary>
    /// Test, ob der der Controller die erforderlichen
    /// Components Trigger und RigidBody hat.
    /// </summary>
    [Test]
    public void ControllerComponents()
    {
        var controller = GameObject.Find("XRControllerRight");
        NUnit.Framework.Assert.NotNull( controller.GetComponent(typeof(Rigidbody)));
        NUnit.Framework.Assert.NotNull( controller.GetComponent(typeof(Collider)));
    }

    private GameObject m_Kapsel;
    private GameObject m_Controller;
    private Vector3 m_ExpectedPosition;
    private readonly Vector3EqualityComparer m_Comparer;
}
