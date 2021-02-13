using System;
using System.Diagnostics;
using Core.Generation;
using Core.MapDto.Tiles;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BenchmarkMapTests
    {
        private TimeSpan Test(Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            action();

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        [Ignore("")]
        [Test]
        public void BenchmarkTest()
        {
            var map = ConstantMapGenerator.GenerateFilled(100, 100);
            var random = new Random();
            /*for (var i = 500_00000; i > 0; i--)
            {
                var x = random.Next(9999);
                var y = random.Next(9999);
                map[x, y] = new Brick(x, y);
            }*/
            var start = new Sand(0, 0);
            var end = new Sand(99, 99);

            Console.WriteLine(Test(() => map.GetPath(start, end)));
        }
    }
}