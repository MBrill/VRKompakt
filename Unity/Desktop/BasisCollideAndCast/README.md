# BasisCollideAndCast

Dieses Unity-Projekt enthält die Assets aus dem Projekt BasisComponents.
Die enthaltenen Szenen enthalten Demos zum Thema Trigger, Collider und Raycast.

Die Assets für die Collisions finden wir im Verzeichnis Assets/Collision.
Analog finden wir die Assets für das Raycasdting im Verzeichnis Assets/Raycasting.

## Szene BasisTrigger
diese Szenen zeigt wie man mit einem statischen Collider Trigger
Trigger-Events abfragen kann.

Das Objekt *KugelLinksVorneKlein4* ist ein statischer Trigger, denn
das Objekt besitzt einen Collider und die Eigenschaft *isTrigger* isdt ausgewählt.
Zusätzlich hat dieses Objekt eine Komponente vom Typ *RigidBody*.

Wir können das Objekt mit Hilfe von Player2D bewegen. diese Klasse
verwendet das neue Input System.

Hauptgegenstand der Demo ist die Komponente *CollierManager*.
Diese C#-Klasse setzt voraus, dass es als Komponenten einem statischenCollider
hinzugefügt wurde. Das wird mit entsprechenden [RequireComponent]-Attributen sichergestellt.
In der Klasse werden die Funmktionen

- OnTriggerEnger,
- OnTriggerExit, und
- OnTriggerStay

implementiert. Es gibt keine weitere Funktionalität, die drei Events werden
auf der Konsole mit Debug.Log angezeigt.

## Szene BasisTouch
Diese Szenen ist ähnlich aufgebaut wie die Szene *BasisTrigger*.
Die Komponenten *TriggerManager* wird durch die Komponenten *TouchHighlighter*
ersetzt. 
In der Klasse werden wieder die Funmktionen

- OnTriggerEnger,
- OnTriggerExit, und
- OnTriggerStay

implementiert. Statt einer Ausgabe auf der Konsole verändern berührte Objekte
ihr Material.

## Szene BasisCollision
Diese Szenen ist sehr ähnlich aufgebaut wie die Szene *BasisTrigger*.
Die Komponente *TriggerManager* am Objekt *KugelLinksVorneKlein4* wurde durch
die Klasse *CollisionManager* ersetzt.

In der Klasse werden die Funmktionen

- OnCollisionEnger,
- OnCollisionExit, und
- OnCollisionStay

implementiert.  Es gibt keine weitere Funktionalität, die drei Events werden
auf der Konsole mit Debug.Log angezeigt.

## Szene BasisCast
Demonstration Raycasting aus *Physics*.

Als Vorbereitung auf den Einsatz in VR-Anwendungen wurde aus dem xRI-Package das Modell eines *generic controller*
in dieses Projekt kopiert. Das Original findet man in den UXR-Projekten im Verzeichnis *ExampleAssets* .
Kopiert wurden die Materialien, die fbx-Dateien und die Prefabs.

ie Funktionalität ist jedoch unabhängig von diesem Modell. Jedes interaktiv steuerbare Objekt in der Szenekönnten 
als Ursprung des Strahls eingesetzt werden.
Für die interaktive Steuerung wird die Komponente *Playercontrol2D* eingesetzt.

In der Szenen gibt es beim Objekt *XRControllerRight* drei verschiedene Komponenten für
das Raycasdting. 
das Input System für das Auslösen des Raycast.   
Die Input Actions sind aktuell so gewählt, dass wir theoretisch
alle drei Komponenten aktivieren können. Die Aktivierung liegt auf den drei maustasten.
Die eigentliche Funktionalität finden wir in Basis-Klassen der verwendeten Komponenten,
die unabhängig von Eingaben sind.

Für ale Raycasting-Klassen gibt es einen Aufzählungstyp, mit dem wir positive
und negative Achsen ansprechen können. Es gibt eine Basis-Klasse *RaycastBase*, die die grundlegende
Funktionalität abbildet.

Die Komponente *SimpleCast* verwendet einen sehr einfachen Raycast in eine der drei Achsen des Weltkoordinantensyxtems. 
Gibt es einen Schnittpunkt
wird, abhängig von der verwendeten Richtung des Strahls, eine Meldung auf der Unity-Konsole ausgegeben.

Die Komponente *Raycast* zeigt bei einem Schnittpunkt mehr Informationen auf der Konsole an 
wie Abstand zum Schnittpunkt und die Koordinaten des Schnittpunkts.
Der Schnittpunkt kann mit Hilfe eines Prefabs *HitPoint* visualisiert werden.

die Komponente *RaycasdtWithLine* visualisiert nicht nur den Schnittpunkt, sondern verwendet eine Instanz
eines *LineRenderers* für den Strahl vom Ursprung des STrahls bis zu einem Schnittpunkt.


Copyright (c) 2023 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
