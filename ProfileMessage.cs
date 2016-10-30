namespace ChatModule
{
    /// <summary>
    /// This class represents a special message contained in ChatMessage used to pass IProfile object among IChatUser.
    /// </summary>
    public class ProfileMessage : TextMessage
    {
        /// <summary>
        /// Property for the profile attached to the text message.
        /// </summary>
        public IProfile Profile { get; private set; }

        /// <summary>
        /// Allocate and initialize a ProfileMessage with the following parameters.
        /// </summary>
        /// <param name="profile">The profile attached to the message.</param>
        /// <param name="message">The text message contained.</param>
        public ProfileMessage(IProfile profile, string message) : base(message)
        {
            Profile = profile;
        }
    }
}
