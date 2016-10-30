using System.ComponentModel;

namespace ChatModule
{
    public enum ChatStatus { Available, Busy, Idle, Offline };

    /// <summary>
    /// This class represents the user account associated to a particular IChatSource.
    /// </summary>
    public class UserAccount
    {
        private ChatStatus status;

        /// <summary>
        /// Property for the username.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Property for the chat status for this account.
        /// </summary>
        /// <remarks>The default value of the chat status is Offline.</remarks>
        public ChatStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    if (StatusChanged != null)
                        StatusChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        /// <summary>
        /// Property for the name of the associated chat source.
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        /// Property for the server address.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Property for the domain address associated to the username.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Property for the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Property for the owner of this user account.
        /// </summary>
        /// <remarks>This can be null when the owner of this user account is yet to be identified.</remarks>
        public IChatUser Owner { get; set; }

        /// <summary>
        /// An event that is triggered when Status property is changed.
        /// </summary>
        public event PropertyChangedEventHandler StatusChanged;

        public UserAccount(string sourceName, string username)
        {
            SourceName = sourceName;
            Username = username;
            status = ChatStatus.Offline;
        }

        /// <summary>
        /// Check if the account can be used to log in.
        /// </summary>
        /// <returns>Returns true if all properties are set to nonempty value.</returns>
        public bool canLog()
        {
            return !Username.Equals("") && Password != null && !Password.Equals("")
                && Server != null && !Server.Equals("") && Domain != null && !Domain.Equals("");
        }
    }
}
