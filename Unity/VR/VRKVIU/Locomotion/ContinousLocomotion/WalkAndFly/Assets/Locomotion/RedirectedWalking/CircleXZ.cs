//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using System.Drawing;
using UnityEngine;

/// <summary>
/// Repräsentation des Kreises für die Curvature Gain.
/// Wir verwenden einen ebenen Kreis in der xz-Ebene.
/// </summary>
public class CircleXZ 
{
    /// <summary>
    ///  Default-Konstruktor für den einheitskreis
    /// </summary>
    public CircleXZ()
    {
        m_Radius = 1.0f;
        m_MidPoint = Vector2.zero;
    }

    public CircleXZ(float r)
    {
        m_Radius = r;
        m_MidPoint = Vector2.zero;
    }
    
    public CircleXZ(Vector2 mid, float r)
    {
        m_Radius = r;
        m_MidPoint = mid;
    }

    /// <summary>
    /// Parameterdarstellung anwenden.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Vector2 Point(float t)
    {
        return m_MidPoint +
               new Vector2(
                   m_Radius * Mathf.Cos(t / m_Radius),
                   m_Radius * Mathf.Sin(t / m_Radius)
               );
    }

    public void SetMidPoint(Vector2 m)
    {
        m_MidPoint = m;
    }
    
    public void SetMidPoint(float x, float y)
    {
        var m = new Vector2(x, y);
        m_MidPoint = m;
    }
    
    public Vector2 Tangent(float t)
    {
        return new Vector2(
            -Mathf.Sin(t / m_Radius),
            Mathf.Cos(t / m_Radius)
            );
    }

    /// <summary>
    /// Den Winkel der Polarkoordinaten für die Tangente.
    /// </summary>
    /// <param name="t">Parameterwert</param>
    /// <returns>Phi in Grad</returns>
    public float TangentAngle(float t)
    {
        var tangent = Tangent(t);
        return  Mathf.Rad2Deg*Mathf.Atan2(tangent.y, tangent.x) - 90.0f;
    }
    
    private float m_Radius;

    private Vector2 m_MidPoint;


}
