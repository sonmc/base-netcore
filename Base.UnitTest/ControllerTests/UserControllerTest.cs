
using Base.Application.UseCases;
using Base.Services.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Base.UnitTest.ControllerTest
{
    public class UserControllerTest
    {
        [Fact]
        //naming convention MethodName_Condition_ExpectedBehavior
        public async Task GetAll_WithCorrectCondition_ShouldReturn200Status()
        {
            /// Arrange
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(_ => _.Users.FindAll()).Returns(UserMockData.GetSampleUser());
            var sut = new CrudUserCtrl(null);

            /// Act
            var result = (OkObjectResult)await sut.List();


            // /// Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
