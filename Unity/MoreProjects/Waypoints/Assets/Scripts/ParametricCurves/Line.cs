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
        ///  Richtungsvektor
        /// </summary>
        private Vector3 _dirVec = Vector3.zero;
        /// <summary>
        /// Berechnung der Punkte für eine Linie zwischen P1 und P2.
        /// </summary>
        /// <remarks>
        /// Wir verwenden das Parameterintervall [0.0, L], dabei
        /// ist L der Abstand zwischen den beiden Punkten.
        /// Der Richtungsvektor für diese Parametrisierung ist
        /// der normierte Vektor _dirVec. Das kompensieren wir im Code
        /// und denken direkt in t aus dem Intervall [0, 1] und normieren
        /// den Vektor nicht.
        /// 
        /// Damit können wir garantieren, dass die Linie nach
        /// Bogenmaß parametrisiert ist.
        /// </remarks>
        protected override void ComputePath()
        {
            _dirVec = p2 - p1;
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            var t = 0.0f;
            var delta = (1.0f) / ((float)NumberOfPoints - 1.0f);
            for (var i = 0; i < NumberOfPoints; i++)
            {
                waypoints[i] = p1 + t * _dirVec;
                velocities[i] = 1.0f;
                t += delta;
            }
        }

        /// <summary>
        /// Berechnung des ersten Lookat-Punkts. 
        /// Die Tangente der Linie stimmt mit dem normierten Richtungsvektor.
        /// überein.
        /// </summary>
        /// <returns>Punkt, der LookAt übergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return p2;
        }
}
