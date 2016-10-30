namespace ChatModule
{
    /// <summary>
    ///  All plugins that implement IChatUser must implement this interface.
    ///  Do not implement this interface otherwise since it won't be properly used.
    /// </summary>
    public interface IProfile
    {
        /// <summary>
        /// Property for name of the associated chat user.
        /// </summary>
        /// <returns>Returns the name.</returns>
        string Name { get; }
    }
}
