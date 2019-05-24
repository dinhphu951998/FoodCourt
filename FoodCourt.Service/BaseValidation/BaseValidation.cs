using Doitsu.Service.Core;
using FoodCourt.Framework;
using FoodCourt.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Service.BaseValidation
{
    public abstract class BaseValidation<TEntity, TViewModel>
        where TEntity: class, new()
        where TViewModel: BaseViewModel<TEntity>, new()
    {
        protected IBaseService<TEntity, TViewModel> service;
        public const string ID_NOT_EXISTED = ErrorMessage.ID_NOT_EXISTED;

        public BaseValidation(IBaseService<TEntity, TViewModel> service)
        {
            this.service = service;
        }

        public async Task<bool> IsEmpty(object id)
        {
            var entity = await service.FindByIdAsync(id);
            if (entity == null) throw new FoodCourtException(ID_NOT_EXISTED);
            return true;

        }


    }
}
