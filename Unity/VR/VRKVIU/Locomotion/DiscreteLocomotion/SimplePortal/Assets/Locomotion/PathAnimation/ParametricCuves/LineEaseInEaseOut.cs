//========= 2021 - 2024 Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Bewegung eines Objekts entlang einer Linie zwischen zwei Punkten.
/// </summary>
/// <remarks>
/// Wir verwenden das Hermite-Polynom H33 für einn Ease-In-Ease-Out Effekt. 
/// </remarks>
public class LineEaseInEaseOut : EaseInEaseOut
{
         [Header("Ease-in-Ease-out Linie")]
        /// <summary>
        /// Anfangspunkt
        /// </summary>
        [Tooltip("Anfangspunkt der Linie")]
        public Vector3 p1 = Vector3.zero;
        
        /// <summary>
        /// Endpunkt
        /// </summary>
        [Tooltip("Endpunkt der Linie")]
        public Vector3 p2 = Vector3.forward;

        /// <summary>
        /// Berechnung der Punkte für eine Linie zwischen P1 und P2.
        /// 
        /// Wir verwenden das Parameterintervall [0.0, L], dabei
        /// ist L der Abstand zwischen den beiden Punkten.
        /// Der Richtungsvektor für diese Parametrisierung ist
        /// der normierte Vektor _dirVec. Das kompensieren wir im Code
        /// und denken direkt in t aus dem Intervall [0, 1] und normieren
        /// den Vektor nicht.
        /// 
        /// Damit können wir garantieren, dass die Linie nach
        /// Bogenmaß parametrisiert ist.
        /// </summary>
        protected override void ComputePath()
        {
            m_arcL = Vector3.Distance(p1, p2);
            m_dirVec = p2 - p1;
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            var t = 0.0f;
            var delta = (1.0f) / ((float)NumberOfPoints - 1.0f);
            for (var i = 0; i < NumberOfPoints; i++)
            {
                waypoints[i] = p1 + Mathf.SmoothStep(0.0f, 1.0f, t) * m_dirVec;
                velocities[i] = H33Prime(t);
                t += delta;
            }
        }

        /// <summary>
        /// Berechnung der ersten Lookat-Punkts. 
        /// Die Tangente der Linie stimmt mit dem normierten Richtungsvektor
        /// überein.
        /// </summary>
        /// <returns>Punkt, der LookAt übergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return p2;
        }
        
        /// <summary>
        /// Bogenlänge der Linie
        /// </summary>
        private float m_arcL = 0.0f;
        
        /// <summary>
        ///  Richtungsvektor
        /// </summary>
        private Vector3 m_dirVec = Vector3.zero;
}
    
