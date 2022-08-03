using Lanches.Models;
using Microsoft.AspNetCore.Mvc;
using Lanches.Repository.Interfaces;

namespace Lanches.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new ViewModels.CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
            };

            return View(carrinhoCompraVM);
        }

        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = BuscaLancheSelecionado(lancheId);
            if(lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionaAoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverItemDoCarrinho(int lancheId)
        {
            var lancheSelecionado = BuscaLancheSelecionado(lancheId);
            if(lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        private Lanche BuscaLancheSelecionado(int lancheId)
        {
            return _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
