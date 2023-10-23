using Castle.Core.Configuration;
using Castle.Core.Logging;
using FSACalculation.Controllers;
using FSACalculation.Data.Entities;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Configuration;
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

            var inMemorySettings = new Dictionary<string, string> {
                    {"TopLevelKey", "TopLevelValue"},
                    {"SectionName:SomeKey", "SectionValue"},
                    //...populate as needed for the test
                };

            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var signInManager = new Mock<SignInManager<UserLogin>>(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<UserLogin>>(), null, null, null, null);
            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object, config);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "test"),
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

            var inMemorySettings = new Dictionary<string, string> {
                    {"TopLevelKey", "TopLevelValue"},
                    {"SectionName:SomeKey", "SectionValue"},
                    //...populate as needed for the test
                };

            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object, config);
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

            var inMemorySettings = new Dictionary<string, string> {
                    {"Tokens:Key", "test token test token test token"},
                    {"Tokens:Issuer", "Issuer"},
                    {"Tokens:Audience", "Audience"},
                };

            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            
            var context = new Mock<HttpContext>();
            var session = new Mock<ISession>();
            context.Setup(c => c.Session).Returns(session.Object);

            var tempData = new TempDataDictionary(context.Object, Mock.Of<ITempDataProvider>());
            tempData["JWToken"] = "JWToken";

            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object, config)
            {
                TempData = tempData
            };
            
            signInManager.Setup(a => a.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var user = new UserLogin { empId = 1, isAdmin = 0, UserName = "test", Email = "test@test.com" };
            userManager.Setup(a => a.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var viewModel = new LoginViewModel { UserName = It.IsAny<string>(), Password = It.IsAny<string>() };

            controller.ControllerContext.HttpContext = context.Object;

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

            var inMemorySettings = new Dictionary<string, string> {
                    {"Tokens:Key", "test token test token test token"},
                    {"Tokens:Issuer", "Issuer"},
                    {"Tokens:Audience", "Audience"},
                };

            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var context = new Mock<HttpContext>();
            var session = new Mock<ISession>();
            context.Setup(c => c.Session).Returns(session.Object);

            var tempData = new TempDataDictionary(context.Object, Mock.Of<ITempDataProvider>());
            tempData["JWToken"] = "JWToken";

            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object, config)
            {
                TempData = tempData
            };

            signInManager.Setup(a => a.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var user = new UserLogin { empId = 1, isAdmin = 1, UserName = "test", Email = "test@test.com" };
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

            var inMemorySettings = new Dictionary<string, string> {
                    {"Tokens:Key", "test token test token test token"},
                    {"Tokens:Issuer", "Issuer"},
                    {"Tokens:Audience", "Audience"},
                };

            Microsoft.Extensions.Configuration.IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var controller = new AccountController(logger.Object, signInManager.Object, userManager.Object, config);

            var user = new UserLogin { empId = 1, isAdmin = 1, UserName = "test", Email = "test@test.com" };
            userManager.Setup(a => a.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

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
