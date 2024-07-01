# PointTugging

In diesem Projekt implementieren wir Point Tugging als Lösung der Aufgabe 3.6.


Die Komponenten für die Fortbewegung sind in den Szenen dem ViveCameraRig zugeordnet.
Es gibt auch eine Kompoente *LocomotionLogger*, mit der wir die Positionen der Bewegung
in einer csv-Datei ablegen können.

## Ausschnitt aus der Originalveräffentlichung von Williams et. al.
*When the trigger of the controller is pressed, the x and z positions
of the controller are saved as the “grabbed” point (дrabx and дrabz ).
While the trigger is held, the position of the controller is checked at
each frame. At each frame, the user translates in the yaw-direction
by the calculated difference between the x, z positions of the current
controller (conx , conz ) and the x, z positions of the grabbed point.
In this manner, the user moves along the xz plane and eye height (yposition)
is preserved. When the trigger is released, the controller
is no longer locked to the grab point. Thus, while locomoting using
Point–Tugging, the user’s updated position (NewPos) is calculated
from the current position of the user (CurPos) as follows: NewPos =
(дrabx −conx ,0,дrabz −conz ) +CurrPos. With this method, users
are able to freely look around in the environment as they locomote
in any direction that they tug. To move through the VE, users have
to click to grab a point in space, pull themselves, release, and then
repeat.*


Copyright (c) 2024 Manfred Brill

**License**: [Creative Commons Attribution 4.0 International (CC BY-NC-SA 4.0)](https://creativecommons.org/licenses/by-nc-sa/4.0/).  

![Lizenzlogo](https://licensebuttons.net/l/by-nc-sa/3.0/de/88x31.png)
