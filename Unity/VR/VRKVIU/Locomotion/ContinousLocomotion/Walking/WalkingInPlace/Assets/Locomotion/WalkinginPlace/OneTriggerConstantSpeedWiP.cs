//========= 2021 - 2024 -  Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Walking-in-Place mit einem Trigger-Objekt
/// </summary>
/// <remarks>
/// Wir beobachten die y-Koordinaten eines Objekts
/// und entscheiden damit,
/// ob wir uns fortbewegen möchten. Keine weiteren Strategien,
/// keine Veränderung der Geschwindigkeit.
///
/// Implementiert LLVM WiP.
/// </remarks>
public class OneTriggerConstantSpeedWiP : InPlaceLocomotion
{
        /// <summary>
        /// Trigger-Objekt
        /// </summary>
        [Tooltip("Welches Objekt wird für die Fortbewegung bewegt?")]
        public GameObject TriggerObject;

        /// <summary>
        /// Walk wird so lange durchgeführt wie das Trigger-Objekt  bewegt wird.
        /// Das entscheiden wir auf Grund der Geschwindigkeit dieser
        /// Veränderung, die wir
        /// mit Hilfe von numerischem Differenzieren schätzen.
        /// </summary>
        protected override void Trigger()
        {

            var position = 0.0f;
            var signalVelocity = 0.0f;

            // Numerisches Differenzieren
            position = TriggerObject.transform.position.y;
            signalVelocity = Mathf.Abs((position - m_LastValue) / Time.deltaTime);
            Moving = signalVelocity > Threshold;

            if (Moving)
            {
                object[] args = {Time.time,
                    position,
                    signalVelocity            
                };
                s_Logger.LogFormat(LogType.Log, gameObject,
                    "{0:G};{1:G};{2:G}", args);
            }
            m_LastValue = position;
        }

        /// <summary>
        /// Speicher für den letzten Wert
        /// </summary>
        private float m_LastValue = 1.6f;
}
