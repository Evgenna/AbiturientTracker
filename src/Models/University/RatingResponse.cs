using Abiturients;
using Majors;
using Statistics;

namespace University
{
    public record RatingResponse(List<Major> Majors, List<Abiturient> Abiturients, StatisticsResponse Statistics);
}