using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Context.Providers.Utility;
using JetBrains.Annotations;
using Sirenix.Utilities;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    ///     Represents a UI object that can be rendered.
    /// </summary>
    /// <typeparam name="TDataContextSealed">Context used to render this object.</typeparam>
    public interface IRenderable<TDataContextSealed> : IRenderable
    {
        /// <summary>
        ///     Gets the data context for this renderer.
        /// </summary>
        public DataContextInfo<TDataContextSealed> DataContext
        {
            get
            {
                // Check if this object is UIObject, if so, get data context
                if (this is UIObjectWithContextBase<TDataContextSealed> uiObject) return uiObject.GetDataContext();

                // Log error if this object is not UIObject
                Guard<UserInterfaceLogConfig>.Error("IRenderable is not supported on " +
                                                    $"{GetType().GetCompilableNiceFullName()}.");
                return default;
            }
        }

        void IRenderable.Render(bool forceRender)
        {
            // Render this object if data context for this object is valid
            if (DataContext.IsValid)
            {
                // If data context is not dirty and rendering is not enforced, do not render
                if (!forceRender && !DataContext.IsDirty) return;

                // If data context is null, do not render
                if (ReferenceEquals(DataContext.Context, null)) return;

                // Run internal rendering
                Render(DataContext.Context);
                Guard<UserInterfaceLogConfig>.Verbose(
                    $"Rendered {GetType().GetCompilableNiceFullName()} [was enforced: {forceRender}].");
            }
            else
            {
                Guard<UserInterfaceLogConfig>.Error(
                    $"Data context is not valid for {GetType().GetCompilableNiceFullName()}.");
            }
        }

        /// <summary>
        ///     Render this object
        /// </summary>
        public void Render([NotNull] TDataContextSealed dataContext);
    }

    /// <summary>
    ///     Internal interface for rendering objects. Do not implement this interface directly.
    ///     See <see cref="IRenderable{TDataContextSealed}" /> instead.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        ///     This method can be used to render the object. It checks
        ///     if context has changed. If it did not, it will not render.
        /// </summary>
        internal void Render(bool forceRender = false);
    }
}