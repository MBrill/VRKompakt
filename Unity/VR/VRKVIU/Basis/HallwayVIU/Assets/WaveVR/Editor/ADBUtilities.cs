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
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;

public class ADBUtilities
{
	public string androidSDKRoot;
	public static string platformToolsFolderName = "platform-tools";
	public static string adbExecutableName = "adb.exe";
	public const int adbNormalExitCode = 0;
	public static int adbUtilitiesErrCode = -9999;

	public string fullADBPath;

	private StringBuilder outputStr = null;
	private StringBuilder errorStr = null;
	private bool initialized = false;
	private bool printLog = true;

	public ADBUtilities(bool enableLog)
	{
		fullADBPath = Path.Combine(Path.Combine(GetRootPath(), platformToolsFolderName), adbExecutableName);
		//UnityEngine.Debug.Log("ADBUtilities Instance Created. Path of ADB is: " + fullADBPath);

		if (!File.Exists(fullADBPath))
		{
			UnityEngine.Debug.Log("adb.exe does not exist under path " + fullADBPath);
			initialized = false;
			return;
		}
		printLog = enableLog;
		initialized = true;
	}

	public int RunADBCommand(string[] args, out string output, out string error)
	{
		if (!initialized)
		{
			output = null;
			error = null;
			return adbUtilitiesErrCode;
		}

		string arg = string.Join(" ", args);

		Process adbProcess = new Process();
		ProcessStartInfo adbStartInfo = new ProcessStartInfo(fullADBPath, arg);

		outputStr = new StringBuilder("");
		errorStr = new StringBuilder("");

		adbStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		adbStartInfo.UseShellExecute = false;
		adbStartInfo.WorkingDirectory = androidSDKRoot;
		adbStartInfo.CreateNoWindow = true;
		adbStartInfo.RedirectStandardOutput = true;
		adbStartInfo.RedirectStandardError = true;

		adbProcess.StartInfo = adbStartInfo;
		adbProcess.OutputDataReceived += new DataReceivedEventHandler(outputDataReceivedEventHandler);
		adbProcess.ErrorDataReceived += new DataReceivedEventHandler(errorDataReceivedEventHandler);
		adbProcess.Start();

		adbProcess.BeginOutputReadLine();
		adbProcess.BeginErrorReadLine();
		adbProcess.WaitForExit();

		int exitCode = adbProcess.ExitCode;
		adbProcess.Close();

		output = outputStr.ToString();
		error = errorStr.ToString();

		outputStr = null;
		errorStr = null;

		return exitCode;
	}

	public int RunADBCommandNoWait(string[] args)
	{
		if (!initialized) return adbUtilitiesErrCode;

		string arg = string.Join(" ", args);

		Process adbProcess = new Process();
		ProcessStartInfo adbStartInfo = new ProcessStartInfo(fullADBPath, arg);

		adbStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		adbStartInfo.UseShellExecute = false;
		adbStartInfo.WorkingDirectory = androidSDKRoot;
		adbStartInfo.CreateNoWindow = true;
		adbStartInfo.RedirectStandardOutput = true;
		adbStartInfo.RedirectStandardError = true;

		adbProcess.StartInfo = adbStartInfo;
		adbProcess.OutputDataReceived += new DataReceivedEventHandler(outputDataReceivedEventHandler);
		adbProcess.ErrorDataReceived += new DataReceivedEventHandler(errorDataReceivedEventHandler);
		adbProcess.Start();

		adbProcess.BeginOutputReadLine();
		adbProcess.BeginErrorReadLine();

		return 0;
	}

	private int StartServer()
	{
		string[] command = { "start-server" };
		string output, error;
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
		}
		else
		{
			PrintADBCommandExecutionFailLog(command, exitCode);
		}
		return exitCode;
	}

	private int KillServer()
	{
		string[] command = { "kill-server" };
		string output, error;
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
		}
		else
		{
			PrintADBCommandExecutionFailLog(command, exitCode);
		}
		return exitCode;
	}

	public int FindDevices(out List<string> deviceList, out string error)
	{
		string output = null;
		string[] command = { "devices" };
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
			deviceList = new List<string>(output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

			deviceList.RemoveAt(0); //Remove First line of output
#if NET_4_6
			if (deviceList.Count > 0 && string.IsNullOrWhiteSpace(deviceList[deviceList.Count - 1]))
#else
			if (deviceList.Count > 0 && (string.IsNullOrEmpty(deviceList[deviceList.Count - 1]) || deviceList[deviceList.Count - 1].Trim().Length == 0))
#endif
				deviceList.RemoveAt(deviceList.Count - 1); //Remove End line of output
		}
		else
		{
			PrintADBCommandExecutionFailLog(command, exitCode);
			deviceList = null;
		}
		return exitCode;
	}

	public int GetPackageList(out List<string> packageList, out string error)
	{
		string output = null;
		string[] command = { "-d shell pm list package" };
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
			packageList = new List<string>(output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

			for (int i = 0; i < packageList.Count; i++)
			{
				packageList[i] = packageList[i].Remove(0, "package:".Length).Trim();
				//UnityEngine.Debug.Log("Device has package: " + packageList[i]);
			}
		}
		else
		{
			PrintADBCommandExecutionFailLog(command, exitCode);
			packageList = null;
		}
		return exitCode;
	}

	public int StartApp(string packageName, out string error)
	{
		string output = null;
		string activityName = "\"" + packageName + "/com.htc.vr.unity.WVRUnityVRActivity\"";
		string[] command = { "-d shell", "am start -a android.intent.action.MAIN -c android.intent.category.LAUNCHER -S -n", activityName };
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
			return exitCode;
		}
		PrintADBCommandExecutionFailLog(command, exitCode);
		return exitCode;
	}

	public int TerminateApp(string packageName, out string error)
	{
		string output = null;
		string[] command = { "-d shell", "am force-stop", packageName };
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
			return exitCode;
		}
		PrintADBCommandExecutionFailLog(command, exitCode);
		return exitCode;
	}

	public int PushFiles(string source, string destination)
	{
		string[] command = { "-d push", "\"" + source + "\"", "\"" + destination + "\"" };
		int exitCode = RunADBCommandNoWait(command);
		return exitCode;
	}

	public int PullFiles(string source, string destination, out string output, out string error)
	{
		string[] command = { "-d pull", "\"" + source + "\"", "\"" + destination + "\"" };
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
			return exitCode;
		}
		PrintADBCommandExecutionFailLog(command, exitCode);
		return exitCode;
	}

	public int RemoveFile(string filePath)
	{
		string[] command = { "-d shell", "rm", "\"" + filePath + "\"" };
		int exitCode = RunADBCommandNoWait(command);
		return exitCode;
	}

	public int RemoveDirectory(string directory, out string error)
	{
		string output = null;
		string[] command = { "-d shell", "rm -r", directory };
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
			return exitCode;
		}
		PrintADBCommandExecutionFailLog(command, exitCode);
		return exitCode;
	}

	public int MakeDirectory(string directory, out string error)
	{
		string output = null;
		string[] command = { "-d shell", "mkdir -p", "\"" + directory + "\"" };
		int exitCode = RunADBCommand(command, out output, out error);
		if (exitCode == adbNormalExitCode)
		{
			PrintADBCommandExecutionSuccessLog(command);
			return exitCode;
		}
		PrintADBCommandExecutionFailLog(command, exitCode);
		return exitCode;
	}

	private string GetRootPath()
	{
#if UNITY_2019_1_OR_NEWER
		bool isUsingEmbeddedSDK = EditorPrefs.GetBool("SdkUseEmbedded");
		if (isUsingEmbeddedSDK)
		{
			androidSDKRoot = Path.Combine(BuildPipeline.GetPlaybackEngineDirectory(BuildTarget.Android, BuildOptions.None), "SDK");
		}
		else
#endif
		{
			androidSDKRoot = EditorPrefs.GetString("AndroidSdkRoot");
		}
		androidSDKRoot = androidSDKRoot.Replace("/", "\\");
		//UnityEngine.Debug.Log("ADB Utilities - AndroidSDKRoot: " + androidSDKRoot);
		return androidSDKRoot;
	}

	private void PrintADBCommandExecutionSuccessLog(string[] command)
	{
		if (printLog)
			UnityEngine.Debug.Log("adb commmand: " + string.Join("", command) + " execution succeed.");
	}

	private void PrintADBCommandExecutionFailLog(string[] command, int exitCode)
	{
		if (printLog)
			UnityEngine.Debug.LogWarning("adb commmand: " + string.Join("", command) + " execution failed with exit code " + exitCode + ".");
	}

	private void outputDataReceivedEventHandler(object dataSender, DataReceivedEventArgs args)
	{
		if (!String.IsNullOrEmpty(args.Data))
		{
			outputStr.Append(args.Data);
			outputStr.Append(Environment.NewLine);
		}
	}

	private void errorDataReceivedEventHandler(object dataSender, DataReceivedEventArgs args)
	{
		if (!String.IsNullOrEmpty(args.Data))
		{
			errorStr.Append(args.Data);
			errorStr.Append(Environment.NewLine);
		}
	}
}
