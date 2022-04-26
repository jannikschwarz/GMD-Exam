using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PlayerSpawnPosition
{
    private static List<float[]> positionsMap1;
    private static List<float[]> positionsMap2;
    private static readonly object padlock = new object();
    private static PlayerSpawnPosition instance = null;

    PlayerSpawnPosition()
    {
        SetupPositions();
    }

    public static PlayerSpawnPosition getInstance()
    {
        lock (padlock)
        {
            if(instance == null)
            {
                instance = new PlayerSpawnPosition();
            }
            return instance;
        }
    }

    private void SetupPositions()
    {
        positionsMap1 = new List<float[]>
            {
                new float[2]{-16.76f, -5.18f},
                new float[2]{ -18.23f, 8.45f },
                new float[2]{ 12.89f, 8.15f },
                new float[2]{ 11.791f, -5.187f }
            };

        positionsMap2 = new List<float[]>
        {
            new float[2]{-20.31f,-3.96f},
            new float[2]{-20.31f,7.07f},
            new float[2]{ 9.57f, 6.079f},
            new float[2]{ 7.87f, -3.909f}
        };
    }

    public List<float[]> getMapOnePositions()
    {
        return positionsMap1;
    }

    public List<float[]> getMapTwoPositions()
    {
        return positionsMap2;
    }
}
