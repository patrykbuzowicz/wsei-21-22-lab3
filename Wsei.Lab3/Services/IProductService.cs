using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsei.Lab3.Entities;
using Wsei.Lab3.Models;

namespace Wsei.Lab3.Services
{
    public interface IProductService
    {
        Task Add(ProductModel product);

        Task<IEnumerable<ProductEntity>> GetAll(string name);
    }
}
