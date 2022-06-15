using System.IO;
using UnityEngine;
using log4net.Config;
public class LogNetXMLConfig : MonoBehaviour
{
    /// <summary>
    /// Laden der XML-Konfiguration f�r Log4Net.
    /// </summary>
    /// <remarks>
    /// Wir lesen die Konfiguration, die in Assets/Resources liegt, in dem
    /// wir diese Klasse an ein Dummy-GameObject h�ngen.
    ///
    /// Mit ConfigureAndWatch stellen wir sicher, dass wir die Konfiguration
    /// sogar w�hrend der Laufzeit ver�ndern k�nnen.
    /// </remarks>
   private  void Awaket()
    {
        XmlConfigurator.ConfigureAndWatch(
                new FileInfo($"{Application.dataPath}/Resources/Log4NetConfig.xml")
                );
    }
}
