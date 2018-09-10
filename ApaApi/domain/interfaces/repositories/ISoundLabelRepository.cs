﻿using domain.models;
using System.Threading.Tasks;

namespace domain.interfaces.repositories
{
    public interface ISoundLabelRepository
    {
        Task<SoundLabel> GetByLabelNumberAsync(int labelNumber);
    }
}
