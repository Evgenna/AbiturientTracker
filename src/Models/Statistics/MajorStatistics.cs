using Majors;

namespace Statistics
{
    public record MajorStatistics(
        Major Major, 
        int AbiturientCount, 
        int AgreementCount, 
        double Contest, 
        int AgreementPassingScore, 
        int CurrentPassingScore
    );
}