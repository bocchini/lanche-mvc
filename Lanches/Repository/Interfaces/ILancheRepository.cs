using Lanches.Models;

namespace Lanches.Repository.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLanchById(int lancheId);
    }
}
