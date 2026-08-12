using Abiturients;
using Majors;

namespace University
{
    public record UniversityData(
        Major Major,
        List<AbiturientResponse> Abiturients
    );
}