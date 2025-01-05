using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Data.Containers
{
    [Serializable]
    public sealed class AddressableReferenceEntry<TEntryType> : IEquatable<AddressableReferenceEntry<TEntryType>>
        where TEntryType : Object
    {
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

        [ShowInInspector] [ReadOnly] 
        [field: SerializeField, HideInInspector]
        public string Address { get; private set; }

        [ShowInInspector] [ReadOnly]
        [field: SerializeField, HideInInspector]
        public AssetReferenceT<TEntryType> Entry { get; private set; }

        public bool Equals(AddressableReferenceEntry<TEntryType> other)
        {
            return Address == other?.Address;
        }

        public static bool operator ==([CanBeNull] AddressableReferenceEntry<TEntryType> a, [CanBeNull] object obj)
        {
            if (ReferenceEquals(a, null)) return obj == null;
            if (ReferenceEquals(a.Address, null)) return obj == null;
            if (ReferenceEquals(a.Entry, null)) return obj == null;

            // If object was deleted from the project folder
            // then check if we're comparing with null
#if UNITY_EDITOR
            AssetDatabase.GUIDToAssetPath(a.Entry.AssetGUID);
            if (!AssetDatabase.AssetPathExists(
                    AssetDatabase.GUIDToAssetPath(a.Entry.AssetGUID)))
                return obj == null;
#endif

            return obj switch
            {
                null => a.Entry == null || string.IsNullOrEmpty(a.Address),
                string address => a.Address == address,
                AssetReferenceT<TEntryType> entry => a.Entry == entry,
                AddressableReferenceEntry<TEntryType> entryDefinition => a.Address == entryDefinition.Address &&
                                                                         a.Entry?.AssetGUID ==
                                                                         entryDefinition.Entry?.AssetGUID,
                _ => false
            };
        }

        public static bool operator !=([CanBeNull] AddressableReferenceEntry<TEntryType> a, [CanBeNull] object obj)
        {
            return !(a == obj);
        }

        public static bool operator ==([CanBeNull] object obj, [CanBeNull] AddressableReferenceEntry<TEntryType> a)
        {
            return a == obj;
        }

        public static bool operator !=([CanBeNull] object obj, [CanBeNull] AddressableReferenceEntry<TEntryType> a)
        {
            return !(obj == a);
        }

        public override bool Equals(object obj)
        {
            return this == obj;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable NonReadonlyMemberInGetHashCode
                return ((Address != null ? Address.GetHashCode() : 0) * 397) ^
                       (Entry != null ? Entry.GetHashCode() : 0);
                // ReSharper restore NonReadonlyMemberInGetHashCode
            }
        }
    }
}