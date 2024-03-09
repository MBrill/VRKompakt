using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Implementierung eines eigenen LogHandlers
/// </summary>
public class CustomLogHandler :  ILogHandler
{
    /// <summary>
    /// Default-Konstruktor.
    /// <remarks>
    /// Wir schreiben die Datei loggingExample.csv
    /// in den dataPath der Anwendung.
    ///
    /// Im Editor ist dies das Verzeichnis Assets.
    /// M�glich w�re auch persistentDataPath,
    /// dann liegt das in Windows in AppData,
    /// wie die Playerlogs.
    /// </remarks>
    /// </summary>
    public CustomLogHandler()
    {
        var filePath = Application.dataPath + "/loggingExample.csv";
        m_FileStream = new FileStream(filePath, 
            FileMode.OpenOrCreate, 
            FileAccess.ReadWrite);
        m_StreamWriter = new StreamWriter(m_FileStream);

        // Den Default Handler durch diese Klasse ersetzen
        Debug.unityLogger.logHandler = this;
    }

    /// <summary>
    /// Konstruktor mit Dateiname.
    /// <param name="fileName">Dateiname</param>
    /// </summary>
    /// <remarks>
    /// Wir schreiben die Datei mit dem Namen FileName
    /// in den dataPath der Anwendung.
    ///
    /// Im Editor ist dies das Verzeichnis Assets.
    /// M�glich w�re auch persistentDataPath,
    /// dann liegt das in Windows in AppData,
    /// wie die Playerlogs.
    /// </remarks>
    public CustomLogHandler(string fileName)
    {
        var filePath = Application.dataPath + "/" + fileName;
        m_FileStream = new FileStream(filePath, 
            FileMode.Create, 
            FileAccess.ReadWrite);
        m_StreamWriter = new StreamWriter(m_FileStream);

        // Den Default Handler durch diese Klasse ersetzen
        Debug.unityLogger.logHandler = this;
    }

    /// <summary>
    /// Schlie�en der Protokolldatei
    /// </summary>
    public void CloseTheLog()
    {
        m_StreamWriter.Close();
    }
    
    /// <summary>
    /// Wir �berschreiben LogFormat aus dem Interface.
    /// </summary>
    /// <remarks>
    /// Im Format-String verwenden wir String-Interpolation.
    /// </remarks>
    /// <param name="logType">Logging-Stufe</param>
    /// <param name="context">GameObject oder anderes Unity Object</param>
    /// <param name="format">Format-String</param>
    /// <param name="args">Werte, die ausgegeben werden sollen</param>
    public void LogFormat(LogType logType, 
        UnityEngine.Object context, 
        String format, 
        params object[] args)
    {
        m_StreamWriter.WriteLine(format, args);
        m_StreamWriter.Flush();
        m_DefaultLogHandler.LogFormat(logType, context, format, args);
    }

    /// <summary>
    /// �berschreiben der Funktion LogException im Interface
    /// </summary>
    /// <param name="exception">Welche Exception ist aufgetreten</param>
    /// <param name="context">GameObject oder anderes Unity Object</param>
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        m_DefaultLogHandler.LogException(exception, context);
    }
    
    /// <summary>
    /// FileStream-Instanz f�r die Ausgabe in eine Datei
    /// </summary>
    private FileStream m_FileStream;
    /// <summary>
    /// Instanz f�r das Schreiben in eine Datei.
    /// </summary>
    private readonly  StreamWriter m_StreamWriter;
    /// <summary>
    /// Sicherstellen, dass der Handler verwendet wird.
    /// </summary>
    private readonly ILogHandler m_DefaultLogHandler 
        = Debug.unityLogger.logHandler;
}
