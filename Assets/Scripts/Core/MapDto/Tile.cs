using DefaultNamespace.Core.Graph;
using DefaultNamespace.Core.MapDto.Tiles;
using UnityEngine;

namespace DefaultNamespace.Core.MapDto
{
    public abstract class Tile : INode
    {
        protected Tile(Vector2Int cords)
        {
            Cords = cords;
        }

        protected Tile(int x, int y)
        {
            Cords = new Vector2Int(x, y);
        }

        public abstract bool Passable { get; }
        public abstract TileType Type {get;}

        public Vector2Int Cords { get; }

        public override bool Equals(object obj)
        {
            return obj is Tile tile && Cords.Equals(tile.Cords);
        }

        public override int GetHashCode()
        {
            return Cords.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Cords.x}, {Cords.y})";
        }
    }
}