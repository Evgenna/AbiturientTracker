using Majors;

namespace Abiturients
{
    /// <summary>
    /// Информация об абитуриенте и его приоритетах при поступлении
    /// </summary>
    public class Abiturient
    {
        public string Uid { get; set; } = string.Empty;
        public int Rating { get; set; }
        public bool HasAgreement { get; set; }
        public List<MajorPriority> MajorPriorities { get; set; } = [];
        // Основной высший приоритет 
        public string? CurrentMajor {get;set;}
        // Высший проходной приоритет
        public string? AgreementMajor {get;set;} 

        public Abiturient(){}

        public Abiturient(Abiturient other)
        {
            Uid = other.Uid;
            Rating = other.Rating;
            HasAgreement = other.HasAgreement;
            MajorPriorities = [..other.MajorPriorities];
            CurrentMajor = other.CurrentMajor;
            AgreementMajor = other.AgreementMajor;
        }
    }
}