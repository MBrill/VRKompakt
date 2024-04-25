//========= 20221 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Ein einfaches Modell eines Flugzeugs
/// 
/// Dieses Modell ist eine Unity-Version des 
/// Flugzeugmodells, das in VTK für die Visualisierung
/// der Animation von Rotationen im Computergrafik-Buch
/// realisiert wurde. Die hier verwendeten Dreiecke verwenden
/// ein linkshändiges Koordinatensystem
/// 
/// Verwendung: ein leeres GameObject im Editor erzeugen
/// und dieses Skript diesem GameObject hinzufügen.
/// Das polygonale Netz wird als Instanz der Klasse PolyMesh
/// erzeugt.
/// Anschließend können wir dem GameObject einen Shader
/// und eine Farbe zuweisen..
/// 
/// Bei der Ausführung der Anwendung wird das polygonale Netz
/// erstellt und dargestellt.
/// </summary>
public class SimpleAirPlane : PolyMesh
{
         /// <summary>
        /// Wir speichern die Geometrie und die Topologie des Dreiecks ab
        /// und legen die Daten in eine Instanz der Klaasse Mesh.
        /// </summary>
        protected override void Create()
         {
             const int numberOfVertices = 31;
            const int numberOfSubMeshes = 25;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[][] topology = new int[numberOfSubMeshes][];
            Material[] materials = new Material[numberOfSubMeshes];
            
            // Die Daten für die Eckpunkte stammen aus der Datei
            // plane.vtk, die beim Schreiben des Buchs "Computergrafik"
            // für die Demonstration von Rotationen und Quaternionen
            // verwendet wurde.
            vertices[0] = new Vector3(0.0f, 0.0f, 4.5f);
            vertices[1] = new Vector3(1.0f, 1.0f, 3.0f);
            vertices[2] = new Vector3(-1.0f, 1.0f, 3.0f);
            vertices[3] = new Vector3(-1.0f, -1.0f, 3.0f);
            vertices[4] = new Vector3(1.0f, -1.0f, 3.0f);
            vertices[5] = new Vector3(1.0f, 1.0f, -3.0f);
            vertices[6] = new Vector3(-1.0f, 1.0f, -3.0f);
            vertices[7] = new Vector3(-1.0f, -1.0f, -3.0f);
            vertices[8] = new Vector3(1.0f, -1.0f, -3.0f);
            // Flügel rechts
            vertices[9] = new Vector3(1.0f, 0.0f, 2.0f);
            vertices[10] = new Vector3(1.0f, 0.0f, 0.0f);
            vertices[11] = new Vector3(5.0f, 0.0f, 0.0f);
            vertices[12] = new Vector3(5.0f, 0.0f, 1.0f);
            // Flügel links
            vertices[13] = new Vector3(-1.0f, 0.0f, 2.0f);
            vertices[14] = new Vector3(-1.0f, 0.0f, 0.0f);
            vertices[15] = new Vector3(-5.0f, 0.0f, 0.0f);
            vertices[16] = new Vector3(-5.0f, 0.0f, 1.0f);
            // Leitwerk rechts
            vertices[17] = new Vector3(0.05f, 0.0f, -3.0f);
            vertices[18] = new Vector3(0.05f, 0.0f, -1.0f);
            vertices[19] = new Vector3(0.05f, 3.0f, -3.0f);
            // Flügel rechts Unterseite
            vertices[20] = new Vector3(1.0f, -0.05f, 2.0f);
            vertices[21] = new Vector3(1.0f, -0.05f, 0.0f);
            vertices[22] = new Vector3(5.0f, -0.05f, 0.0f);
            vertices[23] = new Vector3(5.0f, -0.05f, 1.0f);
            // Flügel links Unterseite
            vertices[24] = new Vector3(-1.0f, -0.05f, 2.0f);
            vertices[25] = new Vector3(-1.0f, -0.05f, 0.0f);
            vertices[26] = new Vector3(-5.0f, -0.05f, 0.0f);
            vertices[27] = new Vector3(-5.0f, -0.05f, 1.0f);
            // Leitwerk links
            vertices[28] = new Vector3(-0.05f, 0.0f, -3.0f);
            vertices[29] = new Vector3(-0.05f, 0.0f, -1.0f);
            vertices[30] = new Vector3(-0.05f, 3.0f, -3.0f);
            
            for (var i = 0; i < numberOfVertices; i++)
            {
                vertices[i] *= ScalingFactor;
            }

            // Die Einträge in der Topologie beziehen sich auf 
            // die Indizes der Eckpunkte.
            // Die Durchlaufrichtung der Indices ist wichtig, da sonst
            // bei Backface Culling die Dreiecke nicht dargestellt werden.
            // Unity definiert ein Frontface als ein Polygon, das 
            // im Uhrzeigersinn durchlaufen wird!
            topology[0] = new int[3] { 0, 1, 2};
            topology[1] = new int[3] {0, 2, 3};
            topology[2] = new int[3] {0, 3, 4};
            topology[3] = new int[3] {0, 4, 1};
            topology[4] = new int[3] {3, 8, 4};
            topology[5] = new int[3] {3, 7, 8};
            topology[6] = new int[3] {1, 5, 2};
            topology[7] = new int[3] {2, 5, 6};
            topology[8] = new int[3] {1, 4, 8};
            topology[9] = new int[3] {1, 8, 5};
            topology[10] = new int[3] {2, 7, 3};
            topology[11] = new int[3] {2, 6, 7};
            topology[12] = new int[3] {5, 8, 7};
            topology[13] = new int[3] {5, 7, 6};
            // Flügel rechts
            topology[14] = new int[3] {10, 12, 11};
            topology[15] = new int[3] {9, 12, 10};
            // Flügel links
            topology[16] = new int[3] {14, 15, 16};
            topology[17] = new int[3] {13, 14, 16};
            // Leitwerk von rechts
            topology[18] = new int[3] {17, 19, 18};
            // Flügel und Leitwerk auch im umgedrehten Reihenfolge,
            // so erhalten wir doppelseitige Anzeige.
            // To Do: weitere Eckpunkte, um z-Buffer fighting zu verhindern
            // Flügel rechts Unterseite
            topology[19] = new int[3] {21, 22, 23};
            topology[20] = new int[3] {20, 21, 23};
            // Flügel links
            topology[22] = new int[3] {25, 27, 26};
            topology[23] = new int[3] {24, 27, 25};
            // Leitwerk von links
            topology[24] = new int[3] {28, 29, 30};
            
            // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
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
            objectFilter.mesh = simpleMesh;
            objectRenderer.materials = materials;
        }
}
