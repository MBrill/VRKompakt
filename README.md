# Virtual Reality Kompakt

Repository mit Beispielen, Lösungen und vielen weiteren Assets zum Buch
*Virtual Reality Kompakt - Implementierung von immersiver Software*, Springer Nature.

## Aufbau des Repository
Das Repository enthält in den Branches develop und main die Basis-Szene aus dem Buch.
Es werden keine VR-Anwendungen in diesen Unity-Projekten erstellt.

Die Varianten für die verschiedenen Möglichkeiten eine VR-Anwendung mit Unity zu erstellen finden wir
im Verzeichnis Unity/VR. Dort finden wir Verzeichnisse, die als Submodule hinzugefügt wurden.
Wie man mit Submodules in git arbeitet ist im README.md in diesem Verzeichnis besschrieben.

Aktuell finden wir die folgenden Varianten:
- Unity XR und XRI
- Vive Input Utility für Steam, Wave und andere Provider
- Desktop-Simulator für die Vive Input Utility
- MiddleVR


## Basis-Szene
In den branches main und develop befindet sich ein Unity-Projekt mit einer Basis-Szene. 
Änderungen an dieser Szene werden ausschließlich in diesen branches durchgeführt.
Parallel zu diesem Unity-Projekt finden wir ein Verzeichnis mit Beispielen zu den Themen
Logging und Testen in Unity. Auch diese Projekte verwenden weitgehend die Basis-Szene, 
wenn nichts anderes gesagt.

Die Basis-Szene in main und develop enthält eine statische Kamera. Um interaktiv in einer
Anwendung oder im Inspektor durch die Basis-Szene zu navigieren und Aktionen auszuführen
gibt es die Variante *Desktop-Simulator* im VRVerzeichnis, in der das Package Vive Input Simulator
enthalten ist. Die Preferences für die VIU sind so eingestellt, dass der Simulator
eingesetzt wird und werden in diesem Submodule nicht verändert. 
Damit können wir die im VR-Rig von VIU enthaltene Kamera mit WASD, Cursor
und Maus steuern, genauso wie die Controller oder Tracker.

Wir denken in einer Tofu-Scale. Damit ist gemeint, dass die main- und develop-Branches
in VRKompakt oberhalb der Submodules liegen. Veränderungen an der Basis-Szene werden
entsprechend in die Submodule eingepflegt.

## Dokumentation
Die Szenen und andere Unity-Assets werden in Markdown-Dokumenten
im Projektverzeichnis dokumentiert. Diese Dokumente werden versioniert.

### Aufbau der Unity-Projekte
Wir verwenden immer die identische Projektstruktur und die folgenden Verzeichnisse
unterhalb von Assets:
- Scenes
- Scripts
- Prefabs
- Materials
- Shader
- Textures

### C\#
In den C\#-Klassen werden die folgenden, an die .NET Namenskonventionen angelehnte,
Konventionen verwendet:

| Kategorie      | Notation    | Beispiel        |
| ------------- | ---------- | -------------- |
| Klasse         | Pascal Case | NumberGenerator |
| Interface      | Pascal Case | IComparable     |
| Methode        | Pascal Case | AsInteger      |
| Variable       | Camel Case  | selectedColor   |
| Klassenelement |             |                 |
| private        | Camel Case  | foregroundColor |
| protected      | Camel Case  | foregroundColor |
| public         | Pascal Case | ForegroundColor |

Wie von Microsoft vorgeschlagen verwenden wir *Verben* für Methoden und *Substantive*
für Klassenelemente und Variable.

## Verzeichnis R
Im Verzeichnis R finden wir einige kleine R- und Rmd-Dokumente, die zeigen wie
wir das Ergebnis von Logging-Ausgaben für die weitere Evaluation einsetzen können.

Copyright (c) 2022 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
