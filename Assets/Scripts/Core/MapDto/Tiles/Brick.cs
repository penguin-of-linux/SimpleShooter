using UnityEngine;

namespace Core.MapDto.Tiles
{
    public class Brick : Tile
    {
        public Brick(Vector2Int cords) : base(cords)
        {
        }

        public Brick(int x, int y) : base(x, y)
        {
        }

        public override bool Passable => false;
        public override TileType Type => TileType.Brick;
    }
}