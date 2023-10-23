using AutoMapper;
using FSACalculation.APIControllers;
using FSACalculation.Data.Entities;
using FSACalculation.Data.Services;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.ContentModel;
using System.Net;

namespace FSACalculation.Test
{
    public class EmpClaimsControllerTest
    {
        [Fact]
        public async void GetAllClaimsTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            IEnumerable<Claims> claimsView = new Claims[] 
            { 
                new Claims { ID = 1 }
            };
            _repository.Setup(m => m.GetAllClaimsAsync())
                .ReturnsAsync(claimsView);

            IEnumerable<ClaimsViewModel> viewModel = new ClaimsViewModel[]
            {
                new ClaimsViewModel
                {
                    ClaimId = 1
                }
            };

            var _mapper = new Mock<IMapper>();
            _mapper.Setup(m => m.Map<IEnumerable<ClaimsViewModel>>(claimsView)).Returns(viewModel);

            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            // Act
            var result = await controller.GetAllClaims();
            var okresult = result.Result as OkObjectResult;

            // Assert
            //verify GetAllClaimsAsync is called 
            _repository.Verify(r => r.GetAllClaimsAsync());

            //verify statuscode and data 
            Assert.Equal((int)HttpStatusCode.OK, okresult.StatusCode);
            Assert.Equal(viewModel, okresult.Value);
        }

        [Fact]
        public async void GetClaimsTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            _repository.Setup(m => m.EmployeeExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            Employee emp = new Employee { Id = 1 };
            _repository.Setup(m => m.GetClaimsAsync(It.IsAny<int>()))
                .ReturnsAsync(emp);

            var viewModel = new EmployeeClaimsViewModel { Id = 1 };

            var _mapper = new Mock<IMapper>();
            _mapper.Setup(m => m.Map<EmployeeClaimsViewModel>(emp)).Returns(viewModel);

            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            // Act
            var result = await controller.GetClaims(It.IsAny<int>());
            var okresult = result.Result as OkObjectResult;

            // Assert
            //verify EmployeeExistAsync, GetClaimsAsync is called 
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaimsAsync(It.IsAny<int>()));

            Assert.Equal((int)HttpStatusCode.OK, okresult.StatusCode);
            Assert.Equal(viewModel, okresult.Value);
        }
        
        [Fact]
        public async void CreateClaimsTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            _repository.Setup(m => m.EmployeeExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            _repository.Setup(m => m.AddClaims(It.IsAny<int>(), It.IsAny<Claims>()))
                .Returns(Task.CompletedTask);

            var _mapper = new Mock<IMapper>();
            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            // Act
            var result = await controller.CreateClaims(It.IsAny<int>(), It.IsAny<ClaimsForCreateViewModel>());
            var okresult = result.Result as OkResult;

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.AddClaims(It.IsAny<int>(), It.IsAny<Claims>()));
            _repository.Verify(r => r.SaveChangesAsync());

            Assert.Equal((int)HttpStatusCode.OK, okresult.StatusCode);
        }

        [Fact]
        public async void UpdateClaimsTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            _repository.Setup(m => m.EmployeeExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            var claim = new Claims { ID = 1};
            _repository.Setup(m => m.GetClaim(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(claim);

            var _mapper = new Mock<IMapper>();
            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            var mockViewModel = new ClaimsForUpdateViewModel();
            mockViewModel.ClaimId = It.IsAny<int>();
            
            // Act
            var result = await controller.UpdateClaims(It.IsAny<int>(), mockViewModel);
            var okResult = result.Result as OkResult;

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaim(It.IsAny<int>(), It.IsAny<int>()));
            _repository.Verify(r => r.SaveChangesAsync());

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public void AdminApprovalAsyncTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            _repository.Setup(m => m.EmployeeExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            var emp = new Employee { Id = 1 };
            _repository.Setup(m => m.GetEmployeeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(emp);
            var claim = new Claims { ID = 1 };
            _repository.Setup(m => m.GetClaim(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(claim);

            var _mapper = new Mock<IMapper>();
            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            var mockViewModel = new ClaimsViewModel();
            mockViewModel.ClaimId = It.IsAny<int>();
            
            // Act
            var result = controller.AdminApprovalAsync(It.IsAny<int>(), It.IsAny<int>(), mockViewModel);
            var okResult = result.Result as OkResult;

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetEmployeeByIdAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaim(It.IsAny<int>(), It.IsAny<int>()));
            _repository.Verify(r => r.SaveChangesAsync());

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void DeleteClaimsTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            _repository.Setup(m => m.EmployeeExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            var claim = new Claims { ID = 1 };
            _repository.Setup(m => m.GetClaim(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(claim);

            var _mapper = new Mock<IMapper>();
            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            var mockViewModel = new ClaimsForUpdateViewModel();
            mockViewModel.ClaimId = It.IsAny<int>();
            
            // Act
            var result = controller.DeleteClaims(It.IsAny<int>(), It.IsAny<int>());
            var okResult = result.Result as OkResult;

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaim(It.IsAny<int>(), It.IsAny<int>()));
            _repository.Verify(r => r.SaveChangesAsync());

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
    }
}
