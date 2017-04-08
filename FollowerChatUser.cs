using System.Collections.Generic;
using System.Drawing;

namespace ChatModule
{
    public class FollowerChatUser : IChatUser
    {
        private SortedList<string, List<UserAccount>> accounts;

        public Color FontColor { get; set; }

        public IProfile Profile { get; set; }

        public FollowerChatUser(string username)
        {
            accounts = new SortedList<string, List<UserAccount>>();
            FontColor = Color.Black;
            Profile = new Profile(username);
        }

        public void add(UserAccount account)
        {
            if (accounts.ContainsKey(account.SourceName))
                accounts[account.SourceName].Add(account);
            else
                accounts.Add(account.SourceName, new List<UserAccount>() { account });
        }

        public IList<UserAccount> retrieve(IChatSource source)
        {
            if (accounts.ContainsKey(source.SourceName))
                return accounts[source.SourceName];
            else
                return null;
        }
    }
}
