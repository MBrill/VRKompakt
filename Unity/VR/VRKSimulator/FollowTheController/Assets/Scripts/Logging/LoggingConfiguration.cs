using log4net.Config;
using UnityEngine;

/// <summary> 
/// Klasse zum Laden der xml-Konfigurationsdatei.
/// </summary>
/// <remarks>
/// Quelle: https://www.linkedin.com/pulse/advanced-logging-unity-log4net-charles-amat
/// </remarks>
public static class LoggingConfiguration 
{ 
        [RuntimeInitializeOnLoadMethod(
            RuntimeInitializeLoadType.BeforeSceneLoad)] 
        private static void ConfigureLogging() 
        { 
            log4net.Config.XmlConfigurator.ConfigureAndWatch(
                new System.IO.FileInfo(
                $"{Application.dataPath}/Resources/log4netConfig.xml")); 
        }
}