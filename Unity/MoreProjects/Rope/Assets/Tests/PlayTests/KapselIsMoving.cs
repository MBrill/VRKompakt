//========= 2024 - Copyright Manfred Brill. All rights reserved. ===========
using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

/// <summary>
/// Test, ob die Verfolgung ausgeführt wird, falls wir das Attribut
/// <Running> auf true setzen im Verfolger.
/// </summary>
public class KapselIsMoving
{
    /// <summary>
    /// Laden der Szene.
    /// </summary>
    /// <remarks>
    ///Die Szene muss in den Build Settings enthalten sein!
    /// </remarks>
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene(
            "Assets/Scenes/RopeLine.unity", 
            LoadSceneMode.Single);
    }

    /// <summary>
    /// Die beiden GameObjects zuweisen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Kapsel = GameObject.Find("Kapsel");
        m_Target = GameObject.Find("XRControllerRight");
    }
    
    /// <summary>
    /// Die Tests erwarten, dass IsRunning false ist
    /// </summary>
    [UnityTest]
    public IEnumerator Running()
    {
        yield return null;
        
        // Verfolger abfragen und in der Komponente 
        // FollowTheTarget die Eigensdchaft IsFollowing
        // auf false setzen.
        var running = 
            m_Kapsel.GetComponent<RopeLineController>().IsRunning;

        NUnit.Framework.Assert.That(running, 
            Is.EqualTo(false));
    }
    
    /// <summary>
    /// Test ob der Follower sich dem Ziel  nähert
    /// </summary>
    [UnityTest]
    public IEnumerator KapselMoves()
    {
        yield return null;
        
        // Verfolger abfragen und in der Komponente 
        // FollowTheTarget die Eigensdchaft IsFollowing
        // auf false setzen.
        var running = 
            m_Kapsel.GetComponent<RopeLineController>().IsRunning;
        // Verfolgung deaktivieren
        running = false;
        // Distanz zwischen Ziel und gewähltem Objekt  berechnen
        var posObject = m_Target.transform.position;
        var posTarget = m_Kapsel.transform.position;
        var distance = (posObject - posTarget).magnitude;
        
        // Jetzt die Verfolgung aktivieren
        running = true;
        
        // Update abwarten und dann Positionen neu abfragen
        yield return new WaitForSeconds(2.0f);
        posObject = m_Target.transform.position;
        posTarget = m_Kapsel.transform.position;
        var distance2 =(posObject - posTarget).magnitude;
        
        // Jetzt sollte die Distanz zwischen den Objekten kleiner
        // geworden sein.
        NUnit.Framework.Assert.That(distance2, 
            Is.LessThan(distance));
    }
    
    /// <summary>
    /// Gameobject für das Ziel der Verfolgung
    /// </summary>
    private GameObject m_Target;
    /// <summary>
    /// GameObject für den Verfolger
    /// </summary>
    private GameObject m_Kapsel;  
}
