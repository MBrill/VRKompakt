using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Datenstruktur für ein Paar aus Bild und Prefab.
/// Intern verarbeiten wir das als Dictionary. Wir benötigen
/// das struct, da der Unity Editor Dictionaries nicht darstellt.
/// </summary>
public struct Pair
{
    public string Image;
    public GameObject Prefab;

    public Pair(string name, GameObject prefab)
    {
        Image = name;
        Prefab = prefab;
    }
}

    /// <summary>
    /// Verwalten der Bilder in einer Instanz von Image
    /// Reference Library und Speichern der Prefabs,
    /// die für jedes Bild dargestellt werden sollen.
    /// </summary>
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ImagePrefabPairs : MonoBehaviour
    {
        public string[] Images;
        public GameObject[] Prefabs;
        private Dictionary<string, GameObject> Objects;
        
        /// <summary>
        /// ARTrackedImageManager Komponente des Objekts.
        /// </summary>
        private ARTrackedImageManager m_TrackedImageManager;
        
        /// <summary>
        ///  Die Bibliothek, die im Image Manager eingestellt ist.
        /// </summary>
        private XRReferenceImageLibrary m_ImageLibrary;

        private Dictionary<string, GameObject> m_Dictionary;
        
        private void Awake()
        {
            m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
            m_ImageLibrary = m_TrackedImageManager.referenceLibrary
                as XRReferenceImageLibrary;

            if (Images.Length == 0) 
                 return;
            
            for (var i=0; i<Images.Length; i++)
            {
                m_Dictionary.Add(Images[i], Prefabs[i]);
            }
        }

        /// <summary>
        /// Callback registrieren
        /// </summary>
        private void OnEnable()
        {
            m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        /// <summary>
        /// Callback deregistrieren
        /// </summary>
        private void OnDisable()
        {
            m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs img)
        {
            foreach (var trackedImage in img.added)
           {
               var imageName = trackedImage.referenceImage.name;
               if (!m_Dictionary.ContainsKey(imageName)) continue;
               var newPrefab = Instantiate(m_Dictionary[imageName], 
                   trackedImage.transform);
               Objects[imageName] = newPrefab;
           }
  
            foreach (var trackedImage in img.updated)
            {
                var imageName = trackedImage.referenceImage.name;
                Objects[imageName].SetActive(
                    trackedImage.trackingState == TrackingState.Tracking);
            }
            
            
            foreach (var trackedImage in img.removed)
            {
                var imageName = trackedImage.referenceImage.name;
                
                Destroy(Objects[imageName]);
                Objects.Remove(imageName);
            }
        }
        



        public void SetPrefabForReferenceImage(XRReferenceImage referenceImage, GameObject alternativePrefab)
        {
            /*m_Prefabs[referenceImage.name] = alternativePrefab;
            if (m_Instantiated.TryGetValue(referenceImage.name, out var instantiatedPrefab))
            {
                m_Instantiated[referenceImage.name] = Instantiate(alternativePrefab, instantiatedPrefab.transform.parent);
                Destroy(instantiatedPrefab);
            }*/
        }

    }
