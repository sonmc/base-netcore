using Base.Core;
using Base.Services;
using BenchmarkDotNet.Attributes;

namespace Base.Benchmark.Services
{
    public class AuthServiceBenchmarks
    {
        private DataContext context;
        private AuthService authService;

        public AuthServiceBenchmarks()
        {
            context = new DataContext();
            authService = new AuthService(context);
        }

        [Benchmark]
        public void CheckPermissionS1Benchmark()
        {
            int userId = 1;
            authService.UpdateLoginTime(userId);
        }
    }
}