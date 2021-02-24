using System;
using UnityEngine;

namespace Core.MapDto.MapObjects
{
    public abstract class Entity
    {
        public readonly Guid Id;
        public Vector2 Cords;
        public int Health;
        
        protected Entity()
        {
            Health = 100;
            Id = Guid.NewGuid();
        }
    }
}