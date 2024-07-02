# WalkAndFly

In diesem Projekt implementieren wir eine steering metaphor und realisieren Walk und Fly.

Das Projekt enthält die Quellen für die Fortbewegung im Verzeichnis *Locomotion*.
Die Komponenten für die Fortbewegung sind in den Szenen dem ViveCameraRig zugeordnet.
Es gibt auch eine Kompoente *LocomotionLogger*, mit der wir die Positionen der Bewegung
in einer csv-Datei ablegen können.

## WalkGang
Wir verändern bei Walk die x- und die z-Koordinate der Anwender. 

Es gibt zwei Versionen für die Controller-Klasse:
- WalkVIU führt die Bewegung durch so lange der Trigger-Button gedrückt gehalten wird.
- WalkVIUSteady führt die Bewegung nach dem Betätigen des Trigger-Buttons so lange aus, bis wieder
der Button betätigt wird.

## FlyGang
Analog zur Szene WalkGang, allerdings verändern wir alle drei Koordinaten der Anwender, 
abhängig von der Orientierung des Controllers. Wieder gibt es die beiden Varianten für
das Triggern der Bewegung in FlyVIU und FlyVIUSteady.


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
