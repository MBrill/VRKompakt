//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Sehr einfaches leaningmodel.
/// </summary>
/// <remarks>
/// Wir projizieren die Kopf-Position (oder die
/// eines anderen übergebenen GameObjects)
/// auf die y=0 Ebene, berechnen
/// ihre Polarkoordinaten.
///
/// Der Radius der Polarkoordinaten bestimmt die Geschwindigkeit,
/// sobald er eine gewisse Größe erreicht hat.
///
/// Aus dem Winkel bestimmen wir die Richtung der Fortbewegung.
///
/// Funktioniert, ist aber im Simulator relativ schwer umzusetzen.
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
    /// Bewegungsrichtung auf den forward-Vektor des Orientierungsobjekts setzen.
    /// </summary>
    protected override void UpdateDirection()
    {
        m_Direction = new Vector3(Mathf.Cos(m_PolarCoordinates[1]),
            0.0f,
            Mathf.Sin(m_PolarCoordinates[1])
        );
    }
    
    protected override void Trigger()
    {
        Debug.Log("In Trigger");
        // Differenz zwischen aktuellem und letzten Wert
        var position = new Vector2(LeaningObject.localPosition.x,
            LeaningObject.localPosition.z);
            
         Debug.Log(position);
         var localCoords = position- m_LastPosition;
         var changeVelocity = localCoords.magnitude / Time.deltaTime;
        Debug.Log(localCoords);

        m_PolarCoordinates = m_Cartesian2Polar(localCoords);
        
        if (changeVelocity >= Threshold)
        {
            Moving = true;
            m_PolarCoordinates = m_Cartesian2Polar(localCoords);
        }
        else
        {
            Moving = false;
        }

        m_LastPosition = position;
    }

    protected static Vector2 m_Cartesian2Polar(Vector2 coordinates)
    {
        var returnVector = new Vector2();
        
        returnVector[0] = coordinates.magnitude;
        returnVector[1] = Mathf.Atan2(coordinates.y, coordinates.x);

        return returnVector;
    }

    protected Vector2 m_PolarCoordinates;

    protected Vector2 m_LastPosition;

}
