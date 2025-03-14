﻿using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using FastUnityCreationKit.Unity.Time;
using UnityEngine;

namespace FastUnityCreationKit.Examples._03_FastMonoBehaviour_TimeModes.Scripts
{
    public abstract class TimeMonoBehaviourExample : CKMonoBehaviour, IOnObjectCreatedCallback, IOnObjectDestroyedCallback,
        IOnObjectUpdateCallback
    {
        public void OnObjectCreated()
        {
            TimeAPI.SetTimeScale(0.5f);
        }

        public void OnObjectDestroyed()
        {
            TimeAPI.SetTimeScale(1f);
        }

        public void OnObjectUpdate(float deltaTime)
        {
            Debug.Log(GetMessage(deltaTime));
        }

        protected abstract string GetMessage(float deltaTime);
    }
}