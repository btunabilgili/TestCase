using System.Text.RegularExpressions;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Infrastructure.Services
{
    public class JobService : IJobService
    {
        public int CalculateJobQualityPoint(Job job)
        {
            int qualityPoint = 0;

            if (job.WorkType is not null)
                qualityPoint += 1;

            if (job.SalaryInformation > 0)
                qualityPoint += 1;

            if (job.SideRights?.Any() == true)
                qualityPoint += 1;

            //Yasaklı kelimeleri veritabanı ya da başka bir sağlayıcıdan çekmek daha doğru olacaktır ancak şimdilik projenin hızlı ilerlemesi açısından kod içerisinde bırakıldı.
            string badWordsPattern = @"\b(mob(i|İ|ı)ng|ırkçılık)\b";

            Regex regex = new(badWordsPattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            if (!regex.IsMatch(job.JobDescription))
                qualityPoint += 2;
            else
                job.JobDescription = regex.Replace(job.JobDescription, " ");

            return qualityPoint;
        }
    }
}
