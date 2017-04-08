namespace ChatModule
{
    /// <summary>
    ///  All chatbot plugins will implement this interface indirectly through IFollowerPlugin.
    ///  Do not implement this interface directly unless IChatSource is being implemented as well.
    /// </summary>
    public interface IFollower : IChatBot
    {
        /// <summary>
        /// Property for the nickname for this follower.
        /// </summary>
        /// <returns>Returns the nickname.</returns>
        /// <remarks>This nickname can be used as the reference to this follower in a conversation
        /// apart from the name in the profile property from IChatUser interface.</remarks>
        string Nickname { get; }

        /// <summary>
        /// Property for the role served by this follower.
        /// </summary>
        /// <returns>Returns the role.</returns>
        /// <remarks>This role can be used as the reference to this follower in a conversation
        /// apart from the name in the profile property from IChatUser interface.</remarks>
        string Role { get; }

        /// <summary>
        /// Listen to a message send for other chat users.
        /// </summary>
        /// <param name="message">A message to be listened to.</param>
        /// <remarks>This will be called when the conversation is not directed to this chat user while available.
        /// This method can be used to jump into the conversation where one is not involved in.</remarks>
        void listen(ChatMessage message);

        /// <summary>
        /// Start up the follower.
        /// </summary>
        /// <remarks>This method is called near the end of MultiDesktop startup process after
        /// everything has been initialized.</remarks>
        void start();
    }
}
