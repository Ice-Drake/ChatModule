using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginSDK;

namespace ChatModule
{
    public abstract class SystemLogFilter
    {
        private static List<SystemLogFilter> filters;

        protected internal static SystemBot Bot { get; internal set; }

        public SystemLogFilter()
        {
            if (filters == null)
                filters = new List<SystemLogFilter>();
            filters.Add(this);
        }

        public static List<ChatMessage> filter(LogMessage message)
        {
            List<ChatMessage> messages = new List<ChatMessage>();
            foreach(SystemLogFilter filter in filters)
            {
                ChatMessage newMessage = filter.refine(message);
                if (newMessage != null)
                    messages.Add(newMessage);
            }
            return messages;
        }

        public abstract ChatMessage refine(LogMessage message);
    }
}
