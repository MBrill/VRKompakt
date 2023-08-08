# Virtual Reality

Unterhalb dieses Verzeichnisses finden wir die Projekte, die als VR-Anwendung
ausgeführt werden können. Die Projekte unterscheiden sich durch die für VR eingesetzten 
Plugins und Packages. Am Verzeichnisnamen erkennen wir bereits die eingesetzte Technik.

Aktuell finden wir die folgenden Varianten:
- Unity XR und XRI
- Vive Input Utility für OpenVR, Oculus, OpenXR, Wave und andere Provider
- Desktop-Simulator für die Vive Input Utility für die Arbeit mit dem Simulator, insbesondere falls kein HMD verfügbar ist.

## Submodules
Die einzelnen Varianten liegen in eigenen Repositories und wurden als Submodules in das Repository
VRKompakt hinzugefügt. Bei einem Clone von VRKompakt sind deshalb die Verzeichnisse für die Varianten
erst einmal leer. 

Um die Repositories ebenfalls zu füllen führen wir zwei Kommandos auf der git-Konsole aus:
```
git submodule init

git submodule update
```
Schneller geht es, wenn wir beim Kommando ```git clone``` direkt die Option
```--recurse-submodules``` verwenden.

Aktuelle Versionen der Repositories erhalten wir, wenn wir in den entsprechenden Verzeichnissen sind,
wie gewohnt mit git pull. Mit git push aktualisieren wir die Repos, ebenfalls wie gewohnt. Wichtig ist daran zu denken, dass
wir bei einer Veränderung der Submodules auch in VRKompakt ein Commit durchführen. 
Veränderungen in den Submodules werden in ```git status```angezeigt und können in ein Commit aufgenommen werden.

Copyright (c) 2022 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
