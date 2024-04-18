//========= 2020 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// �berblenden mit der Winkelhalbierenden.
/// </summary>
public class LinearBlend : ScalarBlend
{
    /// <summary>
    ///Default-Konstruktor
    /// <remarks>
    /// a wird auf 0 gesetzt, b auf 1, fa auf 0 und fb auf 1.
    /// Der aktuelle Wert ist a, delta wird auf 0.01f gesetzt.
    /// </remarks>
    /// </summary>
    public LinearBlend() : base() { }

    /// <summary>
    ///Konstruktor mit Wert im Interfavll [a, b] und Delta.
    /// <remarks>
    /// a wird auf 0 gesetzt, b auf 1, fa auf 0 und fb auf 1.
    /// </remarks>
    /// <param name="theValue">Anfangswert</param>
    /// <param name="theDelta">Wert f�r die Ver�nderung </param>
    /// </summary>
    public LinearBlend(float theValue, float theDelta) : 
        base(theValue, theDelta) { }


    /// <summary>
    /// Wert,  Delta, A und B setzen.
    /// <param name="theValue">Anfangswert</param>
    /// <param name="theDelta">Wert f�r die Ver�nderung </param>
    /// <param name="theA">Linke Invervallgrenze a</param>
    /// <param name="theB">Linke Intervallgrenze b </param>
    /// </summary>
    /// <remarks>
    /// Die Funktionswerte werden auf fa = 0 und fb = b  gesetzt.
    /// </remarks>
    public LinearBlend(float theValue, float theDelta,
        float theA, float theB) : base(theValue, theDelta, theA, theB) { }

    /// <summary>
    /// Wert,  Delta, A und B setzen.
    /// <param name="theValue">Anfangswert im Intervall [a, b]</param>
    /// <param name="theDelta">Wert f�r die Ver�nderung </param>
    /// <param name="theA">Linke Invervallgrenze a</param>
    /// <param name="theB">Linke Intervallgrenze b </param>
    /// <param name="thfeA">Funktionswert am Punkt a</param>
    /// <param name="thfB">Funktionswert am Punkt b </param>
    /// </summary>
    public LinearBlend(float theValue, float theDelta,
        float theA, float thefA,
        float theB, float thefB) :
        base(theValue, theDelta, theA, thefA, theB, thefB) { }

    /// <summary>
    /// Lineares �berblenden von [a, b] auf [fa, fb].
    /// mit der linearen Funktion mit Steigung 1.
    /// </summary>
    /// <param name="t">Wert im Intervall [a, b]</param>
    /// <returns>t selbst.</returns>
    protected override float m_BlendFunction(float t)
    {
        return t;
    }    
}
