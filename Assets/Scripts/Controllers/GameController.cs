using Core.Generation;
using Core.MapDto;
using EntityFactoryDto;
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
            
            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();
            
            var map = ConstantMapGenerator.GenerateSimple();
            Map = map;
            RenderMap(map);
            var player = EntityFactory.CreatePlayer(new EntityCreateOptions
            {
                Position = new Vector2(15, 5),
                Team = Team.Neutral,
                EntityType = EntityType.Medic,
                Damage = 20,
                Health = 100,
                HealingPower = 5,
                HealingRadius = 3
            });
            gameStateController.AddEntity(player);
        }

        void Start()
        {
            var crossHair = ResourceLoader.GetCrossHairTexture();
            Cursor.SetCursor(crossHair, new Vector2(crossHair.width / 2, crossHair.height / 2), CursorMode.ForceSoftware);
        }

        void FixedUpdate()
        {
            HandleKeyboard();
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
        }

        private GameObject player;
        private GameStateController gameStateController;

        [SerializeField] private Tilemap path;
        [SerializeField] private Tilemap block;
    }
}