using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Test der Bindings
/// </summary>
public class Bindings
{
    /// <summary>
    /// Default-Konstruktor für die Testklasse
    /// </summary>
    /// <remarks>
    /// Als Genauigkeit für den Vergleich verwenden wir 0.001.
    /// </remarks>
    public Bindings() { }

    /// <summary>
    /// Laden der Szene.
    /// </summary>
    /// <remarks>
    /// Die Szene muss in den Build Settings stehen!
    /// </remarks>
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene(
            "Assets/Scenes/Basis.unity", 
            LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Die beiden GameObjects zuweisen
    /// </summary>
    [UnitySetUp]
    public IEnumerator UnitySetup()
    {
        yield return null;
        m_Follower = GameObject.Find("Flugzeugmodell");
        m_Target = GameObject.Find("Kapsel");
    }

    /// <summary>
    /// Test ob die Taste "W"  in den Bindings enthalten ist.
    /// </summary>
    [UnityTest]
    public IEnumerator WInBinding()
    {
        var inputasset =
            m_Target.GetComponent<PlayerControl2D>();
        var action = inputasset.PlayAction;

        // Wir fragen den Index für die Binding ab.
        // Ist der Button nicht im Binding erhalten wir -1 als Index!
        var wrongIndex = -1;
        var bindingIndex = action.GetBindingIndexForControl(Keyboard.current.wKey);
        NUnit.Framework.Assert.AreNotEqual(bindingIndex, wrongIndex);
        yield return null;
    }
    
    /// <summary>
    /// Test ob die Taste "S" im Binding enthalten ist.
    /// </summary>
    [UnityTest]
    public IEnumerator SInBinding()
    {
        var inputasset =
            m_Target.GetComponent<PlayerControl2D>();
        var action = inputasset.PlayAction;

        // Wir fragen den Index für die Binding ab.
        // Ist der Button nicht im Binding erhalten wir -1 als Index!
        int wrongIndex;
        wrongIndex = -1;
        var bindingIndex = action.GetBindingIndexForControl(Keyboard.current.sKey);
        NUnit.Framework.Assert.AreNotEqual(bindingIndex, wrongIndex);
        yield return null;
    }
    
    /// <summary>
    /// Test ob die Taste "W"  in den Bindings enthalten ist.
    /// </summary>
    [UnityTest]
    public IEnumerator AInBinding()
    {
        var inputasset =
            m_Target.GetComponent<PlayerControl2D>();
        var action = inputasset.PlayAction;

        // Wir fragen den Index für die Binding ab.
        // Ist der Button nicht im Binding erhalten wir -1 als Index!
        int wrongIndex;
        wrongIndex = -1;
        var bindingIndex = action.GetBindingIndexForControl(Keyboard.current.aKey);
        NUnit.Framework.Assert.AreNotEqual(bindingIndex, wrongIndex);
        yield return null;
    }
    
    /// <summary>
    /// Test ob die Taste "D"  in den Bindings enthalten ist.
    /// </summary>
    [UnityTest]
    public IEnumerator DInBinding()
    {
        var inputasset =
            m_Target.GetComponent<PlayerControl2D>();
        var action = inputasset.PlayAction;

        // Wir fragen den Index für die Binding ab.
        // Ist der Button nicht im Binding erhalten wir -1 als Index!
        int wrongIndex;
        wrongIndex = -1;
        var bindingIndex = action.GetBindingIndexForControl(Keyboard.current.dKey);
        NUnit.Framework.Assert.AreNotEqual(bindingIndex, wrongIndex);
        yield return null;
    }
    
    /// <summary>
    /// Test ob die Taste "P" im Binding für das Flugzeug enthalten ist
    /// </summary>
    [UnityTest]
    public IEnumerator PForMoving()
    {
        var inputasset =
            m_Follower.GetComponent<FollowTheTargetController>();
        var action = inputasset.FollowAction;

        // Wir fragen den Index für die Binding ab.
        // Ist der Button nicht im Binding erhalten wir -1 als Index!
        int wrongIndex;
        wrongIndex = -1;
        var bindingIndex = action.GetBindingIndexForControl(Keyboard.current.pKey);
        NUnit.Framework.Assert.AreNotEqual(bindingIndex, wrongIndex);
        yield return null;
    }
    
    /// <summary>
    /// Test ob die mittlere Maustaste  im Binding für das Flugzeug enthalten ist
    /// </summary>
    [UnityTest]
    public IEnumerator MiddleMouseForMoving()
    {
        var inputasset =
            m_Follower.GetComponent<FollowTheTargetController>();
        var action = inputasset.FollowAction;

        // Wir fragen den Index für die Binding ab.
        // Ist der Button nicht im Binding erhalten wir -1 als Index!
        int wrongIndex;
        wrongIndex = -1;
        var bindingIndex = action.GetBindingIndexForControl(Mouse.current.middleButton);
        Debug.Log(">>> PForMoving ");
        NUnit.Framework.Assert.AreNotEqual(bindingIndex, wrongIndex);
        yield return null;
    }
    
    /// <summary>
    /// Gameobject für das verfolgte Objekt
    /// </summary>
    private GameObject m_Target;
    /// <summary>
    /// GameObject für den Verfolger
    /// </summary>
    private GameObject m_Follower;
}
