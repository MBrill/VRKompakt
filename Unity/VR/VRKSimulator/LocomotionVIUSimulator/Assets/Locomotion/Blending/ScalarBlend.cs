//========= 2020 - 2023 - Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Basisklasse für die Verwlaltung und ÜBerblenden von skalaren Werten.
/// </summary>
/// <remarks>
/// Das Blending ist definidert durch ein Intervall [a, b] und
/// funktionswerte fa, fb an diesen Werten.
/// Außerhalb des Intervalls ist der Wert konstant.
/// </remarks>
public abstract class ScalarBlend
{
    /// <summary>
    /// Set und Get für den skalaren Wert im Intervall[a, b]..
    /// </summary>
    public float Value
    {
        get => m_Blend(m_TValue);
        set => m_Value = value;
    }
    /// <summary>
    /// Set und Get für den skalaren Wert, der durch das Blending entsteht.
    /// </summary>
    public float TValue
    {
        get => m_TValue;
        set => m_TValue = value;
    }
        
        /// <summary>
        /// Mit einem Delta-Wert können wir
        /// den aktuellen Wert erhöhen oder erniedrigen.
        /// </summary>
        public float Delta
        {
            get => m_Delta;
            set => m_Delta = this.Delta;
        }

        /// <summary>
        /// Set und Get fürdie linke Intervallgrenze a.
        /// </summary>
        public float A
        {
            get => m_a;
            set => m_a = this.A;
        }
        
        /// <summary>
        /// Set und Get für ddie rechte Intervallgrenze b.
        /// </summary>
        public float B
        {
            get => m_b;
            set => m_b = this.B;
        }
        
        /// <summary>
        /// Den Wert im intervall [a, b] um ein Delta erhöhen
        /// </summary>
        public virtual void Increase()
        {
            m_TValue +=  m_Delta;
            m_Value = System.Math.Clamp(
                m_Blend(m_TValue),
                m_fa,
                m_fb);
        }
        
        /// <summary>
        /// Den Wert im intervall [a, b] um ein Delta erniedrigen
        /// </summary>
        public virtual void Decrease()
        {
            m_TValue -=  m_Delta;
            m_Value = System.Math.Clamp(
                m_Blend(m_TValue),
                m_fa,
                m_fb);
        }

        /// <summary>
        ///Default-Konstruktor
        /// </summary>
        /// <remarks>
        /// a wird auf 0 gesetzt, b auf 1, fa auf 0 und fb auf 1.
        /// Der aktuelle Wert ist a, delta wird auf 0.01f gesetzt.
        /// </remarks>
        public ScalarBlend()
        {
            m_TValue = 0.0f;
            m_Value = 0.0f;
            m_Delta = 0.01f;
            m_a = 0.0f;
            m_fa = 0.0f;
            m_b = 1.0f;
            m_fb = 1.0f;
        }
        
        /// <summary>
       ///Konstruktor mit Wert im Interfavll [a, b] und Delta.
       /// <remarks>
       /// a wird auf 0 gesetzt, b auf 1, fa auf 0 und fb auf 1.
       /// </remarks>
       /// <param name="theValue">Anfangswert</param>
       /// <param name="theDelta">Wert für die Veränderung </param>
       /// </summary>
       public ScalarBlend(float theValue, float theDelta)
       {
            m_TValue = theValue;
           m_Value = m_Blend(m_TValue);
           m_Delta = theDelta;
           m_a = 0.0f;
           m_fa = 0.0f;
           m_b = 1.0f;
          m_fb =  1.0f;
       }
  
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
        public ScalarBlend(float theValue, float theDelta,
                                       float theA, float theB)
        {
             m_TValue = theValue;
            m_Value = m_Blend(m_TValue);
            m_Delta = theDelta;
            m_a = theA;
            m_b = theB;
            m_fa = 0.0f;
            m_fb = theB;
        }

        /// <summary>
        /// Wert,  Delta, A und B setzen.
        /// <param name="theValue">Anfangswert im Intervall [a, b]</param>
        /// <param name="theDelta">Wert für die Veränderung </param>
        /// <param name="theA">Linke Invervallgrenze a</param>
        /// <param name="theB">Linke Intervallgrenze b </param>
        /// <param name="thfeA">Funktionswert am Punkt a</param>
        /// <param name="thfB">Funktionswert am Punkt b </param>
        /// </summary>
        public ScalarBlend(float theValue, float theDelta,
            float theA, float thefA,
            float theB, float thefB)
        {
          m_TValue = theValue;
          m_Value = m_Blend(m_TValue);
          m_Delta = theDelta;
           m_a = theA;
          m_b = theB;
          m_fa = thefA;
          m_fb = thefB;
        }
        
        /// <summary>
        /// Funktion für das Überblenden der Werte.
        /// Wird in abgeleiteten Klassen implementiert.
        /// </summary>
        /// <param name="t">Parameterwert, für den der Funktionswert
        /// berechnet werden soll.</param>
        protected abstract float m_BlendFunction(float t);

        protected float m_Blend(float t)
        {
            if (t < m_a)
                return m_fa;
            else if (t >= m_b)
                return m_fb;
            
            var blendValue = (m_fb - m_fa)*
                m_BlendFunction((t-m_a)/(m_b - m_a)) + m_fa;
            return blendValue;
        }
        /// <summary>
       /// Der skalare Float-Wert im Intervall [a, b].
       /// </summary>
        protected float m_TValue;
       
        /// <summary>
       /// Das Ergebnis  des Überblendens für den aktuellen Wert
       /// für t=m_TValue.
       /// </summary>
        protected float m_Value;
        
       /// <summary>
       /// Der skalare Float-Wert, den wir für die Veränderung
       /// des skalaren Werts einsetzen.
       /// </summary>
       protected float m_Delta;

       /// <summary>
       ///Linke Intervallgrenze und minimaler Wert für den übergebenen
       /// Wert.
       /// </summary>
       protected float m_a;
  
       /// <summary>
       ///Funktionswert an der linken Intervallgrenze.
       /// </summary>
       /// <remarks>
       /// x-Werte links von a führen zum Funktionswert m_fa,
       /// was durch ein Clamp gewährleistet wird.
       /// </remarks>
       protected float m_fa;
       
       /// <summary>
       /// Maximaler Wert, der angenommen werden kann.
       /// In Increase und Decrease wird ein Clamp durchgeführt.
       /// Damit ist garantiert, dass der Wert immer im zulässigen
       /// Intervall liegt.
       /// </summary>
       protected float m_b;
       
       /// <summary>
       ///Funktionswert an der rechten  Intervallgrenze.
       /// </summary>
       /// <remarks>
       /// x-Werte rechts  von b führen zum Funktionswert m_fb,
       /// was durch ein Clamp gewährleistet wird.
       /// </remarks>
       protected float m_fb;
}
