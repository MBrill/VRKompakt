using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Beispiel für die Reaktion auf den Event
/// TrackedImagesChanged.
/// </summary>
/// <remarks>
/// Protokollausgaben bei einem neuen Bild, das in einer
/// ARFoundation-Anwendung gefunden wird.
/// </remarks>
[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageDetection : MonoBehaviour
{
    /// <summary>
    /// Dateiname für die Logs
    /// </summary>
    [Tooltip("Name der Log-Datei")]
    public string fileName = "MyImageTracker.txt";
        
    /// <summary>
    /// ///Instanz des ARTrackedImageManager
    /// </summary>
    private ARTrackedImageManager m_ImageManager;

    /// <summary>
    /// Eigener LogHandler
    /// </summary>
    private CustomLogHandler m_LogHandler;

    /// <summary>
    /// Instanz des Default-Loggers in Unity
    /// </summary>
    private static readonly ILogger s_Logger = Debug.unityLogger;
    
    /// <summary>
    /// Verbindung zur Komponente vom Typ ARTrackedImageManger herstellen.
    /// </summary>
    private void Awake()
    {
        m_ImageManager = FindObjectOfType<ARTrackedImageManager>();
        m_LogHandler = new CustomLogHandler(fileName);
    }

    /// <summary>
    /// Registrieren des Callbacks für das Event
    /// </summary>
    private void OnEnable()
    {
        m_ImageManager.trackedImagesChanged += OnImageChanged;
    }
    
    /// <summary>
    /// Löschen der Registrierung  des Callbacks für das Event
    /// </summary>
    private void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= OnImageChanged;
        m_LogHandler.CloseTheLog();
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs img)
    {
        var counter = m_ImageManager.trackables.count;
        int oldCounter = 0;

        foreach (var trackedImage in img.added)
        {
            object[] added = {
                "Bild", 
                trackedImage.name, 
                "registriert!"
                };
                s_Logger.LogFormat(LogType.Log, gameObject,
                "{0:c} {1:c}; {2:c}", added);
        }

        foreach (var trackedImage in img.removed)
        {
            object[] removed = {
                "Bild", 
                trackedImage.name, 
                "deregistriert!"
            };
            s_Logger.LogFormat(LogType.Log, gameObject,
                "{0:c} {1:c} {2:c}", removed);
        }

        if (counter != oldCounter)
        {
            object[] count = {
                "Anzahl der verfolgten Bilder", 
                m_ImageManager.trackables.count
            };
            s_Logger.LogFormat(LogType.Log, gameObject,
                "{0:c}: {1:G}", count);

            foreach (var trackedImage in m_ImageManager.trackables)
            {
                object[] names = {
                    "Bild", 
                    trackedImage.referenceImage.name
                };
                s_Logger.LogFormat(LogType.Log, gameObject,
                    "{0:c}: {1:c}", names);
            }
            oldCounter = counter;
        }

    }
}
