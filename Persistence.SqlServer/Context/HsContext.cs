using Microsoft.EntityFrameworkCore;
using Persistence.Infrastructure.Domain;

namespace Persistence.SqlServer.Context
{
    public class HsContext : DbContext
    {

        public Brand Brands { get; set; }

        public Model Models { get; set; }

    }
}
