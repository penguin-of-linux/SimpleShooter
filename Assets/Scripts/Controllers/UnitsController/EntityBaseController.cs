using System;
using Core.MapDto;
using Core.MapDto.MapObjects;
using UnityEngine;

namespace Controllers.UnitsController
{
    public abstract class EntityBaseController : MonoBehaviour, IHaveMapObjectId
    {
        public virtual void Start()
        {
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            healthBar = Instantiate(ResourceLoader.GetHealthBarPrefab());

            Entity = map.Entities[MapObjectId];
        }

        public virtual void Update()
        {
            var position = transform.position;
            healthBar.transform.position = new Vector3(position.x, position.y + 0.4f, position.z);
            
            var health = healthBar.transform.GetChild(0);
            health.transform.localScale = new Vector3(Entity.Health / 100f, 1, 1);
        }

        public void OnDestroy()
        {
            Destroy(healthBar);
        }

        private GameObject healthBar;
        
        public Entity Entity { get; set; }
        public Guid MapObjectId { get; set; }
        

        private Map map;
    }
}