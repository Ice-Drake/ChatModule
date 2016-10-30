using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ChatModule
{
    public delegate void IncomingMessageEventHandler(ChatSourceMessage message);

    public enum SourceStatus { Online, Offline };

    /// <summary>
    ///  All chat source plugins will implement this interface indirectly.
    ///  Do not implement this interface directly since it won't be properly used.
    /// </summary>
    public interface IChatSource
    {
        /// <summary>
        /// Property for chat source name.
        /// </summary>
        /// <remarks>This name will be used as the name of tab item generated.</remarks>
        string SourceName { get; }

        /// <summary>
        /// Property for the text color used for contents from this chat source in ALL tab.
        /// </summary>
        /// <remarks>This text color will be used for contents from this chat source in ALL tab.</remarks>
        Color TextColor { get; }

        /// <summary>
        /// Property for whether the contents are muted from this chat source from the ALL tab.
        /// </summary>
        /// <remarks>This mute option will be determined whether contents from this chat source will be updated in ALL tab.</remarks>
        bool Muted { get; }

        /// <summary>
        /// Property for the master user account.
        /// </summary>
        /// <remarks>This user account cannot be modified except through the setup method.</remarks>
        UserAccount MasterAccount { get; }

        /// <summary>
        /// Property for the list of user accounts in this chat source.
        /// </summary>
        IList<UserAccount> Accounts { get; }

        /// <summary>
        /// An event that is triggered when there is a new incoming message from this chat source.
        /// </summary>
        event IncomingMessageEventHandler IncomingMessage;

        /// <summary>
        /// An event that must be triggered whenever TextColor is changed.
        /// </summary>
        event PropertyChangedEventHandler TextColorChanged;

        /// <summary>
        /// Add the new user to this chat source.
        /// </summary>
        /// <param name="account">User account to be added.</param>
        /// <remarks>This will be called whenever a new chat user joins this chat source.</remarks>
        void addUser(UserAccount account);

        /// <summary>
        /// Create the user account with the specified username.
        /// </summary>
        /// <param name="username">Username to use.</param>
        /// <returns>New user account.</returns>
        /// <remarks>Note that this method will not add the new user account to this chat source automatically.</remarks>
        UserAccount createUser(string username);

        /// <summary>
        /// Find the user account with the specified username.
        /// </summary>
        /// <param name="username">Username to search by.</param>
        /// <returns>Matching user account</returns>
        /// <remarks>This will return null if no matching username is found.</remarks>
        UserAccount findUser(string username);

        /// <summary>
        /// Send the message to this chat source.
        /// </summary>
        /// <param name="message">Message to be send.</param>
        /// <param name="sender">User account of the sender.</param>
        void send(string message, UserAccount sender);

        /// <summary>
        /// Setup the account to be master account for this chat source.
        /// </summary>
        /// <param name="account">Account to be used as master in this chat source.</param>
        /// <returns>True if successful.</returns>
        /// <remarks>This will modify the property value of MasterAcount.</remarks>
        bool setup(UserAccount account);
    }
}
