using AutoMapper;
using FSACalculation.Controllers;
using FSACalculation.Entities;
using FSACalculation.Services;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FSACalculation.Test
{
    public class EmpClaimsControllerTest
    {
        [Fact]
        public void GetAllClaimsTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            IEnumerable<Claims> claimsView = new Claims[] 
            { 
                new Claims { ID = 1 }
            };
            _repository.Setup(m => m.GetAllClaimsAsync())
                .ReturnsAsync(claimsView);

            var _mapper = new Mock<IMapper>();
            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            // Act
            var result = controller.GetAllClaims();

            // Assert
            //verify GetAllClaimsAsync is called 
            _repository.Verify(r => r.GetAllClaimsAsync());
        }

        [Fact]
        public void GetClaimsTest()
        {
            // Arrange
            var _repository = new Mock<IClaimsRepository>();

            _repository.Setup(m => m.EmployeeExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            Employee emp = new Employee { Id = 1 };
            _repository.Setup(m => m.GetClaimsAsync(It.IsAny<int>()))
                .ReturnsAsync(emp);

            var _mapper = new Mock<IMapper>();
            var controller = new EmpClaimsController(_repository.Object, _mapper.Object);

            // Act
            var result = controller.GetClaims(It.IsAny<int>());

            // Assert
            //verify EmployeeExistAsync, GetClaimsAsync is called 
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaimsAsync(It.IsAny<int>()));
        }
        
        [Fact]
        public void CreateClaimsTest()
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
            var result = controller.CreateClaims(It.IsAny<int>(), It.IsAny<ClaimsForCreateViewModel>());

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.AddClaims(It.IsAny<int>(), It.IsAny<Claims>()));
            _repository.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public void UpdateClaimsTest()
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
            var result = controller.UpdateClaims(It.IsAny<int>(), mockViewModel);

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaim(It.IsAny<int>(), It.IsAny<int>()));
            _repository.Verify(r => r.SaveChangesAsync());
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

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetEmployeeByIdAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaim(It.IsAny<int>(), It.IsAny<int>()));
            _repository.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public void DeleteClaimsTest()
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

            // Assert
            //verify methods calls
            _repository.Verify(r => r.EmployeeExistAsync(It.IsAny<int>()));
            _repository.Verify(r => r.GetClaim(It.IsAny<int>(), It.IsAny<int>()));
            _repository.Verify(r => r.SaveChangesAsync());
        }
    }
}
