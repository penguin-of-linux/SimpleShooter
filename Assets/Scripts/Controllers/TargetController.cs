using System;
using DefaultNamespace.Core.MapDto;
using UnityEngine;

namespace DefaultNamespace
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
            
            //if (MapObjectId == map.Player.Id)
            //    map.Player.Dead = true;
            //else
                map.Units.Remove(MapObjectId);
        }
        
        private Map map;
        
    }
}