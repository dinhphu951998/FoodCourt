using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using FoodCourt.Service.CategoryService;
using FoodCourt.Service.FoodService;
using FoodCourt.Service.StoreService;
using Microsoft.EntityFrameworkCore;

namespace FoodCourt.Service.UnitOfWork
{
    public class MyUnitOfWork : Doitsu.Service.Core.UnitOfWork
    {
        private IMapper mapper;
        private IFoodService foodService;
        public IFoodService FoodService
        {
            get
            {
                if(foodService == null)
                {
                    //foodService = new FoodService.FoodService(this, mapper);
                }
                return foodService;
            }
        }

        private IStoreService storeService;
        public IStoreService StoreService
        {
            get
            {
                if (storeService == null)
                {
                    storeService = new StoreService.StoreService(this, mapper);
                }
                return storeService;
            }
        }

        private ICategoryService categoryService;
        public ICategoryService CategoryService
        {
            get
            {
                if (categoryService == null)
                {
                    categoryService = new CategoryService.CategoryService(this, mapper);
                }
                return categoryService;
            }
        }

        public MyUnitOfWork(FoodCourtContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

    }
}
