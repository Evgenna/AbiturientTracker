using Abiturients;
using Majors;
using Settings;
using University;

namespace Statistics
{
    public class StatisticsService()
    {

        public MyStatistics GetMyStatistics(string myId, List<Abiturient> _abiturients)
        {
            int place = _abiturients.FindIndex(a => a.Uid == myId);
            string currentMajor = "";
            string agreementMajor = "";
            var abiturientsForward = _abiturients.Slice(0, place).ToList();
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

        public List<MajorStatistics> GetMajorStatistics(List<UniversityData> universityData)
        {
            List<MajorStatistics> majorStatistics = [];

            foreach (var data in universityData)
            {
                MajorDetails major = data.Major;
                List<AbiturientResponse> abiturients = data.Abiturients;
                int abiturientCount = abiturients.Count();
                int agreementCount = abiturients.Count(a => a.HasAgreement);
                double contest = abiturientCount / major.Places;

                majorStatistics.Add(new MajorStatistics(
                    major,
                    abiturientCount,
                    agreementCount,
                    contest,
                    0,
                    0
                ));
            }

            return majorStatistics;
        }

        public StatisticsResponse GetStatistics(string myId, List<UniversityData> universityData)
        {

            MyStatistics myStatistics = GetMyStatistics(myId, DistributionService.Prepare(universityData));
            List<MajorStatistics> majorStatistics = GetMajorStatistics(universityData);
            return new StatisticsResponse(
                0,
                0,
                myStatistics,
                majorStatistics
            );
        }
    }
}