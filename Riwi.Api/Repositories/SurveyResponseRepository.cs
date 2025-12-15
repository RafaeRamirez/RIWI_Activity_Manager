using Riwi.Api.Data;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Repositories
{
    public class SurveyResponseRepository : Repository<SurveyResponse>, ISurveyResponseRepository
    {
        public SurveyResponseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
