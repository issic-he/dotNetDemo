using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EFDBContext:DbContext
    {
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options)
        {

        }

    }
}
