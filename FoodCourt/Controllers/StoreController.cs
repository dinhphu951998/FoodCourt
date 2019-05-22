using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Service.StoreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodCourt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : FoodCourtController
    {
        IStoreService service;
        public StoreController(ExtensionSettings extensionSettings, MyUserManager userManager,
            IStoreService service) : base(extensionSettings, userManager)
        {
            this.service = service;
        }

        [HttpGet()]
        public async Task<dynamic> GetStore(int id)
        {
            return await ExecuteInMonitoring(async () =>
            {
                return await service.GetStoreByIdAsync(1);
            });
        }
        [HttpPost()]
        public async Task<dynamic> CreateStore(StoreViewModel model)
        {
            return await ExecuteInMonitoring(async () =>
            {
                return await service.CreateStore(model);
            });
        }
    }
}