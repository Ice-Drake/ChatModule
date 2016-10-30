using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChatModule
{
    /// <summary>
    ///  All chatbot plugins will implement this interface indirectly through IFollowerPlugin.
    ///  Do not implement this interface directly unless IChatSource is being implemented as well.
    /// </summary>
    public interface IFollower : IChatUser
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
        /// Property for the queue of outgoing messages from this chat user.
        /// </summary>
        Queue<ChatMessage> OutgoingMessage { get; }

        /// <summary>
        /// Start up the follower.
        /// </summary>
        /// <remarks>This method is called near the end of MultiDesktop startup process after
        /// everything has been initialized.</remarks>
        void start();

        /// <summary>
        /// Listen to a message send for other chat users.
        /// </summary>
        /// <param name="message">A message to be listened to.</param>
        /// <remarks>This will be called when the conversation is not directed to this chat user while available.
        /// This method can be used to jump into the conversation where one is not involved in.</remarks>
        void listen(ChatMessage message);

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
