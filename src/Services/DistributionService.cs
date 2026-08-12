using Majors;
using University;

namespace Abiturients
{
    public class DistributionService
    {
        public List<Abiturient> Prepare(List<UniversityData> universityData)
        {
            var abiturients = new Dictionary<string, Abiturient>();
            
            foreach (var data in universityData)
            {
                var major = data.Major;
                foreach (var abiturient in data.Abiturients)
                {
                    if(!abiturients.TryGetValue(abiturient.Uid, out var a))
                    {
                        a = new Abiturient
                        {
                            Uid = abiturient.Uid,
                            Rating = abiturient.Rating,
                            HasAgreement = abiturient.HasAgreement
                        };

                        abiturients[abiturient.Uid] = a;
                    }

                    a.MajorPriorities.Add(
                        new MajorPriority(major.Id, major.Name, abiturient.Priority)
                    );
                }
            }
            List<Abiturient> abiturientList = [..abiturients.Values];
            return abiturientList;
        }
    }
}