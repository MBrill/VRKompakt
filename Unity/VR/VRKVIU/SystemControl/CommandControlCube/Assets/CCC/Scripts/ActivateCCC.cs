//========= 2023 - -2024 Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Basisklasse für Actions für die interaktive
/// Aktivierung der Komponente CCC.
/// </summary>
/// <remarks>
/// Die von dieser Klaasse abgeleiteten Versionen verwenden entweder
/// das Input System und Unity XR oder Vive Input Utility.
/// </remarks>
public class ActivateCCC : MonoBehaviour
{ 
    /// <summary>
    /// An welcher Stelle soll CCC erscheinen?
    /// </summary>
     [Tooltip("Welches Objekt bestimmt die Posiion beim Einblenden)")]
    public GameObject Position;
    
    /// <summary>
    /// CCC anzeigen oder nicht?
    /// </summary>
    [Tooltip("CCC beim Start anzeigen?")]
    public bool Show = false;

    /// <summary>
    /// GameObject CCC
    /// </summary>
    protected GameObject TheCCC;
    
    /// <summary>
    /// Verbindung zu CCC in der Szene herstellen.
    /// </summary>
    protected void FindTheCCC()
    {
        TheCCC = GameObject.Find("CCC");
        // Position abfragen, an der CCC angezeigt werden soll
        TheCCC.transform.position = Position.transform.position;
    }
}
