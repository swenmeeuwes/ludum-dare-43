using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerColors
{
    public List<PlayerColorPair> Colors = new List<PlayerColorPair>();

    public PlayerColorPair GetRandomColorPair()
    {
        if (Colors.Count == 0)
        {
            return new PlayerColorPair
            {
                AliveColor = Color.magenta,
                DeadColor = Color.red
            };
        }

        return Colors[UnityEngine.Random.Range(0, Colors.Count)];
    }

    [Serializable]
    public class PlayerColorPair
    {
        public Color AliveColor;
        public Color DeadColor;
    }
}
