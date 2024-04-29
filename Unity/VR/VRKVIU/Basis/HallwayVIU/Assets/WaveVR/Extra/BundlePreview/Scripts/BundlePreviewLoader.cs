// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using WVR_Log;

public class BundlePreviewLoader : MonoBehaviour
{
	private static string assetBundlePathRelative = "assetbundles";

	private Dictionary<string, Hash128> oldBundleHash = null;
	private Dictionary<string, Hash128> currentBundleHash = null;
	private List<string> oldAssetBundleNames = null;
	private List<string> currentAssetBundleNames = null;
	private List<SceneData> currentScenes = null;
	private List<AssetBundle> oldLoadedAssetBundles = null;
	private List<AssetBundle> currentLoadedAssetBundles = null;

	private float checkSceneInterval = 1f;
	private string LOG_TAG = "BundlePreviewLoader";

	// Use this for initialization
	void Awake()
	{
		Log.d(LOG_TAG, "Bundle Preview Loader started.");
		StartCoroutine(checkSceneCoroutine());
	}

	private bool LoadSceneHashFromManifest()
	{
		AssetBundle masterBundle = AssetBundle.LoadFromFile(GetAssetBundlePathAbsolute(assetBundlePathRelative));

		if (masterBundle == null)
		{
			Log.d(LOG_TAG, "Failed to Load Asset Bundles");
			return false;
		}
		AssetBundleManifest masterManifest = masterBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

		string[] masterAssetBundles = null;
		if (masterManifest != null)
		{
			masterAssetBundles = masterManifest.GetAllAssetBundles();
		}
		else
		{
			Log.d(LOG_TAG, "Failed to Load Asset Bundle Manifest");
			return false;
		}

		if (!(currentBundleHash == null)) //Save current scene hashes as old scene hash for version control
		{
			oldBundleHash = currentBundleHash;
			oldAssetBundleNames = currentAssetBundleNames;
		}
		currentBundleHash = new Dictionary<string, Hash128>();
		currentAssetBundleNames = new List<string>();

		foreach (string assetBundle in masterAssetBundles)
		{
			currentAssetBundleNames.Add(assetBundle);
			currentBundleHash[assetBundle] = masterManifest.GetAssetBundleHash(assetBundle);
			Log.d(LOG_TAG, "Key: " + assetBundle + " Value: " + currentBundleHash[assetBundle] + " added to current hash dictionary");
		}

		masterBundle.Unload(true);
		return true;
	}

	private bool DetectChangesInAssetBundles()
	{
		if (oldBundleHash == null) //Fresh Load, return all asset bundle names
		{
			return true;
		}
		else
		{
			foreach (string assetBundlePath in currentAssetBundleNames)
			{
				if (currentBundleHash.ContainsKey(assetBundlePath) && oldBundleHash.ContainsKey(assetBundlePath)) //Bundle exists already, check for hash difference
				{
					if (currentBundleHash[assetBundlePath] != oldBundleHash[assetBundlePath]) //hash different, reload
					{
						return true;
					}
				}
				else if (!oldBundleHash.ContainsKey(assetBundlePath)) //New Bundle that does not exist previously
				{
					return true;
				}
			}

			foreach (string assetBundlePath in oldAssetBundleNames)
			{
				if (!currentBundleHash.ContainsKey(assetBundlePath)) //Old Bundle removed
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool LoadAssetBundles()
	{
		if (currentAssetBundleNames.Count <= 0) return false;

		if (currentLoadedAssetBundles != null)
		{
			currentLoadedAssetBundles.Clear();
			currentLoadedAssetBundles = null;
		}

		currentLoadedAssetBundles = new List<AssetBundle>();
		foreach (string assetBundleName in currentAssetBundleNames) //Load individual scene bundles
		{
			Log.d(LOG_TAG, "Loading Asset Bundlle from " + GetAssetBundlePathAbsolute(assetBundleName));
			AssetBundle loadedAssetBundle = AssetBundle.LoadFromFile(GetAssetBundlePathAbsolute(assetBundleName));
			Log.d(LOG_TAG, "Loaded Asset Bundle: " + loadedAssetBundle.name);
			currentLoadedAssetBundles.Add(loadedAssetBundle);
		}
		return true;
	}

	private void UnloadAssetBundles()
	{
		foreach (AssetBundle loadedAssetBundle in oldLoadedAssetBundles)
		{
			Log.d(LOG_TAG, "Unloaded Asset Bundle: " + loadedAssetBundle.name);
			loadedAssetBundle.Unload(true);
		}
		oldLoadedAssetBundles = null;
	}

	private bool GetScenesFromAssetBundles()
	{
		if (currentLoadedAssetBundles.Count <= 0) return false;

		currentScenes = new List<SceneData>();
		foreach (AssetBundle loadedAssetBundle in currentLoadedAssetBundles) //Get all scene paths from loaded asset bundles
		{
			string[] scenePaths = loadedAssetBundle.GetAllScenePaths();
			string currentAssetBundleName = loadedAssetBundle.name;
			if (scenePaths.Length > 0)
			{
				for (int i = 0; i < scenePaths.Length; i++)
				{
					string scenePath = scenePaths[i];
					//Parse scene index from asset bundle name
					int sceneIndex = -1;
					int.TryParse(currentAssetBundleName.Substring(0, 1), out sceneIndex);
					if (sceneIndex == -1)
					{
						Log.d(LOG_TAG, "Parse scene index failed with asset bundle: " + currentAssetBundleName);
						return false;
					}
					//construct scene data when parse success
					SceneData newSceneData = new SceneData(scenePath, Path.GetFileNameWithoutExtension(scenePath), sceneIndex);
					Log.d(LOG_TAG, "Scene data retrieved from Asset Bundle " + currentAssetBundleName + ":"
						+ "\nPath: " + newSceneData.scenePath
						+ "\nName: " + newSceneData.sceneName
						+ "\nIndex: " + newSceneData.sceneIndex);

					currentScenes.Add(newSceneData);
				}
			}
			else
			{
				Log.d(LOG_TAG, "No scenes found in asset bundle: " + currentAssetBundleName);
			}
		}
		return true;
	}

	private void LoadEntranceScene()
	{
		SceneData entranceSceneData = null;
		foreach (SceneData scene in currentScenes) //Find scene with smallest avaliable build index
		{
			if ((entranceSceneData == null))
			{
				entranceSceneData = scene;
			}
			else if (scene.sceneIndex < entranceSceneData.sceneIndex)
			{
				entranceSceneData = scene;
			}
		}

		if (!(entranceSceneData == null))
		{
			Log.d(LOG_TAG, "Loading scene " + entranceSceneData.sceneName + " with index: " + entranceSceneData.sceneIndex);
			StartCoroutine(LoadSceneASyncCoroutine(entranceSceneData));
		}
	}

	private IEnumerator LoadSceneASyncCoroutine(SceneData sceneData)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneData.scenePath, LoadSceneMode.Additive);

		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		Log.d(LOG_TAG, "Finish loading entrance scene:" + sceneData.sceneName);
		SceneManager.SetActiveScene(SceneManager.GetSceneByPath(sceneData.scenePath));
		Log.d(LOG_TAG, "Set active scene:" + sceneData.sceneName);
	}

	private void UnloadScenes()
	{
		if (currentScenes == null) return;
		int sceneCount = currentScenes.Count;
		for (int i = 0; i < sceneCount; i++)
		{
			Log.d(LOG_TAG, "Unload scene:" + currentScenes[i].sceneName);
#if UNITY_2018_3_OR_NEWER
			SceneManager.UnloadSceneAsync(currentScenes[i].scenePath, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
#else
			SceneManager.UnloadSceneAsync(currentScenes[i].scenePath);
			Resources.UnloadUnusedAssets();
#endif
		}
		currentScenes = null;
	}

	private string GetAssetBundlePathAbsolute(string assetBundleName)
	{
		return Path.Combine(Path.Combine(Directory.GetParent(Application.persistentDataPath).FullName, assetBundlePathRelative), assetBundleName);
	}

	IEnumerator checkSceneCoroutine()
	{
		while (true)
		{
			Log.d(LOG_TAG, "Checking for scene changes...");
			if (LoadSceneHashFromManifest())
			{
				Log.d(LOG_TAG, "Main bundle manifest loaded. Detecting changes to asset bundles.");
				if (DetectChangesInAssetBundles())
				{
					Log.d(LOG_TAG, "Changes in Asset Bundles detected.");
					oldLoadedAssetBundles = currentLoadedAssetBundles;
					if (oldLoadedAssetBundles != null) //Found Asset Bundles to be unloaded
					{
						UnloadScenes();
						Log.d(LOG_TAG, "Asset bundles that needs to be unloaded detected.");
						UnloadAssetBundles(); //UnLoad Asset Bundles
						Log.d(LOG_TAG, "Finished unloading asset bundles.");
					}
					if (currentAssetBundleNames.Count > 0) //Found Asset Bundles to be loaded
					{
						Log.d(LOG_TAG, "Asset bundles that needs to be loaded detected.");
						if (LoadAssetBundles()) //Load Asset Bundles
						{
							Log.d(LOG_TAG, "Finished loading asset bundles.");
							if (GetScenesFromAssetBundles()) //Load Scene data from loaded Asset Bundles
							{
								Log.d(LOG_TAG, "Getting scene data from asset bundles.");
								LoadEntranceScene();
							}
						}
					}
				}
			}
			yield return new WaitForSeconds(checkSceneInterval);
		}
	}

	public class SceneData
	{
		public string scenePath;
		public string sceneName;
		public int sceneIndex;

		public SceneData(string path, string name, int index)
		{
			scenePath = path;
			sceneName = name;
			sceneIndex = index;
		}
	}
}
