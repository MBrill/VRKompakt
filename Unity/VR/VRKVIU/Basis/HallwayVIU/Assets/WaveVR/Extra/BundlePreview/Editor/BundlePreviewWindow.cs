// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BundlePreview
{
	public class BundlePreviewWindow : EditorWindow
	{
		public string apkOutputPath;

		private static bool enableADBUtilitiesLog = false;
		private static bool scriptReloaded = false;
		private static bool finishedBuilding = false;

		private static string finalPackageName= ""; //Use this for build
		private static string prevCustomizedPackageName = "";
		private static string customizedPackageName = "com.htc.WaveVRBundlePreview.Loader"; //Preset for customized package name
		private static string defaultPackageNameSuffix = ".preview"; //Default package name is: Application package name + Suffix
		private bool useCustomizePackageName = false;
		private bool prevUseCustomizePackageName = false;
		private static bool windowLoaded = false;
		private static DeviceAndAppStatus currentDeviceAndAppStatus;

		private static string finalPackageName_EditorPrefKey = "FinalPackageName";
		private static string customizedPackageName_EditorPrefKey = "CustomizedPackageName";
		private static string useCustomizedPackageName_EditorPrefKey = "UseCustomizedPackageName";

		[MenuItem("WaveVR/BundlePreview")]
		static void Init()
		{
			BundlePreviewWindow bundlePreviewWindow = GetWindowWithRect<BundlePreviewWindow>(new Rect(0, 0, 600, 400));
			bundlePreviewWindow.Show();
			currentDeviceAndAppStatus = DeviceAndAppStatus.UNKNOWN;
		}

		void OnFocus()
		{
			windowLoaded = true;
		}

		void OnEnable()
		{
			windowLoaded = true;
		}

		void Awake()
		{
			LoadFromPrefs();
		}

		public void OnGUI()
		{
			if (windowLoaded || finishedBuilding)
			{
				if (finishedBuilding)
				{
					scriptReloaded = false;
				}
				LoadFromPrefs();
				GetDeviceAndAppStatus();
				windowLoaded = false;
				finishedBuilding = false;
			}

			GUILayout.Label("Loader App", EditorStyles.boldLabel);
			prevUseCustomizePackageName = useCustomizePackageName;
			GUILayout.BeginHorizontal();
			GUILayout.Label("Use Customized Package Name: ", GUILayout.Width(200));
			useCustomizePackageName = EditorGUILayout.Toggle(useCustomizePackageName);
			GUILayout.EndHorizontal();

			if (useCustomizePackageName)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Customized Package Name: ");
				finalPackageName = customizedPackageName = GUILayout.TextField(customizedPackageName);
				GUILayout.EndHorizontal();
			}
			else
			{
				finalPackageName = PlayerSettings.applicationIdentifier + defaultPackageNameSuffix;
			}

			if (!string.IsNullOrEmpty(prevCustomizedPackageName))
			{
				if (!string.Equals(prevCustomizedPackageName, customizedPackageName))
				{
					SaveToPrefs();
					prevCustomizedPackageName = customizedPackageName;
					GetDeviceAndAppStatus();
				}
			}
			else
			{
				prevCustomizedPackageName = customizedPackageName;
			}

			if (prevUseCustomizePackageName != useCustomizePackageName)
			{
				GetDeviceAndAppStatus();
				SaveToPrefs();
			}

			GUILayout.Label("Current package name: " + finalPackageName);

			GUILayout.Space(3.0f);

			string statusMessage = "Application Status.";

			switch (currentDeviceAndAppStatus)
			{
				case DeviceAndAppStatus.DEVICE_NOT_FOUND:
					statusMessage = "No ADB Devices found. Please check your connection.";
					break;
				case DeviceAndAppStatus.DEVICE_MULTIPLE:
					statusMessage = "Multiple ADB Devices found. check you connected devices.";
					break;
				case DeviceAndAppStatus.APP_NOT_FOUND:
					statusMessage = "Loader app not found on connected device.";
					break;
				case DeviceAndAppStatus.APP_INSTALLED:
					statusMessage = "Loader app found on connected device.";
					if (scriptReloaded)
					{
						statusMessage = "Script changes detected, please rebuild APK to deploy updated scripts.";
					}
					break;
				case DeviceAndAppStatus.UNKNOWN:
					statusMessage = "ADB error occured, status unknown.";
					break;
				
			}
				

			GUILayout.Label("Device and loader app Status: " + statusMessage);

			GUILayout.Space(3.0f);

			GUILayout.BeginHorizontal();
			if (!(currentDeviceAndAppStatus == DeviceAndAppStatus.DEVICE_NOT_FOUND || currentDeviceAndAppStatus == DeviceAndAppStatus.DEVICE_MULTIPLE))
			{
				if (GUILayout.Button("Build and Install", GUILayout.Width(290)))
				{
					//Call BPM Function
					SaveToPrefs();
					BundlePreviewManager.BuildandRunLoaderAPK(finalPackageName);
					GetDeviceAndAppStatus();
					finishedBuilding = true;
					EditorGUIUtility.ExitGUI();
				}
				if (currentDeviceAndAppStatus == DeviceAndAppStatus.APP_INSTALLED)
				{
					if (GUILayout.Button("Uninstall", GUILayout.Width(290)))
					{
						//Call BPM Function
						BundlePreviewManager.UninstallLoaderAPK(finalPackageName, enableADBUtilitiesLog);
						GetDeviceAndAppStatus();
					}
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (currentDeviceAndAppStatus == DeviceAndAppStatus.APP_INSTALLED)
			{
				if (GUILayout.Button("Start App", GUILayout.Width(290)))
				{
					//Call BPM Function
					BundlePreviewManager.StartLoaderApp(finalPackageName, enableADBUtilitiesLog);
				}
				if (GUILayout.Button("Terminate App", GUILayout.Width(290)))
				{
					//Call BPM Function
					BundlePreviewManager.TerminateLoaderApp(finalPackageName, enableADBUtilitiesLog);
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.Space(10.0f);

			GUILayout.Label("Asset Bundles", EditorStyles.boldLabel);
			GUILayout.Label("1. Select the scenes you wish to include in the Asset Bundles in the Build Settings");
			if (GUILayout.Button("Open Build Settings", GUILayout.Width(290)))
			{
				EditorWindow.GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
			}

			GUILayout.Space(5.0f);

			GUILayout.Label("2. Build and Push scenes whenever they are modified to see the changes on device." +
				"\nRemarks: Make sure your scenes are referenced by path instead of index.");

			if (!(currentDeviceAndAppStatus == DeviceAndAppStatus.DEVICE_NOT_FOUND))
			{
				GUILayout.BeginHorizontal();
				if (currentDeviceAndAppStatus == DeviceAndAppStatus.APP_INSTALLED)
				{
					if (GUILayout.Button("Build and Push to device", GUILayout.Width(290)))
					{
						List<SceneData> buildableScenesData = new List<SceneData>();
						//Call BPM Function
						GetBuildSettingScenes(out buildableScenesData);
						if (buildableScenesData == null)
						{
							Debug.Log("Scene for Loader APK should NOT be included in the Build Settings. Please disable it in the list.");
						}
						else if (buildableScenesData.Count <= 0)
						{
							Debug.Log("No scenes found in Build Settings.");
						}
						else
						{
							Debug.Log("Start building selected scenes from Build Settings.");
							SaveToPrefs();
							BundlePreviewManager.BuildAndPushAssetBundles(buildableScenesData, finalPackageName, enableADBUtilitiesLog);
							finishedBuilding = true;
							EditorGUIUtility.ExitGUI();
						}
					}
					if (GUILayout.Button("Remove from device", GUILayout.Width(290)))
					{
						//Call BPM Function
						BundlePreviewManager.RemoveDeviceAssetBundles(finalPackageName, enableADBUtilitiesLog);
					}
				}
				GUILayout.EndHorizontal();
			}

			if (GUILayout.Button("Clear Local Asset Bundles", GUILayout.Width(290)))
			{
				//Call BPM Function
				BundlePreviewManager.RemoveLocalAssetBundles();
			}

			GUILayout.Space(20f);
			bool adbLogToggleValue = EditorGUILayout.Toggle("Show ADB Utility Log: ", enableADBUtilitiesLog, GUILayout.ExpandWidth(true));
			if (adbLogToggleValue != enableADBUtilitiesLog)
			{
				enableADBUtilitiesLog = adbLogToggleValue;
			}
		}

		private void SaveToPrefs()
		{
			EditorPrefs.SetBool(useCustomizedPackageName_EditorPrefKey, useCustomizePackageName);
			EditorPrefs.SetString(customizedPackageName_EditorPrefKey, customizedPackageName);
			EditorPrefs.SetString(finalPackageName_EditorPrefKey, finalPackageName);
		}

		private void LoadFromPrefs()
		{
			useCustomizePackageName = EditorPrefs.GetBool(useCustomizedPackageName_EditorPrefKey, useCustomizePackageName);
			customizedPackageName = EditorPrefs.GetString(customizedPackageName_EditorPrefKey, customizedPackageName);
			finalPackageName = EditorPrefs.GetString(finalPackageName_EditorPrefKey, finalPackageName);
		}

		private static DeviceAndAppStatus GetDeviceAndAppStatus()
		{
			ADBUtilities adbUtilitiesInstance = new ADBUtilities(enableADBUtilitiesLog);
			List<string> deviceList = null;
			string error = null;
			adbUtilitiesInstance.FindDevices(out deviceList, out error);

			if (!(deviceList == null))
			{
				if (deviceList.Count == 0)
				{
					currentDeviceAndAppStatus = DeviceAndAppStatus.DEVICE_NOT_FOUND;
				}
				else if (deviceList.Count == 1)
				{
					error = null;
					List<string> packageList = null;
					adbUtilitiesInstance.GetPackageList(out packageList, out error);
					if (!(packageList == null))
					{
						if (packageList.Contains(finalPackageName))
						{
							currentDeviceAndAppStatus = DeviceAndAppStatus.APP_INSTALLED;
						}
						else
						{
							currentDeviceAndAppStatus = DeviceAndAppStatus.APP_NOT_FOUND;
						}
					}
					else
					{
						currentDeviceAndAppStatus = DeviceAndAppStatus.UNKNOWN;
					}
				}
				else if (deviceList.Count > 1)
				{
					currentDeviceAndAppStatus = DeviceAndAppStatus.DEVICE_MULTIPLE;
				}
				else
				{
					currentDeviceAndAppStatus = DeviceAndAppStatus.UNKNOWN;
				}
			}
			else
			{
				currentDeviceAndAppStatus = DeviceAndAppStatus.UNKNOWN;
			}
			return currentDeviceAndAppStatus;
		}

		private static void GetBuildSettingScenes(out List<SceneData> buildableScenesData)
		{
			buildableScenesData = new List<SceneData>();
			for (int i = 0; i<EditorBuildSettings.scenes.Length; i++)
			{
				EditorBuildSettingsScene currentScene = EditorBuildSettings.scenes[i];
				if (currentScene.enabled)
				{
					string sceneName = Path.GetFileNameWithoutExtension(currentScene.path);
					if (sceneName != "BundlePreviewLoader")
					{
						SceneData newData = new SceneData(currentScene.path, Path.GetFileNameWithoutExtension(currentScene.path), SceneUtility.GetBuildIndexByScenePath(currentScene.path));
						buildableScenesData.Add(newData);
						Debug.Log("Scene from build settings found:" +
							"\nPath: " + newData.scenePath +
							"\nName: " + newData.sceneName +
							"\nBuildIndex: " + newData.sceneIndex);
					}
					else
					{
						buildableScenesData = null;
						return;
					}
				}
			}
		}

		[UnityEditor.Callbacks.DidReloadScripts]
		private static void OnScriptsReloaded()
		{
			scriptReloaded = true;
			//Debug.Log("BundlePreview: Scripts Reload Detected");
		}

		public static void APKBuildFinished()
		{
			scriptReloaded = false;
		}

		public void OnDestroy()
		{
			SaveToPrefs();
		}

		public enum DeviceAndAppStatus
		{
			UNKNOWN = -3,
			DEVICE_NOT_FOUND = -2,
			DEVICE_MULTIPLE = -1,
			APP_NOT_FOUND = 0,
			APP_INSTALLED = 1,
		}
	}
}
