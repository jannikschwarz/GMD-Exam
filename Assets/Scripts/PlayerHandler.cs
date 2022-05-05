using Assets.Scripts.PlayerScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private PlayerInfo _player1;
    private PlayerInfo _player2;
    private PlayerInfo _player3;
    private PlayerInfo _player4;

    public static event Action<int> PlayerDeadAction = delegate { };
    public static event Action<int> PlayerRespawnAction = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        BulletScript.PlayerHitAction += PlayerHit;
        MenuSelection.PlayerSpawnAction += GameStart; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStart(int playerCount, List<string> playerNames)
    {
        _player1 = new PlayerInfo(1,playerNames[0]);
        _player2 = new PlayerInfo(2,playerNames[1]);
        if (playerCount > 2) _player3 = new PlayerInfo(3,playerNames[2]);
        if (playerCount > 3) _player4 = new PlayerInfo(4, playerNames[3]);
    }

    private void PlayerHit(int playerNumber, int damage)
    {
        bool dead;
        switch (playerNumber)
        {
            case (1):
                dead = _player1.Hit(damage);
                if (dead)
                {
                    if(_player1._deathCount == 1)
                    {
                        PlayerDeadAction(1);
                    }
                    else
                    {
                        PlayerRespawnAction(1);
                    }
                }
                break;
            case (2):
                dead = _player2.Hit(damage);
                if (dead)
                {
                    if (_player2._deathCount == 1)
                    {
                        PlayerDeadAction(2);
                    }
                    else
                    {
                        PlayerRespawnAction(2);
                    }
                }
                break;
            case (3):
                dead = _player3.Hit(damage);
                if (dead)
                {
                    if (_player3._deathCount == 1)
                    {
                        PlayerDeadAction(3);
                    }
                    else
                    {
                        PlayerRespawnAction(3);
                    }
                }
                break;
            case (4):
                dead = _player4.Hit(damage);
                if (dead)
                {
                    if (_player4._deathCount == 1)
                    {
                        PlayerDeadAction(4);
                    }
                    else
                    {
                        PlayerRespawnAction(4);
                    }
                }
                break;
        }
    }
}
