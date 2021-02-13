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
            foreach (var kvp in units)
            {
                if (Map.Units.ContainsKey(kvp.Key))
                    Map.Units[kvp.Key].Cords = kvp.Value.transform.position.AsVector2();
                else
                    keysToDelete.Add(kvp.Key);
            }

            foreach (var key in keysToDelete)
                units.Remove(key);

            foreach (var kvp in Map.Units)
            {
                if (!units.ContainsKey(kvp.Key))
                {
                    var unit = CreateUnit(kvp.Value);
                    units[kvp.Key] = unit;
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

            units = new Dictionary<Guid, GameObject>();
            foreach (var unit in map.Units.Values)
            {
                var unitGameObject = CreateUnit(unit);
                units[unit.Id] = unitGameObject;
            }
        }

        private GameObject CreateUnit(Unit unit)
        {
            var unitGameObject = Instantiate(GetUnitPrefab(unit));
            unitGameObject.transform.position = unit.Cords;
            
            unitGameObject.GetComponent<SpriteRenderer>().color = ColorHelper.GetTeamColor(unit.Team);
            
            foreach(var component in unitGameObject.GetComponents<IHaveMapObjectId>())
                component.MapObjectId = unit.Id;

            return unitGameObject;
        }

        private GameObject GetUnitPrefab(Unit unit)
        {
            if (unit is Bot) 
                return ResourceLoader.GetBotPrefab();
        
            if (unit is Player) 
                return ResourceLoader.GetPlayerPrefab();

            return null;
        }
    
        private GameObject player;
        private Dictionary<Guid, GameObject> units;

        [SerializeField] private Tilemap path;
        [SerializeField] private Tilemap block;
    }
}