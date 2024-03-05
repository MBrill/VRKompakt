//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Bewegung eines Objekts entlang einer Linie zwischen zwei Punkten.
/// </summary>
/// <remarks>
/// Wir verwenden das Hermite-Polynom H33 f�r einen Slow-In-Slow-Out Effekt. 
/// </remarks>
  public class LineSlowInSlowOut : SlowInSlowOut
  {
        /// <summary>
        /// Anfangspunkt
        /// </summary>
        [Tooltip("Anfangspunkt der Linie")]
        public Vector3 p1 = Vector3.zero;
        
        /// <summary>
        /// Endpunkt
        /// </summary>
        [Tooltip("Endpunkt der Linie")]
        public Vector3 p2 = Vector3.right;

        /// <summary>
        /// Berechnung der Punkte f�r eine Linie zwischen P1 und P2.
        /// 
        /// Wir verwenden das Parameterintervall [0.0, L], dabei
        /// ist L der Abstand zwischen den beiden Punkten.
        /// Der Richtungsvektor f�r diese Parametrisierung ist
        /// der normierte Vektor m_dirVec. Das kompensieren wir im Code
        /// und denken direkt in t aus dem Intervall [0, 1] und normieren
        /// den Vektor nicht.
        /// 
        /// Damit k�nnen wir garantieren, dass die Linie nach
        /// Bogenma� parametrisiert ist.
        ///
        /// Wir besetzen auch die Bahngeschwindigkeiten an den Wegpunkten.
        /// Daf�r ben�tigen wir die Ableitung des Hermite-Polynoms H33Prime.
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
        /// Berechnung der ersten Lookat-Punkts. 
        /// Die Tangente der Linie stimmt mit dem normierten Richtungsvektor
        /// �berein.
        /// </summary>
        /// <returns>Punkt, der LookAt �bergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return p2;
        }
  }