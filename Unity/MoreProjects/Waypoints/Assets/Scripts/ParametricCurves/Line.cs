//========= 2021- 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Bewegung eines Objekts entlang einer Linie zwischen zwei Punkten       
/// </summary>
public class Line : PathAnimation
{
        /// <summary>
        /// Anfangspunkt
        /// </summary>
        [Tooltip("Anfangspunkt der Linie")]
        public Vector3 p1 = Vector3.zero;
        
        /// <summary>
        /// ZEndpunkt
        /// </summary>
        [Tooltip("Endpunkt der Linie")]
        public Vector3 p2 = Vector3.forward;

        /// <summary>
        /// Berechnung der Punkte f�r eine Linie zwischen P1 und P2.
        /// </summary>
        /// <remarks>
        /// Wir verwenden das Parameterintervall [0, L], dabei
        /// ist L der Abstand zwischen den beiden Punkten.
        /// Der Richtungsvektor f�r diese Parametrisierung ist
        /// der normierte Vektor m_DirVec. Das kompensieren wir im Code
        /// und denken direkt in t aus dem Intervall [0, 1] und normieren
        /// den Vektor nicht.
        /// 
        /// Damit k�nnen wir garantieren, dass die Linie nach
        /// Bogenma� parametrisiert ist.
        ///
        /// Wir besetzen auch die Bahngeschwindigkeiten an den Wegpunkten.
        /// Da wir einen normierten Richtungsvektor einsetzen ist diese
        /// Bahngeschwndigkeit konstant 1.
        /// </remarks>
        protected override void ComputePath()
        {
            var dirVec = p2 - p1;
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            var t = 0.0f;
            var delta = (1.0f) / (float)(NumberOfPoints - 1);
            for (var i = 0; i < NumberOfPoints; i++)
            {
                waypoints[i] = p1 + t * dirVec;
                velocities[i] = 1.0f;
                t += delta;
            }
        }

        /// <summary>
        /// Berechnung des ersten Lookat-Punkts. 
        /// Die Tangente der Linie stimmt mit dem normierten Richtungsvektor.
        /// �berein.
        /// </summary>
        /// <returns>Punkt, der LookAt �bergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return p2;
        }
}
