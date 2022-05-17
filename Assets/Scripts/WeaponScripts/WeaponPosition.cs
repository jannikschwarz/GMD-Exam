using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{

    public sealed class WeaponPosition
    {
        private static List<float[]> _positions;
        private static List<int> _takenPositions;
        private static readonly object padlock = new object();
        private static WeaponPosition _instance = null;
        
        public static bool retrievedPositions { get; set; }

        WeaponPosition(int mapNumber)
        {
            SetupPositions(mapNumber);
            retrievedPositions = false;
        }

        public static WeaponPosition Instance(int mapNumber)
        {
            if(_instance == null)
            {
                _instance = new WeaponPosition(mapNumber);
            }
            return _instance;
        }

        private void SetupPositions(int mapNumber)
        {
            if(mapNumber == 1)
            {
                _positions = new List<float[]>
                {
                    new float[2]{ -19.25f, 0.579f},
                    new float[2]{ -7.7f, 1.66f},
                    new float[2]{ -6.125f, 3.625f},
                    new float[2]{ -7.79f, 6.6f },
                    new float[2]{ 9.7f, 4.62f },
                    new float[2]{ 9.7f, 2.59f },
                    new float[2]{ 1.91f, 1.59f },
                    new float[2]{ 2.2f, 8.62f },
                    new float[2]{ -10.38f, -1.422f }
                };
            }else if(mapNumber == 2)
            {
                _positions = new List<float[]>
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
            _takenPositions = new List<int>();
        }

        public static float[] getRandomPosition()
        {
            if (_takenPositions.Count != 6)
            {
                int range = _positions.Count;
                int random;
                while (true)
                {
                    random = UnityEngine.Random.Range(0, range);
                    if (!_takenPositions.Contains(random))
                    {
                        _takenPositions.Add(random);
                        return _positions[random];
                    }
                }
            }
            else return null;
        }

        public static void RemoveWeapon(float[] pistolP)
        {
            int index = _positions.IndexOf(pistolP);
            _takenPositions.Remove(index);
        }
    }
}
