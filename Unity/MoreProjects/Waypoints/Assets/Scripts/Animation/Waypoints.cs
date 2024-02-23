//========= 2020 - 2024 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
 /// Interaktive Definition von Zielpunkten
 /// 
 /// Die einzelnen
 /// Punkte sind durch GameObjects gegeben, die im Editor
 /// zugewiesen werden.
 /// 
 ///Wir können die Zielpunkte zentral, mit Hilfe dieser Klasse,
 /// visualisieren oder ausblenden.
 /// </summary>
public class Waypoints : MonoBehaviour
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