//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========

using UnityEngine;

/// <summary>
/// Einfache Realisierung der Siebenmeilenstiefel.
/// </summary>
/// <remarks>
/// Wir schätzen die Bewegungsgeschwindigkeit
/// wie im  Buch beschrieben aus der letzten und der aktuellen
/// Position.
/// </remarks>
public class SimpleBootsWithLogs : ScaledWalking
{
    /// <summary>
    /// Feststellen, ob die Bewegung ausgelöst wird.
    /// </summary>
    /// <remarks>
    /// Wir schätzen die Geschwindigkeit mit Hilfe von finiten Differenzen.
    /// Ist das Ergebnis größer als der Schwellwert wird die Skalierung
    /// ausgelöst.
    ///
    ///  In diesem Fall wird hier sofort diePrognose der Bewegungsrichtung
    /// durchgeführt, da wir bereits alle Daten zur Verfügung haben!
    /// </remarks>
    protected override void Trigger()
    {
        var alpha = 0.0f;
        var position = OrientationObject.transform.localPosition;
        var p = position - m_LastPosition;
        
        var signalVelocity = (1.0f / Time.deltaTime) * p;
        var delta = Vector3.Magnitude(signalVelocity) - Threshold;
        Moving = delta > 0.0f;

        if (Moving)
        {
            alpha = Mathf.SmoothStep(0.0f,
                1,
                2.0f * delta / Threshold);

            object[] args =
            {
                Time.time,
                m_LastPosition.x,
                m_LastPosition.y,
                m_LastPosition.z,
                position.x,
                position.y,
                position.z,
                Vector3.Magnitude(signalVelocity)
            };
            s_Logger.LogFormat(LogType.Log, gameObject,
                "{0:G};{1:G};{2:G}, {3:G};{4:G};{5:G};{6:G};{7:G}", args);
            
            m_Direction = OrientationObject.transform.forward;
            m_PredictDirection(p, alpha);
            m_Direction = m_ManipulateDirection(p);
        }
        else
            m_Direction = OrientationObject.transform.forward;

        m_LastPosition = position;
    }

    /// <summary>
    /// Prognose der Bewegungsrichtung mit Hilfe einer Konvexkombinatin.
    /// </summary>
    /// <remarks>
    /// Aktuell wird alpha = 1, falls wir schneller sind
    /// als das 1,5-fache des Schwellwerts.
    /// Dabei verwenden wir eine Ease-in-Ease-out Veränderung.
    /// </remarks>
    /// <param name="p"></param>
    private void m_PredictDirection(Vector3 p, float alpha)
    {
        var localP = p;
        localP.y = 0.0f;
        localP.Normalize();
        // Blickrichtung ablesen, y-Koordinate ist null, wir führen Walk durch!
        var v = OrientationObject.transform.forward;
        v.y = 0.0f;
        v.Normalize();
        m_Direction = Vector3.Lerp(v, localP, alpha);
        m_Direction.Normalize();
        
        object[] args1 =
        {
            alpha,
            v.x,
            v.z,
            localP.x,
            localP.z,
            m_Direction.x,
            m_Direction.z
        };
        s_Logger.LogFormat(LogType.Log, gameObject,
            "{0:G}; {1:G}; {2:G}, {3:G}; {4:G}; {5:G}; {6:G}", args1);
    }
    
    private Vector3 m_ManipulateDirection(Vector3 p)
    {
        p.y = 0.0f;
        p.Normalize();
        // Lokale y-Achase aus p machen
        // Unity verwendet linkskhändiges Koordinatensystem!
        var q = new Vector3(p.z, 0.0f, -p.x);
        var dp = Vector3.Dot(p, m_Direction);
        var dq = Vector3.Dot(q, m_Direction);
        // Wir manipulieren nur den Anteil d_u
        dp = NonlinearScaling(dp);
        return dp * p + dq * q;
    }
    /// <summary>
    /// In Awake den Speicher für die letzte Position sinnvoll füllen.
    /// </summary>
    /// <remarks>
    /// Tun wir das nicht wird direkt im ersten Frame eine
    /// Bewegung ausgelöst!
    /// </remarks>
    protected override void InitializeDirection()
    {
        base.InitializeDirection();
    }
    
    /// <summary>
    /// Speicher für die Vorgänger-Position.
    /// </summary>
    private Vector3 m_LastPosition;
}
