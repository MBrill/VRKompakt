//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Bewegung eines Objekts entlang einer Linie zwischen zwei Punkten.
/// </summary>
/// <remarks>
/// Wir verwenden das Hermite-Polynom H33 für einen Slow-In-Slow-Out Effekt. 
/// </remarks>
  public class LineSlowInSlowOut : RopeAnimation
  {
        /// <summary>
        /// Anfangspunkt
        public Vector3 p1 = Vector3.zero;
        
        /// <summary>
        /// Endpunkt
        /// </summary>
        public Vector3 p2 = Vector3.right;

        /// <summary>
        /// Berechnung der Punkte für eine Linie zwischen P1 und P2.
        /// 
        /// Wir verwenden das Parameterintervall [0.0, L], dabei
        /// ist L der Abstand zwischen den beiden Punkten.
        /// Der Richtungsvektor für diese Parametrisierung ist
        /// der normierte Vektor m_dirVec. Das kompensieren wir im Code
        /// und denken direkt in t aus dem Intervall [0, 1] und normieren
        /// den Vektor nicht.
        /// 
        /// Damit können wir garantieren, dass die Linie nach
        /// Bogenmaß parametrisiert ist.
        ///
        /// Wir besetzen auch die Bahngeschwindigkeiten an den Wegpunkten.
        /// Dafür benötigen wir die Ableitung des Hermite-Polynoms H33Prime.
        /// </summary>
        protected override void ComputePath()
        {
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            var arcL = Vector3.Distance(p1, p2);
            var dirVec = (p2 - p1).normalized;
            waypoints = new Vector3[NumberOfPoints];
            var t = 0.0f;
            var delta = arcL / (float)(NumberOfPoints - 1);
            for (var i = 0; i < NumberOfPoints; i++)
            {
                waypoints[i] = p1 + arcL* H33(t/arcL) * dirVec;
                velocities[i] = H33Prime(t / arcL);
                t += delta;
            }
        }

        /// <summary>
        /// Hermite-Polynom H33.
        /// </summary>
        /// <param name="x">x-Wert im Intervall [0, 1]</param>
        /// <returns>Wert des Hermite-Polynoms</returns>
        private static float H33(float x)
        {
            return x * x * (3.0f - 2.0f * x);
        }

        /// <summary>
        /// Ableitung des Hermite-Polynoms H33.
        /// </summary>
        /// <param name="x">x-Wert im Intervall [0, 1]</param>
        /// <returns>Wert de Ableitung des Hermite-Polynoms</returns>
        private static float H33Prime(float x)
        {
            return 6.0f * x * (1 - x);
        }
  }