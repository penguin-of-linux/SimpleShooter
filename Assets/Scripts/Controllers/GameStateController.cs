using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class GameStateController : MonoBehaviour
    {
        public bool AddEntity(GameObject entity)
        {
            entities[entity.GetInstanceID()] = entity;
            return entities.TryAdd(entity.GetInstanceID(), entity);
        }

        public bool DestroyEntity(GameObject entity)
        {
            Destroy(entity);
            foreach(var disposable in entity.GetComponents<IDisposable>())
                disposable.Dispose();
            return entities.TryRemove(entity.GetInstanceID(), out entity);
        }

        public bool ContainsEntity(int id)
        {
            return entities.ContainsKey(id);
        }

        public ICollection<GameObject> Entities => entities.Values;
    
        private ConcurrentDictionary<int, GameObject> entities { get; set; } = new ConcurrentDictionary<int, GameObject>();
    }
}
