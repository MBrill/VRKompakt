# Verzeichnis VRKVIUSimulator

Zu Beginn der Entwicklung der Beispiele für das Buch wurden die Projekte
für den Simulator und für die VR-Hardware strikt getrennt. Dies bedeutete 
zwar auch die ständige Koordinierung der Quellen, aber teilweise war es schwierig,
die verschiedenen Backends zu handeln.

In der aktuellen Version von Unity und der VIU scheint dieses Problem nicht mehr aufzutreten.
Deshalb finden Sie alle Lösungen der Aufgaben und weitere Beispiele im Verzeichnis VRKVIU.
Dort sind in den VIU Settings immer der Simulator und OpenXR aktiviert. 
Ist keine entsprechende Hardware zu finden startet VIU den Simulator.
Sollte dies bei Ihnen zu Problemen führen deaktivieren Sie einfach das OpenXR-Plugin.

## Verwendete VIU-Version
Die aktuell eingesetzten Versionen sind

- Unity 2022.3 LTS
- Vive Input Utility 1.19.0

## Versionierung
Die Verzeichnisse und Dateien im Assets-Verzeichnis der Unity-Projekte, die zu VIU
gehören werden mit Hilfe von .gitignore ignoriert. Beim Transfer auf einen neuen Rechner
oder einem Clone wird das VIU-Package mit Hilfe von Assets -> Import Package -> Custom Package
dem Projekt hinzugefügt. Anschließend sind alle Externals aufgelöst.


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
