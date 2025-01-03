using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Time;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Examples._04_FastMonoBehaviour_UpdateModes.Scripts
{
    /// <summary>
    /// Internal class for handling example CK04 in Unity subsystem.
    /// </summary>
    public sealed class ExampleObjectsHandler04 : MonoBehaviour
    {
        [SerializeField] [Required] private GameObject objectWithDisabledTimeFastMonoBehaviour;
        private PauseObject _pauseRef;
        
        public void OnObjectStatusChanged(bool isNotActive) => 
            objectWithDisabledTimeFastMonoBehaviour.SetActive(!isNotActive);

        public void SetPauseState(bool isPaused)
        {
            if(!TimeAPI.IsTimePaused)
                TimeAPI.PauseTime(out _pauseRef);
            else
                TimeAPI.ResumeTimeUsing(_pauseRef);
        }

    }
}