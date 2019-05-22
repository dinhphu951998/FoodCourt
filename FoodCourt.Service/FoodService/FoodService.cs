using AutoMapper;
using Doitsu.Service.Core;
using Doitsu.Service.Core.AuthorizeBuilder;
using Doitsu.Service.Core.IdentitiesExtension;
using FoodCourt.Framework;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Enums;
using FoodCourt.Framework.ExternalAuthentication;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Service.CategoryService;
using FoodCourt.Service.StoreService;
using FoodCourt.Service.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.FoodService
{
    public interface IFoodService : IBaseService<Food, FoodViewModel>
    {
        Task<FoodViewModel> AddFoodAsync(FoodViewModel model);
        Task<FoodViewModel> UpdateFoodAsync(FoodViewModel model);
        Task<FoodViewModel> GetActiveFoodByIdAsync(int id);
        //Task<FoodViewModel> GetFoodByIdAsync(int id);
        Task<bool> DeleteFoodAsync(int id);
    }

    public class FoodService : BaseService<Food, FoodViewModel>, IFoodService
    {
        private readonly FoodValidation foodValidation;
        private readonly MyUnitOfWork unitOfWork;

        public FoodService(MyUnitOfWork unitOfWork, IMapper mapper, StoreValidation storeService,
            CategoryValidation categoryService) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.foodValidation = new FoodValidation(this, storeService, categoryService);
        }

       
        #region CRUD
        /// <summary>
        /// Create Food with name, Storeid, CategoryId, ImageUrl, Price is required
        /// Role = StoreManager only
        /// </summary>
        /// <param name="model">FoodCreateViewModel</param>
        /// <returns>new FoodViewModel</returns>
        public async Task<FoodViewModel> AddFoodAsync(FoodViewModel model)
        {
            await foodValidation.IsValidToCreate(model);
            return await this.CreateAsync(model);
        }

        /// <summary>
        /// Update Food model
        /// </summary>
        /// <param name="model">FooodViewModel</param>
        /// <returns>new FoodViewModel</returns>
        public async Task<FoodViewModel> UpdateFoodAsync(FoodViewModel model)
        {
            await foodValidation.IsValidToUpdate(model);
            return await this.UpdateAsync(model);
        }
        /// <summary>
        /// Delete Food By ID
        /// </summary>
        /// <param name="id">integer number</param>
        /// <returns>bool</returns>

        public async Task<bool> DeleteFoodAsync(int id)
        {
            await foodValidation.IsValidToDelete(id);
            await this.DeactiveAsync<int>(id);
            return true;
        }

        /// <summary>
        /// Find Food By Id
        /// </summary>
        /// <param name="id">integer number</param>
        /// <returns>Food</returns>
        public async Task<FoodViewModel> GetActiveFoodByIdAsync(int id)
        {
            return await this.FirstOrDefaultActiveAsync(f => f.Id == id);

        }
        #endregion

    }
}
