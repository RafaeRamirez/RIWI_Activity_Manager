using Riwi.Api.Data;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Repositories
{
    public class SurveyAnswerRepository : Repository<SurveyAnswer>, ISurveyAnswerRepository
    {
        public SurveyAnswerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
