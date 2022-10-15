using UnityEngine;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von 
/// Transform.MoveTowards und Transform.LookAt.
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
    /// Geschwindigkeit des Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit")]
    [Range(1.0F, 20.0F)]
    public float speed = 10.0F;

    /// <summary>
    /// Bewegung in LateUpdate
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: Überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void LateUpdate ()
    {
        // Schrittweite
		float stepSize = speed * Time.deltaTime;

        Vector3 source = transform.position;
        Vector3 target = playerTransform.position;
        // Neue Position berechnen
		transform.position = Vector3.MoveTowards(source, target, stepSize);
        // Orientieren mit FollowTheTarget - wir "schauen" auf das verfolgte Objekt
        transform.LookAt(playerTransform);
    }
}
