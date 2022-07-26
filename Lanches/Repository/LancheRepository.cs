﻿using Lanches.Context;
using Lanches.Models;
using Lanches.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Repository
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(l => l.IsLanchePreferido).Include(c=> c.Categoria);

        public Lanche GetLanchById(int lancheId)
        {
           return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
