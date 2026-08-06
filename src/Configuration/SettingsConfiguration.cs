using Majors;

namespace Settings
{
    public class SettingsConfiguration
    {
        public string Uid {get;set;} = string.Empty;
        public List<MajorDetails> Majors {get;set;} = [];
    }
}