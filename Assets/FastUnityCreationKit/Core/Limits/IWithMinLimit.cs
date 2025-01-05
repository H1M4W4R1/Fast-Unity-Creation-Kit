namespace FastUnityCreationKit.Core.Limits
{
    /// <summary>
    ///     Represents a minimum limit for a number.
    ///     Used on an object with a numeric context - for example a status effect that reduces health by a certain amount.
    /// </summary>
    /// <remarks>
    ///     If TNumber is not numeric type a lot of f-up will happen.
    /// </remarks>
    public interface IWithMinLimit : ILimited
    {
        public double MinLimit { get; }
    }
}