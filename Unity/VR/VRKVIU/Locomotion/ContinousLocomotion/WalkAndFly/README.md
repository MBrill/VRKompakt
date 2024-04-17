# WalkAndFly

In diesem Projekt bewegen wir uns mit kontinuierlicher Fortbewegung, Walk und Fly,
durch die Hallway-Szene.

Das Projekt enthät die Quellen für die Fortbewegung im Verzeichnis *Locomotion*.
Die Komponenten für die Fortbewegung sind in allen Szenen dem ViveCameraRig zugeordnet.
Es gibt auch eine Kompoente *LocomotionLogger*, mit der wir die Positionen der Bewegung
in einer csv-Datei ablegen können.

## HallwayWalk
Wir verändern bei Walk die x- und die z-Koordinate der Anwender. 

Mit dem Grab-Button auf dem rechten Controller wird die Bewegung getriggert. Die Richtung
der Bewegung wird aus der Orientierung des Controllers abgelesen.

## HallwayFly
Analog zur Szene HallwayWalk, allerdings verändern wir alle drei Koordinaten der Anwender, 
abhängig von der Orientierung des Controllers.

## HallwayDifferenceWalk und HallwayDifferenceFly
In diesen Szenen verwenden wir wieder Walk und Fly. Allerdings wird der Trigger nicht mit einem
Button, sondern über den Abstand der beiden Controller ausgelöst. Übertrifft der Abstand einen
im Inspektor einstellbaren Schwellwert (aktuell 1 Meter), dann wir die Bewegung ausgelöst.
Die Richtung der Bewegung wird aus dem Differenzvektor der beiden Controller abgelesen.

Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
