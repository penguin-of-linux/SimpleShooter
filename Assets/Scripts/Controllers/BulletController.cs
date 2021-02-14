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
            var target = other.GetComponent<UnitBaseController>();
            if (target is PlayerController)
                ;
            if (target != null)
            {
                var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
                if (gameController != null)
                {
                    var map = gameController.Map;
                
                    Destroy(target.gameObject);
                    map.Units.Remove(target.Unit.Id);
                }
            }
            Destroy(gameObject);
        }
    }
}