using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{

    public sealed class WeaponPosition
    {
        private static WeaponPosition instance = null;
        private static readonly object padlock = new object();
        private List<float[]> positions;
        private List<int> takenPositions;
        public bool retrievedPositions { get; set; }

        WeaponPosition(int mapNumber)
        {
            SetupPositions(mapNumber);
            retrievedPositions = false;
        }

        private void SetupPositions(int mapNumber)
        {
            if(mapNumber == 1)
            {
                positions = new List<float[]>
                {
                    new float[2]{-12.47f,-1.65f},
                    new float[2]{-14.57f,2.61f},
                    new float[2]{-2.74f,-4.5f},
                    new float[2]{6.13f,0.06f}
                };
            }else if(mapNumber == 2)
            {
                positions = new List<float[]>
                {
                    new float[2]{-12.63f, -0.41f},
                    new float[2]{-12.21f, 4.6f},
                    new float[2]{-4.53f, 3.6f},
                    new float[2]{-4.53f, -2.41f},
                    new float[2]{-10.21f, -3.35f},
                    new float[2]{-4.43f, 6.64f},
                    new float[2]{-0.3f, 2.61f},
                    new float[2]{0.91f, -3.34f}
                };
            }
            takenPositions = new List<int>();
        }

        public static WeaponPosition Instance(int mapNumber)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new WeaponPosition(mapNumber);
                }
                return instance;
            }
        }

        public float[] getRandomPosition()
        {
            int range = positions.Count;
            int random;
            while (true)
            {
                random = UnityEngine.Random.Range(0, range);
                if (!takenPositions.Contains(random))
                {
                    takenPositions.Add(random);
                    return positions[random];
                }
            }
        }
    }
}
