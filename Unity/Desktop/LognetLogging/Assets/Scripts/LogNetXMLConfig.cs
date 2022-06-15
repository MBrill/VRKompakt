using System.IO;
using UnityEngine;
using log4net.Config;
public class LogNetXMLConfig : MonoBehaviour
{
    /// <summary>
    /// Laden der XML-Konfiguration für Log4Net.
    /// </summary>
    /// <remarks>
    /// Wir lesen die Konfiguration, die in Assets/Resources liegt, in dem
    /// wir diese Klasse an ein Dummy-GameObject hängen.
    ///
    /// Mit ConfigureAndWatch stellen wir sicher, dass wir die Konfiguration
    /// sogar während der Laufzeit verändern können.
    /// </remarks>
   private  void Awaket()
    {
        XmlConfigurator.ConfigureAndWatch(
                new FileInfo($"{Application.dataPath}/Resources/Log4NetConfig.xml")
                );
    }
}
