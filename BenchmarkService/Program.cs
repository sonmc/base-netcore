

using BenchmarkDotNet.Running;

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
