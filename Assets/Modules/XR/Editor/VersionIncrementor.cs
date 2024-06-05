using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Xennial.XR.Editor
{
    [InitializeOnLoad]
    public class VersionIncrementor
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            Debug.Log($"Build v{PlayerSettings.bundleVersion} ({PlayerSettings.Android.bundleVersionCode})");
            IncreaseBuild();
        }

        private static void IncrementVersion(int majorIncr, int minorIncr, int buildIncr)
        {
            string[] lines = PlayerSettings.bundleVersion.Split('.');

            int majorVersion = lines.Length > 0 ? int.Parse(lines[0]) + majorIncr : majorIncr;
            int minorVersion = lines.Length > 1 ? int.Parse(lines[1]) + minorIncr : minorIncr;
            int build = lines.Length > 2 ? int.Parse(lines[2]) + buildIncr: buildIncr;

            PlayerSettings.bundleVersion = $"{majorVersion}.{minorVersion}.{build}";
            PlayerSettings.Android.bundleVersionCode++;
        }

        [MenuItem("Xennial Digital/Version Controller/Increase Major Version", false, 11)]
        private static void IncreaseMajor()
        {
            IncrementVersion(1, 0, 0);
        }

        [MenuItem("Xennial Digital/Version Controller/Increase Minor Version", false, 12)]
        private static void IncreaseMinor()
        {
            IncrementVersion(0, 1, 0);
        }

        private static void IncreaseBuild()
        {
            IncrementVersion(0, 0, 1);
        }
    }
}