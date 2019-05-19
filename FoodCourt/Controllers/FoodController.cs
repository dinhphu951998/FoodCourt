using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Service.FoodService;
using FoodCourt.Service.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodCourt.Controllers
{
    public class FoodController : FoodCourtController
    {
        private IFoodService service;
        public FoodController(ExtensionSettings extensionSettings, MyUserManager userManager,
            IFoodService service) 
            : base(extensionSettings, userManager)
        {
            this.service = service;
        }


    }
}