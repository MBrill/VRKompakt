# BasisCollideAndCast

Dieses Unity-Projekt enthält zusätzlich zu den Assets aus dem Projekt BasisComponents
Szenen und C#-Klassen zum Thema Trigger, Collider und Raycast.

Die Assets für die Collisions finden wir im Verzeichnis Assets/Collision.
Analog finden wir die Assets für das Raycasting im Verzeichnis Assets/Raycasting.

Als Vorbereitung auf den Einsatz in VR-Anwendungen verwenden wir in einigen Szenen das Modell eines *generic controller*
aus dem XRI-Package. Das Original findet man in den UXR-Projekten im Verzeichnis *ExampleAssets* .
Kopiert wurden die Materialien, die fbx-Dateien und die Prefabs.

## Szene BasisTrigger
Lösung der Aufgabe 2.3 (a).

Diese Szene zeigt wie man mit einem statischen Collider 
Trigger-Events erzeugen und verarbeiten kann.

Das Objekt *KugelLinksVorneKlein4* ist ein*Trigger*, denn
das Objekt besitzt einen Collider und die Eigenschaft *isTrigger* ist aktiviert.
Zusätzlich hat dieses Objekt eine Komponente vom Typ *RigidBody*.
Wir können das Objekt mit Hilfe von Player2D bewegen. 

Hauptgegenstand der Demo ist die Komponente *ColliderManager*.
Diese C#-Klasse setzt voraus, dass es als Komponenten zu einem statischen Collider
hinzugefügt wurde. Das wird mit entsprechenden [RequireComponent]-Attributen sichergestellt.
In der Klasse werden die Funktionen

- OnTriggerEnger,
- OnTriggerExit, und
- OnTriggerStay

implementiert. Es gibt keine weitere Funktionalität, die drei Events werden
auf der Konsole mit Debug.Log angezeigt.

### Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "WASD" steuern wir die kleine Kugel *KugelLinksVorneKlein4*
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung

## Szene BasisCollision
Lösung der Aufgabe 2.3 (b).

Diese Szene ist sehr ähnlich aufgebaut wie die Szene *BasisTrigger*.
Die Komponente *TriggerManager* am Objekt *KugelLinksVorneKlein4* wurde durch
die Klasse *CollisionManager* ersetzt.

In dieser Klasse werden die Funktionen

- OnCollisionEnger,
- OnCollisionExit, und
- OnCollisionStay

implementiert.  Es gibt keine weitere Funktionalität, die drei Events werden
auf der Konsole mit Debug.Log angezeigt.

### Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "WASD" steuern wir die kleine Kugel *KugelLinksVorneKlein4*
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung

## Szene BasisTouch
Lösung der Aufgabe 2.3 (c).

Diese Szene ist ähnlich aufgebaut wie die Szene *BasisTrigger*.
Die Komponenten *TriggerManager* wird durch die Komponenten *TouchHighlighter*
ersetzt. 
Statt einer Ausgabe auf der Konsole verändern berührte Objekte
ihre Farbe.

### Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "WASD" und den Cursortasten steuern wir wieder das Objekt *KugelLinksVorneKlein4*
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung

## Szene BasisCast
Lösung der Aufgabe 2.4.

In der Szene gibt es beim Objekt *XRControllerRight* drei verschiedene Komponenten für
das Raycasting als Lösung für die Teilaufgaben (a) und (b).

- die Lösung für (a) erhalten wir, wenn wir die Komponente *SimpleCast* aktivieren.
- die Lösung für (b) erhalten wir mit der Komponente *RaycastWithLineController*.

Für ale Raycasting-Klassen gibt es einen Aufzählungstyp, mit dem wir positive
und negative Achsen ansprechen können. Es gibt eine Basis-Klasse *RaycastBase*, die die grundlegende
Funktionalität abbildet.

- Die Komponente *SimpleCast* verwendet einen sehr einfachen Raycast in Richtung eine der drei Achsen des Weltkoordinantensystems. 
Gibt es einen Schnittpunkt
wird, abhängig von der verwendeten Richtung des Strahls, eine Meldung auf der Unity-Konsole ausgegeben.
- Die Komponente *Raycast* zeigt bei einem Schnittpunkt mehr Informationen auf der Konsole an 
wie Abstand zum Schnittpunkt und die Koordinaten des Schnittpunkts.
Der Schnittpunkt kann mit Hilfe eines Prefabs *HitPoint* visualisiert werden.
- Die Komponente *RaycastWithLine* visualisiert nicht nur den Schnittpunkt, sondern verwendet eine Instanz
eines *LineRenderers* für den Strahl vom Ursprung des STrahls bis zu einem Schnittpunkt.

Damit wir die Eingaben interaktiv machen können gibt es für alle drei Komponenten Controller-Klassen.

### Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "WASD" und den Cursortasten steuern wir das Modell des Controllers
- aktuell sind die Bindings so definiert, dass wir theoretisch alle drei Raycaster parallel verwenden können.
Die linke Maustaste löst *SimpleCast*, die mittlere *Raycast* und die rechte Maustaste *RaycastWithLineController* aus.
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung

## Szene SimpleCast
In der Szene gibt es beim Objekt *XRControllerRight* mit der Komponente *SimpleCastController*.
Gibt es einen Schnittpunkt
wird, abhängig von der verwendeten Richtung des Strahls, eine Meldung auf der Unity-Konsole ausgegeben.

Sollten beim Auslösen des Raycasts keine Protokoll-Ausgaben erscheinen überprüfen wir die eingestellte Länge
des Strahls.

### Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "WASD" und den Cursortasten steuern wir das Modell des Controllers
- mit der linken Maustaste lösen wir den Raycast aus
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung

## Szene ControllerCast
In der Szene gibt es beim Objekt *XRControllerRight* mit der Komponente *RaycastController*.
Gibt es einen Schnittpunkt
wird, abhängig von der verwendeten Richtung des Strahls, eine Meldung auf der Unity-Konsole ausgegeben.
Wir erhalten detaillierte Ausgaben und der Schnittpunkt kann visualisiert werden.

Sollten beim Auslösen des Raycasts keine Protokoll-Ausgaben erscheinen überprüfen wir die eingestellte Länge
des Strahls.

### Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "WASD" und den Cursortasten steuern wir das Modell des Controllers
- mit der mittleren Maustaste lösen wir den Raycast aus
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung

## Szene CastWithLine
In der Szene gibt es beim Objekt *XRControllerRight* mit der Komponente *RayCastController*.
Gibt es einen Schnittpunkt
wird, abhängig von der verwendeten Richtung des Strahls, eine Meldung auf der Unity-Konsole ausgegeben.
Wir erhalten detaillierte Ausgaben und der Schnittpunkt kann visualisiert werden.

Sollten beim Auslösen des Raycasts keine Protokoll-Ausgaben erscheinen überprüfen wir die eingestellte Länge
des Strahls.

### Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "WASD" und den Cursortasten steuern wir das Modell des Controllers
- mit der rechten Maustaste lösen wir den Raycast aus
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
