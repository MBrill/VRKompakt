//========= 2020 -  2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;
using HTC.UnityPlugin.Vive;


/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Transform.MoveTowards und Transform.LookAt.
/// </summary>
/// <remarks>
/// In dieser Version können wir das Verfolgen an-
/// und abschalten. Typischer Weise verfolgen wir einen der Controller.
/// Wir verfolgen nur, falls an diesem Controller der Trigger oder ein andere,
/// im Inspektor konfigurierbarer Button gedrückt ist.
/// Die Position des Verfolgers und des Controllers werden mit Log4Net
/// protokolliert.
/// </remarks>
public class FollowTheControllerWithLogs : MonoBehaviour
{
   /// <summary>
    /// Welcher Controller wird verfolgt?
    /// </summary>
    /// <remarks>
    ///Default ist links..
    /// </remarks>
    [Tooltip("Welcher Controller (links/rechts) wird verfolgt?")]
    public controllerChoice FollowedController = controllerChoice.Links;
    /// <summary>
    /// Der verwendete Button kann im Editor mit Hilfe
    /// eines Pull-Downs eingestellt werden.
    /// </summary>
    /// <remarks>
    /// Default ist der Trigger des Controllers. Der Controller
    /// wird verfolgt, so lange der Button gedrückt gehalten
    /// wird.
    /// </remarks>
    [Tooltip("Welcher Button auf dem Controller soll verwendet werden?")]
    public ControllerButton theButton = ControllerButton.Trigger;
    /// <summary>
    /// Die Bewegung erfolgt, falls dieser logische Wert true ist.
    /// </summary>
    [Tooltip("Soll sich der Verfolger sofort bewegen, ohne Button Click?")]
    public bool Move = false;
    /// <summary>
    /// Geschwindigkeit des Verfolgers
    /// </summary>
    [Tooltip("Geschwindigkeit des Verfolgers")]
    [Range(0.2F, 10.0F)]
    public float speed = 1.0F;
    /// <summary>
    /// Position und Orientierung des verfolgten Controllers
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Handrodes ausgewählten Controllers
    /// </summary>
    private HandRole m_viveRole = HandRole.LeftHand;
    
    /// <summary>
    /// Instanz einesLog4Net Loggers
    /// </summary>
    private static readonly log4net.ILog Logger 
        = log4net.LogManager.GetLogger(typeof(FollowTheControllerWithLogs));
    
    private void Awake()
    {
        string[] names = new string[] { "LeftHand", "RightHand" };
        player = GameObject.Find(names[(int)FollowedController]);
        if (player == null)
            Debug.LogError("Kein Controller in der Szene vorhanden");
        
        // Die HandRole setzen. Die Variable benötigen wir bei der
        // Registrierung der Listener.
        if (FollowedController == controllerChoice. Rechts)
            m_viveRole = HandRole.RightHand;
    }

    /// <summary>
    /// Bewegung in LateUpdate
    /// </summary>
    /// <remarks>
    /// Erster Schritt: Button abfragen und bewege, falls gedrücktn.
    /// Wir geben die forward-Richtung des gesteuerten Objekts
    /// mit Hilfe von Debug.dDrawRay aus. Darauf achten, dass die
    /// Ausgabe der Gizmos im Player aktiviert ist!
    /// </remarks>
    private void LateUpdate()
    {
        if (!Move)
            return;

        var source = transform.position;
        var target = player.transform.position;
        // Schrittweite
        var stepSize = speed * Time.deltaTime;
        // Neue Position berechnen
        transform.position = Vector3.MoveTowards(source, target, stepSize);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(player.transform);

        object[] args =
        {
            gameObject.name,
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z,
        };
        Logger.InfoFormat("{0}; {1}; {2}; {3}", args);
        
        object[] args1 =
        {
            "Controller",
            target.x,
            target.y,
            target.z
        };
        Logger.InfoFormat("{0}; {1}; {2}; {3}", args1);
    }

    private void m_Go()
    {
        Debug.Log("In Go");
        Move = true;
    }
    
    private void m_Stop()
    {
        Move = false;
    }
    
    /// <summary>
    /// Listener für den Controller registrieren
    /// </summary>
    private void OnEnable()
    {
        ViveInput.AddListenerEx(m_viveRole,
            theButton,
            ButtonEventType.Down,
            m_Go);

        ViveInput.AddListenerEx(m_viveRole,
            theButton,
            ButtonEventType.Up,
            m_Stop);
    }
    
    /// <summary>
    /// Listener wieder aus der Registrierung
    /// herausnehmen beim Beenden der Anwendung
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(m_viveRole,
            theButton,
            ButtonEventType.Down,
            m_Go);

        ViveInput.RemoveListenerEx(m_viveRole,
            theButton,
            ButtonEventType.Up,
            m_Stop);
    }
}
