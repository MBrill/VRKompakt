//========= 2022 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Klasse für die Erzeugung eines Hexaeders mit Seitenlänge 1
/// </summary>
/// <remarks>
/// Diese Klasse wird von der virtuellen Klasse PolyMesh abgeleitet.
/// Die Facetten werden jeweils mit zwei Dreiecken realisiert.
/// </remarks>
public class Hexaeder : PolyMesh
{
       /// <summary>
       /// Wir speichern die Geometrie und die Topologie des Dreiecks ab
       /// und legen die Daten in eine Instanz der Klaasse Mesh.
       /// </summary>
        protected override void Create()
        {
            const int numberOfVertices = 8;
            const int numberOfSubMeshes = 12;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[][] topology = new int[numberOfSubMeshes][];
            Material[] materials = new Material[numberOfSubMeshes];

            vertices[0] = new Vector3( 0.5f,  0.5f, -0.5f );
            vertices[1] = new Vector3( 0.5f,  0.5f,  0.5f );
            vertices[2] = new Vector3(-0.5f,  0.5f,  0.5f );
            vertices[3] = new Vector3( -0.5f, 0.5f, -0.5f ) ;
            vertices[4] = new Vector3( 0.5f, -0.5f, -0.5f ) ;
            vertices[5] = new Vector3( 0.5f, -0.5f,  0.5f);
            vertices[6] = new Vector3(-0.5f, -0.5f,  0.5f);
            vertices[7] = new Vector3(-0.5f, -0.5f, -0.5f);

            // In der Basisklasse gibt einen Skalierungsfaktor,
            // den wir hier anwenden.
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

