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

namespace FoodCourt.Service.IdentityService
{
    public interface IIdentityService : IBaseService<MyIdentity, MyIdentityViewModel>
    {
        Task<TokenAuthorizeModel> AuthorizeAsync(MyUserManager userManager, LoginViewModel viewModel);
        Task<TokenAuthorizeModel> RegisterAsync(MyUserManager userManager, RegisterViewModel viewModel);
        Task<TokenAuthorizeModel> RegisterExternalAsync(MyUserManager userManager, RegisterExternalViewModel viewModel);
    }

    public class IdentityService : BaseService<MyIdentity, MyIdentityViewModel>, IIdentityService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly SignInManager<MyIdentity> _signInManager;
        private ExternalAuthenticationFactory _externalAuthenticationFactory;
        private ExtensionSettings _extensionSettings;

        public IdentityService(IUnitOfWork unitOfWork, IMapper mapper, 
            IHttpContextAccessor httpContext, SignInManager<MyIdentity> signInManager,
            ExternalAuthenticationFactory _externalAuthenticationFactory, ExtensionSettings _extensionSettings) : base(unitOfWork, mapper)
        {
            this._httpContext = httpContext;
            this._signInManager = signInManager;
            this._externalAuthenticationFactory = _externalAuthenticationFactory;
            this._extensionSettings = _extensionSettings;
        }

        public async Task<TokenAuthorizeModel> AuthorizeAsync(MyUserManager userManager, LoginViewModel viewModel)
        {
            MyIdentity identity = viewModel.ToEntity();
            var user = await userManager.FindByNameAsync(identity.UserName);
            if (user == null) throw new FoodCourtException(ErrorMessage.USER_IS_NOT_EXIST);

            bool result = await userManager.CheckPasswordAsync(user, viewModel.Password);

            if (result)
            {
                return await user.AuthorizeAsync(userManager, _extensionSettings.appSettings);
            }
            throw new FoodCourtException(ErrorMessage.PASSWORD_NOT_VALID);
        }

        public async Task<TokenAuthorizeModel> RegisterAsync(MyUserManager userManager, RegisterViewModel viewModel)
        {
            //save
            var identity = viewModel.ToEntity();
            identity.PasswordHash = userManager.PasswordHasher.HashPassword(identity, viewModel.Password);
            IdentityResult result = await userManager.CreateAsync(identity);
            
            if (result.Succeeded)
            {
                await updateOtherInfoForIdentity(userManager, identity);
                return await identity.AuthorizeAsync(userManager, _extensionSettings.appSettings);
            }
            throw new FoodCourtException(ErrorMessage.USER_CREATE_FAIL);
        }

        public async Task<TokenAuthorizeModel> RegisterExternalAsync(MyUserManager userManager, RegisterExternalViewModel viewModel)
        {
            var identity = viewModel.ToEntity();
            var authenticationType = Enum.Parse<ExternalAuthenticationType>(viewModel.Provider, true);

            _externalAuthenticationFactory.Create(authenticationType).GetUserDetails(identity, viewModel.ProviderIdToken);

            identity.UserName = identity.Email;

            //If user exist in the system
            var user = await userManager.FindByNameAsync(identity.UserName);
            if(user != null)
            {
                return await user.AuthorizeAsync(userManager, _extensionSettings.appSettings);
            }

            //if user is not exist in the system
            IdentityResult result = await userManager.CreateAsync(identity);

            if (result.Succeeded)
            {
                await updateOtherInfoForIdentity(userManager, identity);
                return await identity.AuthorizeAsync(userManager, _extensionSettings.appSettings);
            }

            throw new FoodCourtException(ErrorMessage.USER_CREATE_FAIL);
        }

        private async Task updateOtherInfoForIdentity(MyUserManager userManager, MyIdentity identity)
        {
            await userManager.UpdateNormalizedEmailAsync(identity);
            await userManager.UpdateNormalizedUserNameAsync(identity);
            await userManager.AddToRoleAsync(identity, RoleType.MEMBER.ToString());
        }
    }
}
