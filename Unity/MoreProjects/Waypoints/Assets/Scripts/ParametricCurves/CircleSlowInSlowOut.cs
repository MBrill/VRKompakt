//========= 2021- 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Parameterdarstellung für einen nach Bogenmaß parametrisierten Kreis
/// in der xz-Ebene.
/// </summary>
public class CircleSlowInSlowOut : SlowInSlowOut
{
        /// <summary>
        /// Radius
        /// </summary>
        [Range(3.0f, 12.0f)]
        [Tooltip("Radius")]
        public float Radius = 6.0f;
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
            float t = 0.0f,
                  x = 0.0f;
            var delta = (2.0f * Mathf.PI * Radius) / (float)NumberOfPoints;

            for (var i = 0; i < NumberOfPoints; i++)
            {
                x = t / (2.0f * Mathf.PI * Radius);
                waypoints[i].x = Radius * Mathf.Cos(H33(x));
                waypoints[i].y = Height;
                waypoints[i].z = Radius * Mathf.Sin(H33(x));
                velocities[i] = H33Prime(x);
                t += delta;
            }
        }

        /// <summary>
        /// Berechnung der ersten Lookat-Punkts. 
        /// Wir berechnen die Tangente am ersten Punkt des Kreises
        /// und berechnen einen Punkt auf der Gerade durch ersten Zielpunkt
        /// mit Richtungsvektor Tangente als ersten Lookat-Punkt.
        /// 
        /// Wir verwenden nicht den Geschwindigkeitsvektor für die Berechnung,
        /// da wir aktuell davon ausgehen, dass wir beim Parameterwert a=0 starten.
        /// Dann ist die erste Orientierung durch forward, die z-Achse,
        /// gegeben.
        /// </summary>
        /// <returns>Punkt, der LookAt übergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return new Vector3(0.0f, 0.0f, 1.0f);
        }
}