//========= 2022 -2024 -  Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Basisklasse für die Go Go Technik.
/// </summary>
/// <remarks>
/// Hier finden wir alle Methoden und Variablen,
/// die unabnhängig vom verwendeten VR Package sind.
/// </remarks>
public class GoGo : MonoBehaviour
{
        
    /// <summary>
    /// Schwellwert, bei dem Go-Go umschaltet in Meter.
    /// </summary>
    /// <remarks>
    /// Der Abstand zwischen Rig-Position und einer der Controller
    /// zu Beginn im Simulator beträgt ung. 1.6 Meter.
    /// </remarks>
    public float Threshold = 2.0f;

    /// <summary>
    /// Konstantek  in der Funktion.
    /// </summary>
    /// <remarks>
    /// Defaultwert wie in Paper und Tex auf 1/6 gesetztt.
    /// </remarks>
    public float ParameterK =  0.16666666f;
    
    /// <summary>
    /// Nichtlinearer Offset in Go-Go
    /// </summary>
    /// <param name="r">Abstand zwischen Head und Controller</param>
    /// <returns></returns>
    protected float m_Poly(float r)
    {
        if (r > Threshold)
            return ParameterK * (r - Threshold)*(r - Threshold);
        else
            return 0.0f;
    }

    /// <summary>
    /// Wir gehen davon aus, dass der parent des aktuellen
    /// Objekts eines der VIU-Rigs ist!
    /// </summary>
    protected GameObject m_Rig;
    
    /// <summary>
    /// Die Richtung vom Rig  zum Controller
    /// </summary>
    /// <remarks>
    /// Wir gehen davon aus, dass dieser Vektor normiert ist!
    /// </remarks>
    protected Vector3 m_OffsetRay;
    
    /// <summary>
    /// Instanz eines Log4Net Loggers
    /// </summary>
    protected static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(GoGo));
}
