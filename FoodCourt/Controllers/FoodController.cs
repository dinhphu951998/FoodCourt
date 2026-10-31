using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Service.FoodService;
using FoodCourt.Service.OrderService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodCourt.Controllers
{
    [Authorize(Roles = "STOREMANAGER", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FoodController : FoodCourtController
    {
        private IFoodService service;
        public FoodController(ExtensionSettings extensionSettings, MyUserManager userManager,
            IFoodService service)
            : base(extensionSettings, userManager)
        {
            this.service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<dynamic> GetActiveFood(int id)
        {
            return await ExecuteInMonitoring(async () =>
            {
                return await service.GetActiveFoodByIdAsync(id);
            });
        }

        [HttpGet("page")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFoodPage(int id)
        {
            var food = await service.GetActiveFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            var html = $"<html><body><h1>{food.Name}</h1><p>{food.Description}</p><img src=\"{food.ImageUrl}\" /></body></html>";
            return Content(html, "text/html");
        }

        [HttpPost]
        public async Task<dynamic> CreateFood(FoodViewModel model)
        {
            return await ExecuteInMonitoring(async () =>
            {
                return await service.AddFoodAsync(model);
            });
        }
        [HttpPut]
        public async Task<dynamic> UpdateFood(FoodViewModel model)
        {
            return await ExecuteInMonitoring(async () =>
            {
                return await service.UpdateFoodAsync(model);
            });
        }
        [HttpDelete]
        public async Task<dynamic> DeleteFood(int id)
        {
            return await ExecuteInMonitoring(async () =>
            {
                return await service.DeleteFoodAsync(id);
            });
        }

        //get food by category type (ACTIVE)


        //get food by store ID (ACTIVE)





    }
}
