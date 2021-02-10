using Core.MapDto;
using DefaultNamespace.Core.MapDto;
using DefaultNamespace.Core.MapDto.MapObjects;
using DefaultNamespace.Core.MapDto.Tiles;
using UnityEngine;

namespace DefaultNamespace.Core.Generation
{
    public class ConstantMapGenerator
    {
        public static Map GenerateFilled(int width, int height)
        {
            var tiles = new Tile[width, height];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                tiles[x, y] = new Sand(new Vector2Int(x, y));

            return new Map(tiles);
        }

        public static Map GenerateSimple()
        {
            var width = 20;
            var height = 15;
            
            var map = ConstantMapGenerator.GenerateFilled(width, height);
            
            FillBricks(map);
            AddPlayer(map);
            AddBots(map);

            return map;
        }

        private static void FillBricks(Map map)
        {
            var width = map.Width;
            var height = map.Height;
            
            for (var i = 0; i < width; i++)
            {
                map[i, 0] = new Brick(i, 0);
                map[i, height - 1] = new Brick(i, height - 1);
            }

            for (var i = 0; i < height; i++)
            {
                map[0, i] = new Brick(0, i);
                map[width - 1, i] = new Brick(width - 1, i);
            }
            
            for (var i = 1; i < 8; i++)
            {
                map[i, 5] = new Brick(i, 5);
                map[width - 1 - i, height - 5 - 1] = new Brick(width - 1 - i, height - 5 - 1);
                map[5, height - i - 1] = new Brick(5, height - i - 1);
                map[width - 1 - 5 - 1, i] = new Brick(width - 1 - 5 - 1, height - i);
            }

            for (var j = 1; j < 4; j++)
            {
                map[2, j] = new Brick(2, j);
                map[width - 2 - 1, height - j - 1] = new Brick(width - 2 - 1, height - j - 1);
                map[j, height - 1 - 2] = new Brick(width - 2 - 1, height - 1 - 2);
                map[width - j - 1, 2] = new Brick(width - j - 1, 2);
            }
        }

        private static void AddPlayer(Map map)
        {
            var player = new Player
            {
                Cords = new Vector2(15, 5),
                Team = Team.Neutral
            };
            map.Units[player.Id] = player;
        }

        private static void AddBots(Map map)
        {
            
            for (var i = 0; i < 0; i++)
            {
                var bot = new Bot
                {
                    Cords = new Vector2(9.5f, 8.5f),
                    Team = Team.Blue
                };
                map.Units[bot.Id] = bot;
            }
        }
    }
}