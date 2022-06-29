using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Ein Beispiel für einen Unit-Test in Unity mit UTF und NUnit
/// </summary>
public class AmountTest
{
    /// <summary>
    /// Vorbereiten des Tests
    /// </summary>
    [SetUp]
    public void SetUp() 
    {
        two = new Money(2.0f);
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
        //NUnit.Framework.Assert.Fail("Noch nichts implementiert!");
        NUnit.Framework.Assert.AreEqual(correctValue, 
                                                              two.Amount, 
                                                              m_Accuracy);
        
    }
    
    private Money two;
    private double correctValue = 2.0;
    private readonly double m_Accuracy = 0.001;

}
