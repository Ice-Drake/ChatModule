namespace ChatModule
{
    /// <summary>
    /// This class represents the basic text message contained inside ChatMessage.
    /// </summary>
    public class TextMessage
    {
        /// <summary>
        /// Property for the text message contained.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Allocate and initialize a TextMessage with the following parameter.
        /// </summary>
        /// <param name="message">The text message to be contained.</param>
        public TextMessage(string message)
        {
            Message = message;
        }
    }
}
