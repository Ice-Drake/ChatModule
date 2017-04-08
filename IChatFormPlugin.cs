using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ChatModule
{
    public abstract class IChatFormPlugin : UserControl, IChatSource
    {
        private SourceStatus status;
        private Color textColor;

        public IChatFormPlugin()
        {
            Size = new Size(400, 313);
            Muted = false;
        }

        /// <summary>
        /// Property for the status on this chat source.
        /// </summary>
        /// <remarks>This will notify user of its current status.</remarks>
        public SourceStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                if (StatusChanged != null)
                    StatusChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

        /// <summary>
        /// An event that must be triggered whenever Status is changed.
        /// </summary>
        public event PropertyChangedEventHandler StatusChanged;

        /// <summary>
        /// Connect to this chat source.
        /// </summary>
        /// <returns>True if successful.</returns>
        /// <remarks>Only if there is a valid call to setup method will this method be executed.</remarks>
        public abstract bool connect();

        /// <summary>
        /// Disconnect from this chat source.
        /// </summary>
        /// <returns>True if successful.</returns>
        /// <remarks>Only if there is a valid call to setup method will this method be executed.</remarks>
        public abstract bool disconnect();

        #region Method from the IChatSource interface

        /// <summary>
        /// Property for chat source name.
        /// </summary>
        /// <remarks>This name will be used as the name of tab item generated.</remarks>
        public abstract string SourceName { get; }

        /// <summary>
        /// Property for the text color used for contents from this chat source in ALL tab.
        /// </summary>
        /// <remarks>This text color will be used for contents from this chat source in ALL tab.</remarks>
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                if (TextColorChanged != null)
                    TextColorChanged(this, new PropertyChangedEventArgs("TextColor"));
            }
        }

        /// <summary>
        /// Property for whether the contents are muted from this chat source from the ALL tab.
        /// </summary>
        /// <remarks>This mute option will be determined whether contents from this chat source will be updated in ALL tab.</remarks>
        public bool Muted { get; set; }

        /// <summary>
        /// Property for the master user account.
        /// </summary>
        /// <remarks>This user account cannot be modified except through the setup method.</remarks>
        public UserAccount MasterAccount { get; private set; }

        /// <summary>
        /// Property for the list of user accounts in this chat source.
        /// </summary>
        public abstract IList<UserAccount> Accounts { get; }

        /// <summary>
        /// An event that is triggered when there is a new incoming message from this chat source.
        /// </summary>
        public abstract event IncomingMessageEventHandler IncomingMessage;

        /// <summary>
        /// An event that must be triggered whenever TextColor is changed.
        /// </summary>
        public event PropertyChangedEventHandler TextColorChanged;

        /// <summary>
        /// Add the new user to this chat source.
        /// </summary>
        /// <param name="account">User account to be added.</param>
        /// <remarks>This will be called whenever a new chat user joins this chat source.</remarks>
        public abstract void addUser(UserAccount account);

        /// <summary>
        /// Create the user account with the specified username.
        /// </summary>
        /// <param name="username">Username to use.</param>
        /// <returns>New user account.</returns>
        /// <remarks>Note that this method will not add the new user account to this chat source automatically.</remarks>
        public abstract UserAccount createUser(string username);

        /// <summary>
        /// Find the user account with the specified username.
        /// </summary>
        /// <param name="username">Username to search by.</param>
        /// <returns>Matching user account</returns>
        /// <remarks>This will return null if no matching username is found.</remarks>
        public abstract UserAccount findUser(string username);

        /// <summary>
        /// Send the message to this chat source.
        /// </summary>
        /// <param name="message">Message to be send.</param>
        /// <param name="sender">User account of the sender.</param>
        public abstract void send(string message, UserAccount sender);

        /// <summary>
        /// Setup the account to be master account for this chat source.
        /// </summary>
        /// <param name="account">Account to be used as master in this chat source.</param>
        /// <returns>True if it belongs to this chat source.</returns>
        /// <remarks>This will modify the property value of MasterAcount.</remarks>
        public virtual bool setup(UserAccount account)
        {
            if (!SourceName.Equals(account.SourceName) || !account.canLog())
                return false;
            else
            {
                MasterAccount = account;
                return true;
            }
        }

        #endregion
    }
}
