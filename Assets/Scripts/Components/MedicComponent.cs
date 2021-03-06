using System;
using Controllers;
using UnityEngine;

namespace Components
{
    public class MedicComponent : MonoBehaviour
    {
        public int HealingRadius { get; set; }
        public int HealingPower { get; set; }

        private void Start()
        {
            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();
        }

        private void FixedUpdate()
        {
            var now = DateTime.Now;
            if (lastHealingTime + healthPeriod < now)
            {
                foreach (var entity in gameStateController.Entities)
                {
                    if ((entity.transform.position - transform.position).ToVector2().magnitude < HealingRadius)
                    {
                        var healthComponent = entity.GetComponent<HealthComponent>();
                        healthComponent.Health = Math.Min(100, healthComponent.Health + HealingPower);
                    }
                }

                lastHealingTime = now;
            }
        }

        private readonly TimeSpan healthPeriod = TimeSpan.FromSeconds(0.5);

        private DateTime lastHealingTime;
        private GameStateController gameStateController;
    }
}