﻿namespace FastUnityCreationKit.Editor.Postprocessing.Interfaces
{
    public interface IPreprocessSavedAsset
    {
        internal void _PreprocessSavedAsset(string assetPath);
        public void PreprocessSavedAsset(string assetPath);
    }
}