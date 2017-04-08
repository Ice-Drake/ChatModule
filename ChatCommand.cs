using PluginSDK;

namespace ChatModule
{
    public enum ChatCommandType { Create, Login, Logout, Modify, Remove, Setup }

    public class ChatCommand : ICommand
    {
        public string SourceName { get; protected set; }

        public string Username { get; protected set; }

        public CommandStatus Status { get; set; }

        public ChatCommandType Type { get; private set; }

        public ChatCommand(ChatCommandType type)
        {
            Type = type;
            Status = CommandStatus.CREATED;
        }

        public virtual bool setup(string[] parameters)
        {
            if (parameters.Length == 2 && Type != ChatCommandType.Modify)
            {
                Status = CommandStatus.INITIALED;
                SourceName = parameters[0];
                Username = parameters[1];
                return true;
            }
            else
            {
                MessageLogger.log("This command requires two parameters, but given " + parameters.Length + " parameter(s).", MessageType.DEBUG);
                return false;
            }
        }
    }
}
