namespace ChatModule
{
    /// <summary>
    ///  All chat source plugins that allows account registration should implement this interface along with chat source plugin interface.
    ///  Do not implement this interface alone since it won't be properly used.
    /// </summary>
    public interface IChatSourceAccountManager
    {
        /// <summary>
        /// Create an account.
        /// </summary>
        /// <param name="account">Account to be created.</param>
        /// <returns>Returns true if successful.</returns>
        bool createAccount(UserAccount account);

        /// <summary>
        /// Log in the existing account.
        /// </summary>
        /// <param name="account">Account to be log in with.</param>
        /// <returns>Returns true if successful.</returns>
        bool login(UserAccount acount);

        /// <summary>
        /// Log off the existing account.
        /// </summary>
        /// <param name="account">Account to be log off with.</param>
        /// <returns>Returns true if successful.</returns>
        bool logoff(UserAccount account);

        /// <summary>
        /// Modify an existing account.
        /// </summary>
        /// <param name="account">Account to be modified.</param>
        /// <returns>Returns true if successful.</returns>
        bool modifyAccount(UserAccount account);

        /// <summary>
        /// Remove an existing account.
        /// </summary>
        /// <param name="account">Account to be removed.</param>
        /// <returns>Returns true if successful.</returns>
        bool removeAccount(UserAccount account);
    }
}
