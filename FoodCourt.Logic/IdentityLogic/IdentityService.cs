using AutoMapper;
using Doitsu.Service.Core;
using Doitsu.Service.Core.AuthorizeBuilder;
using Doitsu.Service.Core.IdentitiesExtension;
using FoodCourt.Framework;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Models;
using FoodCourt.Framework.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Logic.IdentityLogic
{
    public interface IIdentityService : IBaseService<MyIdentity, MyIdentityViewModel>
    {
        Task<TokenAuthorizeModel> AuthorizeAsync(MyUserManager userManager, LoginViewModel viewModel);
        Task<TokenAuthorizeModel> RegisterAsync(MyUserManager userManager, RegisterViewModel viewModel);
    }

    public class IdentityService : BaseService<MyIdentity, MyIdentityViewModel>, IIdentityService
    {
        public IdentityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<TokenAuthorizeModel> AuthorizeAsync(MyUserManager userManager, LoginViewModel viewModel)
        {
            MyIdentity identity = viewModel.ToEntity();
            var user = await userManager.FindByNameAsync(identity.UserName);
            if (user == null) throw new FoodCourtException(ErrorMessage.USER_IS_NOT_EXIST);

            bool result = await userManager.CheckPasswordAsync(user, viewModel.Password);

            if (result)
            {
                return await user.AuthorizeAsync(userManager);
            }
            throw new FoodCourtException(ErrorMessage.PASSWORD_NOT_VALID);
        }

        public async Task<TokenAuthorizeModel> RegisterAsync(MyUserManager userManager, RegisterViewModel viewModel)
        {
            //save
            var identity = viewModel.ToEntity();
            await userManager.UpdateNormalizedEmailAsync(identity);
            await userManager.UpdateNormalizedUserNameAsync(identity);
            await userManager.AddToRoleAsync(identity, RoleType.MEMBER.ToString());
            IdentityResult result = await userManager.CreateAsync(identity, viewModel.Password);
            if (result.Succeeded)
            {
                return await identity.AuthorizeAsync(userManager);
            }
            throw new FoodCourtException(ErrorMessage.USER_CREATE_FAIL);
        }
    }
}
