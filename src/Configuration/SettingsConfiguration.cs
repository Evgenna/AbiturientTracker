using Majors;

namespace Settings
{
    public class SettingsConfiguration
    {
        public string Uid {get;set;} = string.Empty;
        public List<Major> Majors {get;set;} = [];
    }
}