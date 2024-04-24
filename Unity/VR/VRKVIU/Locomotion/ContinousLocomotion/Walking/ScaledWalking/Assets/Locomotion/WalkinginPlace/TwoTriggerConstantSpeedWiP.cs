//========= 2021 - 2023 Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

 /// <summary>
 /// Arm-Swinging mit beiden Armen, um eine Locomotion auszulösen.
 /// </summary>
 /// <remarks>
 /// Wir beobachten die y-Koordinaten beider Arme  und entscheiden damit,
 /// ob wir uns fortbewegen möchten. Keine weiteren Strategien.
 /// </remarks>
 public class TwoTriggerConstantSpeedWiP : InPlaceLocomotion
 {
        [Header("GameObjects für die Bewegung der Arme")]
        /// <summary>
        /// GameObject, mit dem wir die Bewegung des rechten Arms überwachen
        /// </summary>
        [Tooltip("Rechter Arm")]
        public GameObject TriggerRight;
        /// <summary>
        /// GameObject, mit dem wir die Bewegung des linken Arms überwachen
        /// </summary>
        [Tooltip("Linker Arm")]
        public GameObject TriggerLeft;
     

        /// <summary>
        /// Walk wird so lange durchgeführt wie das Trigger-Objekt  bewegt wird.
        /// Das entscheiden wir auf Grund der Geschwindigkeit dieser
        /// Veränderung, die wir
        /// mit Hilfe von numerischem Differenzieren schätzen.
        /// </summary>
        protected override void Trigger()
        {
            // Numerisches Differenzieren
            var positionRight = TriggerRight.transform.position.y;
            var positionLeft = TriggerLeft.transform.position.y;
            var signalVelocityRight = (positionRight - lastValueRight) / Time.deltaTime;
            var signalVelocityLeft = (positionLeft - lastValueLeft) / Time.deltaTime;
            Moving = (Mathf.Abs(signalVelocityRight) > Threshold) || (Mathf.Abs(signalVelocityLeft) > Threshold) ;

            lastValueRight = positionRight;
            lastValueLeft = positionLeft;
        }

        /// <summary>
        /// Speicher für den letzten Wert
        /// </summary>
        private float lastValueRight = 1.6f,
                          lastValueLeft = 1.6f;
    }