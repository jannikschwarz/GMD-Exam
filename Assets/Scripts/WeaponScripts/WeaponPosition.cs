using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{

    public sealed class WeaponPosition
    {
        private static WeaponPosition instance = null;
        private static readonly object padlock = new object();
        private List<float[]> positions1;
        private List<float[]> positions2;
        private List<float[]> positions3;
        private List<int> takenPositions;

        WeaponPosition()
        {
            SetupPositions();
        }

        private void SetupPositions()
        {
            positions1 = new List<float[]>
            {
                new float[2]{-12.47f,-1.65f},
                new float[2]{-14.57f,2.61f},
                new float[2]{-2.74f,-4.5f},
                new float[2]{6.13f,0.06f}
            };

            positions2 = new List<float[]>
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

            takenPositions = new List<int>();
        }

        public static WeaponPosition Instance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new WeaponPosition();
                }
                return instance;
            }
        }

        public float[] getRandomPosition(int mapNumber)
        {
            int numberOfAvailablePositions = takenPositions.Count;
            int random;

            if (numberOfAvailablePositions == 0)
            {
                random = UnityEngine.Random.Range(0,4);
                takenPositions.Add(random);
                switch (mapNumber)
                {
                    case (1):
                        return positions1[random];
                    case (2):
                        return positions2[random];
                    case (3):
                        return positions3[random];
                    default:
                        return null;
                }
            }else if(numberOfAvailablePositions == positions1.Count)
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
                        switch (mapNumber)
                        {
                            case (1):
                                return positions1[random];
                            case (2):
                                return positions2[random];
                            case (3):
                                return positions3[random];
                            default:
                                return null;
                        }
                    }
                }
            }
        }
    }
}
