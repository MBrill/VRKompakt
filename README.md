# Virtual Reality Kompakt

Repository mit Beispielen, Lösungen und vielen weiteren Assets zum Buch
*Virtual Reality Kompekt*, Springer Nature.

## Aufbau des repository
Für das repository gibt es mehrere branches. Diese branches enthalten die Varianten
für die verschiedenen VR-Plugins und -Packages. Um zu verhindern, dass Unity beim Öffnen
der Projekte mit nicht-versionierten Assets Probleme bekommt enthält jeder branches
ein entsprechendes Projekt-Verzeichnis.

Alle Unity-Projekte sind unterhalb des Verzeichnisses *Unity* zu finden.

## Basis-Szene
In den branches main und develop befindet sich ein Unity-Projekt mit einer Basis-Szene. 
Änderungen an dieser Szene werden ausschließlich in diesen branches durchgeführt und mit Hilfe
merge und anderen Aktionen in die weiteren branches integriert.
Parallel zu diesem Unity-Projekt finden wir ein Verzeichnis mit Beispielen zu den Themen
Logging und Testen in Unity. Auch diese Projekte verwenden weitgehend die Basis-Szene, 
wenn nichts anderes gesagt. Diese Projekte sind Bestandteil der Variante *Desktop*.

Die Basis-Szene in main und develop enthält eine statische Kamera. Um interaktiv in einer
Anwendung oder im Inspektor durch die Basis-Szene zu navigieren und Aktionen auszuführen
gibt es die Variante *DesktopSimulator*, in der das Package Vive Input Simulator
enthalten ist. Die Preferences für die VIU sind so eingestellt, dass der Simulator
eingesetzt wird. Damit können wir die im VR-Rig von VIU enthaltene Kamera mit WASD, Cursor
und Maus steuern, genauso wie die Controller oder Tracker.

## Varianten
Aktuell sind die folgenden Varianten der Basis-Szene vorgesehen:

| Variante         | branch           |
| ---------------- | -----------------|
| Desktop          | main und develop |
| DesktopSimulator | DesktopSimulator    |
| VIU              | VIU              |
| MiddleVR         | MiddleVR 2.0     |
| UnityXR          | Unity XR/XRI     |
| Steam            | Steam            |

Diese Tabelle stellt eine Tofu-Skala dar. Die Variante Desktop ist die oberste
Variante. Änderungen dort werden auf die weiter unter liegenden Varianten
übertragen.

Zu den Varianten gibt es jeweils bei Bedarf hotfix- und develop-branches, die
möglichst früh mit den Varianten mit Hilfe von merge synchronisiert werden.

## Konventionen

### Dokumentation
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
Im Verzeichnis R finden wir einige kleine R-Dokumente, die zeigen wie
wir das Ergebnis von Logging-Ausgaben für die weitere Evaluation einsetzen können.

Copyright (c) 2022 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
