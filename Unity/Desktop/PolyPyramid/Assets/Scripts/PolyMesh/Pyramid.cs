//========= 2023 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Klasse für die Erzeugung einer Pyramide mit quadratischer Grundfläche.
/// </summary>
/// <remarks>
/// Diese Klasse wird von der virtuellen Klasse PolyMesh abgeleitet.
/// Die Grundfläche hat die Kantenlänge 1, die Höhe der Pyramide
/// kann mit Hilfe einer public-Variablen eingestellt werden.
/// </remarks>
public class Pyramid : PolyMesh
{
        /// <summary>
        /// Höhe der Pyramide über die Grundfläche.
        /// </summary>
        /// <remarks>
        /// Default der Höhe ist 1.0.
        /// </remarks>
        public float Height = 1.0f;
        /// <summary>
       /// Wir speichern die Geometrie und die Topologie des Dreiecks ab
       /// und legen die Daten in eine Instanz der Klasse Mesh.
       /// </summary>
        protected override void Create()
        {
            const int numberOfVertices = 5;
            const int numberOfSubMeshes = 6;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[][] topology = new int[numberOfSubMeshes][];
            Material[] materials = new Material[numberOfSubMeshes];

            vertices[0] = new Vector3( -0.5f,  0.0f, -0.5f );
            vertices[1] = new Vector3( 0.5f,  0.0f,  -0.5f );
            vertices[2] = new Vector3(0.5f,  0.0f,  0.5f );
            vertices[3] = new Vector3( -0.5f, 0.0f, 0.5f ) ;
            vertices[4] = new Vector3( 0.0f, Height, 0.0f) ;

            // In der Basisklasse gibt einen Skalierungsfaktor,
            // den wir hier anwenden.
            for (var i = 0; i < numberOfVertices; i++)
                vertices[i] *= ScalingFactor;
            
            // Die Einträge in der Topologie beziehen sich auf 
            // die Indizes der Eckpunkte.
            topology[0] = new int[3] { 0, 1, 3 };
            topology[1] = new int[3] { 2, 3, 1 };
            topology[2] = new int[3] { 0, 4, 1 };
            topology[3] = new int[3] { 1, 4, 2 };
            topology[4] = new int[3] { 3, 4, 0 };
            topology[5] = new int[3] { 2, 4, 3 };

            // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
            // Es wäre möglich weniger als vier SubMeshes zu erzeugen,
            // solange wir keine Dreiecke in einem Submesh haben, die eine
            // gemeinsame Kante aufweisen!
            Mesh simpleMesh = new Mesh()
            {
                vertices = vertices,
                subMeshCount = numberOfSubMeshes
            };

            var mat = CreateMaterial();
            for (var i = 0; i < numberOfSubMeshes; i++)
            {
                simpleMesh.SetTriangles(topology[i], i);
                materials[i] = mat;
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
