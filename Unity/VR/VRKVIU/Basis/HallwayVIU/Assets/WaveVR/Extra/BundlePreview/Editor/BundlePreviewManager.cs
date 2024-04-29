// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build.Reporting;
#endif
using UnityEngine;

namespace BundlePreview
{
	public class BundlePreviewManager
	{
		private static string assetBundleManifestFileExtension = ".manifest";

		private static string local_AssetBundlePathRelative = "AssetBundleBuildDir";
		private static string local_AssetBundlePathAbsolute = "";
		private static string local_RemoteAssetBundlePullPathRelative = "RemoteABTemp";
		private static string local_RemoteAssetBundlePullPathAbsolute = "";
		private static string remote_AssetBundlePathRelative = "assetbundles";
		private static string remote_AssetBundlePathAbsolute = "";

		private static string local_LoaderAPKPathRelative = "LoadAPKBuildDir";
		private static string loaderAPKFileName = "BundlePreview_Loader.apk";
		private static string loaderScenePathRelative = "Scenes/BundlePreviewLoader.unity";
		private static string loaderScenePathAbsolute = "";

		private static string androidAppDataPath = "/sdcard/Android/data";

		private static string originalCompanyName, originalProductName, originalPackageName, originalVersion;
		private static ScriptingImplementation originalScriptingBackEnd;
#if UNITY_2018_3_OR_NEWER
		private static ManagedStrippingLevel originalManagedStrippingLevel;
#else
		private static StrippingLevel originalStrippingLevel;
#endif
		private static bool originalStripEngineCode;
		private static int originalBundleVersionCode;

		private static string loaderCompanyName = "HTC Corp.";
		private static string loaderProductName = "BundlePreview";
		private static string loaderPackageName = null;
		private static string loaderVersion = "1.0.0";
		private static ScriptingImplementation loaderScriptingBackEnd = ScriptingImplementation.Mono2x;
#if UNITY_2018_3_OR_NEWER
		private static ManagedStrippingLevel loaderManagedStrippingLevel = ManagedStrippingLevel.Disabled;
#else
		private static StrippingLevel loaderStrippingLevel = StrippingLevel.Disabled;
#endif
		private static bool loaderStripEngineCode = false;
		private static int loaderBundleVersionCode = 1;

		//Build and Push Asset Bundle
		public static void BuildAndPushAssetBundles(List<SceneData> sceneDataList, string loaderAPKPackageName, bool enableADBUtilitiesLog)
		{
			DateTime build_beginning_timestamp = DateTime.Now;
			if (BuildAssetBundles(sceneDataList))
			{
				Debug.Log("AssetBundle built successfully in: " + (DateTime.Now - build_beginning_timestamp).TotalSeconds + " secs");
				DateTime push_beginning_timestamp = DateTime.Now;
				if (PushAssetBundles(loaderAPKPackageName, enableADBUtilitiesLog))
				{
					Debug.Log("Asset Bundles pushed successfully in: " + (DateTime.Now - push_beginning_timestamp).TotalSeconds + " secs");

					return;
				}
			}
		}

		//Build Asset Bundle
		private static bool BuildAssetBundles(List<SceneData> sceneDataList)
		{
			//Get local asset bundle build path
			local_AssetBundlePathAbsolute = Path.Combine(Path.Combine(Directory.GetParent(Application.dataPath).FullName, local_AssetBundlePathRelative), remote_AssetBundlePathRelative).Replace("/", "\\");
			if (!Directory.Exists(local_AssetBundlePathAbsolute))
			{
				Directory.CreateDirectory(local_AssetBundlePathAbsolute);
				Debug.Log("Asset Bundle build directory not found, creating new directory at: " + local_AssetBundlePathAbsolute);
			}

			//Start building
			Dictionary<string, string> sceneAssetReferences = new Dictionary<string, string>();
			Dictionary<string, List<string>> sceneAssetMapping = new Dictionary<string, List<string>>();
			List<AssetBundleBuild> assetBundleBuildList = new List<AssetBundleBuild>();

			//Create Asset Bundles for Scenes Selected in Build Settings
			foreach (SceneData sceneData in sceneDataList)
			{
				//Mark all unique asset dependencies for each scene
				string[] sceneAssetDependencies = AssetDatabase.GetDependencies(sceneData.scenePath);
				if (sceneAssetDependencies.Length > 0)
				{
					foreach (string sceneAssetPath in sceneAssetDependencies)
					{
						string fileExtension = Path.GetExtension(sceneAssetPath);
						if (!sceneAssetReferences.ContainsKey(sceneAssetPath) && !fileExtension.Equals(".cs") && !fileExtension.Equals(".unity"))
						{
							sceneAssetReferences[sceneAssetPath] = sceneData.sceneName;
							if (!sceneAssetMapping.ContainsKey(fileExtension))
							{
								sceneAssetMapping[fileExtension] = new List<string>();
							}
							sceneAssetMapping[fileExtension].Add(sceneAssetPath);
							Debug.Log("Added Unique Asset: " + sceneAssetPath);
						}
					}
				}

				AssetBundleBuild sceneBuild = new AssetBundleBuild();
				sceneBuild.assetNames = new string[] { sceneData.scenePath };
				sceneBuild.assetBundleName = sceneData.sceneIndex + "_" + sceneData.sceneName;
				assetBundleBuildList.Add(sceneBuild);
			}

			//Create AssetBundles for unique asset dependencies of selected scenes
			Dictionary<string, List<string>>.KeyCollection fileTypes = sceneAssetMapping.Keys;
			foreach (string fileType in fileTypes)
			{
				List<string> assetsOfType = null;
				sceneAssetMapping.TryGetValue(fileType, out assetsOfType);
				if (assetsOfType != null)
				{
					AssetBundleBuild assetBuild = new AssetBundleBuild();
					assetBuild.assetNames = assetsOfType.ToArray<string>();
					assetBuild.assetBundleName = "assets_" + fileType.Substring(1);
					assetBundleBuildList.Add(assetBuild);
				}
			}

			//Use BuildPipiline to process all Asset Bundle Builds
			BuildPipeline.BuildAssetBundles(local_AssetBundlePathAbsolute, assetBundleBuildList.ToArray(), BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);
			return true;
		}

		//Push Asset Bundle
		private static bool PushAssetBundles(string loaderAPKPackageName, bool enableADBUtilitiesLog)
		{
			List<string> filesToBePushed = new List<string>();
			List<string> filesToBeDeleted = new List<string>();
			ADBUtilities adbUtilitiesInstance = new ADBUtilities(enableADBUtilitiesLog);

			remote_AssetBundlePathAbsolute = androidAppDataPath + "/" + loaderAPKPackageName + "/" + remote_AssetBundlePathRelative;
			string remote_MasterAssetBundlePathAbsolute = remote_AssetBundlePathAbsolute + "/" + remote_AssetBundlePathRelative;
			//Debug.Log("remote_AssetBundlePathAbsolute: " + remote_AssetBundlePathAbsolute);
			local_RemoteAssetBundlePullPathAbsolute = Path.Combine(Directory.GetParent(Application.dataPath).FullName, local_RemoteAssetBundlePullPathRelative).Replace("/", "\\");
			//Debug.Log("local_RemoteAssetBundlePullPathAbsolute: " + local_RemoteAssetBundlePullPathAbsolute);
			if (!Directory.Exists(local_RemoteAssetBundlePullPathAbsolute))
			{
				Directory.CreateDirectory(local_RemoteAssetBundlePullPathAbsolute);
				Debug.Log("Remote manifest temp directory not found, creating new directory at: " + local_RemoteAssetBundlePullPathAbsolute);
			}
			//Check Asset Bundle Manifest
			//1. Pull remote manifest files
			string output, error = null;
			if (adbUtilitiesInstance.PullFiles(remote_MasterAssetBundlePathAbsolute, local_RemoteAssetBundlePullPathAbsolute, out output, out error) == ADBUtilities.adbNormalExitCode)
			{
				//Read remote manifest
				Debug.Log("Reading Remote manifest...");
				AssetBundle pulledBundle = AssetBundle.LoadFromFile(Path.Combine(local_RemoteAssetBundlePullPathAbsolute, remote_AssetBundlePathRelative));

				if (pulledBundle == null)
				{
					Debug.Log("Failed to load pulled remote bundle.");
					return false;
				}
				AssetBundleManifest pulledManifest = pulledBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
				Dictionary<string, Hash128> pulledBundleHashDictionary = new Dictionary<string, Hash128>();
				string[] pulledAssetBundles = null;
				if (pulledManifest != null)
				{
					pulledAssetBundles = pulledManifest.GetAllAssetBundles();
					foreach (string assetBundle in pulledAssetBundles)
					{
						pulledBundleHashDictionary[assetBundle] = pulledManifest.GetAssetBundleHash(assetBundle);
						Debug.Log("Key: " + assetBundle + " Value: " + pulledBundleHashDictionary[assetBundle] + " added to pulled hash dictionary");
					}
				}
				pulledBundle.Unload(true);

				//Read local manifest 
				Debug.Log("Reading Local manifest...");
				AssetBundle builtBundle = AssetBundle.LoadFromFile(Path.Combine(local_AssetBundlePathAbsolute, remote_AssetBundlePathRelative));

				if (builtBundle == null)
				{
					Debug.Log("Failed to load built local bundle.");
					return false;
				}
				AssetBundleManifest builtManifest = builtBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
				Dictionary<string, Hash128> builtBundleHashDictionary = new Dictionary<string, Hash128>();
				string[] builtAssetBundles = null;
				if (builtManifest != null)
				{
					builtAssetBundles = builtManifest.GetAllAssetBundles();
					foreach (string assetBundle in builtAssetBundles)
					{
						builtBundleHashDictionary[assetBundle] = builtManifest.GetAssetBundleHash(assetBundle);
						Debug.Log("Key: " + assetBundle + " Value: " + builtBundleHashDictionary[assetBundle] + " added to built hash dictionary");
					}
				}
				builtBundle.Unload(true);

				//2. Compare Remote manifest files and local manifest files
				foreach (string builtAssetBundle in builtAssetBundles)
				{
					if (!pulledBundleHashDictionary.ContainsKey(builtAssetBundle) || pulledBundleHashDictionary[builtAssetBundle] != builtBundleHashDictionary[builtAssetBundle])
					{
						filesToBePushed.Add(builtAssetBundle);
						filesToBePushed.Add(builtAssetBundle + assetBundleManifestFileExtension);
						Debug.Log("Asset Bundle to be pushed: " + builtAssetBundle);
					}
				}

				foreach (string pulledAssetBundle in pulledAssetBundles)
				{
					if (!builtBundleHashDictionary.ContainsKey(pulledAssetBundle))
					{
						filesToBeDeleted.Add(pulledAssetBundle);
						filesToBeDeleted.Add(pulledAssetBundle + assetBundleManifestFileExtension);
						Debug.Log("Asset Bundle to be deleted: " + pulledAssetBundle);
					}
				}

				//3. Must push Master Bundle and manifest
				filesToBePushed.Add(remote_AssetBundlePathRelative);
				filesToBePushed.Add(remote_AssetBundlePathRelative + assetBundleManifestFileExtension);
			}
			else
			{
				if (output.Contains("does not exist"))
				{
					//Create remote directory and add all asset bundles to push list
					Debug.Log("Creating new asset bundle directory on remote.");
					if (adbUtilitiesInstance.MakeDirectory(remote_AssetBundlePathAbsolute, out error) == ADBUtilities.adbNormalExitCode)
					{
						string[] builtAssetBundlePaths = Directory.GetFiles(local_AssetBundlePathAbsolute);
						if (builtAssetBundlePaths.Length > 0)
						{
							foreach (string builtAssetBundle in builtAssetBundlePaths)
							{
								filesToBePushed.Add(Path.Combine(local_AssetBundlePathAbsolute, builtAssetBundle));
								Debug.Log("Asset Bundle to be pushed: " + builtAssetBundle);
							}
						}
					}
				}
			}


			//Push Asset Bundles if needed
			if (filesToBePushed.Count > 0)
			{
				foreach (string file in filesToBePushed)
				{
					string assetBundleLocalPath = Path.Combine(local_AssetBundlePathAbsolute, file);
					adbUtilitiesInstance.PushFiles(assetBundleLocalPath, remote_AssetBundlePathAbsolute);
				}
				Debug.Log("Number of bundles pushed: " + filesToBePushed.Count / 2);
			}

			//Delete Assest Bundles if needed
			if (filesToBeDeleted.Count > 0)
			{
				foreach (string file in filesToBeDeleted)
				{
					string assetBundleDevicePath = remote_AssetBundlePathAbsolute + "/" + Path.GetFileName(file);
					adbUtilitiesInstance.RemoveFile(assetBundleDevicePath);
					Debug.Log("Remove: " + assetBundleDevicePath);
				}
				Debug.Log("Number of bundles deleted: " + filesToBeDeleted.Count / 2);
			}

			//Clear pulled files
			if (Directory.Exists(local_RemoteAssetBundlePullPathAbsolute))
			{
				string[] subDirectories = Directory.GetDirectories(local_RemoteAssetBundlePullPathAbsolute);
				if (subDirectories.Length > 0)
				{
					foreach (string directory in subDirectories)
					{
						string[] filePaths = Directory.GetFiles(directory);
						foreach (string file in filePaths)
						{
							File.Delete(file);
						}
						Directory.Delete(directory);
					}
				}
			}
			return true;
		}

		//Remove local Asset Bundle
		public static void RemoveLocalAssetBundles()
		{
			local_AssetBundlePathAbsolute = Path.Combine(Path.Combine(Directory.GetParent(Application.dataPath).FullName, local_AssetBundlePathRelative), remote_AssetBundlePathRelative).Replace("/", "\\");
			if (Directory.Exists(local_AssetBundlePathAbsolute))
			{
				Debug.Log("Remove local asset bundles.");
				string[] filePaths = Directory.GetFiles(local_AssetBundlePathAbsolute);
				foreach (string file in filePaths)
				{
					File.Delete(file);
				}
			}
			else
			{
				Debug.Log("Local asset bundle directory not found.");
			}
		}

		//Remove device Asset Bundle
		public static void RemoveDeviceAssetBundles(string loaderAPKPackageName, bool enableADBUtilitiesLog)
		{
			string error = null;
			ADBUtilities adbUtilitiesInstance = new ADBUtilities(enableADBUtilitiesLog);
			remote_AssetBundlePathAbsolute = androidAppDataPath + "/" + loaderAPKPackageName + "/" + remote_AssetBundlePathRelative;
			Debug.Log("Remove device Asset Bundle Files from " + remote_AssetBundlePathAbsolute);
			if (adbUtilitiesInstance.RemoveDirectory(remote_AssetBundlePathAbsolute, out error) == ADBUtilities.adbNormalExitCode)
			{
				Debug.Log("Removed Asset Bundle Files from device.");
			}
			else
			{
				Debug.Log("Error occured when removeing asset bundle from device: " + error);
			}
		}


		//Build LoaderAPK
		public static void BuildandRunLoaderAPK(string loaderAPKPackageName)
		{
			loaderPackageName = loaderAPKPackageName;

			//Get path to this script
			string[] allPaths = Directory.GetFiles(Application.dataPath, "BundlePreviewManager.cs", SearchOption.AllDirectories);
			if (allPaths.Length > 1)
			{
				Debug.Log("Multiple BundlePreviewManager Scripts found, please check your SDK import.");
				return;
			}
			string pathToManager = allPaths[0];

			//Get apk build path
			string finalAPKBuildDirectory = Path.Combine(Directory.GetParent(Application.dataPath).FullName, local_LoaderAPKPathRelative).Replace("/", "\\");
			if (!Directory.Exists(finalAPKBuildDirectory))
			{
				Directory.CreateDirectory(finalAPKBuildDirectory);
				Debug.Log("APK build directory not found, creating new directory at: " + finalAPKBuildDirectory);
			}
			string finalAPKBuildPath = Path.Combine(finalAPKBuildDirectory, loaderAPKFileName);

			//Get loader scene path
			loaderScenePathAbsolute = Path.Combine(Directory.GetParent(Directory.GetParent(pathToManager).FullName).FullName, loaderScenePathRelative).Replace("\\", "/");
			Debug.Log("Loader Scene Path: " + loaderScenePathAbsolute);
			if (!File.Exists(loaderScenePathAbsolute))
			{
				Debug.Log("Loader Scene Not Found, please check SDK import.");
				return;
			}
			Debug.Log("Loader Scene Found, start Building Loader APK.");

			BackupAndChangePlayerSettings();

#if UNITY_2018_1_OR_NEWER
			BuildReport report = BuildPipeline.BuildPlayer(new string[] { loaderScenePathAbsolute }, finalAPKBuildPath, BuildTarget.Android,
				BuildOptions.Development | BuildOptions.AutoRunPlayer);

			if (report.summary.result == BuildResult.Succeeded)
			{
				Debug.Log("Loader APK build successful, begin auto run on target device.");

			}
			else if (report.summary.result == BuildResult.Failed)
			{
				Debug.Log("Loader APK build failed. Error: " + report);
			}
#else
			string error = BuildPipeline.BuildPlayer(new string[] { loaderScenePathAbsolute }, finalAPKBuildPath, BuildTarget.Android,
				BuildOptions.Development | BuildOptions.AutoRunPlayer);

			if (string.IsNullOrEmpty(error))
			{
				Debug.Log("Loader APK build successful, begin auto run on target device.");
			}
			else
			{
				Debug.Log("Loader APK build failed. Error: " + error);
			}
#endif
			RestorePlayerSettings();
			BundlePreviewWindow.APKBuildFinished();
		}

		//Uninstall LoaderAPK
		public static void UninstallLoaderAPK(string loaderAPKPackageName, bool enableADBUtilitiesLog)
		{
			//Run adb command to uninstall apk
			ADBUtilities adbUtilitiesInstance = new ADBUtilities(enableADBUtilitiesLog);
			string output, error;
			string[] command = { "-d shell", "pm uninstall", loaderAPKPackageName };
			if (adbUtilitiesInstance.RunADBCommand(command, out output, out error) == ADBUtilities.adbNormalExitCode)
			{
				Debug.Log("Loader APK uninstalled successfully");
			}
			else
			{
				Debug.Log("Loader APK uninstalled failed with error: " + error);
			}
		}

		//Start Loader App
		public static void StartLoaderApp(string loaderAPKPackageName, bool enableADBUtilitiesLog)
		{
			ADBUtilities adbUtilitiesInstance = new ADBUtilities(enableADBUtilitiesLog);
			string error;
			int retCode = adbUtilitiesInstance.StartApp(loaderAPKPackageName, out error);
			if (retCode == ADBUtilities.adbNormalExitCode)
			{
				Debug.Log("Loader App Started.");
			}
		}

		//Terminate Loader App
		public static void TerminateLoaderApp(string loaderAPKPackageName, bool enableADBUtilitiesLog)
		{
			ADBUtilities adbUtilitiesInstance = new ADBUtilities(enableADBUtilitiesLog);
			string error;
			int retCode = adbUtilitiesInstance.TerminateApp(loaderAPKPackageName, out error);
			if (retCode == ADBUtilities.adbNormalExitCode)
			{
				Debug.Log("Loader App terminated.");
			}
		}

		//Backup Player Settings of original project and change the to loader APK settings before building loader APK
		private static void BackupAndChangePlayerSettings()
		{
			originalCompanyName = PlayerSettings.companyName;
			originalProductName = PlayerSettings.productName;
			originalPackageName = PlayerSettings.applicationIdentifier;
			originalVersion = PlayerSettings.bundleVersion;
			originalBundleVersionCode = PlayerSettings.Android.bundleVersionCode;
			originalScriptingBackEnd = PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android);

			PlayerSettings.companyName = loaderCompanyName;
			PlayerSettings.productName = loaderProductName;
			PlayerSettings.applicationIdentifier = loaderPackageName;
			PlayerSettings.bundleVersion = loaderVersion;
			PlayerSettings.Android.bundleVersionCode = loaderBundleVersionCode;
			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, loaderScriptingBackEnd);

#if UNITY_2018_3_OR_NEWER
			originalManagedStrippingLevel = PlayerSettings.GetManagedStrippingLevel(BuildTargetGroup.Android);
			PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.Android, loaderManagedStrippingLevel);
#else
			originalStrippingLevel = PlayerSettings.strippingLevel;
			PlayerSettings.strippingLevel = loaderStrippingLevel;
#endif
			originalStripEngineCode = PlayerSettings.stripEngineCode;
			PlayerSettings.stripEngineCode = loaderStripEngineCode;
		}

		//Restore Player Settings of 
		private static void RestorePlayerSettings()
		{
			PlayerSettings.companyName = originalCompanyName;
			PlayerSettings.productName = originalProductName;
			PlayerSettings.applicationIdentifier = originalPackageName;
			PlayerSettings.bundleVersion = originalVersion;
			PlayerSettings.Android.bundleVersionCode = originalBundleVersionCode;
			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, originalScriptingBackEnd);

#if UNITY_2018_3_OR_NEWER
			PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.Android, originalManagedStrippingLevel);
#else
			PlayerSettings.strippingLevel = originalStrippingLevel;
#endif
			PlayerSettings.stripEngineCode = originalStripEngineCode;
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
