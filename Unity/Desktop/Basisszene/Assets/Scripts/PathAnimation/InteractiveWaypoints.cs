//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

// Namesapce
namespace VRKL.MBU
{
    /// <summary>
    /// Interaktive Definition von Zielpunkten
    /// 
    /// Die einzelnen
    /// Punkte sind durch GameObjects gegeben, die im Editor
    /// zugewiesen werden können.
    /// 
    /// wir können die Zielpunkte zentral, mit Hilfe dieser Klasse,
    /// visualisieren oder ausblenden.
    /// </summary>
    public class InteractiveWaypoints : MonoBehaviour
    {
        /// <summary>
        /// Array mit den Wegpunkten
        /// </summary>
        public GameObject[] waypoints;
        /// <summary>
        /// Sollen die Zielobjekte gerendert werden während der Laufzeit?
        /// </summary>
        public bool showTheWaypoints = true;

        /// <summary>
        /// Instanzen der Renderer für die Zielobjekte
        /// </summary>
        private MeshRenderer[] ren;

        /// <summary>
        /// Renderer einstellen und alles vorbereiten
        /// </summary>
        private void Awake()
        {
            if (waypoints.Length > 1)
            {
                this.ren = new MeshRenderer[waypoints.Length];

                for (int i = 0; i < waypoints.Length; i++)
                {
                    this.ren[i] = waypoints[i].GetComponent(typeof(MeshRenderer)) as MeshRenderer;
                    this.ren[i].enabled = showTheWaypoints;
                }
            }
            else
                Debug.LogError("Fehler - Keine GameObjects als Zielobjekte in der Szene!");
        }
    }
}