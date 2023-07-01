using RCN.Data;
using RCN.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RCN.Repository
{
    public class PedidosRepository
    {
        private AppDbContext _context;

        public PedidosRepository(AppDbContext context)
        {
            _context = context; 
        }

        public PedidosRepository()
        {
        }

        public Pedidos GetPedidosById(int id)
        {
            return _context.Pedidos.FirstOrDefault(p => p.ID == id);
        }

        public void AddPedidos(Pedidos pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public void UpdatePedidos(Pedidos pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePedidos(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();
        }
    }
}
