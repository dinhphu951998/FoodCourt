using Doitsu.Service.Core;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Service.BaseValidation;
using FoodCourt.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.StoreService
{
    public class StoreValidation : BaseValidation<Store, StoreViewModel>
    {
        public new const string ID_NOT_EXISTED = ErrorMessage.STOREID_NOT_EXISTED;
        public StoreValidation(IStoreService service) : base(service)
        {
        }
    }
}
