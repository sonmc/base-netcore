

using Base.Core;
using Base.Services;
using BenchmarkDotNet.Attributes;

namespace BenchmarkService.Services
{
    [MemoryDiagnoser]
    public class UserServiceBenchmarks
    {
        private DataContext context;
        private UserService userService;

        public UserServiceBenchmarks()
        {
            context = new DataContext();
            userService = new UserService(context);
        }

        [Benchmark]
        public bool CheckPermissionS1Benchmark()
        {
            int userId = 1;
            string module = "CrudUserCtrl";
            string action = "users";
            return userService.CheckPermission(userId, module, action);
        }

    }
}
