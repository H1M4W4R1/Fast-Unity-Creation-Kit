using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Core.Extensions;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Data.Abstract;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Editor.Extensions;
using FastUnityCreationKit.Editor.Postprocessing.Abstract;
using JetBrains.Annotations;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Editor.Postprocessing.Annotations
{
    [UsedImplicitly]
    public sealed class AutoRegisterInAttributeProcessor : QuickAssetProcessor<AutoRegisterInAttributeProcessor>
    {
        protected override bool AssetIsRequired => true;

        public override void PostprocessCreatedAsset(string assetPath)
        {
            if (!IsAssetAvailable) return;
            TryToRegisterInContainers(CurrentAsset);
        }

        internal static bool IsAddressableContainer(Type type)
        {
            return type.ImplementsOpenGenericClass(typeof(AddressableDataContainer<>));
        }

        internal static void TryToRegisterInContainers([NotNull] Object obj)
        {
            Type type = obj.GetType();

            // Check if object has AutoRegisterInAttribute
            IEnumerable<AutoRegisterInAttribute> registerInAttribute =
                CustomAttributeExtensions.GetCustomAttributes<AutoRegisterInAttribute>(type, true);

            // Ensure object is registered in specified databases
            foreach (AutoRegisterInAttribute registerAttribute in registerInAttribute)
            {
                if (obj.IsAddressable())
                {
                    // Addressable object can only be registered in addressable database
                    if (!IsAddressableContainer(registerAttribute.Type))
                    {
                        Guard<ValidationLogConfig>.Error(
                            $"Cannot register {obj.name} in {registerAttribute.Type.GetCompilableNiceFullName()}. " +
                            "Type is not a valid AddressableDataContainer.");
                        continue;
                    }

                    TryRegisterInContainer(obj, registerAttribute, GetAddressableObjectToRegister);
                }
                else
                {
                    // Regular object cannot be added to addressable database
                    if (IsAddressableContainer(registerAttribute.Type))
                    {
                        Guard<ValidationLogConfig>.Error(
                            $"Cannot register {obj.name} in {registerAttribute.Type.GetCompilableNiceFullName()}. " +
                            "Type is a valid AddressableDataContainer. Object is not addressable.");
                        continue;
                    }

                    TryRegisterInContainer(obj, registerAttribute, SelfObjectReference);
                }
            }
        }

        private static void TryRegisterInContainer(
            [NotNull] Object obj,
            [NotNull] AutoRegisterInAttribute registerAttribute,
            Func<Object, Type, AutoRegisterInAttribute, object> getRegistryObjectRequestAction)
        {
            // Get object type and base type used to define database
            Type type = obj.GetType();
            IEnumerable<Type> baseClasses = type.GetBaseClasses();

            Type foundBaseClass = null;

            // Find the first base class that has the specified type
            // without inheritance (this attribute is defined on the base class)
            foreach (Type baseClass in baseClasses)
            {
                IEnumerable<AutoRegisterInAttribute> attributes =
                    baseClass.GetCustomAttributes<AutoRegisterInAttribute>(false);

                // Get first attribute that matches the specified type
                AutoRegisterInAttribute attribute =
                    attributes.FirstOrDefault(a => a.Type == registerAttribute.Type);

                // Check if attribute is not null, if found, assign it to the base class
                if (attribute == null) continue;
                foundBaseClass = baseClass;
                break;
            }

            if (foundBaseClass == null)
            {
                Guard<ValidationLogConfig>.Error(
                    $"Cannot register {obj.name} in {registerAttribute.Type.GetCompilableNiceFullName()}. " +
                    "[AutoRegisterIn] not found on any base class.");
                return;
            }

            // Get database type
            Type databaseSubtype = registerAttribute.Type;
            Type databaseType = typeof(AddressableDatabase<,>).MakeGenericType(databaseSubtype, foundBaseClass);

            // Get instance of the database
            PropertyInfo property =
                databaseType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            object database = property?.GetValue(null);

            // Check if database is not null
            if (database == null)
            {
                Guard<ValidationLogConfig>.Error(
                    $"Cannot register {obj.name} in {databaseSubtype.GetCompilableNiceFullName()} ({databaseType.GetCompilableNiceFullName()}). Database instance not found. " +
                    "Are you sure it has public 'Instance' property?");
                return;
            }

            // Get method to register object
            MethodInfo registerMethod = databaseSubtype.GetMethod("Add");
            MethodInfo checkMethod = databaseSubtype.GetMethod("Contains");

            // Check if method is not null
            if (registerMethod == null || checkMethod == null)
            {
                Guard<ValidationLogConfig>.Error(
                    $"Cannot register {obj.name} in {databaseSubtype.GetCompilableNiceFullName()}. 'Add' or 'Contains' method not found. " +
                    "Are you sure it exists and is public?");
                return;
            }

            try
            {
                // Get object to register
                object objToRegister = getRegistryObjectRequestAction(obj, foundBaseClass, registerAttribute);

                // Ensure object is not already registered
                if (checkMethod.Invoke(database, new[] {objToRegister}) is true) return;

                // Register object in database (add kvp)
                registerMethod.Invoke(database, new[] {objToRegister});

                Guard<ValidationLogConfig>.Debug(
                    $"Registered {obj.name} in {databaseSubtype.GetCompilableNiceFullName()} database.");

                // Reserialize database!
                if (database is Object unityObject) EditorUtility.SetDirty(unityObject);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);

                // Warn that adding failed, probably trying to add Addressable Asset to non-addressable
                // database.  
                Guard<ValidationLogConfig>.Warning(
                    $"Failed to register {obj.name} in {databaseSubtype.GetCompilableNiceFullName()}. " +
                    "Are you trying to add Addressable Asset to non-addressable database?");
            }
        }

        private static object GetAddressableObjectToRegister(
            [NotNull] Object obj,
            [NotNull] Type foundBaseClass,
            AutoRegisterInAttribute registerAttribute)
        {
            // Get reference to object
            (string address, AssetReference reference) = obj.GetAssetReference(foundBaseClass);

            // Prepare reference to be proper one
            Type assetReferenceTType = typeof(AssetReferenceT<>).MakeGenericType(foundBaseClass);
            object assetReferenceT = reference.CastToReflected(assetReferenceTType);

            // Create new KeyValuePair instance to be used in Contains and Add methods   
            object[] parameters = {address, assetReferenceT};
            object kvp = Activator.CreateInstance(
                typeof(AddressableReferenceEntry<>).MakeGenericType(foundBaseClass),
                parameters);

            return kvp;
        }

        [NotNull] private static object SelfObjectReference(
            [NotNull] Object obj,
            [NotNull] Type foundBaseClass,
            AutoRegisterInAttribute registerAttribute)
        {
            return obj;
        }
    }
}