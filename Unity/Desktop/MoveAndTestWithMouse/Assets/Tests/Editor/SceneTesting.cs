using System.Collections;
using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Test der Szene. 
/// </summary>
/// <remarks>
/// Dabei gehen wir davon aus,
///  dass es nur eine Szene gibt, die auch ge�ffnet ist.
/// Wir f�hren das als Editor-Test aus, wir sparen uns das Laden
/// der Szene.
/// </remarks>
public class SceneTesting
{
    /// <summary>
    /// Default-Konstruktor f�r die Testklasse
    /// </summary>
    /// <remarks>
    /// Als Genauigkeit f�r den Vergleich verwenden wir 0.001,
    /// als Skalierungsfaktor f�r das Flugzeug erwarten wir 0.3.
    /// </remarks>
    public SceneTesting()
    {
        m_Accuracy = 0.001f;
        m_Speed = 20.0f;
    }

    /// <summary>
    /// Die beiden GameObjects zuweisen
    /// </summary>
    [SetUp]
    public void Setup()
    {
        m_Player = GameObject.Find("Player");
    }
    
    /// <summary>
    /// Test ob es das GameObject mit dem Namen "Player"
    /// in der Szene gibt.
    /// </summary>
    [Test]
    public void PlayerExists()
    {
        NUnit.Framework.Assert.NotNull(m_Player);
    }

    /// <summary>
    /// Test ob der Skalierungsfaktor f�r den Airplane korrekt gesetzt ist.
    /// </summary>
    [Test]
    public void PlayerSpeedIsCorrect()
    {
        var value = 
            m_Player.GetComponent<PlayerControl>().Speed;
        NUnit.Framework.Assert.AreEqual(
            m_Speed,
            value,
            m_Accuracy
        );
    }
    /// <summary>
    /// Gameobject f�r den Player
    /// </summary>
    private GameObject m_Player;
    /// <summary>
    /// Wert f�r die Geschwindigkeit
    /// </summary>
    private readonly float m_Speed;
    /// <summary>
    /// Genauigkeit f�r den Vergleich von float-Werten
    /// </summary>
    private readonly float m_Accuracy;
}
