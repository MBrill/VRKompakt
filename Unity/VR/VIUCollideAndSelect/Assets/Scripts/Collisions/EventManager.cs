using HTC.UnityPlugin.ColliderEvent;
using UnityEngine;

/// <summary>
/// Klasse, die auf der Konsole ausgibt, dass einVIU Collider-Event
/// mit dem GameObject stattgefunden hat, zu dem diese Klasse
/// hinzugef�gt ist.
/// </summary>
/// <remarks>
/// Voraussetzung: das Prefab ViveColliders ist in der Szene enthalten.
///
/// Folgende Events werden behandelt:
/// - HoverEnter
/// - PressEnter
/// - PressExit
///
/// Der Contoller-Button der f�r die Press-Events eingesetzt
/// wird kann im Inspektor ver�ndert werden.
/// </remarks>
public class EventManager : MonoBehaviour, 
    IColliderEventHoverEnterHandler,
    IColliderEventHoverExitHandler,
    IColliderEventPressEnterHandler,
    IColliderEventPressExitHandler
{
    /// <summary>
    /// Button f�r das Press-Event
    /// </summary>
    [Tooltip("Welcher Controller-Button soll f�r Press verwendet werden?")]
    public ColliderButtonEventData.InputButton selectButton = ColliderButtonEventData.InputButton.Trigger;
    
   /// <summary>
   ///  Funktion, die bei HoverEnter aufgerufen wird
   /// </summary>
   /// <param name="eventData"></param>
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        Debug.Log("Ber�hrung hat begonnen!");
    }

   /// <summary>
   ///  Funktion, die bei HoverExit aufgerufen wird
   /// </summary> 
   public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
    {
        Debug.Log("Ber�hrung ist beendet!");
    }
    
   /// <summary>
   ///  Funktion, die bei PressEnter aufgerufen wird
   /// </summary> 
   public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        Debug.Log("Selektion hat stattgefunden!");
    }

   /// <summary>
   ///  Funktion, die bei PressExit aufgerufen wird
   /// </summary> 
   public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
        Debug.Log("Selektion ist aufgehoben!");
    }


}
