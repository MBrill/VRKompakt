using NUnit.Framework;

public class MoneyWithRoundingTest
{
    /// <summary>
    /// Vorbereiten des Tests
    /// </summary>
    [SetUp]
    public void SetUp() 
    {
        m_Two = new MoneyWithRounding(2.0f);
        m_Rounded = new MoneyWithRounding(1.995f);
    }

    /// <summary>
    /// Unit-Test
    /// </summary>
    /// <remarks>
    /// Ein Unit-Test ist eine Funktion mit der Annotation [Test]
    /// </remarks>
    [Test]
    public void NewAmountTest()
    {
        NUnit.Framework.Assert.AreEqual(m_CorrectValue, 
            m_Two.Amount, 
            m_Accuracy);
        
    }
  
    /// <summary>
    /// Unit-Test für die Funktion Add
    /// </summary>
    [Test]
    public void AddTest()
    {
        MoneyWithRounding sum = m_Two.Add(m_Two);
        NUnit.Framework.Assert.AreEqual(
            4.0,
            sum.Amount,
            m_Accuracy);
    }

    [Test]
    public void RoundingTest()
    {
        NUnit.Framework.Assert.AreEqual(
            m_CorrectValue,
            m_Rounded.Amount,
            m_Accuracy
        );
    }

    private MoneyWithRounding m_Two, m_Rounded;
    private readonly double m_CorrectValue = 2.0;
    private readonly double m_Accuracy = 0.001;
}
