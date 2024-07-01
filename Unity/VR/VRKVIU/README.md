# Verzeichnis VRKVIU

Unterhalb dieses Verzeichnisses finden wir die Projekte, die als VR-Anwendung
ausgeführt werden können. In diesen Projekten wird Vive Input Utility 1.19 eingesetzt.
In diesen Projekten ist in den VIU Settings immer der Simulator und OpenXR ausgewählt.
Haben Sie keine VR-Hardware am Rechner angeschlossen sollte ohne Änderung der Einstellungen
der Simulator gestartet werden.

Sie finden Projekte zu den drei großen Teilmengen der XR-Interfaces in den Verzeichnissen

- SystemControl (mit der Implementierung von CCC)
- SelectGrabManipulate
- Locomotion

## Verwendete VIU-Versionen
Die aktuell eingesetzten Versionen sind

- Unity 2022.3 LTS
- Vive Input Utility 1.19.0


Die Verzeichnisse und Dateien im Assets-Verzeichnis der Unity-Projekte, die zu VIU
gehören werden mit Hilfe von .gitignore ignoriert. Beim Transfer auf einen neuen Rechner
oder einem Clone wird das VIU-Package mit Hilfe von Assets -> Import Package -> Custom Package
dem Projekt hinzugefügt. Anschließend sind alle Externals aufgelöst.

# Lösungen

| Verzeichnis         | Beschreibung    |
| -------------       | ---------- | 
| FirstInteractionVIU | Lösung der Aufgabe 3.1                           |
| FollowTheController | Lösung der Aufgabe 3.2                           |
| SystemControl/CommandControlCube  | Lösung der Aufgabe 3.3             |
| Locomotion/ContinousLocomotion/Point Tugging  | Lösung der Aufgabe 3.6 |


# Weitere Anwendungen
Es gibt eine Reihe von weiteren Anwendungen oder Varianten zu den Lösungen aus dem Buch.
Das Verzeichnis *Basis* enthält die Basis-Szene und die Szene *Gang* als VR-Anwendung
ohne weitere Funktionalität. 

World-in-a-Minituare als Komponente einer VR-Anwendung
finden Sie ebenfalls hier, im Projekt *ShowTheWim*.


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
