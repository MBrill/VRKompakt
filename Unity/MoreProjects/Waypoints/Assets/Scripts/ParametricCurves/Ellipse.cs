//========= 2021 - 2024 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;


    /// <summary>
    /// Bewegung eines Objekts entlang eines Kreises       
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
        [Range(0.01f, 10.0f)]
        [Tooltip("Zweite Halbache der Ellipse")]
        public float RadiusB = 0.5f;
        /// <summary>
        /// Variable, die die y-Koordinate des GameObjects abfragt.
        ///
        /// Wir verändern diese y-Höhe nicht, die Ellipse liegt parallel zur
        /// x-z-Ebene.
        /// </summary>
        [Range(0.0f, 15.0f)]
        [Tooltip("Höhe über der xz-Ebene")]
        public float Height = 1.0f;
        /// <summary>
        /// Berechnung der Punkte für eine Ellipse mit Brennpunkt im Ursprung,
        /// 
        /// Wir verwenden das Parameterintervall [0.0, 2.0*pi]. Damit haben wir
        /// keine Parametrisierung nach Bogenlänge!
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
                velocities[i] = speed(t);
                t += delta;
            }
        }

        /// <summary>
        /// Berechnung der ersten Lookat-Punkts. 
        /// Wir berechnen die Tangente am ersten Punkt der Ellipse
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
            return waypoints[1];
        }


        /// <summary>
        /// Bahngeschwindigkeit für die Ellipse
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private float speed(float t)
        {
            return Mathf.Sqrt(RadiusA * RadiusA * Mathf.Sin(t) * Mathf.Sin(t) +
                              RadiusB * RadiusB * Mathf.Cos(t) * Mathf.Cos(t));
        }
    }
