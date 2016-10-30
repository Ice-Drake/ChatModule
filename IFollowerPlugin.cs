using PluginSDK;

namespace ChatModule
{
    /// <summary>
    ///  All chatbot plugins must implement this interface.
    /// </summary>
    public interface IFollowerPlugin : IFollower, IDatabase
    {
        
    }
}
