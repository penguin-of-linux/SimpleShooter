using System.Linq;
using DefaultNamespace.Core.Generation;
using DefaultNamespace.Core.MapDto.Tiles;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace DefaultNamespace.Tests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Empty()
        {
            var map = ConstantMapGenerator.GenerateFilled(10, 10);

            var path = map.GetPath(map[1, 1], map[1, 1]);

            path.Should().BeEmpty();
        }

        /*
         |_0|__|__|__|_1|
         |__|__|__|__|__|
         |__|__|__|__|__|
         */
        [Test]
        public void Simple1()
        {
            var map = ConstantMapGenerator.GenerateFilled(5, 3);

            var path = map.GetPath(map[0, 0], map[4, 0]);

            path.Length.Should().Be(4);
            var expected = new[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(2, 0),
                new Vector2Int(3, 0),
                new Vector2Int(4, 0)
            };

            path.Select(x => x.Cords).Should().BeEquivalentTo(expected);
        }


        /*
         |_0|__|__|__|__|
         |__|__|__|__|__|
         |__|__|__|__|_1|
         */
        [Test]
        public void Simple2()
        {
            var map = ConstantMapGenerator.GenerateFilled(5, 3);

            var path = map.GetPath(map[0, 0], map[4, 2]);

            path.Length.Should().Be(4);
        }

        /*
         |_0|__|__|__|__|
         |__|__|[]|__|__|
         |__|__|__|__|_1|
         */
        [Test]
        public void Wall1()
        {
            var map = ConstantMapGenerator.GenerateFilled(5, 3);
            map[2, 1] = new Brick(new Vector2Int(2, 1));

            var path = map.GetPath(map[0, 0], map[4, 2]);

            path.Length.Should().Be(5);
        }

        /*
         |_0|__|__|__|__|
         |__|__|[]|[]|__|
         |__|__|[]|[]|_1|
         */
        [Test]
        public void Wall2()
        {
            var map = ConstantMapGenerator.GenerateFilled(5, 3);
            map[2, 1] = new Brick(new Vector2Int(2, 1));
            map[2, 2] = new Brick(new Vector2Int(2, 2));
            map[3, 1] = new Brick(new Vector2Int(3, 1));
            map[3, 2] = new Brick(new Vector2Int(3, 2));

            var path = map.GetPath(map[0, 0], map[4, 2]);
            var expected = new[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(2, 0),
                new Vector2Int(3, 0),
                new Vector2Int(4, 0),
                new Vector2Int(4, 1),
                new Vector2Int(4, 2)
            };

            path.Select(x => x.Cords).Should().BeEquivalentTo(expected);
        }
        
        /*
         |_0|_1|__|__|__|
         |__|__|__|__|__|
         |__|__|__|__|__|
         */
        [Test]
        public void Simple_OneStep()
        {
            var map = ConstantMapGenerator.GenerateFilled(5, 3);

            var path = map.GetPath(map[0, 0], map[1, 0]);
            var expected = new[]
            {
                new Vector2Int(1, 0)
            };

            path.Select(x => x.Cords).Should().BeEquivalentTo(expected);
        }
    }
}