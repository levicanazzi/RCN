using RCN.Data;
using RCN.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RCN.Repository
{
    public class ProdutosRepository
    {
        private AppDbContext _context;

        public ProdutosRepository(AppDbContext context)
        {
            _context = context;
        }

        public Produtos GetProdutosById(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.ID == id);
        }

        public void AddProdutos(Produtos produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void UpdateProdutos(Produtos produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProdutos(int id)
        {
            var produto = _context.Produtos.Find(id);
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
    }
}
