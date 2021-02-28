using Components;
using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        public int Damage { get; set; }
        
        private void Start()
        {
            Destroy(gameObject, 5f);
            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var healthComponent = other.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                var shootComponent = other.GetComponent<ShootComponent>();
                healthComponent.Health -= Damage;
                if (healthComponent.Health <= 0)
                    gameStateController.DestroyEntity(healthComponent.gameObject);
            }
            Destroy(gameObject);
        }

        private GameStateController gameStateController;
    }
}