namespace Core.MapDto.MapObjects
{
    public abstract class Unit : Entity
    {
        public Team Team { get; set; }
        public int Damage { get; set; }

        protected Unit()
        {
            Damage = 50;
        }
    }
}