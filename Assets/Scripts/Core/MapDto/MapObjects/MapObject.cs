using System;
using UnityEngine;

namespace DefaultNamespace.Core.MapDto.MapObjects
{
    public abstract class MapObject
    {
        public readonly Guid Id;
        public Vector2 Cords;
        
        protected MapObject()
        {
            Id = Guid.NewGuid();
        }
    }
}