namespace FastUnityCreationKit.Core.Initialization
{
    public interface IInitializable
    {
        /// <summary>
        /// Internal field that stores the initialization state.
        /// Used to determine if the object is initialized.
        /// To get this state please use <see cref="IsInitialized"/>
        /// </summary>
        public bool InternalInitializationStatusStorage { get; protected set; }

        /// <summary>
        /// Returns true if the object is initialized.
        /// </summary>
        public bool IsInitialized
        {
            get => InternalInitializationStatusStorage;
            protected set => InternalInitializationStatusStorage = value;
        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public void Initialize() => EnsureInitialized();
        
        /// <summary>
        /// Internal initialization method.
        /// </summary>
        protected void _Initialize();
        
        /// <summary>
        /// Ensures that the object is initialized.
        /// If the object is not initialized, it will be initialized.
        /// </summary>
        public void EnsureInitialized()
        {
            if(IsInitialized) return;
            
            _Initialize();
            IsInitialized = true;
        }
    }
}