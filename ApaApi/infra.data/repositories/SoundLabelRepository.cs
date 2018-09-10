using System.Threading.Tasks;
using domain.interfaces.repositories;
using domain.models;
using infra.data.context;
using Microsoft.EntityFrameworkCore;

namespace infra.data.repositories
{
    public class SoundLabelRepository : ISoundLabelRepository
    {
        private readonly ApaContext _context;

        public SoundLabelRepository(ApaContext context)
        {
            _context = context;
        }

        public Task<SoundLabel> GetByLabelNumberAsync(int labelNumber) => 
            _context.SoundLabel.FirstOrDefaultAsync(s => s.LabelNumber == labelNumber);
    }
}
