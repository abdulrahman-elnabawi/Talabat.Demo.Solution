using AdminDashboard.Helpers;
using AdminDashboard.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products =await unitOfWork.Repository<Product>().GetAllAsync();
            var mappedProducts = mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductViewModel>>(products);
            return View(mappedProducts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Image != null)
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                else
                    model.PictureUrl = "images/products/hat-react2.png";

                var mappedProduct = mapper.Map<ProductViewModel,Product>(model);
                await unitOfWork.Repository<Product>().AddAsync(mappedProduct);
                await unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            var mappedProduct = mapper.Map<Product,ProductViewModel>(product);
            return View(mappedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();
            if(ModelState.IsValid)
            {
                if(model.Image != null)
                {
                    if(model.PictureUrl != null)
                    {
                        PictureSettings.DeleteFile(model.PictureUrl, "products");
                        model.PictureUrl = PictureSettings.UploadFile(model.Image, "products"); 
                    }
                    else
                    {
                        model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");

                    }
                    
                }
                var mappedProduct = mapper.Map<ProductViewModel, Product>(model);
                unitOfWork.Repository<Product>().Update(mappedProduct);
                var result = await unitOfWork.Complete();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult>Delete(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            var mappedProduct =mapper.Map<Product,ProductViewModel>(product);
            return View(mappedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id,ProductViewModel model)
        {
            if(id !=model.Id)
                return NotFound();
            try
            {
                var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
                if (product.PictureUrl != null)
                    PictureSettings.DeleteFile(product.PictureUrl, "products");

                unitOfWork.Repository<Product>().Delete(product);
                await unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            catch (System.Exception)
            {

                return View(model);
            }
        }
    }
}
