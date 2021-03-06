using System;
using Core.MapDto;
using UnityEngine;
using Tile = UnityEngine.Tilemaps.Tile;

public static class ResourceLoader
{
    public static Texture2D GetCrossHairTexture()
    {
        return crossHair;
    }

    public static GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }
    
    public static GameObject GetHealthBarPrefab()
    {
        return healthBarPrefab;
    }

    public static Tile GetTile(TileType type, System.Random random = null)
    {
        switch (type)
        {
            case TileType.Brick:
                return brick;
            case TileType.Sand:
                var number = random?.Next(64) ?? 0;
                return sand[number];
            default:
                throw new NotSupportedException($"Unknown tile type {type}");
        }
    }
        
    public static GameObject GetShooterPrefab()
    {
        return shooterPrefab;
    }

    public static GameObject GetBoxPrefab()
    {
        return boxPrefab;
    }
    
    public static GameObject GetMedicPrefab()
    {
        return medicPrefab;
    }

    public static void Initialize()
    {
        InitializeTiles();
            
        crossHair = Resources.Load<Texture2D>("crosshair");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet/bullet");
        
        healthBarPrefab = Resources.Load<GameObject>("Prefabs/HealthBar");
            
        shooterPrefab = Resources.Load<GameObject>("Prefabs/Units/Shooter");
        medicPrefab = Resources.Load<GameObject>("Prefabs/Units/Medic/Medic");
        
        boxPrefab = Resources.Load<GameObject>("Prefabs/Box/Box");
    }
        
    private static void InitializeTiles()
    {
        sand = new Tile[64];
        for (var i = 0; i < 64; i++)
        {
            sand[i] = Resources.Load<Tile>($"Tilemap/hard_sand/hard_sand_{i}");
        }
            
        brick = Resources.Load<Tile>("Tilemap/wall/brick");
    }

    private static Tile[] sand;
    private static Tile brick;

    private static Texture2D crossHair;
    private static GameObject bulletPrefab;
    private static GameObject healthBarPrefab;

    private static GameObject shooterPrefab;
    private static GameObject medicPrefab;
    
    private static GameObject boxPrefab;
}