# VIUCollideAndSelect

Raycast in einer VR-Anwendung auf der Basis der Raycast-Klassen, die wir für die Desktop-Anwendungen
bereits implementiert haben. Die Funktionalität ist identisch mit den Projekten für den Desktop,
aber es gibt Klassen im Verzeichnis *Assets/Scripts/Interactions*, die die Raycasts mit Hilfe
von HTC Vive Input Utility auslösen. Diese Klassen finden wir als Komponente im ViveRig.

Bei RightHand verwenden wir die Komponente *RaycastWithLineVIU* - wir sehen den Strahl und den Endpunkt.
Bei LeftHand verwenden wir die Komponente *RaycastVIU*, bei der nur der Endpunkt visualisiert wird.

Die Treffer der Raycaste werden, falls in den Komponenten die Option für die Logs aktiviert ist,
mit Debug auf der Konsole ausgegeben.

## Eingaben für die Anwendung
In der VR-Anwendung können wir die folgenden Eingaben machen:

- Mit den Trigger-Buttons der beiden Controller können wir die Raycasts auslösen.

## Bemerkung
Wir benötigen keine VivePointer oder ViveCurvedPointer aus VIU!

Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
