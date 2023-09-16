using Base.Core.Schemas;

namespace Base.UnitTest.ControllerTest
{
    public class UserMockData
    {
        public static List<User> GetSampleUser()
        {
            List<User> output = new List<User>
            {
                new User
                {
                    UserName = "Jhon",
                    Email = "jhon@gmail.com",
                }
            };
            return output;
        }
    }
}
