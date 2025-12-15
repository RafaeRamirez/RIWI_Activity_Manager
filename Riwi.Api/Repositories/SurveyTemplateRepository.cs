using Riwi.Api.Data;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Repositories
{
    public class SurveyTemplateRepository : Repository<SurveyTemplate>, ISurveyTemplateRepository
    {
        public SurveyTemplateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
