using NUnit.Framework;

/// <summary>
/// Test des linearen Blendings mit Default-Konstruktor
/// </summary>
public class LinearDefaultConstructor
{
    /// <summary>
    /// Vorbereiten des Tests
    /// </summary>
    [SetUp]
    public void SetUp() 
    {
        m_Accuracy = 0.001f;
        blender = new LinearBlend();
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
        var expected = 1.0f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }
    
    [Test]
    public void Increase()
    {
        blender.TValue = 0.6f;
        blender.Increase();
        var expected = 0.61f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }
    
    [Test]
    public void Decrease()
    {
        blender.TValue = 0.6f;
        blender.Decrease();
        var expected = 0.59f;
        NUnit.Framework.Assert.AreEqual(expected, blender.Value, m_Accuracy);
    }
    
    private float m_Accuracy;
    
    private LinearBlend blender;
}
