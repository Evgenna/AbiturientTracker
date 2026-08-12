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

        /// <summary>
        /// Распределение абитуриентов по доступным специальностям с учетом приоритетов и поданных согласий
        /// </summary>
        /// <param name="abiturients">Список абитуриентов</param>
        /// <param name="majorDetails">Список специальностей</param>
        /// <returns>Список абитуриентов с назначенными специальностями</returns>
        public static List<Abiturient> Distribute(List<Abiturient> abiturients, List<Major> majorDetails)
        {
            var abiturientList = abiturients.Select(a => new Abiturient(a)).ToList();

            var takingPlaces = new Dictionary<string, int>(); // Места на основной высший приоритет
            var agreementPlaces = new Dictionary<string, int>(); // Места на высший проходной приоритет
            foreach (Major major in majorDetails)
            {
                takingPlaces[major.Id] = 0;
                agreementPlaces[major.Id] = 0;
            }
            foreach (var abiturient in abiturientList)
            {
                abiturient.MajorPriorities = [.. abiturient.MajorPriorities.OrderBy(a => a.Priority)];

                bool currentPlace = false; // Занял ли абитуриент место в основном высшем приоритете
                bool agreementPlace = false; // Занял ли абитуриент место в высшем проходном приоритете

                foreach (var priority in abiturient.MajorPriorities)
                {
                    var majorId = priority.Id;
                    var major = majorDetails.Find(m => m.Id == majorId);

                    if (major is null)
                        continue;

                    var majorPlaces = major.Places;

                    // Если места по обоим приоритетам заняты, то переходим к следующему приоритету
                    if (takingPlaces[majorId] >= majorPlaces && agreementPlaces[majorId] >= majorPlaces) continue;

                    // Если у абитуриента определены оба приоритета, то дальнейшие приоритеты не рассматриваются
                    if (agreementPlace && currentPlace) break;

                    // Распределение по основному высшему приоритету
                    if (takingPlaces[majorId] < majorPlaces && !currentPlace)
                    {
                        currentPlace = true;
                        takingPlaces[majorId]++;
                        abiturient.CurrentMajor = priority.Name;
                    }
                    // Распределение по высшему проходному приоритету
                    if (agreementPlaces[majorId] < majorPlaces && !agreementPlace && abiturient.HasAgreement)
                    {
                        agreementPlace = true;
                        agreementPlaces[majorId]++;
                        abiturient.AgreementMajor = priority.Name;
                    }
                }
            }
            return abiturientList;
        }
    }
}