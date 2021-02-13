using System.Collections.Generic;
using System.Linq;
using Core.Graph;
using UnityEngine;

namespace Core.MapDto
{
    public partial class Map : Graph<Tile>
    {
        public readonly Tile[,] Tiles;

        public Map(Tile[,] tiles)
        {
            Tiles = tiles;
        }

        public int Height => Tiles.GetLength(1);
        public int Width => Tiles.GetLength(0);

        public Tile this[int x, int y]
        {
            set => Tiles[x, y] = value;
            get => Tiles[x, y];
        }

        public Tile this[Vector2Int vec]
        {
            set => Tiles[vec.x, vec.y] = value;
            get => Tiles.ValueAt(vec);
        }

        protected override IEnumerable<Tile> GetNeighbours(Tile node)
        {
            var cords = node.Cords;
            var directNeighbours = new[]
            {
                cords + Vector2Int.up,
                cords + Vector2Int.down,
                cords + Vector2Int.left,
                cords + Vector2Int.right
            };
            var diagonalNeighbours = new[]
            {
                cords + new Vector2Int(1, 1),
                cords + new Vector2Int(-1, 1),
                cords + new Vector2Int(1, -1),
                cords + new Vector2Int(-1, -1)
            };

            foreach (var vec in directNeighbours.Where(CanPass))
                yield return Tiles.ValueAt(vec);

            foreach (var vec in diagonalNeighbours.Where(x => IsDiagonalNeighbour(x, node.Cords)))
                yield return Tiles.ValueAt(vec);
        }

        protected override IComparer<Tile> CreateComparer(params Tile[] args)
        {
            return new DistanceToTargetNodeComparer(args.Single());
        }

        private bool CanPass(Vector2Int vec)
        {
            return vec.x >= 0 && vec.x < Width && vec.y >= 0 && vec.y < Height && Tiles.ValueAt(vec).Passable;
        }

        private bool IsDiagonalNeighbour(Vector2Int end, Vector2Int start)
        {
            return CanPass(end)
                   && Tiles.ValueAt(new Vector2Int(end.x, start.y)).Passable
                   && Tiles.ValueAt(new Vector2Int(start.x, end.y)).Passable;
        }
    }
}