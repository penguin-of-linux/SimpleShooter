using Components;
using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var target = other.GetComponent<HealthComponent>();
            if (target != null)
            {
                gameStateController.DestroyEntity(target.gameObject);
            }
            Destroy(gameObject);
        }

        private GameStateController gameStateController;
    }
}