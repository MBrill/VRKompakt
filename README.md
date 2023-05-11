# Virtual Reality Kompakt

Repository mit Beispielen, Lösungen und vielen weiteren Assets zum Buch
*Virtual Reality Kompakt - Implementierung von immersiver Software*, Springer Vieweg, 2023.

## Aufbau des Repository
Im Verzeichnis Unity finden Sie wie der Name schon sagt die Unity-Projekte zum Text und den Übungsaufgaben.
Im Verzeichnis *Desktop* finden Sie die Basis-Szenen und Variationen dazu aus dem Kapitel *Anwendungsentwicklung
mit Unity*.

Parallel zum Verzeichnis *Desktop* finden Sie Unterverzeichnisse *Loggging* und *Tests*
zu den Themen Protokollieren mit *ILoggier* und *log4net* bzw. Unit-Tests.

Im Verzeichnis *VR* finden Sie VR-Anwendungen. Diese Anwendungen sind nochmals gegliedert nach den Packages,
die für die Realisierung der VR-Funktionalität eingesetzt werden: 
- VRKUXR enthält Lösungen auf der Basis von Unity XR,
- VRKVIU enthält Lösungen auf der Basis von Vive Input Utility,
- VRKVIUSimulator enthält Lösungen auf der Basis des Simulators von Vive Input Utility.


## Dokumentation
Die Szenen und andere Unity-Assets werden in Markdown-Dokumenten
im Projektverzeichnis dokumentiert. 


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

Copyright (c) 2023 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
