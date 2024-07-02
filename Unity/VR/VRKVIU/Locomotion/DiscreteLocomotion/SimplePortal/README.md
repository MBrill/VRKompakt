# SimplePortal

Im Buch beschreiben wir ein ganz einfaches Portal, das in der Szene *Gang* auf der Basis
von z-Koordinaten arbeitet. Wird eine definierbare z-Koordinate überschritten erfolgt eine Pfad-Anmation.

Hier implementieren wir ein Portal, das von einem definierten Punkt in der Szene zu einer Zielposition
führt - wie ein Teleport. Das Portal wird aktiviert, falls wir nah genug am Portal sind. Dabei wird der
Abstand in der x-z-Ebene berechnet. Ist das Portal aktiv können wir mit Hilfe eines Buttons auf dem Controller
die Positionsveränderung ausführen.

Für die Visualisierung des Portals und der Zielposition finden wir zwei Prefabs und ein Material für die
visuelle Anzeige, dass das Portal aktiv ist. Die beiden Positionen werden durch zwei leere GameObjects
in der Szene definiert.

## Eingaben für die Anwendung
In der VR-Anwendung können wir die folgenden Eingaben machen:

- Bewegen wir uns in die Nähe des Portals wird dieses aktiviert. Wir erkennen dies an einer 
Farbveränderung des Portals und der Zielposition.
- mit dem im Inspektor ausgewählten Button auf einem der Controller können wir die
Positionsveränderung auslösen.

Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
