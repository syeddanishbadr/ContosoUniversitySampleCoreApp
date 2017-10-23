using Framework.Core;
using Framework.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ServiceLayer.Controllers;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ServiceLayer.xUnitTests
{
    public class StudentControllerTest
    {
        Mock<DataBaseContext> mocDbContext;
        Mock<ILogger<UnitOfWork>> mocILoggerUnitOfWork;
        Mock<IStudentRepository> mocIStudentRespository;
        Mock<UnitOfWork> mocUnitOfWorkRepo;
        Mock<ILogger<StudentController>> mocILoggerStudentController;
        StudentController studentController;

        public StudentControllerTest()
        {
            //Arrange
            mocDbContext = new Mock<DataBaseContext>();
            mocILoggerUnitOfWork = new Mock<ILogger<UnitOfWork>>();
            mocIStudentRespository = new Mock<IStudentRepository>();
            mocUnitOfWorkRepo = new Mock<UnitOfWork>(mocDbContext.Object, mocILoggerUnitOfWork.Object, mocIStudentRespository.Object);
            //mocUnitOfWorkRepo = new Mock<IUnitOfWork>();
            mocILoggerStudentController = new Mock<ILogger<StudentController>>();
            studentController = new StudentController(mocUnitOfWorkRepo.Object, mocILoggerStudentController.Object);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            //Act 
            var result = await studentController.Post(student: null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
