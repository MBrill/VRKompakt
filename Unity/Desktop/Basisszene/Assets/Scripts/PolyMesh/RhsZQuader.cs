//========= 2021 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

// Namespace
namespace VRKL.MBU
{
    /// <summary>
    /// Klasse für die Erzeugung eines Quaders in y-Richtung eines rechtshändigen Koordinatensystems.
    /// <remarks>
    /// Da wir das rechtshändige Koordinaten in Unity (verwendet ein LHS) verwenden und die Pfad-Animation
    /// mit Hilfe von MoveToward realisiert wird entspricht die visualiserte z-Richtung, die Binormale,
    /// der up-Richtung in Unity - also der y-Achse in Unity.
    /// </remarks>
    /// </summary>
    public class RhsZQuader : PolyMesh
    {
        /// <summary>
        /// Wir erzeugen für jedes Face des Quaders ein SubMesh.
        /// Auch das Material muss anschließend für jedes SubMesh erzeugt
        /// und zugewiesen werden. Das könnte verwendet werden um
        /// für jedes Face ein eigenes Material zu verwenden.
        ///
        /// Der Quader wurde von der Klasse Hexaeder abgeleitet und
        /// die x-Koordinaten wurden verändert. Der Quader startet
        /// bei x=0 und endet bei x=1. Die y- und z-Koordinaten
        /// wurden verkleinert, da wir eine Achse visualisieren möchten.
        /// </summary>
        protected override void Create()
        {
            const int numberOfVertices = 8;
            const int numberOfSubMeshes = 12;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[][] topology = new int[numberOfSubMeshes][];
            Material[] materials = new Material[numberOfSubMeshes];

            vertices[0] = new Vector3( 0.05f,  1.0f, -0.05f );
            vertices[1] = new Vector3( 0.05f,  1.0f,  0.05f );
            vertices[2] = new Vector3(-0.05f,  1.0f,  0.05f );
            vertices[3] = new Vector3( -0.05f, 1.0f, -0.05f ) ;
            vertices[4] = new Vector3( 0.05f,  0.0f, -0.05f ) ;
            vertices[5] = new Vector3( 0.05f,  0.0f,  0.05f);
            vertices[6] = new Vector3(-0.05f,  0.0f,  0.05f);
            vertices[7] = new Vector3(-0.05f,  0.0f, -0.05f);

            for (var i = 0; i < numberOfVertices; i++)
                vertices[i] *= ScalingFactor;
            
            // Die Einträge in der Topologie beziehen sich auf 
            // die Indizes der Eckpunkte.
            topology[0] = new int[3] { 0, 2, 1 };
            topology[1] = new int[3] { 2, 0, 3 };
            topology[2] = new int[3] { 5, 6, 4 };
            topology[3] = new int[3] { 4, 6, 7 };
            topology[4] = new int[3] { 5, 0, 1 };
            topology[5] = new int[3] { 0, 5, 4 };
            topology[6] = new int[3] { 2, 6, 5 };
            topology[7] = new int[3] { 5, 1, 2 };
            topology[8] = new int[3] { 4, 3, 0 };
            topology[9] = new int[3] { 3, 4, 7 };
            topology[10] = new int[3] { 6, 2, 7 };
            topology[11] = new int[3] { 7, 2, 3 };

            // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
            // Es wäre möglich weniger als vier SubMeshes zu erzeugen,
            // solange wir keine Dreiecke in einem Submesh haben, die eine
            // gemeinsame Kante aufweisen!
            Mesh simpleMesh = new Mesh()
            {
                vertices = vertices,
                subMeshCount = numberOfSubMeshes
            };
            // Wir nutzen nicht aus, dass wir pro Submesh ein eigenes
            // Material verwenden.
            for (var i = 0; i < numberOfSubMeshes; i++)
            {
                simpleMesh.SetTriangles(topology[i], i);
                materials[i] = meshMaterial;
            }

            // Unity die Normalenvektoren und die Bounding-Box berechnen lassen.
            simpleMesh.RecalculateNormals();
            simpleMesh.RecalculateBounds();
            simpleMesh.OptimizeIndexBuffers();

            // Zuweisungen für die erzeugten Komponenten
            this.objectFilter.mesh = simpleMesh;
            this.objectRenderer.materials = materials;
        }
    }
}