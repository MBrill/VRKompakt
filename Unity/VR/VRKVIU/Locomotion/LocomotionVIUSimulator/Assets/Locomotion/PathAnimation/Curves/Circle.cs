//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

 /// <summary>
/// Parameterdarstellung für einen Kreis in der xz-Ebene.
/// </summary>
/// <remarks>
/// Der Kreis ist nach Bogenmaß parametrisiert.
///
/// Als Default liegt
/// der Kreis in der Ebene y=0. Dies kann mit Hilfe der Eigenschaft
/// Height verändert werden.
/// </remarks>
 public class Circle : PathAnimation
{
         [Header("Kreis")]
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
        [Range(-15.0f, 15.0f)]
        [Tooltip("Höhe über der xz-Ebene")]
        public float Height = 0.0f;
        
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
            var delta = (2.0f * Mathf.PI) / (float)NumberOfPoints;

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
        /// Berechnung der ersten Lookat-Punkts. 
        /// Wir berechnen die Tangente am ersten Punkt des Kreises
        /// und berechnen einen Punkt auf der Gerade durch ersten Zielpunkt
        /// mit Richtungsvektor Tangente als ersten Lookat-Punkt.
        /// 
        /// Wir verwenden nicht den Geschwindigkeitsvektor f�r die Berechnung,
        /// da wir aktuell davon ausgehen, dass wir beim Parameterwert a=0 starten.
        /// Dann ist die erste Orientierung durch forward, die z-Achse,
        /// gegeben.
        /// </summary>
        /// <returns>Punkt, der LookAt �bergeben werden kann</returns>
        protected override Vector3 ComputeFirstLookAt()
        {
            return new Vector3(0.0f, 0.0f, 1.0f);
        }
}
