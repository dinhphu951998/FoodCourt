using Microsoft.AspNetCore.Mvc;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodCourt.Framework;
using FoodCourt.Framework.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FoodCourt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCourtController : ControllerBase
    {
        public ExtensionSettings extensionSettings { get; }
        public MyIdentity myIdentity { get; set; }

        public MyUserManager userManager
        {
            get;
            set;
        }

        public ClaimsPrincipal currentUser { get => this.User; }


        public int? CurrentUserId
        {
            get
            {
                return myIdentity?.Id;
            }
        }

        public string CurrentUsername
        {
            get
            {
                return myIdentity?.UserName;
            }
        }


        public FoodCourtController(ExtensionSettings extensionSettings, MyUserManager userManager)
        {
            this.userManager = userManager;
            if (currentUser != null)
                myIdentity = userManager.GetUserAsync(currentUser)?.Result;
            this.extensionSettings = extensionSettings;
        }

        protected dynamic ExecuteInMonitoring(Func<dynamic> function)
        {
            dynamic result;
            try
            {
                result = function();
            }
            catch (FoodCourtException ex)
            {
                return BaseResponse.GetErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return BaseResponse.GetErrorResponse(ex.ToString());
            }
            return BaseResponse.GetSuccessResponse(result);
        }

        protected async Task<dynamic> ExecuteInMonitoring(Func<Task<dynamic>> function)
        {
            dynamic result;
            try
            {
                result = await function();
            }
            catch (FoodCourtException ex)
            {
                return BaseResponse.GetErrorResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return BaseResponse.GetErrorResponse(ex.ToString());
            }
            return BaseResponse.GetSuccessResponse(result);
        }
    }
}
