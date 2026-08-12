using Abiturients;
using Majors;
using Settings;
using University;

namespace Statistics
{
    public class StatisticsService()
    {
        public MyStatistics GetMyStatistics(string myId, List<Abiturient> abiturients)
        {
            int place = abiturients.FindIndex(a => a.Uid == myId);
            string? currentMajor = abiturients[place].CurrentMajor;
            string? agreementMajor = abiturients[place].AgreementMajor;
            var abiturientsForward = abiturients.Take(place).ToList();
            int withAgreement = abiturientsForward.Count(a => a.HasAgreement);
            int withoutAgreement = abiturientsForward.Count(a => !a.HasAgreement);

            return new MyStatistics(
                place + 1,
                currentMajor,
                agreementMajor,
                withAgreement,
                withoutAgreement
            );
        }

        public List<MajorStatistics> GetMajorStatistics(List<UniversityData> universityData, List<Abiturient> abiturientList)
        {
            List<MajorStatistics> majorStatistics = [];

            foreach (var data in universityData)
            {
                Major major = data.Major;
                List<AbiturientResponse> abiturients = data.Abiturients;
                int abiturientCount = abiturients.Count();
                int agreementCount = abiturients.Count(a => a.HasAgreement);
                double contest = (double)abiturientCount / major.Places;

                // Проходные баллы для специальности по основному и проходному приоритетам
                int agreementPassingScore = abiturientList.Where(a => a.AgreementMajor == major.Id).Select(a => a.Rating).DefaultIfEmpty(0).Min();
                int currentPassingScore = abiturientList.Where(a => a.CurrentMajor == major.Id).Select(a => a.Rating).DefaultIfEmpty(0).Min();

                majorStatistics.Add(new MajorStatistics(
                    major,
                    abiturientCount,
                    agreementCount,
                    contest,
                    agreementPassingScore,
                    currentPassingScore
                ));
            }

            return majorStatistics;
        }

        public StatisticsResponse GetStatistics(string myId, List<UniversityData> universityData)
        {
            var abiturients = DistributionService.Distribute(DistributionService.Prepare(universityData), [.. universityData.Select(u => u.Major)]);
            MyStatistics myStatistics = GetMyStatistics(myId, abiturients);
            List<MajorStatistics> majorStatistics = GetMajorStatistics(universityData, abiturients);
            return new StatisticsResponse(
                abiturients.Count(),
                abiturients.Count(a => a.HasAgreement),
                myStatistics,
                majorStatistics
            );
        }
    }
}