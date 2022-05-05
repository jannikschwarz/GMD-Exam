using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerInfo
    {
        public int _hitPoints { get; set; }
        public int _playerNumber { get; set; }
        public string _playerName { get; set; }
        public int _deathCount { get; set; }

        public PlayerInfo(int playerNumber, string playerName = "")
        {
            _playerNumber = playerNumber;
            _playerName = playerName;
            _deathCount = 0;
        }

        public bool Hit(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0) return true; _deathCount++;
            return false;
        }
    }
}
