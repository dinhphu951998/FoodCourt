using Doitsu.Service.Core;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.CategoryService
{
    public class CategoryValidation : BaseValidation.BaseValidation<Category, CategoryViewModel>
    {

        public new const string ID_NOT_EXISTED = ErrorMessage.CATEGORYID_NOT_EXISTED;

        public CategoryValidation(ICategoryService service) : base(service)
        {
        }
    }
}
