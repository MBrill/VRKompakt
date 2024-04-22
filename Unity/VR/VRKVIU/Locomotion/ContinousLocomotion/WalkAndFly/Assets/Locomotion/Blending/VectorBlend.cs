//========= 2020 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

 /// <summary>
 /// Klasse, die einen Vektor mit drei  float-Komponenten verwaltet.
 /// Es gibt Funktionen für die Veränderung der Koordinaten, und es wird
 /// für jede Komponenten ein zulässiges Intervall definiert,
 /// das mit Hilfe von Clamp eingehalten wird.
 /// </summary>
public class VectorBlend 
{
     /// <summary>
        /// Set und Get für den skalaren Wert
        /// </summary>
        public Vector3 value
        {
            get => m_value;
            set => m_value = this.value;
        }
        
        /// <summary>
        /// Set und Get für das Delta zum
        /// Verändern des Werts
        /// </summary>
        public Vector3 delta
        {
            get => m_delta;
            set => m_delta = this.delta;
        }

        /// <summary>
        /// Set und Get für das A des Werts
        /// </summary>
        public Vector3 minimum
        {
            get => m_min;
            set => m_min = this.minimum;
        }
        
        /// <summary>
        /// Set und Get für das B des Werts
        /// </summary>
        public Vector3 maximum
        {
            get => m_max;
            set => m_max = this.maximum;
        }
        
        /// <summary>
        /// Den Wert um ein Delta erhöhen
        /// </summary>
        public void Increase()
        {
            Vector3 sum = m_value + delta;
            m_value.x = Mathf.Clamp(sum.x, m_min.x, m_max.x);
            m_value.y = Mathf.Clamp(sum.y, m_min.y, m_max.y);
            m_value.z = Mathf.Clamp(sum.z, m_min.z, m_max.z);
        }
        
        /// <summary>
        /// Den Wert um ein Delta erniedrigen
        /// </summary>
        public void Decrease()
        {
            Vector3 diff = m_value - delta;
            m_value.x = Mathf.Clamp(diff.x, m_min.x, m_max.x);
            m_value.y = Mathf.Clamp(diff.y, m_min.y, m_max.y);
            m_value.z = Mathf.Clamp(diff.z, m_min.z, m_max.z);
        }

        /// <summary>
       /// Wert und Delta setzen.
       /// <remarks>
       /// A und B wird auf -infty und infty gesetzt.
       /// </remarks>
       /// <param name="theMValue">Anfangswerte</param>
       /// <param name="theMDelta">Werte für die Veränderung </param>
       /// </summary>
       public VectorBlend(Vector3 theMValue, Vector3 theMDelta)
       {
           m_value = theMValue;
           m_delta = theMDelta;
           m_min = Vector3.negativeInfinity;;
           m_max = Vector3.positiveInfinity;
       }
  
        /// <summary>
        /// Wert,  Delta, A und B setzen.
        /// <param name="theMValue">Anfangsweret</param>
        /// <param name="theMDelta">Weret für die Veränderung </param>
        /// <param name="theMMin">Minimalr Werte</param>
        /// <param name="theMDelta">Maximale Werte </param>
        /// </summary>
        public VectorBlend(Vector3 theMValue, Vector3 theMDelta,
                                      Vector3 theMMin, Vector3 theMMax)
        {
            m_value = theMValue;
            m_delta = theMDelta;
            m_min = theMMin;
            m_max = theMMax;
        }

        /// <summary>
       /// Der Vektort, den diese Klasse liefert.
       /// </summary>
       private Vector3 m_value;
       
       /// <summary>
       /// DerVektorr, den wir für die Veränderung
       /// des skalaren Werts einsetzen.
       /// </summary>
       private Vector3 m_delta;

       /// <summary>
       /// Minimale Werte, die angenommen werden können.
       /// In Increase und Decrease wird ein Clamp durchgeführt.
       /// Damit ist garantiert, dass der Wert immer im zulässigen
       /// Intervall liegt.
       /// </summary>
       private Vector3 m_min;
  
       /// <summary>
       /// Maximalr Werte, die angenommen werden können.
       /// In Increase und Decrease wird ein Clamp durchgeführt.
       /// Damit ist garantiert, dass der Wert immer im zulässigen
       /// Intervall liegt.
       /// </summary>
       private Vector3 m_max;
}
