using Lanches.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Lanches.Repository.Interfaces;

namespace Lanches.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanches = _lancheRepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Hamburgers ";
            return View(lancheListViewModel);
        }
    }
}
