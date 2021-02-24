using System;
using System.Collections.Generic;
using Core.Generation;
using Core.MapDto;
using Core.MapDto.MapObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public Map Map;

        void Awake()
        {
            ResourceLoader.Initialize();
            var map = ConstantMapGenerator.GenerateSimple();
            Map = map;
            RenderMap(map);
        }

        void FixedUpdate()
        {
            HandleKeyboard();

            var keysToDelete = new List<Guid>();
            foreach (var kvp in entities)
            {
                if (Map.Entities.ContainsKey(kvp.Key))
                    Map.Entities[kvp.Key].Cords = kvp.Value.transform.position.AsVector2();
                else
                    keysToDelete.Add(kvp.Key);
            }

            foreach (var key in keysToDelete)
                entities.Remove(key);

            foreach (var kvp in Map.Entities)
            {
                if (!entities.ContainsKey(kvp.Key))
                {
                    var unit = CreateEntity(kvp.Value);
                    entities[kvp.Key] = unit;
                }
            }
        }

        private void HandleKeyboard()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }

        private void RenderMap(Map map)
        {
            var random = new System.Random();
        
            for(var x = 0; x < map.Width; x++)
            for (var y = 0; y < map.Height; y++)
            {
                var tile = ResourceLoader.GetTile(map[x, y].Type, random);
                var tilemap = map[x, y].Type == TileType.Sand ? path : block;
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }

            entities = new Dictionary<Guid, GameObject>();
            foreach (var entity in map.Entities.Values)
            {
                var unitGameObject = CreateEntity(entity);
                entities[entity.Id] = unitGameObject;
            }
        }

        private GameObject CreateEntity(Entity entity)
        {
            GameObject unitGameObject = null;
            if (entity is Unit unit)
            {
                unitGameObject = Instantiate(GetUnitPrefab(unit));

                unitGameObject.GetComponent<SpriteRenderer>().color = ColorHelper.GetTeamColor(unit.Team);
            }

            if (entity is Box)
            {
                unitGameObject = Instantiate(ResourceLoader.GetBoxPrefab());
            }

            if (unitGameObject != null)
            {
                unitGameObject.transform.position = entity.Cords;
                foreach (var component in unitGameObject.GetComponents<IHaveMapObjectId>())
                    component.MapObjectId = entity.Id;
            }
            
            return unitGameObject;
        }

        private GameObject GetUnitPrefab(Unit unit)
        {
            if (unit is Bot) 
                return ResourceLoader.GetBotPrefab();
        
            if (unit is Player) 
                return ResourceLoader.GetMedicPrefab();

            return null;
        }
    
        private GameObject player;
        private Dictionary<Guid, GameObject> entities;

        [SerializeField] private Tilemap path;
        [SerializeField] private Tilemap block;
    }
}