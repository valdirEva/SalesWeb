using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //injetando dependencia de servico
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //ação para chamar pagina onde criar novo vendedor.Chama view Create
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();// busca todos departamentos cadastrados no BD,
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
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

        //metodo para criar tela de confirmação de delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //metodo que chama a função delete do service.
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);

            // redireciona para pagina index de seller após realizar a operação 
            return RedirectToAction(nameof(Index));
        }

        // detalhe de seller
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Ação Edit 
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }

        }
    }