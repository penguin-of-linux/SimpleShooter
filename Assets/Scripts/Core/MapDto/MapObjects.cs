using System;
using System.Collections.Generic;
using Core.MapDto.MapObjects;

namespace Core.MapDto
{
    public partial class Map
    {
        public volatile Dictionary<Guid, Unit> Units = new Dictionary<Guid, Unit>();
        //public volatile Player Player;
    }
}