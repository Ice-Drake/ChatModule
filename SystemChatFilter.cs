using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginSDK;

namespace ChatModule
{
    public class SystemChatFilter : SystemLogFilter
    {
        private UserAccount designated;

        public SystemChatFilter(UserAccount account)
        {
            designated = account;
        }

        public override ChatMessage refine(LogMessage message)
        {
            if (message.Type == MessageType.INFORMATION || message.Type == MessageType.ERROR)
            {
                return new ChatMessage(Bot.source, Bot.account, designated, new TextMessage(message.Message));
            }
            return null;
        }
    }
}
