using System;
using System.Collections.Generic;
using DefaultNamespace.Core.MapDto.MapObjects;

namespace DefaultNamespace.Core.MapDto
{
    public partial class Map
    {
        public volatile Dictionary<Guid, Unit> Units = new Dictionary<Guid, Unit>();
        //public volatile Player Player;
    }
}