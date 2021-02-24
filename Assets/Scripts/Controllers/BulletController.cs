using Controllers.UnitsController;
using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var target = other.GetComponent<EntityBaseController>();
            if (target != null)
            {
                var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
                if (gameController != null)
                {
                    var map = gameController.Map;

                    var entity = map.Entities[target.Entity.Id];
                    entity.Health -= 50;
                    if (entity.Health <= 0)
                    {
                        Destroy(target.gameObject);
                        map.Entities.Remove(target.Entity.Id);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}