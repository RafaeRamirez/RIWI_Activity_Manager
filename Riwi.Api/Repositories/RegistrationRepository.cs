using Riwi.Api.Data;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Repositories
{
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
