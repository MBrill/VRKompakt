using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///   Raycast in Richtung einer der Koordinatenachsen des lokalen
/// Koordinatensystems eines Objekts.
/// </summary>
/// <remarks>
/// Wir geben bei einem Treffer den Namen des getroffenen
/// Objekts aus, und auch die Koordinaten des Schnittpunkts.
/// </remarks>
public class Raycast : MonoBehaviour
{
    /// <summary>
    ///  Aufzählungstyp für die Koordinatenachsen
    /// </summary>
    public enum Directions
    {
        Right = 0,
        Left = 1,
        Up = 2,
        Down = 3,
        Forward= 4,
        Back = 5
    }
    
    /// <summary>
    /// EWelche Achse des lokalen Koordinatensystems  verwenden wir
    /// als Richtung des Raycasts
    /// </summary>
    [Tooltip("Richtungsvektor")]
    public Directions Dir = Directions.Forward;
    
    /// <summary>
    /// Maximale Länge des Strahls
    /// </summary>
     [Tooltip("Maximale Länge des Strahls")]
    [Range(1.0f, 10.0f)]
    public float MaxLength = 2.0f;

    /// <summary>
    /// Dieses Prefab wird an einem berechneten Schnittpunkt dargestellt.
    /// </summary>
    [Tooltip("Prefab für die Visualisierung des Schnittpunkts")]
    public GameObject HitVis;

    /// <summary>
    /// Sollen Informationen über das Raycasting protokolliert werden?
    /// </summary>
    public bool RayLogs = false;
    
    /// <summary>
    /// Auslösen eines Ray-Casts mit Tastendruck
    /// </summary>
    public InputAction CastAction;

    /// <summary>
    /// Soll der Ray-Cast ausgeführt werden?
    /// </summary>
    private bool m_cast = false;

    /// <summary>
    /// Feld mit den sechs lokalen Koordinatenachsenals Richtungen für den Cast
    /// </summary>
    /// <remarks>
    /// Wie man im Editor sieht sind die lokalen Achsten des Controller-prefabs
    /// verdreht. Deshalb müssen wir right/left und forware/back tauschen!
    private readonly Vector3[] m_axis =
    {
        Vector3.left,
        Vector3.right,
        Vector3.up,
        Vector3.down,
        Vector3.back,
        Vector3.forward
    };
    /// <summary>
    /// Feld mit Ausgaben zu den Koordinatenachsesn
    /// </summary>
    private readonly String[] m_Log =
    {
        "Es gibt ein Objekt rechts von mir!",
        "Es gibt ein Objekt links von mir!",
        "Es gibt ein Objekt oberhalb von mir!",
        "Es gibt ein Objekt unterhalb von mir!",
        "Es gibt ein Objekt vor mir!",
        "Es gibt ein Objekt hinter mir!",
    };
    
    /// <summary>
    /// Callback registrieren für den Tastendruck.
    /// </summary>
    private void Awake()
    {
        CastAction.started += OnPress;
        CastAction.canceled += OnRelease;
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir  unsere Action.
    /// </summary>
    private void OnEnable()
    {
        CastAction.Enable();
    }

    private void Start()
    {
        HitVis = Instantiate(HitVis,
            new Vector3(0.0f, 0.0f, 0.0f),
                          Quaternion.identity);
        HitVis.SetActive(true);
        HitVis.GetComponent<MeshRenderer>().enabled = false;
    }
    /// <summary>
    /// Callback für die  Action CastAction.
    ///<summary>
    private void OnPress(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
    }
    /// <summary>
    /// Callback für die  Action CastAction.
    ///<summary>
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        m_cast = ctx.ReadValueAsButton();
    }
    
    /// <summary>
    /// In Disable für die Szene de-deaktivieren wir unsere Action.
    /// </summary>
    private void OnDisable()
    {
        CastAction.Disable();
    }

    /// <summary>
    ///  Raycasting wird in FixedUpdate ausgeführt!
    /// </summary>
    /// <remarks>
    /// Wir führen den Raycast auf Tastendruck aus, sonst wird
    /// die Konsole mit den immer gleichen Meldungen überschwemmt.
    ///
    /// Wir protokollieren einige Ergebnisse der Schnittberechnung
    /// und stellen am berechneten Schnittpunkt ein kleines Prefab dar.
    /// </remarks>
    void FixedUpdate()
    {
        RaycastHit hitInfo;
        var ax = transform.TransformDirection(m_axis[(int) Dir]);
        if (m_cast && Physics.Raycast(
            transform.position,
            ax,
            out hitInfo,
            MaxLength))
        {
            if (RayLogs)
            {
                Debug.Log(m_Log[(int) Dir]);
                Debug.Log("Getroffen wurde das Objekt " + hitInfo.collider);
                Debug.Log("Der Abstand zu diesem Objekt ist "
                          + hitInfo.distance
                          + " Meter");
            }
            // Prefab um Schnittpunkt visualisieren
            HitVis.GetComponent<MeshRenderer>().enabled = true;
            HitVis.transform.position = hitInfo.point;
        }
        else
            // HitVisausblenden
            HitVis.GetComponent<MeshRenderer>().enabled = false;
    }
}
