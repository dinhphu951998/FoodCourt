using AutoMapper;
using Doitsu.Service.Core;
using Doitsu.Service.Core.AuthorizeBuilder;
using Doitsu.Service.Core.IdentitiesExtension;
using FoodCourt.Framework;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Enums;
using FoodCourt.Framework.ExternalAuthentication;
using FoodCourt.Framework.Helpers;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.FoodService
{
    public interface IFoodService : IBaseService<Food, FoodViewModel>
    {
        
    }

    public class FoodService : BaseService<Food, FoodViewModel>, IFoodService
    {
        private readonly IHttpContextAccessor _httpContext;

        public FoodService(IUnitOfWork unitOfWork, IMapper mapper, 
            IHttpContextAccessor httpContext) : base(unitOfWork, mapper)
        {
            this._httpContext = httpContext;
        }

    }
}
