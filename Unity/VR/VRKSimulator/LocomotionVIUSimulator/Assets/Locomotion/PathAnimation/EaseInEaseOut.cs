//========= 200 - 2023 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Pfad-Animation mit einem Ease-in-Ease-out Verhalten auf der
/// Basis des Hermite-Polynoms H33.
/// </summary>
public abstract class EaseInEaseOut : PathAnimation
{
        /// <summary>
        /// Hermite-Polynom H33 
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
