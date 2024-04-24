using NUnit.Framework;

/// <summary>
/// Test a=-1, b=1, fa = -2, fb = -2.
/// Die lineare Funktion ist h(t) = 2t.
/// </summary>
public class LinearBlendTest
{
    /// <summary>
    /// Vorbereiten des Tests
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        m_Accuracy = 0.001f;
        float a = -1.0f,
            b = 1.0f,
            fa = -2.0f,
            fb = 2.0f,
            tv = 0.5f,
            delta = 0.1f;
        blender = new LinearBlend(tv, delta, a, fa, b, fb);
    }

    [Test]
    public void Inside()
    {
        blender.TValue = 0.5f;
        var expected = 1.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void LeftOfA()
    {
        blender.TValue = -2.0f;
        var expected = -2.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void RightOfB()
    {
        blender.TValue = 2.0f;
        var expected = 2.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void Left()
    {
        blender.TValue = -1.0f;
        var expected = -2.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void Right()
    {
        blender.TValue = 1.0f;
        var expected = 2.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void Increase()
    {
        blender.TValue = 0.0f;
        blender.Increase();
        var expected = 0.2f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }
    
    [Test]
    public void IDerease()
    {
        blender.TValue = 0.4f;
        blender.Decrease();
        var expected = 0.6f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }
    private float m_Accuracy;

    private LinearBlend blender;
}