
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Busca todos vendedores
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        //insere um objeto tipo seller no BD
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        //busca um seller pelo ID.
        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        //remove um sellel.
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
