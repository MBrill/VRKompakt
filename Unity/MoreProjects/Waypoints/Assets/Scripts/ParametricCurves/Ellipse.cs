//========= 2021 - 2024 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

    /// <summary>
    /// Bewegung eines Objekts entlang einer Ellipse
    /// in der xz-Ebene.
    /// </summary>
    public class Ellipse : PathAnimation
    {
        /// <summary>
        /// Erste Halbachse a der Ellipse
        /// </summary>
        [Range(0.01f, 3.0f)]
        [Tooltip("Erste Halbache der Ellipse")]
        public float RadiusA = 0.5f;
        
        /// <summary>
        /// Zweite Halbachse b der Ellipse
        /// </summary>
        [Range(0.01f, 3.0f)]
        [Tooltip("Zweite Halbache der Ellipse")]
        public float RadiusB = 0.5f;
        
        /// <summary>
        /// Variable, die die y-Koordinate des GameObjects abfragt.
        ///
        /// Wir ver�ndern diese y-H�he nicht, die Ellipse liegt parallel zur
        /// x-z-Ebene.
        /// </summary>
        [Range(0.0f, 15.0f)]
        [Tooltip("H�he �ber der xz-Ebene")]
        public float Height = 1.0f;
        
        /// <summary>
        /// Berechnung der Punkte f�r eine Ellipse mit Brennpunkt im Ursprung,
        /// 
        /// Wir verwenden das Parameterintervall [0.0, 2.0*pi]. Damit haben wir
        /// keine Parametrisierung nach Bogenl�nge!
        /// </summary>
        protected override void ComputePath()
        {
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            float t = 0.0f;
            var delta = (2.0f * Mathf.PI) / (float)(NumberOfPoints-1);
            for (int i = 0; i < NumberOfPoints; i++)
            {
                waypoints[i].x = RadiusA * Mathf.Cos(t);
                waypoints[i].y = Height;
                waypoints[i].z = RadiusB * Mathf.Sin(t);
                velocities[i] = m_Velocity(t);
                t += delta;
            }
        }
        
        /// <summary>
        /// Bahngeschwindigkeiten f�r die Ellipse
        /// </summary>
        /// <param name="t">Parameterwert</param>
        /// <returns>Bahngeschwindigkeit am Punkt mit Parameter t</returns>
        private float m_Velocity(float t)
        {
            return Mathf.Sqrt(RadiusA * RadiusA * Mathf.Sin(t) * Mathf.Sin(t) +
                              RadiusB * RadiusB * Mathf.Cos(t) * Mathf.Cos(t));
        }
    }
