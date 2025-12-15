using Riwi.Api.Data;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
