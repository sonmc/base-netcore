using Base.Core.Schemas;

namespace Base.UnitTest.ServiceTest.MockData
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
                    GroupIds = "[1]",
                    Password = "123456",
                }
            };
            return output;
        }
    }
}
