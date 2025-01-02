using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace FastUnityCreationKit.Editor.Utility
{
    public static class OpenPathsToolbar
    {
        [MenuItem("Paths/Project Path")]
        public static void OpenPersistentDataPath()
        {
            Process.Start(Application.persistentDataPath);
        }
        
        [MenuItem("Paths/Streaming Assets Path")]
        public static void OpenStreamingAssetsPath()
        {
            Process.Start(Application.streamingAssetsPath);
        }
        
        [MenuItem("Paths/Data Path")]
        public static void OpenDataPath()
        {
            Process.Start(Application.dataPath);
        }
        
        [MenuItem("Paths/Temporary Cache Path")]
        public static void OpenTemporaryCachePath()
        {
            Process.Start(Application.temporaryCachePath);
        } 

    }
}