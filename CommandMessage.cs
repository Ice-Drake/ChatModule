using PluginSDK;

namespace ChatModule
{
    public class CommandMessage : TextMessage
    {
        public ICommand Command { get; private set; }

        public CommandMessage(ICommand command, string message) : base(message)
        {
            Command = command;
        }
    }
}
