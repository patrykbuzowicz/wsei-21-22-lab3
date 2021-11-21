using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsei.Lab3.Database;
using Wsei.Lab3.Entities;
using Wsei.Lab3.Models;

namespace Wsei.Lab3.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductModel product)
        {
            var entity = new ProductEntity
            {
                Name = product.Name,
                Description = product.Description,
                IsVisible = product.IsVisible,
            };

            await _dbContext.Products.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var viewModel = new ProductStatsViewModel
            {
                NameLength = product.Name.Length,
                DescriptionLength = product.Description.Length,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List(string name)
        {
            IQueryable<ProductEntity> productsQuery = _dbContext.Products;

            if (!string.IsNullOrEmpty(name))
            {
                productsQuery = productsQuery.Where(x => x.Name.Contains(name));
            }

            var products = await productsQuery.ToListAsync();

            return View(products);
        }
    }
}
