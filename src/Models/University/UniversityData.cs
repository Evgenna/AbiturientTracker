using Abiturients;
using Majors;

namespace University
{
    public record UniversityData(
        MajorDetails Major,
        List<AbiturientResponse> Abiturients
    );
}