//========= 2021- 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Parameterdarstellung für einen nach Bogenmaß parametrisierten Kreis
/// in der xz-Ebene mit Slow-In-Slow-Out Verhalten.
/// </summary>
public class CircleSlowInSlowOut : SlowInSlowOut
{
        /// <summary>
        /// Radius
        /// </summary>
        [Range(0.01f, 3.0f)]
        [Tooltip("Radius")]
        public float Radius = 1.0f;
        
        /// <summary>
         /// Variable, die die y-Koordinate des GameObjects abfragt.
         ///
         /// Wir verändern diese y-Höhe nicht, der Kreis liegt parallel zur
         /// x-z-Ebene.
         /// </summary>
        [Range(0.0f, 4.0f)]
        [Tooltip("Höhe über der xz-Ebene")]
        public float Height = 1.0f;
        
        /// <summary>
         /// Berechnung der Punkte für einen Kreis mit Mittelpunkt im Ursprung
         /// 
         /// Wir verwenden das Parameterintervall [0.0, 2.0*pi * r].
         /// Damit stellen wir sicher, dass der Kreis nach Bogenmaß parametrisiert ist.
         /// </summary>
        protected override void ComputePath()
        {
            const float twoPi = 2.0f * Mathf.PI;
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            var arcL = twoPi  * Radius;

            var t = 0.0f;
            var x = 0.0f;
            var delta = arcL / (float)(NumberOfPoints-1);

            for (var i = 0; i < NumberOfPoints; i++)
            {
                x = t / arcL;
                waypoints[i].x = Radius * Mathf.Cos(twoPi*H33( x));
                waypoints[i].y = Height;
                waypoints[i].z = Radius * Mathf.Sin(twoPi*H33(x));
                velocities[i] = H33Prime(x);
                t += delta;
            }
        }

        /// <summary>
        /// Berechnung des ersten Lookat-Punkts. 
        /// gegeben.
        /// </summary>
        /// <returns>Punkt, der LookAt übergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return waypoints[1];
        }
}