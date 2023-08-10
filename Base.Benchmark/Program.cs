

using BenchmarkDotNet.Running;
using BenchmarkService.Services;

namespace BenchmarkService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<UserServiceBenchmarks>();
        }
    }
}
