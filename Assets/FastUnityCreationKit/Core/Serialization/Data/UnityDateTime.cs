using System;
using UnityEngine;

namespace FastUnityCreationKit.Core.Serialization.Data
{
    /// <summary>
    /// Represents nicely-serialized DateTime.
    /// </summary>
    [Serializable]
    public struct UnityDateTime
    {
        [SerializeField] [HideInInspector]
        private long ticks;
        
        public UnityDateTime(DateTime dateTime)
        {
            ticks = dateTime.Ticks;
        }
        
        public DateTime ToDateTime()
        {
            return new DateTime(ticks);
        }
        
        public static implicit operator UnityDateTime(DateTime dateTime)
        {
            return new UnityDateTime(dateTime);
        }

        public static implicit operator DateTime(UnityDateTime unityDateTime)
        {
            return unityDateTime.ToDateTime();
        }
    }
}