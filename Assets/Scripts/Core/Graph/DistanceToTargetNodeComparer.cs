using System.Collections.Generic;
using DefaultNamespace.Core.MapDto;
using UnityEngine;

namespace DefaultNamespace.Core.Graph
{
    public class DistanceToTargetNodeComparer : IComparer<Tile>
    {
        private readonly Vector2Int target;

        public DistanceToTargetNodeComparer(Tile target)
        {
            this.target = target.Cords;
        }

        public int Compare(Tile x, Tile y)
        {
            return GetDistance(x.Cords).CompareTo(GetDistance(y.Cords));
        }

        private float GetDistance(Vector2Int vec)
        {
            return (target - vec).magnitude;
        }
    }
}