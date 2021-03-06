﻿using System.Threading.Tasks;
using domain.interfaces.repositories;
using domain.entities;
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

        public Task<SoundLabel> GetByLabelNumberAsync(int labelNumber)
            => _context
                .SoundLabel
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.LabelNumber == labelNumber);
    }
}
