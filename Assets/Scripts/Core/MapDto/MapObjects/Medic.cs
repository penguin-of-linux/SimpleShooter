namespace Core.MapDto.MapObjects
{
    public class Medic : Unit
    {
        public int HealingPower { get; set; }
        public int HealingRadius { get; set; }
        
        public Medic()
        {
            HealingPower = 2;
            HealingRadius = 1;
        }
    }
}