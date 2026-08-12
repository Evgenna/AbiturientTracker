namespace Statistics
{
    /// <summary>
    /// Статистика по конкурсу
    /// </summary>
    /// <param name="TotalCount">Количество поданных заявлений</param>
    /// <param name="AgreementCount">Количество поданных согласий на зачисление</param>
    /// <param name="MyStatistic">Статистика пользователя программы</param>
    /// <param name="MajorStatistics">Статистика по специальностям</param>
    public record StatisticsResponse(int TotalCount, int AgreementCount, MyStatistics MyStatistic, List<MajorStatistics> MajorStatistics);
}