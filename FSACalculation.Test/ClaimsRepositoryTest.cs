using FSACalculation.DBContext;
using FSACalculation.Entities;
using FSACalculation.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol.Core.Types;
using System.Reflection.Metadata;

namespace FSACalculation.Test
{
    public class ClaimsRepositoryTest
    {
        //private Mock<IClaimsRepository> _repository;
        //private ClaimsRepository repository;

        //public ClaimsRepositoryTest()
        //{
        //    _repository = new Mock<IClaimsRepository>();
        //    _repository.SetupAllProperties();

        //    repository = new ClaimsRepository(_repository.Object);
        //}

        private readonly IClaimsRepository _repository;
        //public ClaimsRepositoryWithRepository(IClaimsRepository repository)
        //{
        //    _repository = repository;
        //}

        [Fact]
        public void AddClaims()
        {
            //Mock<FSAInfoContext> mockContext = new Mock<FSAInfoContext>();
            //var sut = new ClaimsRepository(mockContext.Object);

            //var empId = 1;
            //var claims = new Claims { ID = 1 };

            //var decision = sut.AddClaims(empId, claims);

            //Assert.Contains()
        }

        [Fact]
        public async void EmployeeExistAsync_ExistTest()
        {
            //Mock<IClaimsRepository> repository = new Mock<IClaimsRepository>();
            //int empId = 1;
            //repository.SetupAllProperties();
            //repository.Setup(a => a.EmployeeExistAsync(It.IsAny<int>())).Returns(Task.FromResult(true));

            //var option = new DbContextOptions<FSAInfoContext>();
            //var context = new Mock<FSAInfoContext>(option);
            ////var emp = new Employee { Id = 1 };
            //var emp = new Mock<Employee>();
            //context.Setup(x => x.Set<Employee>().Add(It.IsAny<Employee>())).Returns(new Employee { Id = 1 });
            //var repo = new ClaimsRepository(context.Object);

            //var result = repo.EmployeeExistAsync(empId);

            //Assert.Equal(result, Task.FromResult(true));

            // Arrange
            //var repositoryMock = new Mock<IClaimsRepository>();
            //repositoryMock
            //    .Setup(r => r.EmployeeExistAsync(1))
            //    .Returns(Task.FromResult(true));

            //var controller = new ClaimsRepositoryTest(repositoryMock.Object);

            //// Act
            //var blog = controller.("Blog2");

            //// Assert
            //repositoryMock.Verify(r => r.GetBlogByName("Blog2"));
            //Assert.Equal("http://blog2.com", blog.Url);

        }
    }
}