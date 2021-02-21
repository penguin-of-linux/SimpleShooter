using Core.MapDto;
using UnityEngine;

namespace EntityFactoryDto
{
    public class EntityCreateOptions
    {
        public EntityType EntityType { get; set; }
        public Team Team { get; set; }
        public Vector2 Position { get; set; }
    }
}