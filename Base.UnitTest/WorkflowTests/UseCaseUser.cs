using Base.Application.UseCase.User.Crud;
using Base.Services.Base;
using Base.Utils;
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
            var flow = new CrudUserFlow(new UnitOfWork());

            /// Act
            var result = flow.List();

            // /// Assert
            Assert.NotNull(result);
            Assert.Equal(Message.SUCCESS, result.Status);
            Assert.NotNull(result.Result);
        }
    }
}
