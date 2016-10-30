using System;
using System.Collections.Generic;
using System.Threading;
using PluginSDK;

namespace ChatModule
{
    public class ChatManager
    {
        public IList<IChatUser> ChatUsers { get; private set; }

        public IChatUser MainUser { get; private set; }

        public Queue<ChatSourceMessage> NewMessages { get; private set; }

        private SortedList<string, IChatSource> chatSources;

        public ChatManager()
        {
            ChatUsers = new List<IChatUser>();
            NewMessages = new Queue<ChatSourceMessage>();
            chatSources = new SortedList<string, IChatSource>();
        }

        public void addUser(IChatUser newUser)
        {
            ChatUsers.Add(newUser);
        }

        public bool addSource(IChatSource newSource)
        {
            if (!chatSources.ContainsKey(newSource.SourceName))
            {
                chatSources.Add(newSource.SourceName.ToUpper(), newSource);
                newSource.IncomingMessage += new IncomingMessageEventHandler(newSource_IncomingMessage);

                if (MainUser != null)
                {
                    //Assume that either there is only either none or at least one user account
                    if (MainUser.retrieve(newSource) == null)
                    {
                        UserAccount masterAccount;
                        if (MainUser.Profile != null)
                            masterAccount = newSource.createUser(MainUser.Profile.Name);
                        else
                            masterAccount = newSource.createUser("Boss");
                    }
                    else
                        newSource.setup(MainUser.retrieve(newSource)[0]);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public IChatSource getSource(string sourceName)
        {
            try
            {
                return chatSources[sourceName];
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public void send(ChatSourceMessage message)
        {
            IList<UserAccount> accounts = MainUser.retrieve(message.Source);
            if (accounts != null && accounts.Count > 0)
            {
                message.Source.send(message.Message, accounts[0]);
            }
        }

        public void changeMainUser(IChatUser user)
        {
            if (MainUser != user)
            {
                MainUser = user;
                foreach (IChatSource source in chatSources.Values)
                {
                    IList<UserAccount> accounts = MainUser.retrieve(source);
                    
                    //Check if there is an existing user account
                    if (accounts != null && accounts.Count > 0)
                    {
                        source.setup(accounts[0]);
                    }
                    else
                    {
                        //Create a new user account
                        UserAccount newAccount;

                        if (MainUser.Profile != null)
                        {
                            //Create a new user with its name as username
                            newAccount = source.createUser(MainUser.Profile.Name);
                        }
                        else
                        {
                            //Create a new user with random username
                            newAccount = source.createUser("User");
                        }
                        newAccount.Owner = MainUser;
                        source.setup(newAccount);
                    }
                }
            }
        }

        private void newSource_IncomingMessage(ChatSourceMessage message)
        {
            if (!message.Source.Muted)
            {
                lock (NewMessages)
                {
                    NewMessages.Enqueue(message);
                }
            }
        }
    }
}
