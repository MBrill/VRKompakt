using UnityEngine;

/// <summary>
/// Kent Becks Beispiel für Unit-Tests als C#-Klasse
/// </summary>
/// <remarks>
///Die gespeicherten beträge werden auf zwei Nachkommastellen
/// gerundet!
/// </remarks>
public class MoneyWithRounding
{
    /// <summary>
    /// _Default-Konstruktor, Betrag ist 0.
    /// </summary>
    public MoneyWithRounding() 
    {
        m_Amount = 0.0f;
    }
    
    /// <summary>
    /// Konstruktor mit Angaben über den Geldbetrag
    /// </summary>
    /// <param name="amount">Geldbetrag</param>
    public MoneyWithRounding(float amount)
    {
        m_Amount = System.MathF.Round(amount, 2);
    }

    /// <summary>
    /// Addieren von Geldbeträgen
    /// </summary>
    /// <param name="m>Instanz der Klasse MoneyWithRounding mit Geldbetrag</param>
    public MoneyWithRounding Add(MoneyWithRounding m)
    { 
        return new MoneyWithRounding(Amount + m.Amount);
    }
    
    /// <summary>
    /// Setzen und lesen des Geldbetrags
    /// </summary>
    public float Amount
    {
        get => m_Amount;
        set => m_Amount = System.MathF.Round(value, 2);
    }
    
    /// <summary>
    /// Geldbetrag, der verwaltet wird
    /// </summary>
    private float m_Amount=0.0f;
}