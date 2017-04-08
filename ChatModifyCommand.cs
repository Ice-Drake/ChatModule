using PluginSDK;

namespace ChatModule
{
    public class ChatModifyCommand : ChatCommand
    {
        public string Password { get; set; }

        public ChatModifyCommand() : base(ChatCommandType.Modify)
        {

        }

        public override bool setup(string[] parameters)
        {
            if (parameters.Length == 3)
            {
                Status = CommandStatus.INITIALED;
                SourceName = parameters[0];
                Username = parameters[1];
                Password = parameters[2];
                return true;
            }
            else
            {
                MessageLogger.log("ModifyAccount command requires three parameters, but given " + parameters.Length + " parameter(s).", MessageType.DEBUG);
                return false;
            }
        }
    }
}
