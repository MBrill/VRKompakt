//========= 2021- 2024  - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Bewegung eines Objekts entlang einer Linie, die durch das Objekt
/// gegeben ist, an dem diese Komponente hängt, und dem Schwerpunkt
/// eines weiteren GameObjects. 
/// </summary>
public class Line : RopeAnimation
{
    /// <summary>
    /// Das Objekt, das wir bewegen möchten.
    /// </summary>
    [Tooltip("Das bewegte Objekt")]
    public GameObject TargetObject;
    
        
    /// <summary>
    /// Anfangspunkt der Linie, wird aus der Position
    /// des gewählten Objekts bestimmt.
    /// </summary>
    private Vector3 m_p1;

    /// <summary>
    /// Endpunkt der Linie, stimmt mit der Position
    /// des Objekts überein, an diese Komponente
    /// angehängt ist.
    /// </summary>
    [Tooltip("Endpunkt der Linie")] 
    private Vector3 m_p2;

    private void Awake()
    {
        m_p2 = TargetObject.transform.position;
        m_p1 = transform.position;
    }
    
    /// <summary>
    /// Berechnung der Punkte für eine Linie zwischen P1 und P2.
    /// </summary>
    /// <remarks>
    /// Wir verwenden das Parameterintervall [0, L], dabei
    /// ist L der Abstand zwischen den beiden Punkten.
    /// Der Richtungsvektor für diese Parametrisierung ist
    /// der normierte Vektor m_DirVec. Das kompensieren wir im Code
    /// und denken direkt in t aus dem Intervall [0, 1] und normieren
    /// den Vektor nicht.
    /// 
    /// Damit können wir garantieren, dass die Linie nach
    /// Bogenmaß parametrisiert ist.
    ///
    /// Wir besetzen auch die Bahngeschwindigkeiten an den Wegpunkten.
    /// Da wir einen normierten Richtungsvektor einsetzen ist diese
    /// Bahngeschwndigkeit konstant 1.
    /// </remarks>
        protected override void ComputePath()
        {
            var dirVec = m_p2 - m_p1;
            waypoints = new Vector3[NumberOfPoints];
            velocities = new float[NumberOfPoints];
            var t = 0.0f;
            var delta = (1.0f) / (float)(NumberOfPoints - 1);
            for (var i = 0; i < NumberOfPoints; i++)
            {
                waypoints[i] = m_p1 + t * dirVec;
                velocities[i] = 1.0f;
                t += delta;
            }
        }
}