using System;
using System.Collections.Generic;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;
using NUnit.Framework;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Compilation;
using Assembly = System.Reflection.Assembly;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    [InitializeOnLoad] public static class QuickAssetProcessorCore
    {
        private static readonly List<Type> _quickAssetProcessorTypes = new();

        // Asset postprocessor methods 
        private static readonly List<MethodInfo> _onPostprocessAllAssets = new();
        private static readonly List<MethodInfo> _onAssetCreated = new();

        // Asset preprocessor methods
        private static readonly List<MethodInfo> _onWillCreateAsset = new();
        private static readonly List<MethodInfo> _onWillDeleteAsset = new();
        private static readonly List<MethodInfo> _onWillMoveAsset = new();
        private static readonly List<MethodInfo> _onWillSaveAssets = new();

        // Asset script preprocessor methods
        private static readonly List<MethodInfo> _beforeCompilationStarted = new();
        private static readonly List<MethodInfo> _afterCompilationFinished = new();

        static QuickAssetProcessorCore()
        {
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
            CompilationPipeline.compilationStarted += OnCompilationStarted;
            CompilationPipeline.compilationFinished += OnCompilationFinished;
        }

        /// <summary>
        ///     Creates an asset at the specified path.
        /// </summary>
        public static void CreateAsset(string path, Object asset)
        {
            AssetDatabase.CreateAsset(asset, path);

            // Notify that asset was created
            foreach (MethodInfo method in _onAssetCreated) method.Invoke(null, new object[] {path});
        }

        private static void OnCompilationFinished(object obj)
        {
            // Call all script processors 
            foreach (MethodInfo method in _afterCompilationFinished) method.Invoke(null, null);
        }

        private static void OnCompilationStarted(object obj)
        {
            // Call all script processors
            foreach (MethodInfo method in _beforeCompilationStarted) method.Invoke(null, null);
        }

        /// <summary>
        ///     Preload all QuickAssetProcessor types when assembly was reloaded.
        /// </summary>
        private static void OnAfterAssemblyReload()
        {
            // Those must be processed on reload to prevent missing events
            _beforeCompilationStarted.Clear();
            _afterCompilationFinished.Clear();

            LoadAllQuickAssetProcessorTypes();
        }

        /// <summary>
        ///     Unload all QuickAssetProcessor types before assembly reload.
        /// </summary>
        private static void OnBeforeAssemblyReload()
        {
            _quickAssetProcessorTypes.Clear();

            _onPostprocessAllAssets.Clear();
            _onAssetCreated.Clear();

            _onWillCreateAsset.Clear();
            _onWillDeleteAsset.Clear();
            _onWillMoveAsset.Clear();
            _onWillSaveAssets.Clear();
        }

        private static void TryRegisterAssetPostprocessor(Type type)
        {
            Type assetPostprocessorBaseType = typeof(QuickAssetProcessorBase<>).MakeGenericType(type);

            // Get asset postprocessor internal class
            Type assetPostprocessorType =
                assetPostprocessorBaseType.GetNestedType(nameof(AssetModifiedPostprocessor));
            if (assetPostprocessorType == null)
            {
                // If asset postprocessor type is not found, log an error
                Guard<ValidationLogConfig>.Error(
                    $"Asset postprocessor type not found in '{assetPostprocessorBaseType.GetCompilableNiceFullName()}'.");
            }
            else
            {
                Type fixedType = assetPostprocessorType.MakeGenericType(type);

                // Register all methods in the asset postprocessor
                TryRegisterMethod(fixedType, nameof(AssetModifiedPostprocessor.OnPostprocessAllAssets),
                    _onPostprocessAllAssets);
                TryRegisterMethod(fixedType, nameof(AssetModifiedPostprocessor.OnAssetCreated), _onAssetCreated);
            }
        }

        private static void TryRegisterAssetPreprocessor(Type type)
        {
            Type assetPostprocessorBaseType = typeof(QuickAssetProcessorBase<>).MakeGenericType(type);

            // Get asset preprocessor internal class 
            Type assetPreprocessorType =
                assetPostprocessorBaseType.GetNestedType(nameof(AssetModifiedPreprocessor));
            if (assetPreprocessorType == null)
            {
                Guard<ValidationLogConfig>.Error(
                    $"Asset preprocessor type not found in '{assetPostprocessorBaseType.GetCompilableNiceFullName()}'.");
            }
            else
            {
                Type fixedType = assetPreprocessorType.MakeGenericType(type);

                TryRegisterMethod(fixedType, nameof(AssetModifiedPreprocessor.OnWillDeleteAsset),
                    _onWillDeleteAsset);
                TryRegisterMethod(fixedType, nameof(AssetModifiedPreprocessor.OnWillCreateAsset),
                    _onWillCreateAsset);
                TryRegisterMethod(fixedType, nameof(AssetModifiedPreprocessor.OnWillMoveAsset), _onWillMoveAsset);
                TryRegisterMethod(fixedType, nameof(AssetModifiedPreprocessor.OnWillSaveAssets),
                    _onWillSaveAssets);
            }
        }

        private static void TryRegisterScriptProcessor(Type type)
        {
            Type assetPostprocessorBaseType = typeof(QuickAssetProcessorBase<>).MakeGenericType(type);

            // Get asset preprocessor internal class  
            Type scriptProcessorType = assetPostprocessorBaseType.GetNestedType(nameof(ScriptProcessor));
            if (scriptProcessorType == null)
            {
                Guard<ValidationLogConfig>.Error(
                    $"Asset preprocessor type not found in '{assetPostprocessorBaseType.GetCompilableNiceFullName()}'.");
            }
            else
            {
                Type fixedType = scriptProcessorType.MakeGenericType(type);

                TryRegisterMethod(fixedType, nameof(ScriptProcessor.BeforeCompilationStarted),
                    _beforeCompilationStarted);
                TryRegisterMethod(fixedType, nameof(ScriptProcessor.AfterCompilationFinished),
                    _afterCompilationFinished);
            }
        }

        private static void TryRegisterMethod(
            [NotNull] Type type,
            [NotNull] string methodName,
            List<MethodInfo> table)
        {
            // Search for the method in the type
            MethodInfo foundMethod =
                type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (foundMethod == null)
                Guard<ValidationLogConfig>.Error(
                    $"{methodName} method not found in '{type.GetCompilableNiceFullName()}'.");
            else
                table.Add(foundMethod);
        }

        /// <summary>
        ///     Loads all QuickAssetProcessor types.
        /// </summary>
        private static void LoadAllQuickAssetProcessorTypes()
        {
            // Loop through all types in all assemblies
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            foreach (Type type in assembly.GetTypes())
            {
                // Skip abstract types
                if (type.IsAbstract) continue;

                // Check if the type is a subclass of QuickAssetProcessorBase
                if (type.ImplementsOpenGenericClass(typeof(QuickAssetProcessorBase<>)))
                    _quickAssetProcessorTypes.Add(type);
            }

            // Sort list by priority
            _quickAssetProcessorTypes.Sort((a, b) =>
            {
                // Get order property 
                OrderAttribute aAttr = a.GetCustomAttribute<OrderAttribute>();
                OrderAttribute bAttr = b.GetCustomAttribute<OrderAttribute>();

                int valueA = aAttr?.Order ?? 0;
                int valueB = bAttr?.Order ?? 0;

                return valueA.CompareTo(valueB);
            });

            // Register all asset postprocessors
            foreach (Type type in _quickAssetProcessorTypes)
            {
                TryRegisterAssetPostprocessor(type);
                TryRegisterAssetPreprocessor(type);
                TryRegisterScriptProcessor(type);
            }
        }

        public sealed class ScriptProcessor
        {
            internal static void BeforeCompilationStarted()
            {
            }


            internal static void AfterCompilationFinished()
            {
            }
        }

        public sealed class AssetModifiedPreprocessor : AssetModificationProcessor
        {
            internal static void OnWillCreateAsset(string assetName)
            {
                foreach (MethodInfo method in _onWillCreateAsset)
                    // Call OnWillCreateAsset method
                    method.Invoke(null, new object[] {assetName});
            }

            internal static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
            {
                AssetDeleteResult result = AssetDeleteResult.DidNotDelete;
                foreach (MethodInfo method in _onWillDeleteAsset)
                {
                    // Call OnWillDeleteAsset method 
                    result = (AssetDeleteResult) method.Invoke(null, new object[] {assetPath, options});

                    // If asset was not deleted or delete failed, break the loop
                    if (result != AssetDeleteResult.DidNotDelete) break;
                }

                return result;
            }

            internal static AssetMoveResult OnWillMoveAsset(string fromPath, string toPath)
            {
                AssetMoveResult result = AssetMoveResult.DidNotMove;
                foreach (MethodInfo method in _onWillMoveAsset)
                {
                    // Call OnWillMoveAsset method
                    result = (AssetMoveResult) method.Invoke(null, new object[] {fromPath, toPath});

                    // If asset was not moved, break the loop
                    if (result != AssetMoveResult.DidNotMove) break;
                }

                return result;
            }

            internal static string[] OnWillSaveAssets(string[] paths)
            {
                foreach (MethodInfo method in _onWillSaveAssets)
                    // Call OnWillSaveAssets method, ignore return value
                    method.Invoke(null, new object[] {paths});

                return paths;
            }
        }

        public sealed class AssetModifiedPostprocessor : AssetPostprocessor
        {
            internal static void OnPostprocessAllAssets(
                string[] importedAssets,
                string[] deletedAssets,
                string[] movedAssets,
                string[] movedFromAssetPaths)
            {
                foreach (MethodInfo method in _onPostprocessAllAssets)
                    // Call OnPostprocessAllAssets method 
                    method.Invoke(null,
                        new object[] {importedAssets, deletedAssets, movedAssets, movedFromAssetPaths});
            }

            [CanBeNull] public object OnAssetCreated(string path)
            {
                return null;
            }
        }
    }
}