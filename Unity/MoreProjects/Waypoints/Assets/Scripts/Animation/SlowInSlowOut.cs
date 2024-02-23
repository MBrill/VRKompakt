//========= 2021- 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
///  Realisierun eines Slow-In-Slow-Out Verhaltens
/// bei einer Pfad-Animation.
/// </summary>
/// <remarks>
/// Wie in der Lehrveranstaltung Computerorientierte Mathematik
/// setzen wir ein Hermite-Polynom ein.
///
/// Wir m�ssen sicher stellen, dass die Kurve nach Bogenma�
/// parametrisiert ist!
/// </remarks>
public abstract class SlowInSlowOut : PathAnimation
{
        /// <summary>
        /// Hermite-Polynom H33.
        /// </summary>
        /// <param name="x">x-Wert im Intervall [0, 1]</param>
        /// <returns>Wert des Hermite-Polynoms</returns>
        protected float H33(float x)
        {
            return x * x * (3.0f - 2.0f * x);
        }

        /// <summary>
        /// Ableitung des Hermite-Polynoms H33.
        /// </summary>
        /// <param name="x">x-Wert im Intervall [0, 1]</param>
        /// <returns>Wert de Ableitung des Hermite-Polynoms</returns>
        protected float H33Prime(float x)
        {
            return 6.0f * x * (1 - x);
        }
}
