using System;
using Core.MapDto;
using UnityEngine;

namespace Controllers
{
    public class TargetController : MonoBehaviour, IHaveMapObjectId
    {
        public Guid MapObjectId { get; set; }

        void Start()
        {
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;
        }
        
        public void Hit()
        {
            Destroy(gameObject);
            map.Units.Remove(MapObjectId);
        }
        
        private Map map;
        
    }
}