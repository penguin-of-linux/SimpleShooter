using System;
using System.Linq;
using Core.MapDto.MapObjects;

namespace Controllers.UnitsController
{
    public class MedicController : UnitBaseController
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var healer = (Medic) Entity;
            foreach (var unit in map.Units)
            {
                if ((unit.Cords - healer.Cords).magnitude < healer.HealingRadius)
                {
                    unit.Health = Math.Min(100, unit.Health + healer.HealingPower);
                }
            }
        }
    }
}