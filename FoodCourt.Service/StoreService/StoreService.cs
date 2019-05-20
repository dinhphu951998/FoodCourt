using AutoMapper;
using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.StoreService
{
    public interface IStoreService: IBaseService<Store, StoreViewModel>
    {

        Task<StoreViewModel> GetStoreById(int id);
        Task<StoreViewModel> CreateStore(StoreViewModel model);
    }
    public class StoreService : BaseService<Store, StoreViewModel>, IStoreService
    {
        private readonly IHttpContextAccessor _httpContext;
        public StoreService(IUnitOfWork unitOfWork, IMapper mapper, 
            IHttpContextAccessor httpContext) : base(unitOfWork, mapper)
        {
            _httpContext = httpContext;
        }

        #region CRUD

        public async Task<StoreViewModel> GetStoreById(int id)
        {
            return await this.FindByIdAsync(id);
        }
        public async Task<StoreViewModel> CreateStore(StoreViewModel model)
        {
            return await this.CreateAsync(model);
        }
        #endregion
    }
}
