using System;
using System.Linq;
using Components;
using Core.MapDto;
using EntityFactoryDto;
using UnityEngine;

namespace Controllers
{
    public class SpawnController : MonoBehaviour
    {
        void Start()
        {
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();
        }

        void FixedUpdate()
        {
            var now = DateTime.Now;
            if (count < 100 && lastSpawn + generatePeriod < now)
            {
                Generate();
                lastSpawn = now;
                count++;
            }
        }

        void Generate()
        {
            var rnd = new System.Random();

            var team = rnd.Next(2) == 0 ? Team.Blue : Team.Red;
            var cords = team == Team.Red 
                ? new Vector2(1.5f, 1.5f) 
                : new Vector2(map.Width - 1.5f, map.Height - 1.5f);
            var bot = EntityFactory.CreateBotShooter(new EntityCreateOptions
            {
                EntityType = EntityType.Shooter,
                Position = cords,
                Team = team,
                Damage = 20,
                Health = 100
            });
            gameStateController.AddEntity(bot);
        }

        private int count = 0;
        private GameStateController gameStateController;
        private Map map;
        private readonly TimeSpan generatePeriod = TimeSpan.FromSeconds(0.1);
        private DateTime lastSpawn;
    }
}
