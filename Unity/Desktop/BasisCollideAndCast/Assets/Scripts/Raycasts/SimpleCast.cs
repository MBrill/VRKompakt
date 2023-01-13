using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  Einfaches Beispiel eines Raycast in Richtung einer der Koordinatenachsen.
/// </summary>
public class SimpleCast : MonoBehaviour
{
    /// <summary>
    ///  Aufzählungstyp für die Koordinatenachsen
    /// </summary>
    public enum Directions
    {
        Right,
        Left,
        Up,
        Down,
        Forward,
        Back
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
    private readonly Vector3[] m_axis =
    {
        Vector3.right,
        Vector3.left,
        Vector3.up,
        Vector3.down,
        Vector3.forward,
        Vector3.back
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
    /// </remarks>
    void FixedUpdate()
    {
        var ax = transform.TransformDirection(m_axis[(int) Dir]);
        if (m_cast && Physics.Raycast(transform.position, 
            ax, 
            MaxLength))
                Debug.Log(m_Log[(int) Dir]);
    }
}
