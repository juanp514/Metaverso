using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor.PackageManager.UI;

namespace Xennial.Editor
{
    public static class PackageManager
    {
        private static string _serviceLocatorPackage = "http://186.28.57.76:3000/Xennial/Services.git";
        private static string _apiPackage = "http://186.28.57.76:3000/Xennial/XennialAPI.git";
        private static string _multilanguagePackage = "http://186.28.57.76:3000/Xennial/Multilanguage.git";
        private static string _taskManagerPackage = "http://186.28.57.76:3000/Xennial/TaskManager.git";
        private static string _openXRPackage = "com.unity.xr.openxr@1.10.0";
        private static string _interactionToolKitPackage = "com.unity.xr.interaction.toolkit@2.5.2";
        private static string _xrHands = "com.unity.xr.hands@1.3.0";
        private static string _openXRMetaPackage = "com.unity.xr.meta-openxr@1.0.1";
        private static string _starterAssetsSampleName = "Starter Assets";
        private static string _handInteractionSampleName = "Hands Interaction Demo";
        private static string _handVisualizerSampleName = "HandVisualizer";

        private static AddRequest _request;

        private static string GetFullPackageName(string packageName, string packageVersion)
        {
            return string.IsNullOrEmpty(packageVersion) ? packageName : $"{packageName}@{packageVersion}";
        }

        [MenuItem("Xennial Digital/Packages/Tools/Install Service Locator (Required for most packages)", false, 0)]
        private static void InstallServiceLocatorPackage()
        {
            InstallPackage(_serviceLocatorPackage);
        }

        [MenuItem("Xennial Digital/Packages/Tools/Install Xennial API", false, 1)]
        private static void InstallAPIPackage()
        {
            InstallPackage(_apiPackage);
        }

        [MenuItem("Xennial Digital/Packages/Tools/Install Multilanguage", false, 2)]
        private static void InstallMultilanguagePackage()
        {
            InstallPackage(_multilanguagePackage);
        }

        [MenuItem("Xennial Digital/Packages/Tools/Install Task Manager", false, 3)]
        private static void InstallTaskManagerPackage()
        {
            InstallPackage(_taskManagerPackage);
        }

        [MenuItem("Xennial Digital/Packages/XR/Install Open XR", false, 4)]
        private static void InstallOpenXRPackage()
        {
            InstallPackage(_openXRPackage);
        }

        [MenuItem("Xennial Digital/Packages/XR/Install Interaction Toolkit", false, 5)]
        private static void InstallInteractionToolkitPackage()
        {
            InstallPackage(_interactionToolKitPackage);
        }

        [MenuItem("Xennial Digital/Packages/XR/Install XR Hands", false, 6)]
        private static void InstallXRHandsPackage()
        {
            InstallPackage(_xrHands);
        }

        [MenuItem("Xennial Digital/Packages/XR/Meta/Install OpenXR Meta (Required for Mixed Reality)", false, 7)]
        private static void InstallOpenXRMetaPackage()
        {
            InstallPackage(_openXRMetaPackage);
        }

        [MenuItem("Xennial Digital/Packages/XR/Install Required Samples", false, 8)]
        private static void DownloadedSamples()
        {
            string[] packageInfo = _interactionToolKitPackage.Split('@');

            Sample sample;

            if (TryFindSample(packageInfo[0], packageInfo[1], _starterAssetsSampleName, out sample))
            {
                sample.Import(Sample.ImportOptions.OverridePreviousImports);
            }

            if (TryFindSample(packageInfo[0], packageInfo[1], _handInteractionSampleName, out sample))
            {
                sample.Import(Sample.ImportOptions.OverridePreviousImports);
            }

            packageInfo = _xrHands.Split('@');

            if (TryFindSample(packageInfo[0], packageInfo[1], _handVisualizerSampleName, out sample))
            {
                sample.Import(Sample.ImportOptions.OverridePreviousImports);
            }
        }

        private static void InstallPackage(string packageName)
        {
            _request = Client.Add(packageName);
            EditorApplication.update += Progress;
            EditorUtility.DisplayProgressBar("Installing Package", packageName, 1);
        }

        private static void Progress()
        {
            if (_request.IsCompleted)
            {
                if (_request.Status == StatusCode.Success)
                {
                    Debug.Log("Installed: " + _request.Result.packageId);
                }
                else if (_request.Status >= StatusCode.Failure)
                {
                    Debug.Log(_request.Error.message);
                }

                EditorApplication.update -= Progress;
                EditorUtility.ClearProgressBar();
            }
        }

        private static bool TryFindSample(string packageName, string packageVersion, string sampleDisplayName, out Sample sample)
        {
            sample = default;

            IEnumerable<Sample> packageSamples;
            try
            {
                packageSamples = Sample.FindByPackage(packageName, packageVersion);
            }
            catch (Exception e)
            {
                Debug.LogError($"Couldn't find samples of the {GetFullPackageName(packageName, packageVersion)} package; action aborted. Exception: {e}");
                return false;
            }

            if (packageSamples == null)
            {
                Debug.LogWarning($"Couldn't find samples of the {GetFullPackageName(packageName, packageVersion)} package; action aborted.");
                return false;
            }

            foreach (Sample packageSample in packageSamples)
            {
                if (packageSample.displayName == sampleDisplayName)
                {
                    sample = packageSample;
                    return true;
                }
            }

            Debug.LogWarning($"Couldn't find {sampleDisplayName} sample in the {GetFullPackageName(packageName, packageVersion)} package; action aborted.");
            return false;
        }
    }
}