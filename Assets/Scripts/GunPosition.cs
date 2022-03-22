using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{

    public sealed class GunPosition
    {
        private static GunPosition instance = null;
        private static readonly object padlock = new object();
        public List<float[]> positions;
        public List<int> takenPositions;

        GunPosition()
        {
            SetupPositions();
        }

        private void SetupPositions()
        {
            positions = new List<float[]>
            {
                new float[2]{-12.47f,-1.65f},
                new float[2]{-14.57f,2.61f},
                new float[2]{-2.74f,-4.5f},
                new float[2]{6.13f,0.06f}
            };

            takenPositions = new List<int>();
        }

        public static GunPosition Instance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GunPosition();
                }
                return instance;
            }
        }

        public float[] getRandomPosition()
        {
            int numberOfAvailablePositions = takenPositions.Count;
            int random;

            if (numberOfAvailablePositions == 0)
            {
                random = UnityEngine.Random.Range(0,4);
                takenPositions.Add(random);
                return positions[random];
            }else if(numberOfAvailablePositions == positions.Count)
            {
                throw (new Exception("No more spots left in positions"));
            }
            else
            {
                while (true)
                {
                    random = UnityEngine.Random.Range(0, 4);
                    if (!takenPositions.Contains(random))
                    {
                        takenPositions.Add(random);
                        return positions[random];
                    }
                }
            }
        }
    }
}
