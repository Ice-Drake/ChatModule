using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using PluginSDK;

namespace ChatModule
{
    public class FollowerChatUser : IChatUser
    {
        private SortedList<string, UserAccount> accounts;

        public Color FontColor { get; set; }

        public IProfile Profile { get; set; }

        public FollowerChatUser(string username)
        {
            accounts = new SortedList<string, UserAccount>();
            FontColor = Color.Black;
            Profile = new Profile(username);
        }

        public void add(UserAccount account)
        {
            accounts.Add(account.SourceName, account);
        }

        public IList<UserAccount> retrieve(IChatSource source)
        {
            //Currently, handling one account per chat source.
            List<UserAccount> list = new List<UserAccount>();
            list.Add(accounts[source.SourceName]);
            return list;
        }
    }
}
