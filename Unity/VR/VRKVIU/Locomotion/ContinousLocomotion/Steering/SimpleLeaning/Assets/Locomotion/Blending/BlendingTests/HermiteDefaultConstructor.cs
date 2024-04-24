using NUnit.Framework;

public class HermiteDefaultConstructor
{
    /// <summary>
    /// Vorbereiten des Tests
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        m_Accuracy = 0.001f;
        blender = new HermiteBlend();
    }

    [Test]
    public void Inside()
    {
        blender.TValue = 0.5f;
        var expected = 0.5f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void LeftOfA()
    {
        blender.TValue = -1.0f;
        var expected = 0.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void RightOfB()
    {
        blender.TValue = 2.0f;
        var expected = 1.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void Left()
    {
        blender.TValue = 0.0f;
        var expected = 0.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void Right()
    {
        blender.TValue = 1.0f;
        var expected =1.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }

    [Test]
    public void Increase()
    {
        blender.TValue = 0.49f;
        blender.Increase();
        var expected = 0.5f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }
    
    [Test]
    public void IDerease()
    {
        blender.TValue = 0.51f;
        blender.Decrease();
        var expected = 0.5f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }
    private float m_Accuracy;

    private HermiteBlend blender;
}
