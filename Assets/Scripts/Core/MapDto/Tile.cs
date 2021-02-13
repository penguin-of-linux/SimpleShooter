using Core.Graph;
using UnityEngine;

namespace Core.MapDto
{
    public abstract class Tile : INode
    {
        public abstract bool Passable { get; }
        public abstract TileType Type {get;}

        public Vector2Int Cords { get; }

        protected Tile(Vector2Int cords)
        {
            Cords = cords;
        }

        protected Tile(int x, int y)
        {
            Cords = new Vector2Int(x, y);
        }
        
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