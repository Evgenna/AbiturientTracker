namespace Statistics
{
    /// <summary>
    /// Статистика по пользователю программы
    /// </summary>
    /// <param name="MyUid">Номер абитуриента пользователя</param>
    /// <param name="CurrentPlace">Текущее место в конкурсе</param>
    /// <param name="CurrentMajor">Специальность по основному высшему приоритету</param>
    /// <param name="AgreementMajor">Специальность по высшему проходному приоритету</param>
    /// <param name="WithoutAgreement">Сколько абитуриентов перед пользователем</param>
    /// <param name="WithAgreement">Сколько абитуриентов перед пользователем подали согласия</param>
    public record MyStatistics(string MyUid, int CurrentPlace, string? CurrentMajor, string? AgreementMajor, int WithoutAgreement, int WithAgreement);
}