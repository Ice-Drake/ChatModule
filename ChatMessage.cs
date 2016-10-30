namespace ChatModule
{
    /// <summary>
    /// This class represents the chat message used among IChatUser.
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// Property for the chat source this chat message belongs to.
        /// </summary>
        public IChatSource Source { get; private set; }

        /// <summary>
        /// Property for the user account this chat message was send by.
        /// </summary>
        public UserAccount Sender { get; private set; }

        /// <summary>
        /// Property for the user account this chat message is send to.
        /// </summary>
        /// <remarks>This can return null to indicate the chat message is directed to all available chat users.</remarks>
        public UserAccount Recipient { get; private set; }

        /// <summary>
        /// Property for the text message inside this chat message.
        /// </summary>
        /// <remarks>It is highly recommended to handle all various contents of chat messages apart from just TextMessage. In other
        /// words, it is best to check the content for all derived classes of TextMessage such as ChatSourceMessage or ProfileMessage.</remarks>
        public TextMessage Content { get; private set; }

        /// <summary>
        /// Property for whether the text message is hidden from view.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Allocate and initialize a ChatMessage with the following parameters.
        /// </summary>
        /// <param name="source">Chat source that this chat message belongs to.</param>
        /// <param name="sender">User account that this chat message originates from.</param>
        /// <param name="recipient">User account that this chat message is send to.</param>
        /// <param name="content">Content of this chat message.</param>
        public ChatMessage(IChatSource source, UserAccount sender, UserAccount recipient, TextMessage content)
        {
            Source = source;
            Sender = sender;
            Recipient = recipient;
            Content = content;
            Hidden = false;
        }
    }
}
