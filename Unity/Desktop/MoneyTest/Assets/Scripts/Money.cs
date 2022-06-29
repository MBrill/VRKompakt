using System;

/// <summary>
/// Kent Becks Beispiel für Unit-Tests als C#-Klasse
/// </summary>
/// <remarks>
///Die gespeicherten beträge werden auf zwei Nachkommastellen
/// gerundet!
/// </remarks>
public class Money 
{
    /// <summary>
    /// _Default-Konstruktor, Betrag ist 0.
    /// </summary>
    /// <param name="amount">Geldbetrag</param>
    public Money() 
    {
        m_Amount = 0.0f;
    }
    
    /// <summary>
    /// Konstruktor mit Angaben über den Geldbetrag
    /// </summary>
    /// <param name="amount">Geldbetrag</param>
    public Money(float amount) 
    {
        m_Amount = amount;
    }

    /// <summary>
    /// Setzen und lesen des Geldbetrags
    /// </summary>
    public float Amount
    {
        get => m_Amount;
        set
        {
            m_Amount = value;
        }
    }
    
    /// <summary>
    /// Geldbetrag, der verwaltet wird
    /// </summary>
    private float m_Amount=0.0f;
}
