# MoreInteractions

Lösung der Aufgabe 2.2. mit einem Input Action Asset. 
Eine Komponente **Player Input** repräsentiert die Eingaben eines
Nutzers. Deshalb fassen wir die Objekte, die Eingaben verarbeiten
in einer Hierarchie mit dem Namen *GamePlay* zusammen.
Wie schon im Buch steuern wir die Kapsel mit der Action **Move**.
Das Flugzeugmodell ist der Verfolger. Hier verwenden wir die Action
**Following** und starten bzw. stoppen die Verfolgung.
Mit ESC können wir mit der Action **Quit** wieder die ganze Anwendung
beenden

Die Komponente **Player Input** finden wir in *GamePlay*, dort stellen
wir als Behaviour  **Broadcast Messages** ein. So stellen wir sicher,
dass die Eingaben an die entsprechenden Objekte geschickt und dort verarbeitet werden.


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
