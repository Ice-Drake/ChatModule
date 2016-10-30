using PluginSDK;

namespace ChatModule
{
    public class Profile : IProfile
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public Profile(string name)
        {
            Name = name;
        }
    }
}
