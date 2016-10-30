namespace ChatModule
{
    public class ChatSourceMessage
    {
        /// <summary>
        /// Property for the chat source this message is send to.
        /// </summary>
        public IChatSource Source { get; private set; }
        
        /// <summary>
        /// Property for the name of the sender.
        /// </summary>
        public string Sender { get; private set; }

        /// <summary>
        /// Property for the message.
        /// </summary>
        public string Message { get; private set; }

        public ChatSourceMessage(IChatSource source, string sender, string message)
        {
            Source = source;
            Sender = sender;
            Message = message;
        }
    }
}
