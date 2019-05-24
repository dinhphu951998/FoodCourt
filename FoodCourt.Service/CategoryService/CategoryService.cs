using AutoMapper;
using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.CategoryService
{
    public interface ICategoryService : IBaseService<Category, CategoryViewModel>
    {
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
    }

    public class CategoryService : BaseService<Category, CategoryViewModel>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Find Category by Id
        /// </summary>
        /// <param name="id">integer number</param>
        /// <returns>Category</returns>
        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            return await this.FindByIdAsync(id);
        }
    }
}
