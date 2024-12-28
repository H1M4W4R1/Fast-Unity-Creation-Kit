namespace FastUnityCreationKit.Status.Interfaces
{
    /// <summary>
    /// Marker interface for status that has percentage, and thus
    /// it's level is scaled up by <see cref="IPercentageStatus.PERCENTAGE_SCALE"/>
    /// </summary>
    public interface IPercentageStatus
    {
        public const long PERCENTAGE_SCALE = 100_00;
        
    }
}