namespace FastUnityCreationKit.Context.Interface
{
    /// <summary>
    /// Represents a context of information.
    /// Can be used to store information that is needed in multiple places.
    /// </summary>
    /// <remarks>
    /// Context should be a structure that contains pointers or values.
    /// <br/>
    /// If Unity team will finally update this dinosaur-old version of C# to a newer one, it will be possible to use
    /// ref structs for this purpose as C#13 allows to use interfaces for ref structs.<br/><br/>
    /// Context should not contain any logic except for the logic that is needed to manage the context.
    /// It should be implemented with a builder pattern.
    /// </remarks>
    public interface IContext
    {
        
    }
}