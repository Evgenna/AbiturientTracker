using Majors;
using University;

namespace Abiturients
{
    public class DistributionService
    {
        public static List<Abiturient> Prepare(List<UniversityData> universityData)
        {
            var abiturients = new Dictionary<string, Abiturient>();

            foreach (var data in universityData)
            {
                var major = data.Major;
                foreach (var abiturient in data.Abiturients)
                {
                    if (!abiturients.TryGetValue(abiturient.Uid, out var a))
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
            List<Abiturient> abiturientList = [.. abiturients.Values];
            return abiturientList;
        }

        public static List<Abiturient> Distribute(List<Abiturient> abiturients, List<MajorDetails> majorDetails)
        {
            var abiturientList = new List<Abiturient>(abiturients);

            var takingPlaces = new Dictionary<string, int>();
            var agreementPlaces = new Dictionary<string, int>();
            foreach (MajorDetails major in majorDetails)
            {
                takingPlaces[major.Id] = 0;
                agreementPlaces[major.Id] = 0;
            }
            foreach (var abiturient in abiturientList)
            {
                abiturient.MajorPriorities = [.. abiturient.MajorPriorities.OrderBy(a => a.Priority)];
                bool currentPlace = false;
                bool agreementPlace = false;
                foreach (var priority in abiturient.MajorPriorities)
                {
                    var majorId = priority.Id;
                    var major = majorDetails.Find(m => m.Id == majorId);

                    if (major is null)
                        continue;

                    var majorPlaces = major.Places;


                    if (takingPlaces[majorId] >= majorPlaces && agreementPlaces[majorId] >= majorPlaces) break;

                    if (takingPlaces[majorId] < majorPlaces && !currentPlace)
                    {
                        currentPlace = true;
                        takingPlaces[majorId]++;
                        abiturient.CurrentMajor = priority.Name;
                    }
                    if (agreementPlaces[majorId] < majorPlaces && !agreementPlace && abiturient.HasAgreement)
                    {
                        agreementPlace = true;
                        agreementPlaces[majorId]++;
                        abiturient.AgreementMajor = priority.Name;
                    }
                    if (agreementPlace && currentPlace) break;
                }
            }
            return abiturientList;
        }
    }
}