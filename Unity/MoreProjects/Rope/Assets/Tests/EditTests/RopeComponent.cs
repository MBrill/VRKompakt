//========= 2024 - Copyright Manfred Brill. All rights reserved. ===========
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Tests für die Komponenten am Objekt Kapsel
/// </summary>
public class RopeComponent
{
    public RopeComponent()
    {
        //const float m_Accuracy = 0.001f;
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
    }
    
    /// <summary>
    /// Test, ob der der Controller die erforderlichen
    /// Components Trigger und RigidBody hat.
    /// </summary>
    [Test]
    public void KapselComponents()
    {
        NUnit.Framework.Assert.NotNull( m_Kapsel.GetComponent(typeof(RopeLineController)));
    }
    
    private GameObject m_Kapsel;
    private GameObject m_Controller;
}
