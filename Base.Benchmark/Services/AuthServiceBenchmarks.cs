using Base.Core;
using Base.Services;
using BenchmarkDotNet.Attributes;

namespace Base.Benchmark.Services
{
    public class AuthServiceBenchmarks
    {
        private DataContext context;
        private UserService userService;

        public AuthServiceBenchmarks()
        {
            context = new DataContext();
            userService = new UserService(context);
        }

        [Benchmark]
        public void CheckPermissionS1Benchmark()
        {
            int userId = 1;
            userService.UpdateLoginTime(userId);
        }
    }
}