using System;
using FastUnityCreationKit.Editor.Postprocessing.Interfaces;
using FastUnityCreationKit.Core.Logging;
using UnityEditor;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Editor.Postprocessing.Abstract
{
    /// <summary>
    /// Helper class for creating custom asset postprocessors.
    /// </summary>
    public abstract class QuickAssetProcessor<TSelfSealed> : QuickAssetProcessorBase<TSelfSealed>,
        IPreprocessDeletedAsset, IPreprocessCreatedAsset, IPreprocessMovedAsset, IPreprocessSavedAsset,
        IPostprocessDeletedAsset, IPostprocessCreatedAsset, IPostprocessMovedAsset
        where TSelfSealed : QuickAssetProcessor<TSelfSealed>, new()
    {
        private Object _currentAsset;
        
        /// <summary>
        /// Current asset path.
        /// </summary>
        protected string CurrentAssetPath { get; private set; }
        
        /// <summary>
        /// GUID of the current asset.
        /// </summary>
        protected string CurrentAssetGUID { get; private set; }
        
        /// <summary>
        /// Type of the current asset.
        /// </summary>
        protected Type CurrentAssetType { get; private set; }

        /// <summary>
        /// If true asset will be preloaded during the preprocessing.
        /// </summary>
        protected abstract bool AssetIsRequired { get; }
        
        /// <summary>
        /// Gets whether the current asset is available (checks if asset type is null).
        /// </summary>
        protected bool IsAssetAvailable => CurrentAssetType != null;

        protected Object CurrentAsset
        {
            set => _currentAsset = value;
            get
            {
                if (!IsAssetAvailable)
                {
                    Guard<ValidationLogConfig>.Error("Asset is not available.");
                    return null;
                }

                if (!AssetIsRequired)
                {
                    Guard<ValidationLogConfig>.Error("Asset is available. Override 'AssetIsRequired' as true to load the asset.");
                    return null;
                }
                
                return _currentAsset;
            }
        }

        AssetDeleteResult IPreprocessDeletedAsset._PreprocessDeletedAsset(string assetPath, RemoveAssetOptions options)
        {
            AssignAssetDataFromPath(assetPath);
            return PreprocessDeletedAsset(assetPath, options);
        }

        public virtual AssetDeleteResult PreprocessDeletedAsset(string assetPath, RemoveAssetOptions options) => AssetDeleteResult.DidNotDelete;

        void IPreprocessCreatedAsset._PreprocessCreatedAsset(string assetPath)
        {
            AssignAssetDataFromPath(assetPath);
            PreprocessCreatedAsset(assetPath);
        }

        public virtual void PreprocessCreatedAsset(string assetPath)
        {
            
        }

        AssetMoveResult IPreprocessMovedAsset._PreprocessMovedAsset(string fromPath, string toPath)
        {
            AssignAssetDataFromPath(fromPath);
            return PreprocessMovedAsset(fromPath, toPath);
        }

        public virtual AssetMoveResult PreprocessMovedAsset(string fromPath, string toPath) => AssetMoveResult.DidNotMove;

        void IPreprocessSavedAsset._PreprocessSavedAsset(string assetPath)
        {
            AssignAssetDataFromPath(assetPath);
            PreprocessSavedAsset(assetPath);
        }

        public virtual void PreprocessSavedAsset(string assetPath)
        {
            
        }

        void IPostprocessDeletedAsset._PostprocessDeletedAsset(string assetPath)
        {
            AssignAssetDataFromPath(assetPath);
            PostprocessDeletedAsset(assetPath);
        }

        public virtual void PostprocessDeletedAsset(string assetPath)
        {
            
        }

        void IPostprocessCreatedAsset._PostprocessCreatedAsset(string assetPath)
        {
            AssignAssetDataFromPath(assetPath);
            PostprocessCreatedAsset(assetPath);
        }

        public virtual void PostprocessCreatedAsset(string assetPath)
        {
            
        }

        void IPostprocessMovedAsset._PostprocessMovedAsset(string fromPath, string toPath)
        {
            AssignAssetDataFromPath(toPath);
            PostprocessMovedAsset(fromPath, toPath);
        }

        public virtual void PostprocessMovedAsset(string fromPath, string toPath)
        {
            
        }

        private void AssignAssetDataFromPath(string assetPath)
        {
            CurrentAssetPath = assetPath;
            CurrentAssetGUID = AssetDatabase.AssetPathToGUID(assetPath);
            CurrentAssetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            
            // Load the asset if it is required
            if (AssetIsRequired)
                CurrentAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
        }

        public virtual void BeforeCompilationStarted()
        {
            
        }

        public virtual void AfterCompilationFinished()
        {
            
        }
    }
}