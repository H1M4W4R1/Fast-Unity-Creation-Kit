using System;
using FastUnityCreationKit.Editor.Postprocessing.Interfaces;
using JetBrains.Annotations;
using UnityEditor;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    /// <summary>
    /// Base class for Asset Postprocessor. Can be used to quickly create custom
    /// postprocessing behaviour.
    /// </summary>
    public abstract class QuickAssetProcessorBase<TSelfSealed> 
        where TSelfSealed : QuickAssetProcessorBase<TSelfSealed>, new()
    {
        private static TSelfSealed _instance;
        
        /// <summary>
        /// Instance of the derived class.
        /// </summary>
        [NotNull] protected static TSelfSealed Instance => _instance ??= new TSelfSealed();

        /// <summary>
        /// Order of the postprocessor. Default is 0.
        /// </summary>
        protected virtual int Order => 0;

        /// <summary>
        /// Creates an asset at the specified path.
        /// </summary>
        internal static void CreateAsset(string path, Object asset) =>
            QuickAssetProcessorCore.CreateAsset(path, asset);

        public sealed class ScriptProcessor
        {
            [UsedImplicitly]
            internal static void BeforeCompilationStarted()
            {
                if (Instance is IBeforeCompilationStarted preprocessCompilation)
                    preprocessCompilation.BeforeCompilationStarted();
                
            }
            
            [UsedImplicitly]
            internal static void AfterCompilationFinished()
            {
                if (Instance is IAfterCompilationFinished postprocessCompilation)
                    postprocessCompilation.AfterCompilationFinished();
            }
        }
        
        /// <summary>
        /// Handles asset modification preprocessing detection
        /// </summary>
        public sealed class AssetModifiedPreprocessor
        {
            [UsedImplicitly]
            internal static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
            {
                // Check if the instance implements the IPreprocessDeletedAsset interface
                if (Instance is IPreprocessDeletedAsset preprocessDeletedAsset)
                    return preprocessDeletedAsset._PreprocessDeletedAsset(assetPath, options);

                // Default behaviour
                return AssetDeleteResult.DidNotDelete;
            }

            [UsedImplicitly]
            internal static void OnWillCreateAsset(string assetName)
            {
                // Check if the instance implements the IPreprocessCreatedAsset interface
                if (Instance is IPreprocessCreatedAsset preprocessCreatedAsset)
                    preprocessCreatedAsset._PreprocessCreatedAsset(assetName);
            }

            [UsedImplicitly]
            internal static AssetMoveResult OnWillMoveAsset(string fromPath, string toPath)
            {
                // Check if the instance implements the IPreprocessMovedAsset interface
                if (Instance is IPreprocessMovedAsset preprocessMovedAsset)
                    return preprocessMovedAsset._PreprocessMovedAsset(fromPath, toPath);

                // Default behaviour
                return AssetMoveResult.DidNotMove;
            }

            [UsedImplicitly]
            internal static void OnWillSaveAssets([NotNull] string[] paths)
            {
                // Handle all saved assets
                foreach (string path in paths)
                {
                    // Check if the instance implements the IPreprocessSavedAsset interface
                    if (Instance is IPreprocessSavedAsset preprocessSavedAsset)
                        preprocessSavedAsset._PreprocessSavedAsset(path);
                }
            }
        }

        public sealed class AssetModifiedPostprocessor
        {
            [UsedImplicitly]
            internal static void OnAssetCreated(string assetPath)
            {
                // Check if the instance implements the IPostprocessCreatedAsset interface 
                if (Instance is IPostprocessCreatedAsset postprocessCreatedAsset)
                    postprocessCreatedAsset._PostprocessCreatedAsset(assetPath);
            }
            
            [UsedImplicitly]
            internal static void OnPostprocessAllAssets([NotNull] string[] importedAssets,
                [NotNull] string[] deletedAssets,
                [NotNull] string[] movedAssets,
                [NotNull] string[] movedFromAssetPaths)
            {
                if (movedFromAssetPaths == null) throw new ArgumentNullException(nameof(movedFromAssetPaths));
                if(Instance is IPostprocessAllAssets postprocessAllAssets)
                    postprocessAllAssets.PostprocessAllAssets(importedAssets, deletedAssets, movedAssets, movedFromAssetPaths);
                
                // Handle all imported assets
                foreach (string importedAsset in importedAssets)
                {
                    // Check if the instance implements the IPostprocessImportedAsset interface
                    if (Instance is IPostprocessCreatedAsset postprocessImportedAsset)
                        postprocessImportedAsset._PostprocessCreatedAsset(importedAsset);
                }
                
                // Handle all deleted assets
                foreach (string deletedAsset in deletedAssets)
                {
                    // Check if the instance implements the IPostprocessDeletedAsset interface
                    if (Instance is IPostprocessDeletedAsset postprocessDeletedAsset)
                        postprocessDeletedAsset._PostprocessDeletedAsset(deletedAsset);
                }
                
                // Handle all moved assets
                for (int i = 0; i < movedAssets.Length; i++)
                {
                    // Check if the instance implements the IPostprocessMovedAsset interface
                    if (Instance is IPostprocessMovedAsset postprocessMovedAsset)
                        postprocessMovedAsset._PostprocessMovedAsset(movedAssets[i], movedFromAssetPaths[i]);
                }
            }
        }
    }
}