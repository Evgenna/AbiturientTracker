using Majors;

namespace Abiturients
{
    public class AbiturientResponse
    {
        public string Uid { get; set; } = string.Empty;
        public int Rating { get; set; }
        public bool HasAgreement { get; set; }
        public List<MajorPriority> MajorPriorities { get; set; } = [];
    }
}