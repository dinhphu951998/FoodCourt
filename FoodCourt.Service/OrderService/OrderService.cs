using AutoMapper;
using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.OrderService
{
    public interface IOrderService : IBaseService<Order, OrderViewModel>
    {

    }

    public class OrderService : BaseService<Order, OrderViewModel>, IOrderService
    {
        private readonly IHttpContextAccessor _httpContext;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContext) : base(unitOfWork, mapper)
        {
            this._httpContext = httpContext;
        }

    }
}
