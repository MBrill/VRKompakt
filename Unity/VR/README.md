# Virtual Reality

Unterhalb dieses Verzeichnisses finden wir die Projekte, die als VR-Anwendung
ausgeführt werden können. Aktuell finden Sie hier Versionen für den Simulatur
von Vive Input Utility. 

## Submodules
Die Lösung für VIU ist aktuell als Submodule in das Repository
VRKompakt hinzugefügt. Bei einem Clone von VRKompakt sind deshalb die Verzeichnisse für die Varianten
erst einmal leer. 

Um die Repositories ebenfalls zu füllen führen wir zwei Kommandos auf der git-Konsole aus:
```
git submodule init

git submodule update
```
Schneller geht es, wenn wir beim Kommando ```git clone``` direkt die Option
```--recurse-submodules``` verwenden.

Anfang des Jahres 2024 wird sich diese Architektur ändern und die Quellen für die VR-Projekte werden
direkt in diesem Repository verfügbar sein!


Copyright (c) 2023 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
