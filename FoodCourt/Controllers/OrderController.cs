using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Service.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodCourt.Controllers
{
    public class OrderController : FoodCourtController
    {
        private IOrderService service;
        public OrderController(ExtensionSettings extensionSettings, MyUserManager userManager, 
            IOrderService service) 
            : base(extensionSettings, userManager)
        {
            this.service = service;
        }



    }
}