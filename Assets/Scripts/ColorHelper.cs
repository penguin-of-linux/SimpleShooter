using System;
using Core.MapDto;
using UnityEngine;

namespace DefaultNamespace
{
    public class ColorHelper
    {
        public static Color GetTeamColor(Team team)
        {
            switch (team)
            {
                case Team.Blue:
                    return new Color(0,0, 128f);
                case Team.Red:
                    return new Color(128f,0, 0);
                case Team.Neutral:
                    return Color.gray;
                default:
                    throw new ArgumentException();
            }
        }
    }
}