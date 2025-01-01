using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Data.Containers
{
    [Serializable]
    public sealed class AddressableReferenceEntry<TEntryType> : IEquatable<AddressableReferenceEntry<TEntryType>> where TEntryType : Object
    {
        [ShowInInspector] [ReadOnly] [OdinSerialize]
        public string Address { get; private set; }
         
        [ShowInInspector] [ReadOnly] [OdinSerialize]
        public AssetReferenceT<TEntryType> Entry { get; private set; }

        public AddressableReferenceEntry(string address, AssetReferenceT<TEntryType> entry)
        {
            Address = address;
            Entry = entry;
        }
        
        public AddressableReferenceEntry(string address)
        {
            Address = address;
            Entry = new AssetReferenceT<TEntryType>(address);
        }
        
        public AddressableReferenceEntry(string address, AssetReference entry)
        {
            Address = address;
            Entry = (AssetReferenceT<TEntryType>) entry;
        }
  
        public static bool operator ==(AddressableReferenceEntry<TEntryType> a, object obj)
        { 
            if(ReferenceEquals(a, null)) return obj == null;
            if(ReferenceEquals(a.Address, null)) return obj == null;
            if(ReferenceEquals(a.Entry, null)) return obj == null;
            
            // If object was deleted from the project folder
            // then check if we're comparing with null
            #if UNITY_EDITOR
                UnityEditor.AssetDatabase.GUIDToAssetPath(a.Entry.AssetGUID);
                if(!UnityEditor.AssetDatabase.AssetPathExists(UnityEditor.AssetDatabase.GUIDToAssetPath(a.Entry.AssetGUID)))
                    return obj == null;
            #endif

            return obj switch
            {
                null => a.Entry == null || string.IsNullOrEmpty(a.Address),
                string address => a.Address == address,
                AssetReferenceT<TEntryType> entry => a.Entry == entry,
                AddressableReferenceEntry<TEntryType> entryDefinition => a.Address == entryDefinition.Address &&
                                                                         a.Entry?.AssetGUID == entryDefinition.Entry?.AssetGUID,
                _ => false
            };
        }
        
        public static bool operator !=(AddressableReferenceEntry<TEntryType> a, object obj)
        {
            return !(a == obj);
        }
        
        public static bool operator ==(object obj, AddressableReferenceEntry<TEntryType> a)
        {
            return a == obj;
        }

        public static bool operator !=(object obj, AddressableReferenceEntry<TEntryType> a)
        {
            return !(obj == a);
        }
        
        public override bool Equals(object obj)
        {
            return this == obj;
        }

        public bool Equals(AddressableReferenceEntry<TEntryType> other)
        { 
            return Address == other.Address;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Address != null ? Address.GetHashCode() : 0) * 397) ^ (Entry != null ? Entry.GetHashCode() : 0);
            }
        }
    }
}