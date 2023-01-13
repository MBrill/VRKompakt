using UnityEngine;

/// <summary>
/// Klasse mit Funktionen für die Reaktion auf den CollisionTrigger.
/// </summary>
/// <remarks>
/// Wir implementieren Funktionen wie OnTriggerEnter und OnTriggerExit.
/// Diese Funktionen erhalten die Information über das Objekt, mit
/// dem die Kollision stattgefunden hat.
/// </remaks>
public class TriggerManager : MonoBehaviour
{
        /// <summary>
        /// Das GameObject ist mit einem weiteren Objekt in der Szene kollidiert.
        /// Die Kollision hat gerade begonnen.
        /// </summary>
        /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
        void OnTriggerEnter(Collider otherObject)
        {
            Debug.Log(">>> OnTriggerEnter");
            Debug.Log("Trigger-Event bei " + otherObject.name + " erstmals ausgelöst");
            Debug.Log("<<< OnTriggerEnter");
        }

        /// <summary>
        /// Da Die Kollision mit einem weiteren Objekt in der Szene
        /// ist beendet.
        /// </summary>
        /// <param name="otherObject">Objekt, mit dem die Kollision stattgefunden hat</param>
        void OnTriggerExit(Collider otherObject)
        {
            Debug.Log(">>> OnTriggerExit");
            Debug.Log("Trigger-Event bei " + otherObject.name + " ist beendet!");
            Debug.Log("<<< OnTriggerExit");
        }
        
        /// <summary>
        /// Die Kollision mit einem weiteren Objekt in der Szene
        /// hält an.
        /// </summary>
        /// <param name="otherObject">Objekt, mit dem die Kollision stattfindet</param>
        void OnTriggerStay(Collider otherObject)
        {
            Debug.Log(">>> OnTriggerStay");
            Debug.Log("Trigger-Event mit " + otherObject.name);
            Debug.Log("<<< OnTriggerStay");
        }
}
