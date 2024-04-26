//========= 2022 -2024 -  Copyright Manfred Brill. All rights reserved. ===========
using HTC.UnityPlugin.Vive;
using UnityEngine;

/// <summary>
/// Komponente mit Interface auf der Basis von Vive Input Utility.
/// </summary>
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
        if (m_Rig == null)
            Debug.Log("VivePoseTracker nicht gefunden!");
        m_TrackerData = gameObject.GetComponent<VivePoseTracker>();
        if (m_TrackerData == null)
            Debug.Log("VivePoseTracker nicht gefunden!");

        m_computeTheOffset();
    }
    
    /// <summary>
    /// Offset für den Controller in Update berechnen.
    /// </summary>
    private void Update()
    {
        Logger.Info(">>> GoGo.Update");
        m_computeTheOffset();
        Logger.Info("<<< GoGo.Update");
    }
    
    /// <summary>
    /// Berechnung des nichtlinearen Offsets für den Controller
    /// </summary>
    protected void m_computeTheOffset()
    {
        Logger.Info(">>> GoGo.m_computeTheOffset");
        m_OffsetRay = m_Rig.transform.position - transform.position;
        // Abstand
        var r = Vector3.Magnitude(m_OffsetRay);
        Logger.Info("Abstand");
        Logger.Info(r);
        // Richtungsvektor normieren
        m_OffsetRay.Normalize();
        
        m_TrackerData.posOffset = m_Poly(r) * m_OffsetRay;
        Logger.Info(m_TrackerData.posOffset);
    }
    
    /// <summary>
    /// Die beiden Controller haben eine Komponente
    /// VivePoseTracker, die wir manipulieren!
    /// </summary>
    protected VivePoseTracker m_TrackerData;

}
