

using Base.Core;
using Base.Services;
using BenchmarkDotNet.Attributes;

namespace BenchmarkService.Services
{
    [MemoryDiagnoser]
    public class UserServiceBenchmarks
    {
        private DataContext context; // Initialize your database context
        private UserService userService;

        public UserServiceBenchmarks()
        {
            context = new DataContext(); // Initialize your database context
            userService = new UserService(context); // Initialize an instance of your UserService class
        }

        [Benchmark]
        public bool CheckPermissionS1Benchmark()
        {
            return userService.CheckPermissionActionS1(1, "users"); // Call your UpdateLoginTime method
        }

        [Benchmark]
        public bool CheckPermissionS2Benchmark()
        {
            return userService.CheckPermissionActionS2(1, "users"); // Call your UpdateLoginTime method
        }
    }
}
