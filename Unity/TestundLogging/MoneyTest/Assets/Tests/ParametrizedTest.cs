using NUnit.Framework;

/// <summary>
/// Ein Beispiel für einen Unit-Test in Unity mit UTF und NUnit.
/// Wir parametrisieren den Test-
/// </summary>
[TestFixture(3.0, 0.001)]
public class ParametrizedTest
{
    public ParametrizedTest(double t1, double t2) 
    {
        m_CorrectValue = t1;
        m_Accuracy = t2;
        m_Two = new Money((float)t1);
    }

    [Test]
    public void TestConstructor()
    {
        NUnit.Framework.Assert.AreEqual(m_CorrectValue, 
            m_Two.Amount, 
            m_Accuracy);
    }

    private Money m_Two;
    private double m_CorrectValue ;
    private double m_Accuracy;
}
