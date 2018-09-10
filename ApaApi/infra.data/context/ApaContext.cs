using domain.models;
using Microsoft.EntityFrameworkCore;

namespace infra.data.context
{
    public class ApaContext : DbContext
    {
        public ApaContext(DbContextOptions options) : base(options)
        { }

        public virtual DbSet<SoundLabel> SoundLabel { get; set; }
    }
}
