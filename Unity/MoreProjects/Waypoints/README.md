# Waypoints

Bewegung eines Objekts entlang von Wegpunkten, die mit der C#-Klasse WaypointManager
verwaltet werden. Die Wegpunkte können im Inspektor gesetzt werden oder mit Parameterkurven
berechnet werden. Damit können wir Pfadanimation durchführen.

Das Projekt enthält verschiedene Szenen:
- Waypoints zeigt die Verwendung von im Inspektor definierten Wegpunkten.
- Line visualisiert das Durchlaufen einer durch zwei Punkte gegebenen Linie
- Ellipse visualisiert das Durchlaufen einer Ellipse
- Circle SlowInSlowOut visualisiert das Durchlaufen eines Kreises; dabei verwenden
wir Hermite-Polynome für in Slow-In-Slow-Out Verhalten.

## Eingaben für die Anwendung
In der Desktop-Anwendung können wir die folgenden Eingaben machen:

- mit "R" steuern wir das Durchlaufen der wegpunkte
- mit "V" steuern wir bei den Parameterkurven, ob die Kurve zusätzlich
mit Hilfe eines Polygonzugs visualisiert wird
- mit "ESC" stoppen wir die Ausführung im Editor und in der Anwendung


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
