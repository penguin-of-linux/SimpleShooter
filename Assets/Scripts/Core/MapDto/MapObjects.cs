using System;
using System.Collections.Generic;
using System.Linq;
using Core.MapDto.MapObjects;

namespace Core.MapDto
{
    public partial class Map
    {
        public volatile Dictionary<Guid, Entity> Entities = new Dictionary<Guid, Entity>();
        public IEnumerable<Unit> Units => Entities.Values.Where(x => x is Unit).Cast<Unit>();
    }
}