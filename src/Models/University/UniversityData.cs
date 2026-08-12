using Abiturients;
using Majors;

namespace University
{
    /// <summary>
    /// Данные, полученные от университета
    /// </summary>
    /// <param name="Major">Специальность и инфомация о ней</param>
    /// <param name="Abiturients">Абитуриенты, участвующие в конкурсном списке</param>
    public record UniversityData(
        Major Major,
        List<AbiturientResponse> Abiturients
    );
}