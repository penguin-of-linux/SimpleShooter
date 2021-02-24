using System;
using Controllers.UnitsController;
using Core.MapDto.MapObjects;
using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour, IHaveMapObjectId
    {
        // Host id
        public Guid MapObjectId { get; set; }
        
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
                    var host = (ShootUnit)map.Entities[MapObjectId];
                    entity.Health -= host.Damage;
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