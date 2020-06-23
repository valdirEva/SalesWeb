using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //injetando dependencia de servico
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //ação para chamar pagina onde criar novo vendedor.Chama view Create
        public IActionResult Create()
        {
            return View();
        }

        //ação create, chama operação da classe de servico.
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);

            // redireciona para pagina index de seller após realizar a operação 
            return RedirectToAction(nameof(Index));
        }
    }
}