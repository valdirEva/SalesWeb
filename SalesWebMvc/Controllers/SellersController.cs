using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /*realiza a chamada de busca no serviço. sincrona
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        */

        //realiza a chamada de busca no serviço. assincrona
        public async Task<IActionResult>Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        /*ação para chamar pagina onde criar novo vendedor.Chama view Create. operação sincrona
        public IActionResult Create()
        {
            
            var departments = _departmentService.FindAll();// busca todos departamentos cadastrados no BD,
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        */
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        //realiza a chamada de busca no serviço. assincrona


        /*ação create, chama operação da classe de servico. operação sincrona
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)// função para verificar validação caso JS esteja desabilitado no navegador.
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            _sellerService.Insert(seller);

            // redireciona para pagina index de seller após realizar a operação 
            return RedirectToAction(nameof(Index));
        }
        */

        //ação create, chama operação da classe de servico. operação assincrona
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)// função para verificar validação caso JS esteja desabilitado no navegador.
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            await _sellerService.InsertAsync(seller);

            // redireciona para pagina index de seller após realizar a operação 
            return RedirectToAction(nameof(Index));
        }

        /*metodo para criar tela de confirmação de delete. operação sincrona
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found" });
            }
            return View(obj);
        }
        */

        //metodo para criar tela de confirmação de delete. operação assincrona
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found" });
            }
            return View(obj);
        }

        /*metodo que chama a função delete do service.operação sincrona
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);

            // redireciona para pagina index de seller após realizar a operação 
            return RedirectToAction(nameof(Index));
        }
        */

        //metodo que chama a função delete do service.operação assincrona
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);

                // redireciona para pagina index de seller após realizar a operação 
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Can't delete seller because he/she has sales" });
            }
        }

        /* detalhe de seller. operação sincrona
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found" });
            }
            return View(obj);
        }
        */

        // detalhe de seller. operação assincrona
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found" });
            }
            return View(obj);
        }

        /*Ação Edit . operação sincrona
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }
        */


        //Ação Edit . operação assincrona
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID not found" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        /* editar no metodo post para realizar operação sincrona
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)// função para verificar validação caso JS esteja desabilitado no navegador.
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e )
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }
        */

        // editar no metodo post para realizar operação assincrona
        [HttpPost]//anotação para indicar que a ação é de post
        [ValidateAntiForgeryToken]//anotsção de segurança
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)// função para verificar validação caso JS esteja desabilitado no navegador.
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        //Ação de erro
        public IActionResult Error (string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        }
    }