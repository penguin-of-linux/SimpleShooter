namespace Core.MapDto.MapObjects
{
    public class ShootUnit : Unit
    {
        public int Damage { get; set; }
        
        public ShootUnit()
        {
            Damage = 10;
        }
    }
}