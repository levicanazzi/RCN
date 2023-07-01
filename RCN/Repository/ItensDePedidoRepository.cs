using RCN.Data;
using RCN.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RCN.Repository
{
    public class ItemPedidoRepository
    {
        private AppDbContext _context;

        public ItemPedidoRepository(AppDbContext context)
        {
            _context = context; 
        }

        public ItensDePedido GetItemPedidoById(int id)
        {
            return _context.ItensDePedido.FirstOrDefault(i => i.ID == id);
        }

        public void AddItemPedido(ItensDePedido itemPedido)
        {
            _context.ItensDePedido.Add(itemPedido);
            _context.SaveChanges();
        }

        public void UpdateItemPedido(ItensDePedido itemPedido)
        {
            _context.Entry(itemPedido).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteItemPedido(int id)
        {
            var itemPedido = _context.ItensDePedido.Find(id);
            _context.ItensDePedido.Remove(itemPedido);
            _context.SaveChanges();
        }
    }
}
