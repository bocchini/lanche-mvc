using Lanches.Context;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Models
{
    public class CarrinhoCompra
    { 
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<AppDbContext>();

            string carrinhoId = session.GetString("CarrinhoId")??Guid.NewGuid().ToString();

            session.SetString("CarrinhoId",carrinhoId);

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionaAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = VerificaCarrinho(lanche);
            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItem()
        {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Lanche)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinItens = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);
            _context.CarrinhoCompraItens.RemoveRange(carrinItens);
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            return total;
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var quantidadeLocal = 0;
            var carrinhoCompraItem = VerificaCarrinho(lanche);
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        private CarrinhoCompraItem VerificaCarrinho(Lanche lanche)
        {
            return _context.CarrinhoCompraItens.SingleOrDefault(
                c => c.Lanche.LancheId == lanche.LancheId && c.CarrinhoCompraId == CarrinhoCompraId);
        }
    }
}
