using System.Collections.Generic;

namespace ChatModule
{
    /// <summary>
    ///  All chatbot plugins will implement this interface indirectly through IFollowerPlugin.
    ///  Do not implement this interface directly unless IChatSource is being implemented as well.
    /// </summary>
    public interface IChatBot : IChatUser
    {
        /// <summary>
        /// Property for the queue of outgoing messages from this chat user.
        /// </summary>
        Queue<ChatMessage> OutgoingMessage { get; }

        /// <summary>
        /// Receive a message send for this chat user.
        /// </summary>
        /// <param name="message">A message to be received.</param>
        /// <remarks>This will be called when the conversation is directed to this chat user.</remarks>
        void receive(ChatMessage message);

        /// <summary>
        /// Join a chat source.
        /// </summary>
        /// <param name="source">The chat source of whom is joining in.</param>
        /// <remarks>This chat user must join the designated chat source before a conversation can be initiated in that source.</remarks>
        void join(IChatSource source);
    }
}