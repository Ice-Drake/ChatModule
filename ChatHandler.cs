using PluginSDK;

namespace ChatModule
{
    public class ChatHandler : CommandHandler
    {
        public ChatManager ChatManager { get; private set; }

        public ChatHandler(ChatManager chatManager) : base("Chat")
        {
            ChatManager = chatManager;
        }

        public override ICommand create(string command)
        {
            if (command.Equals("CreateAccount"))
                return new ChatCommand(ChatCommandType.Create);
            else if (command.Equals("Login"))
                return new ChatCommand(ChatCommandType.Login);
            else if (command.Equals("Logoff"))
                return new ChatCommand(ChatCommandType.Logout);
            else if (command.Equals("ModifyAccount"))
                return new ChatModifyCommand();
            else if (command.Equals("RemoveAccount"))
                return new ChatCommand(ChatCommandType.Remove);
            else
                return null;
        }

        public override bool execute(ICommand command)
        {
            if (command is ChatCommand)
            {
                ChatCommandType type = ((ChatCommand)command).Type;
                IChatSource source = ChatManager.getSource(((ChatCommand)command).SourceName);
                string username = ((ChatCommand)command).Username;

                if (type == ChatCommandType.Create)
                {
                    if (source.findUser(username) != null)
                    {
                        MessageLogger.log("Cannot create account. Username \"" + username + "\" already existed.");
                        command.Status = CommandStatus.FAILED;
                        return true;
                    }
                    UserAccount account = source.createUser(username);
                    source.addUser(account);
                }                
                else //if command is either log in, log off, or remove account
                {
                    UserAccount account = source.findUser(username);
                    if (account == null)
                    {
                        MessageLogger.log("No account found with username \"" + username + "\".");
                        command.Status = CommandStatus.FAILED;
                        return true;
                    }

                    if (type == ChatCommandType.Setup)
                    {
                        source.setup(account);
                    }
                    else if (source is IChatSourceAccountManager)
                    {
                        if (command is ChatModifyCommand)
                        {
                            account.Password = ((ChatModifyCommand)command).Password;
                            ((IChatSourceAccountManager)source).modifyAccount(account);
                        }
                        else if (type == ChatCommandType.Login)
                            ((IChatSourceAccountManager)source).login(account);
                        else if (type == ChatCommandType.Logout)
                            ((IChatSourceAccountManager)source).logoff(account);
                        else if (type == ChatCommandType.Remove)
                            ((IChatSourceAccountManager)source).removeAccount(account);
                        else
                        {
                            MessageLogger.log("Chat command type is not supported.");
                            command.Status = CommandStatus.FAILED;
                            return true;
                        }
                    }
                    else
                    {
                        MessageLogger.log("Account modification is unavailable for this source.", MessageType.WARNING);
                        command.Status = CommandStatus.FAILED;
                        return true;
                    }
                }

                //Command Successfully Executed
                command.Status = CommandStatus.COMPLETED;
                return true;
            }
            else
                return false;
        }
    }
}
