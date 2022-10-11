//========= 2022 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Abstrakte Klasse für die Erzeugung eines polygonalen Netzes während der Laufzeit einer Anwendung
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))] 
[ExecuteInEditMode]
public abstract class PolyMesh : MonoBehaviour
{
        /// <summary>
        /// Kategorie des Shaders, den wir einsetzen.
        /// </summary>
        /// <remarks>
        /// Wir verwenden "Standard" als Default. Falls wir
        /// eine Colormap verwenden bietet es sich an, diesen
        /// Default auf "Unlit/Texture" zu ändern.
        /// </remarks>
        [Tooltip("Kategorie für den verwendeten Shader")]
        public string m_ShaderName = "Standard";
        
        /// <summary>
        /// Farbe für das Netz.
        /// </summary>
        /// <remarks>
        ///Default ist Gelb.
        /// </remarks>
        [Tooltip("Material für die Farbe des polygonalen Netzes")]
        public Color netColor = Color.yellow;
        
        /// <summary>
        /// Wir erzeugen eine Material-Komponente,
        /// so dass wir im Editor dem Netz ein Material zuweisen können.
        /// </summary>

        [Range(0.05f, 1.5f)] [Tooltip("Skalierungsfaktor für gleichmässige Skalierung")]
        public float ScalingFactor = 1.0f;
        /// <summary>
        /// Wir benötigen eine Instanz der Klasse MeshFilter.
        /// </summary>
        protected MeshFilter objectFilter;
        /// <summary>
        /// Mit Hilfe einer Instanz von MeshRenderer stellen wir den platonischen Körper dar.
        /// </summary>
        protected MeshRenderer objectRenderer;

        /// <summary>
        /// Material erstellen.
        /// </summary>
        /// <remarks>
        /// Wir verwenden die eingestellte Shader-Kategorie und
        /// die Farbe.
        ///
        /// Dabei nutzen wir nicht aus, dass wir pro Dreieck ein eigenes
        /// Material vergeben können.
        /// </remarks>
        protected Material CreateMaterial()
        {
            var mat = new Material(Shader.Find(m_ShaderName))
            {
                color = netColor
            };
            return mat;
        }
        /// <summary>
        /// Das polygonale Netz wird in der abgeleiteten Klasse
        /// mit Hilfe der Funktion Create erzeugt!
        /// </summary>
        protected abstract void Create();

        /// <summary>
        /// In Awake MeshRenderer und MeshFilter zuordnen und Netz erzeugen.
        /// 
        /// <remarks>
        /// Die Start-Funktion wird hier nicht deklariert und kann in 
        /// abgeleiteten Klassen implementiert werden!
        /// </remarks>
        /// </summary>
        protected virtual void Awake()
        {
            this.objectFilter = gameObject.GetComponent<MeshFilter>();
            this.objectRenderer = gameObject.GetComponent<MeshRenderer>();

            // Polygonales Netz erzeugen
            Create();
        }
}
