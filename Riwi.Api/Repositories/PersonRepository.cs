using Riwi.Api.Data;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
