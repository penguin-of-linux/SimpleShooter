using Components;
using UnityEngine;

namespace EntityFactoryDto
{
    public static class EntityFactory
    {
        public static GameObject CreatePlayer(EntityCreateOptions options)
        {
            var gameObject = CreateUnit(options);
            gameObject.AddComponent<PlayerKeyboardHandleComponent>();
            gameObject.AddComponent<CameraTargetComponent>();
            gameObject.AddComponent<PlayerMouseRotationComponent>();
            gameObject.AddComponent<MedicComponent>();

            gameObject.GetComponent<MedicComponent>().HealingPower = options.Damage;
            gameObject.GetComponent<MedicComponent>().HealingRadius = options.HealingRadius;

            return gameObject;
        }

        public static GameObject CreateBotShooter(EntityCreateOptions options)
        {
            var gameObject = CreateUnit(options);
            gameObject.AddComponent<ShootComponent>();
            gameObject.AddComponent<BotComponent>();  
            gameObject.AddComponent<CameraTargetComponent>();  
            

            gameObject.GetComponent<ShootComponent>().Damage = options.Damage;

            return gameObject;
        }
        
        private static GameObject CreateUnit(EntityCreateOptions options)
        {
            var gameObject = Object.Instantiate(GetUnitPrefab(options.EntityType));
            gameObject.transform.position = options.Position;
            
            gameObject.GetComponent<SpriteRenderer>().color = ColorHelper.GetTeamColor(options.Team);
            gameObject.AddComponent<TransformComponent>();
            gameObject.AddComponent<TeamComponent>();
            gameObject.AddComponent<HealthComponent>();

            gameObject.GetComponent<TeamComponent>().Team = options.Team;
            gameObject.GetComponent<HealthComponent>().Health = options.Health;

            return gameObject;
        }

        private static GameObject GetUnitPrefab(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.Shooter:
                    return ResourceLoader.GetShooterPrefab();
                case EntityType.Medic:
                    return ResourceLoader.GetMedicPrefab();
            }

            return null;
        }
    }
}