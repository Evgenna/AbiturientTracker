namespace Statistics
{
    public record StatisticsResponse(int TotalCount, int AgreementCount, MyStatistics MyStatistic, List<MajorStatistics> MajorStatistics);
}