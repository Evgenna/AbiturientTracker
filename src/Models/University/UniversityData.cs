using Abiturients;
using Majors;

namespace University
{
    public record UniversityData(
        List<MajorDetails> Majors,
        List<Abiturient> Abiturients
    );
}