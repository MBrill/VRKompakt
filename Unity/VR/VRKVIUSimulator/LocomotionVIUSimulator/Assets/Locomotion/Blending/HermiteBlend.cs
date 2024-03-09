//========= 2020 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
///Überblenden mit dem H_3^3 Polynom aus Mathf.SmoothStep.
/// </summary>
public class HermiteBlend : ScalarBlend
{
   /// <summary>
    ///Default-Konstruktor
    /// <remarks>
    /// a wird auf 0 gesetzt, b auf 1, fa auf 0 und fb auf 1.
    /// Der aktuelle Wert ist a, delta wird auf 0.01f gesetzt.
    /// </remarks>
    /// </summary>
    public HermiteBlend() : base() { }

    /// <summary>
    ///Konstruktor mit Wert im Interfavll [a, b] und Delta.
    /// <remarks>
    /// a wird auf 0 gesetzt, b auf 1, fa auf 0 und fb auf 1.
    /// </remarks>
    /// <param name="theValue">Anfangswert</param>
    /// <param name="theDelta">Wert für die Veränderung </param>
    /// </summary>
    public HermiteBlend(float theValue, float theDelta) : 
        base(theValue, theDelta) { }


    /// <summary>
    /// Wert,  Delta, A und B setzen.
    /// <param name="theValue">Anfangswert</param>
    /// <param name="theDelta">Wert für die Veränderung </param>
    /// <param name="theA">Linke Invervallgrenze a</param>
    /// <param name="theB">Linke Intervallgrenze b </param>
    /// </summary>
    /// <remarks>
    /// Die Funktionswerte werden auf fa = 0 und fb = b  gesetzt.
    /// </remarks>
    public HermiteBlend(float theValue, float theDelta,
        float theA, float theB) : base(theValue, theDelta, theA, theB) { }

    /// <summary>
    /// Wert,  Delta, A und B setzen.
    /// <param name="theValue">Anfangswert im Intervall [a, b]</param>
    /// <param name="theDelta">Wert für die Veränderung </param>
    /// <param name="theA">Linke Invervallgrenze a</param>
    /// <param name="theB">Linke Intervallgrenze b </param>
    /// <param name="thfeA">Funktionswert am Punkt a</param>
    /// <param name="thfB">Funktionswert am Punkt b </param>
    /// </summary>
    public HermiteBlend(float theValue, float theDelta,
        float theA, float thefA,
        float theB, float thefB) :
        base(theValue, theDelta, theA, thefA, theB, thefB) { }

    /// <summary>
    /// Überblenden von [a, b] auf [fa, fb] mit dem H_3^3 Polynom
    /// t * t * (3.0f - 2.0f * t).
    /// </summary>
    /// <param name="t">Wert im Intervall [a, b]</param>
    /// <returns>Funktionswert H_3^3(t).</returns>
    /// To Do: Auswertung mit Horner-Schema.
    protected override float m_BlendFunction(float t)
    {
        return Mathf.SmoothStep(0.0f, 1.0f, t);
    }
}
