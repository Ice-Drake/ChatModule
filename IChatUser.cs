using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChatModule
{
    /// <summary>
    ///  All chatbot plugins will implement this interface indirectly through IFollowerPlugin.
    ///  Do not implement this interface directly unless IChatSource is being implemented as well.
    /// </summary>
    public interface IChatUser
    {
        /// <summary>
        /// Property for the font color used for messages send by this chat user.
        /// </summary>
        Color FontColor { get; }

        /// <summary>
        /// Property for the profile of this chat user.
        /// </summary>
        IProfile Profile { get; }

        /// <summary>
        /// Add an user account to this chat user.
        /// </summary>
        /// <param name="account">User account to be added</param>
        void add(UserAccount account);

        /// <summary>
        /// Retrieve the user account associated with the designated chat source.
        /// </summary>
        /// <param name="source">Chat source to search by</param>
        /// <returns>A list of user accounts associated</returns>
        /// <remarks>This will return null if there is no user account of the matching chat source.</remarks>
        IList<UserAccount> retrieve(IChatSource source);
    }
}
