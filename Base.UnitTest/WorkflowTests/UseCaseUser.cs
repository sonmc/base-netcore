using Base.App.UseCases;
using Base.Services;
using Base.UnitTest.ControllerTest;
using Base.Utils;
using Moq;
using Xunit;

namespace Base.UnitTest.WorkflowTest
{
    public class UseCaseUser
    {
        // CrudUserFlow
        [Fact]
        //naming convention MethodName_Condition_ExpectedBehavior
        public async Task GetAll_WithCorrectCondition_ResultDataAndStatusSuccess()
        {
            /// Arrange
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(_ => _.Users.FindAll()).Returns(UserMockData.GetSampleUser());
            var flow = new CrudUserFlow((IUnitOfWork)uow);

            /// Act
            var result = flow.List();

            // /// Assert
            Assert.NotNull(result);
            Assert.Equal(Message.SUCCESS, result.Status);
            Assert.NotNull(result.Result);
        }
    }
}
