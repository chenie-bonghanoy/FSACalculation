using Castle.Core.Logging;
using FSACalculation.Controllers;
using FSACalculation.Entities;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FSACalculation.Test
{
    public class AccountControllerTest
    {
        [Fact]
        public void Login_Redirect()
        {
            //arrange
            var logger = new Mock<ILogger<AccountController>>();
            var userManager = new Mock<UserManager<UserLogin>>(
                Mock.Of<IUserStore<UserLogin>>(), null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<UserLogin>>(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<UserLogin>>(), null, null, null, null);
            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "testValue"),
                                        new Claim(ClaimTypes.Name, "test@test.com")
                                   }, "TestAuthentication"));
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //act 
            var result = (RedirectToActionResult)controller.Login();

            //assert
            Assert.Equal("Claims", result.ActionName);
        }

        [Fact]
        public void Login_View()
        {
            //arrange
            var logger = new Mock<ILogger<AccountController>>();
            var userManager = new Mock<UserManager<UserLogin>>(
                Mock.Of<IUserStore<UserLogin>>(), null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<UserLogin>>(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<UserLogin>>(), null, null, null, null);
            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //act
            var result = (ViewResult)controller.Login();

            //assert
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void Login_User()
        {
            //arrange
            var logger = new Mock<ILogger<AccountController>>();
            var userManager = new Mock<UserManager<UserLogin>>(
                Mock.Of<IUserStore<UserLogin>>(), null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<UserLogin>>(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<UserLogin>>(), null, null, null, null);
            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object);
            
            signInManager.Setup(a => a.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var user = new UserLogin { empId = 1, isAdmin = 0 };
            userManager.Setup(a => a.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var viewModel = new LoginViewModel { UserName = It.IsAny<string>(), Password = It.IsAny<string>() };

            //act
            var result = (RedirectToActionResult) controller.Login(viewModel).Result;

            //assert
            Assert.Equal("Claims", result.ActionName);
        }

        [Fact]
        public void Login_Admin()
        {
            //arrange
            var logger = new Mock<ILogger<AccountController>>();
            var userManager = new Mock<UserManager<UserLogin>>(
                Mock.Of<IUserStore<UserLogin>>(), null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<UserLogin>>(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<UserLogin>>(), null, null, null, null);
            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object);

            signInManager.Setup(a => a.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var user = new UserLogin { empId = 1, isAdmin = 1 };
            userManager.Setup(a => a.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var viewModel = new LoginViewModel { UserName = It.IsAny<string>(), Password = It.IsAny<string>() };

            //act
            var result = (RedirectToActionResult)controller.Login(viewModel).Result;

            //assert
            Assert.Equal("AdminApproval", result.ActionName);
        }

        [Fact]
        public void Login_Failed()
        {
            //arrange
            var logger = new Mock<ILogger<AccountController>>();
            var userManager = new Mock<UserManager<UserLogin>>(
                Mock.Of<IUserStore<UserLogin>>(), null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<UserLogin>>(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<UserLogin>>(), null, null, null, null);
            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object);

            signInManager.Setup(a => a.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var viewModel = new LoginViewModel { UserName = It.IsAny<string>(), Password = It.IsAny<string>() };

            //act
            var result = controller.Login(viewModel);

            //assert
            Assert.Equal("Failed to Login", controller.ModelState.Root.Errors.FirstOrDefault().ErrorMessage);
        }
    }
}
