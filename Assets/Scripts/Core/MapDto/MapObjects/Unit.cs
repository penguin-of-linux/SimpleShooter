using Core.MapDto;

namespace DefaultNamespace.Core.MapDto.MapObjects
{
    public abstract class Unit : MapObject
    {
        public Team Team { get; set; }
    }
}