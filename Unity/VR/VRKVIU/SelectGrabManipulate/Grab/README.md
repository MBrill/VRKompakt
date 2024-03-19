# Grab

Grabbing für ein Objekt in der Szene auf der Basis von Vive Input Utility.

Bevor wir entscheiden, welche Objekte wir greifen und damit in der Position und Orientierung
verändern können fügen wir dem VR-Rig das Prefab *ViveCollider* hinzu. Damit werden 
für die beiden Controller Collider mitbewegt.

Greifbar in der Basis-Szene sind die beiden Objekte *Kapsel* und einer der kleinen Kugeln links,
*KugelVorneKlein4*.

Ein Objekt kann mit der VIU *gegriffen* werden, es ist *grabbable*, wenn wir im Inspector diesem GameObject
eine der Klassen ```BasicGrabbable``` oder ```StickyGrababble```
aus dem Verzeichnis
```
Assets/HTC.UnityPlugin/ViveInputUtility/Scripts
```
oder die Klasse ```ViveColliderEventCaster``` im Verzeichnis
```
Assets/HTC.UnityPlugin/ViveInputUtility/Scripts/ViveColliderEvent
```
hinzufügen. 
Wir erhalten diese Klassen auch mit Hilfe des Buttons *Add Component*
in Inspector des Objekts.
Die Klassen finden wir in der Kategorie *HTC/VIU/ObjectGrabber*.
Prinzipiell muss ein Objekt, das wir mit einer dieser Klassen bewegen möchten eine *RigidBody*-Komponente
und einen Collider enthalten, da VIU die Berührung über Kollissions-Events realisiert.

Ein Objekt, dem wir die Klasse ```StickyGrababble``` hinzufügen können wir auch mit Hilfe des Controllers bewegen, wenn wir
den Button loslassen. Die Auswahl wird aufgehoben, wenn wir erneut den Button betätigen.
Das ist in der Praxis häufig gut anwendbar, da wir die Benutzer nicht dazu zwingen, den
Controller zu bewegen und gleichzeitig einen Button gedrückt zu halten.
Wie sich das Objekt nach dem Loslassen verhält entscheiden wir über die Einträge
in der *Rigidbody*-Komponente des Objekts. 

Objekte mit der Komponente ```BasicGrabbable```
bewegen wir so lange der Button gedrückt ist. Das restliche Verhalten ist analog zu vorher.

Wir können in den beiden Klassen entscheiden, ob wir bei der Bewegung der Objekte die Physik verwenden
und damit andere Objekte mitbewegen. Das entscheiden wir mit der Option ```Unblockable Grab``` im Inspektor.

Sehr nützlich ist die Klasse ```MaterialChanger```, die wir im gleichen Verzeichnis finden wir die beiden
Grab-Klassen. Hier können wir neben der normalen Farbe eines greifbaren Objekts drei weitere Farben angeben,
je nachdem ob wir ein *Hover* darüber machen (damit können wir gut erkennen, dass wir ein Objekt in der Szene
greifen können) und dass wir es aktull gegriffen haben.

Copyright (c) 2022 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
