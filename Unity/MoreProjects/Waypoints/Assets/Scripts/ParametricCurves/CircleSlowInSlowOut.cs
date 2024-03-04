//========= 2021- 2024  - Copyright Manfred Brill. All rights reserved. ===========

using System;
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
        [Range(0.01f, 6.0f)]
        [Tooltip("Radius")]
        public float Radius = 1.0f;
        /// <summary>
         /// Variable, die die y-Koordinate des GameObjects abfragt.
         ///
         /// Wir verändern diese y-Höhe nicht, der Kreis liegt parallel zur
         /// x-z-Ebene.
         /// </summary>
        [Range(0.0f, 15.0f)]
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
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            var t = 0.0f;
            var arcLength = 2.0f * Mathf.PI * Radius;
            var delta = arcLength / (float)(NumberOfPoints-1);

            for (var i = 0; i < NumberOfPoints; i++)
            {
                var x = t / arcLength;
                waypoints[i].x = Radius * Mathf.Cos(arcLength*H33(x));
                waypoints[i].y = Height;
                waypoints[i].z = Radius * Mathf.Sin(arcLength*H33(x));
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