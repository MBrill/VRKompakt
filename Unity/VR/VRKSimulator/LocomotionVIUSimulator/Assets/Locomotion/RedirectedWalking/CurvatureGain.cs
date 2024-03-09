//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Curvature Gain  Controller 
/// </summary>
public class CurvatureGain : RedirectionController
{
    [Header("Curvature Gain")]
    
    /// <summary>
    /// Radius des angestrebente Kreises
    /// </summary>
    /// <remarks>
    /// Wir ignorieren die Grenzen aus der Literatur,
    /// da unsere Arbeitsbereichs sowieso kleiner sind.
    ///
    /// Der größte Kreis, falls der Arbeitsbereich 3x3  ist,
    /// liegt für den Radiius bei ung. 1.4 m, dann haben wir
    /// noch Spielraum an den Grenzen.
    ///
    /// ToDo: Funktion schreiben, die für die Ausmaße des Arbeitsbereichs
    /// den Radius setzt.
    /// </remarks>
    [Tooltip("Radius der angestrebten Kreisbahn ")] 
    [Range(0.5f, 2.0f)]
    public float Radius = 1.0f;
    
    
    protected void Awake()
    {
        m_LastValue = TrackedObject.localPosition.z;
        m_Circle = new CircleXZ(Radius);
    }
    
    /// <summary>
    /// Die Redirection anwenden.
    /// </summary>
    protected override void Redirect()
    {
        // Eulerwinkel werden in Grad verwaltet!
        var diff = TrackedObject.localPosition.z - m_LastValue;

        if (Mathf.Abs(diff) > Mathf.Epsilon)
        {
            Debug.Log("Redirect");
            Debug.Log(diff);
            m_Circle.SetMidPoint(TrackedObject.localPosition.x- Radius,
                TrackedObject.localPosition.z);
            // Mi t diff arbeiten?
            var circlePosition = m_Circle.Point(TrackedObject.localPosition.z);
            Debug.Log(circlePosition);
            gameObject.transform.Translate(-circlePosition.x, 0.0f, circlePosition.y);
            Debug.Log(gameObject.transform.position);
            var tangentAngles= new Vector3(gameObject.transform.rotation.eulerAngles.x,
                m_Circle.TangentAngle(diff),
                gameObject.transform.rotation.eulerAngles.z);
            var rot = gameObject.transform.rotation;
            //Debug.Log(tangentAngles.y);
            rot.eulerAngles = tangentAngles;
            //gameObject.transform.rotation = rot;
        }
        m_LastValue = TrackedObject.localPosition.z;
    }

    /// <summary>
    ///  Instanz eines Kresises in der xz-Ebene für den Spieelkreis.
    /// </summary>
    private CircleXZ m_Circle;
    
    /// <summary>
    /// Speicher für den Vorgänger-Wert des tegtrackten Objekts.
    /// </summary>
    private float m_LastValue;
}
