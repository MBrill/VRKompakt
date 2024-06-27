# XRayCasts

Lösung der Aufgabe 3.4.

Für die Raycasts setzen wir die Komponenten ein, die wir bereits für den Desktop
implementiert haben. Wir finden diese Komponenten im Projekt VIUCastAndSelect, das 
im gleichen Verzeichnis wie dieses Projekt zu finden ist.

Auswählbar in der Beispielszene sind Objekte mit dem Tag *Selectable*. Alle anderen
sollen transparent dargestellt werden.
Die Funktionalität ist in der Klasse *XRayCast* implementiert.

Wir verwenden die Funktion *RaycastAll*, die ein ungeordnetes Array aller Treffer
zurückliefert. Um besser arbeiten zu können wandeln wir dieses Array in eine 
List<RaycastHit> um. Diese sortieren wir nach dem Abstandswert der Treffer und suchen
anschließend nach dem ersten auswählbaren Objekt in dieser Liste. Alle weiteren
Listenelemente werden gelöscht, die Visualisierung des Treffers wird an diesem letzten
Element dargestellt und alle Elemente davor in der Liste werden transparent dargestellt.

## Eingaben für die Anwendung
In der VR-Anwendung können wir die folgenden Eingaben machen:

- Mit den Trigger-Buttons des Controllers können wir den Raycast auslösen.


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
