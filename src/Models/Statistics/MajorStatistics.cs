using Majors;

namespace Statistics
{
    /// <summary>
    /// Статистика по специальностям
    /// </summary>
    /// <param name="Major">Специальность</param>
    /// <param name="AbiturientCount">Количество абитуриентов</param>
    /// <param name="AgreementCount">Количество поданных согласий</param>
    /// <param name="Contest">Конкурс на место</param>
    /// <param name="AgreementPassingScore">Проходной балл с учетом поданных согласий (высший проходной приоритет)</param>
    /// <param name="CurrentPassingScore">Проходной балл, если все абитуриенты подадут согласие (основной проходной приоритет)</param>
    public record MajorStatistics(
        Major Major, 
        int AbiturientCount, 
        int AgreementCount, 
        double Contest, 
        int AgreementPassingScore, 
        int CurrentPassingScore
    );
}