//========= 2021 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Sehr einfaches leaningmodel.
/// </summary>
/// <remarks>
/// Wir projizieren die Kopf-Position (oder die
/// eines anderen übergebenen GameObjects)
/// auf die y=0 Eben und, berechnen
/// die Polarkoordinaten dieser Projektion.
/// Der Radius der Polarkoordinaten bestimmt die Geschwindigkeit,
/// sobald er eine gewisse Größe überschreitet.
/// Aus dem Winkel bestimmen wir die Richtung der Fortbewegung.
/// </remarks>
public class SimpleLeaning : LeaningModels
{
    /// <summary>
    /// Berechnung der Geschwindigkeit der Fortbewegung
    /// </summary>
    /// <remarks>
    /// Wir rechnen die km/h aus dem Interface durch Division
    /// mit 3.6f in m/s um.
    /// </remarks>
    protected override void UpdateSpeed()
    {
        m_Speed = m_PolarCoordinates[0];
    }
    
    /// <summary>
    /// Bewegungsrichtung auf den forward-Vektor
    /// des Orientierungsobjekts setzen.
    /// </summary>
    protected override void UpdateDirection()
    {
        m_Direction = new Vector3(Mathf.Cos(m_PolarCoordinates[1]),
            0.0f,
            Mathf.Sin(m_PolarCoordinates[1])
        );
    }
    
    /// <summary>
    /// Auslösen der Bewegung, falls der Radius der Projektion
    /// den Schwellwert überschreitet.
    /// </summary>
    protected override void Trigger()
    {
        Debug.Log(">>> Trigger");
        // Differenz zwischen aktuellem und letzten Wert
        var position = new Vector2(LeaningObject.localPosition.x,
            LeaningObject.localPosition.z);
            
         Debug.Log(position);
         var localCoords = position- m_LastPosition;
         var changeVelocity = localCoords.magnitude / Time.deltaTime;
        Debug.Log(localCoords);

        if (changeVelocity >= Threshold)
        {
            Moving = true;
            m_PolarCoordinates = m_Cartesian2Polar(localCoords);
        }
        else
            Moving = false;

        m_LastPosition = position;
        Debug.Log("<<< Trigger");
    }

    /// <summary>
    /// Vektor mit den Polarkoordinaten der Projektion auf die
    /// x-z Ebene
    /// </summary>
    private Vector2 m_PolarCoordinates;

    /// <summary>
    /// Letzte Position zum Vergleich mit der aktuellen Position,
    /// in Polarkoordinaten.
    /// </summary>
    private Vector2 m_LastPosition;
}
