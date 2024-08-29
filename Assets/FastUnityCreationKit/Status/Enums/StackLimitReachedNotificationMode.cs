namespace FastUnityCreationKit.Status.Enums
{
    /// <summary>
    /// Represents the mode of notification when the maximum stack count is reached.
    /// <ul>
    /// <li>If <see cref="Once"/> is used, the notification is sent only once when the stack count reaches the maximum limit
    /// even if it is increased further.</li>
    /// <li>
    /// If <see cref="EveryTime"/> is used, the notification is sent
    /// each time the stack count is increased and reaches or keeps the maximum limit.
    /// </li>
    /// </ul>
    /// </summary>
    public enum MaxStackLimitReachedNotificationMode
    {
        Once,
        EveryTime
    }
    
    /// <summary>
    /// Represents the mode of notification when the minimum stack count is reached.
    /// <ul>
    /// <li>If <see cref="Once"/> is used, the notification is sent only once when
    /// the stack count reaches the minimum limit
    /// </li>
    /// <li>
    /// If <see cref="EveryTime"/> is used, the notification is sent
    /// each time the stack count is decreased and reaches or keeps the minimum limit.
    /// </li>
    /// </ul>
    /// </summary>
    public enum MinStackLimitReachedNotificationMode
    {
        Once,
        EveryTime
    }
}