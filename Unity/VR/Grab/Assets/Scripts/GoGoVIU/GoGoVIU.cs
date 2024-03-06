using HTC.UnityPlugin.Vive;
using UnityEngine;

public class GoGoVIU : GoGo
{
    /// <summary>
    /// Feststellen, an welchem Controller das Script angehängt ist.
    /// </summary>
    void Awake()
    {
        // Wir gehen davon aus, dass dieses Klaasse als Komponente
        // an einem der Rigs von VIU hängt.
        // Dann ist der parent des gameObjects der Rig.
        m_Rig = GameObject.Find(gameObject.transform.parent.name);
        m_TrackerData = gameObject.GetComponent<VivePoseTracker>();
        if (m_TrackerData == null)
            Debug.Log("VivePoseTracker nicht gefunden!");

        m_computeTheOffset();
    }
    
    private void Update()
    {
        Logger.Debug(">>> GoGo.Update");
        m_computeTheOffset();
        Logger.Debug("<<< GoGo.Update");
    }
    
    /// <summary>
    /// Berechnung des nichtlinearen Offsets für den Controller
    /// </summary>
    protected void m_computeTheOffset()
    {
        m_OffsetRay = m_Rig.transform.position - transform.position;
        // Abstand
        var r = Vector3.Magnitude(m_OffsetRay);
        // Richtungsvektor normieren
        m_OffsetRay.Normalize();

        m_TrackerData.posOffset = m_Poly(r) * m_OffsetRay;
    }
    
    /// <summary>
    /// Die beiden Controller haben eine Komponente
    /// VivePoseTracker, die wir manipulieren!
    /// </summary>
    protected VivePoseTracker m_TrackerData;
    
    /// <summary>
    /// Instanz eines Log4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(GoGoVIU));
    
}
