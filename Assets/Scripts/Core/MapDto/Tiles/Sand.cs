using UnityEngine;

namespace DefaultNamespace.Core.MapDto.Tiles
{
    public class Sand : Tile
    {
        public Sand(Vector2Int cords) : base(cords)
        {
        }

        public Sand(int x, int y) : base(x, y)
        {
        }

        public override bool Passable => true;
        public override TileType Type => TileType.Sand;
    }
}