//========= 2021 - 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Parameterdarstellung für einen nach Bogenmaß parametrisierten Kreis
/// in der xz-Ebene.
/// </summary>
public class Circle : PathAnimation
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
        [Range(0.0f, 6.0f)]
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
            var delta = (2.0f * Mathf.PI) / (float)(NumberOfPoints-1);

            for (var i = 0; i < NumberOfPoints; i++)
            {
                waypoints[i].x = Radius * Mathf.Cos(t);
                waypoints[i].y = Height;
                waypoints[i].z = Radius * Mathf.Sin(t);
               velocities[i] = Radius;
                t += delta;
            }
        }

        /// <summary>
        /// Berechnung des ersten Lookat-Punkts. 
        /// </summary>
        /// <returns>Punkt, der LookAt �bergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return waypoints[1];
        }
}
