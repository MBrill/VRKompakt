using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

/// <summary>
/// Bewegungen in der Welt und Überprüfen der Modellveränderung
/// </summary>
public class MoveTheWorld
{
    /// <summary>
    ///  Konstruktor
    /// </summary>
    public MoveTheWorld()
    {
        m_Accuracy = 0.001f;
        m_Comparer = new Vector3EqualityComparer(m_Accuracy );
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
    /// Setup für die Szene - wir suchen die Objekte über die Namen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_MiniWorld = GameObject.Find("MiniWorld");
        m_Scale =  m_MiniWorld.GetComponent<WiM>().ScaleFactor;
        m_Offset = GameObject.Find("Offset");
        m_Cube = GameObject.Find("ScalingCube");
        m_ModelCube = GameObject.Find("ScalingCube_Modell");
    }

    /// <summary>
    /// Wir bewegen den ScalingCube nach rechts (deltax=0.1)
    /// und nach oben (dy = 0.2) und überprüfen
    /// anschließend die veränderten Positionen
    /// im Modell.
    /// </summary>
    [UnityTest]
    public IEnumerator MoveOfCubeCorrectInModel()
    {
        var trans = new Vector3(0.1f, 0.2f, 0.0f);
        var deltaTrans = m_Scale * trans;
        var oldModelPos = m_ModelCube.transform.position;
        
        m_Cube.transform.position += trans;
        
        yield return new WaitForFixedUpdate();
        
        m_MiniWorld.GetComponent<WiM>().Refresh();
        
        yield return new WaitForFixedUpdate();
        // Modell neu abfragen
        m_ModelCube = GameObject.Find("ScalingCube_Modell");
        var changed = m_ModelCube.transform.position;
        var transModel = changed - oldModelPos ;
        
        NUnit.Framework.Assert.That(
            transModel,
            Is.EqualTo(deltaTrans).Using(m_Comparer));
        
        yield return null;
    }
    
    /// <summary>
    /// Wir bewegen das Modell des Objekts ScalingCube
    /// im Modell nach linkss (deltax=-scale* 0.1)
    /// und nach vorne (dzy = scale* 0.2) und überprüfen
    /// anschließend die veränderten Positionen
    /// in der Szene.
    /// </summary>
    [UnityTest]
    public IEnumerator MoveOfCubeModelCorrectInScene()
    {
        var trans = new Vector3(-0.01f, 0.0f, 0.02f);
        var transWorld = (1.0f/m_Scale) * trans;
        
        var oldPos = m_Cube.transform.position;
        m_ModelCube.transform.position += trans;
        m_Cube.transform.position = ModelToWorld(
            m_Scale,
            m_ModelCube.transform.position,
            m_MiniWorld.transform.position
        );
        
        yield return new WaitForFixedUpdate();
        // Modell neu abfragen
        m_Cube = GameObject.Find("ScalingCube");
        var changed = m_Cube.transform.position;
        var transModel = changed - oldPos ;
        
        NUnit.Framework.Assert.That(
            transModel,
            Is.EqualTo(transWorld).Using(m_Comparer));
        
        yield return null;
    }
    
    /// <summary>
    /// Skalierungsfaktor in der Klasse WiM
    /// </summary>
    private float m_Scale;
    
    /// <summary>
    /// Umrechnung von Modell- in Weltkoordinaten
    /// </summary>
    /// <param name="scale">Skalierungsfaktor</param>
    /// <param name="mp">Modellkoordinaten</param>
    /// <param name="rp">Koordinaten des Wurzelobjekts der WiM</param>
    /// <returns>Weltkoordinaten</returns>
    private static Vector3 ModelToWorld(float scale, Vector3 mp, Vector3 rp)
    {
        return  (1.0f/scale)*(mp-rp);
    }
    
    /// <summary>
    /// Umrechnung von Welt- in die Modellkoordinaten
    /// </summary>
    /// <param name="scale">Skalierungsfaktor</param>
    /// <param name="o">Weltposition</param>
    /// <param name="rp"">Koordinaten des Wurzelobjekts der WiM</param>
    /// <param name="off"">Offset in WiM</param>
    /// <returns>Modellkoordinaten</returns>
    private static Vector3 WorldToModel(float scale, Vector3 o, Vector3 rp, Vector3 off)
    {
        return  scale * o + rp + off;
    }
    
    /// <summary>
    /// GameObjects für die Tests
    /// </summary>
    private GameObject m_MiniWorld,
        m_Offset,
        m_Cube,
        m_ModelCube;
    
    /// <summary>
    /// Genauigkeit für den Verbleich von float und Vector3
    /// </summary>
    private float m_Accuracy;

    /// <summary>
    /// Vergleichsklasse für Unity-Klasse Vector3
    /// </summary>
    private Vector3EqualityComparer m_Comparer;
}
