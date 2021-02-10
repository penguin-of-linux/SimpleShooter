using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.MapDto;
using DefaultNamespace.Core.MapDto;
using DefaultNamespace.Core.MapDto.MapObjects;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    void Start()
    {
        var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
        if (gameController != null) map = gameController.Map;
    }

    void FixedUpdate()
    {
        var now = DateTime.Now;
        if (lastSpawn + generatePeriod < now)
        {
            Generate();
            lastSpawn = now;
        }
    }

    void Generate()
    {
        var redCount = map.Units.Values.Count(x => x.Team == Team.Red);
        var blueCount = map.Units.Values.Count(x => x.Team == Team.Blue);

        var team = blueCount >= redCount ? Team.Red : Team.Blue;
        
        //var team = lastTeam == Team.Blue ? Team.Red : Team.Blue;
        var cords = team == Team.Red 
            ? new Vector2(1.5f, 1.5f) 
            : new Vector2(map.Width - 1.5f, map.Height - 1.5f);
        var bot = new Bot
        {
            Cords = cords,
            Team = team
        };
        map.Units[bot.Id] = bot;

        lastTeam = team;
    }

    private Map map;
    private readonly TimeSpan generatePeriod = TimeSpan.FromSeconds(1);
    private DateTime lastSpawn;
    private Team lastTeam = Team.Blue;
}
