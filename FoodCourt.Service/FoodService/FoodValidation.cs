using Doitsu.Service.Core;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Service.BaseValidation;
using FoodCourt.Service.CategoryService;
using FoodCourt.Service.StoreService;
using FoodCourt.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.FoodService
{
    public class FoodValidation : BaseValidation<Food, FoodViewModel>
    {
        private StoreValidation storeValidation;
        private CategoryValidation categoryValidation;

        public new const string ID_NOT_EXISTED = ErrorMessage.FOODID_NOT_EXISTED;

        public FoodValidation(IFoodService service, 
            StoreValidation storeValidation, CategoryValidation categoryValidation) : base(service)
        {
            this.storeValidation = storeValidation;
            this.categoryValidation = categoryValidation;
        }

        public async Task<bool> IsValidToCreate(FoodViewModel viewModel)
        {
            await storeValidation.IsEmpty(viewModel.StoreId.Value);
            await categoryValidation.IsEmpty(viewModel.CategoryId.Value);
            return true;
            
        }
        public async Task<bool> IsValidToUpdate(FoodViewModel viewModel)
        {
            await this.IsEmpty(viewModel.Id);
            await storeValidation.IsEmpty(viewModel.StoreId.Value);
            await categoryValidation.IsEmpty(viewModel.CategoryId.Value);
            return true;
        }

        public async Task<bool> IsValidToDelete(int id )
        {
            await this.IsEmpty(id);
            return true;

        }




    }
}
