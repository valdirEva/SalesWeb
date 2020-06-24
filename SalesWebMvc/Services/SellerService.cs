
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //retorna lista de departamentos ordenado por nome. async,Task<>, para requisição ser assincrona.ToList() para operação sincrona, ToListAsync para operação assincrona.
        // await para informar que operação é assincrona.
        //Busca todos vendedores
        public async Task<List<Seller>> FindAllAsync()
            
        {
            return await _context.Seller.ToListAsync();
    }

        /*insere um objeto tipo seller no BD operação sincrona
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        */

        // operação de inserção assincrona.
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        /*busca um seller pelo ID.operação sincrona
        public Seller FindById(int id)
        {
            //Include(obj => obj.Department) pra fazer un join na tabela departament e trazer o departamento do vendedor, incluir using Microsoft.EntityFrameworkCore;
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        */

        //busca um seller pelo ID.operação assincrona
        public async Task<Seller> FindByIdAsync(int id)
        {
            //Include(obj => obj.Department) pra fazer un join na tabela departament e trazer o departamento do vendedor, incluir using Microsoft.EntityFrameworkCore;
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        /*remove um sellel. sincrona
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        */

        //remove um sellel.assincrona
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        /*atualizar um seller. sincrona
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
        */

        //atualizar um seller. assincrona
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny =await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
