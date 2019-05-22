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

        Task<StoreViewModel> GetStoreByIdAsync(int id);
        Task<StoreViewModel> CreateStore(StoreViewModel model);
    }
    public class StoreService : BaseService<Store, StoreViewModel>, IStoreService
    {
        public StoreService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Find Store by Id
        /// </summary>
        /// <param name="id">integer number</param>
        /// <returns>Store</returns>
        public async Task<StoreViewModel> GetStoreByIdAsync(int id)
        {
            return await this.FindByIdAsync(id);
        }

        #region CRUD

        
        public async Task<StoreViewModel> CreateStore(StoreViewModel model)
        {
            return await this.CreateAsync(model);
        }
        #endregion
    }
}
