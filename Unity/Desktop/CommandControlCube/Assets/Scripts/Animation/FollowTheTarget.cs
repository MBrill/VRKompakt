using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Vector3.MoveTowards und Transform.LookAt.
/// </summary>
/// 
public class FollowTheTarget : MonoBehaviour
{
    /// <summary>
    /// Position und Orientierung des verfolgten Objekts
    /// </summary>
    [Tooltip("Das verfolgte Objekt")]
    public Transform playerTransform;

    /// <summary>
    /// Wir können das Verfolgen an- und ausschalten.
    /// </summary>
    public bool IsFollowing = false;
    
    /// <summary>
    /// Wir folgen der Dokumentation von Unity
    /// und dem Abschnitt "Embedding Actions in MonoBehaviours".
    ///
    /// Wir erzeugen eine Button-Action. Das Binding wird das Keyboard
    /// verwendet und die Taste P.
    /// </summary>
    public InputAction FollowAction;
    
    /// <summary>
    /// Geschwindigkeit des Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit")]
    [Range(1.0F, 20.0F)]
    public float speed = 10.0F;
    
    /// <summary>
    /// Eine Action hat verschiedene Zustände, für
    /// die wir Callbacks regristieren können.
    /// Wir könnten wie in der Unity-Dokumentation
    /// teilweise gezeigt das hier gleich mit implementieren.
    /// Hier entscheiden wir uns dafür, die Funktion
    /// OnPress zu registrieren, die wir implementieren
    /// und die den Wert von IsFollowing toggelt.
    /// </summary>
    private void Awake()
    {
        FollowAction.performed += OnPress;
    }
    
    /// <summary>
    /// In Enable für die Szene aktivieren wir auch unsere Action.
    /// </summary>
    private void OnEnable()
    {
        FollowAction.Enable();
    }
    
    /// <summary>
    /// Bewegung in Update
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: Überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void Update ()
    {
        if (!IsFollowing) return;
        transform.position = Vector3.MoveTowards(transform.position,
            playerTransform.position,
            speed * Time.deltaTime);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(playerTransform);
    }
    
    /// <summary>
    /// In Disable für die Szene de-aktivieren wir auch unsere Action.
    /// </summary>
    private void OnDisable()
    {
        FollowAction.Disable();
    }

    private void OnPress(InputAction.CallbackContext ctx)
    {
        IsFollowing = !IsFollowing;
    }
}
