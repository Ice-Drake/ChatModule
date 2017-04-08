using System;
using System.Collections.Generic;
using System.Drawing;
using PluginSDK;

namespace ChatModule
{
    public class SystemBot : MessageLogger, IChatBot
    {
        private ChatHandler chatHandler;
        internal IChatSource source;
        internal UserAccount account;

        public Color FontColor
        {
            get
            {
                return Color.Gray;
            }
        }

        public Queue<ChatMessage> OutgoingMessage { get; private set; }

        public IProfile Profile { get; private set; }

        public SystemBot(ChatManager chatManager)
        {
            chatHandler = new ChatHandler(chatManager);
            
            OutgoingMessage = new Queue<ChatMessage>();
            Profile = new Profile("System");
        }

        public override void write(LogMessage message)
        {
            List<ChatMessage> messages = SystemLogFilter.filter(message);
            lock (OutgoingMessage)
            {
                foreach (ChatMessage m in messages)
                {
                    OutgoingMessage.Enqueue(m);
                }
            }
        }

        public void add(UserAccount account)
        {
            this.account = account;
            account.Status = ChatStatus.Available;
        }

        public IList<UserAccount> retrieve(IChatSource source)
        {
            if (account == null || !account.SourceName.Equals(source.SourceName))
                return null;
            else
                return new List<UserAccount>() { account };
        }

        public void join(IChatSource source)
        {
            this.source = source;
        }

        public void receive(ChatMessage message)
        {
            if (message.Content.Message.ToLower().IndexOf("what time is it") >= 0)
            {
                lock (OutgoingMessage)
                {
                    OutgoingMessage.Enqueue(new ChatMessage(message.Source, message.Recipient, message.Sender, new TextMessage("It is now " + DateTime.Now.ToString("h:mm tt") + ".")));
                }
            }
            else if (message.Content.Message.ToLower().IndexOf("what date is today") >= 0)
            {
                lock (OutgoingMessage)
                {
                    OutgoingMessage.Enqueue(new ChatMessage(message.Source, message.Recipient, message.Sender, new TextMessage("Today is " + DateTime.Today.ToString("dddd, MMMM dd, yyyy") + ".")));
                }
            }
            else if (message.Content is CommandMessage)
            {
                ICommand receivedCommand = ((CommandMessage)message.Content).Command;
                if (receivedCommand is ChatCommand)
                {
                    if (!chatHandler.execute(receivedCommand))
                    {
                        lock (OutgoingMessage)
                        {
                            OutgoingMessage.Enqueue(new ChatMessage(message.Source, message.Recipient, message.Sender, new TextMessage("The received command can not be successfully executed.")));
                        }
                    }
                }
                else
                {
                    if (!CommandHandler.enqueue(receivedCommand))
                    {
                        lock (OutgoingMessage)
                        {
                            OutgoingMessage.Enqueue(new ChatMessage(message.Source, message.Recipient, message.Sender, new TextMessage("The received command is not supported.")));
                        }
                    }
                }
            }
        }
    }
}
