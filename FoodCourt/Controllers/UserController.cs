using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Logic.IdentityLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodCourt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : FoodCourtController
    {
        private IIdentityService service;
        public UserController(IIdentityService service, ExtensionSettings extensionSettings, MyUserManager userManager) : base(extensionSettings, userManager)
        {
            this.service = service;
        }


        [HttpPost("login")]
        public Task<dynamic> Authorize(LoginViewModel viewModel)
        {
            return ExecuteInMonitoring( async () =>
            {
                return await service.AuthorizeAsync(this.userManager, viewModel);
            });
        }

        [HttpPost("register")]
        public Task<dynamic> Register(RegisterViewModel viewModel)
        {
            return ExecuteInMonitoring(async () =>
            {
                return await service.RegisterAsync(this.userManager, viewModel);
            });
        }

    }
}