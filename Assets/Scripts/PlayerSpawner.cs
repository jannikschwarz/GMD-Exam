using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;
    private PlayerSpawnPosition _spawn;
    private List<float[]> _positions;
    private List<int> _haveRespawned;
    private List<int> _playerOut;

    // Start is called before the first frame update
    void Start()
    {
        _spawn = PlayerSpawnPosition.getInstance();
        PlayerInfo.MapDeath += RespawnPlayer;
        PlayerInfo.KilledByPlayer += KilledByPlayer;
        _haveRespawned = new List<int>();
        _playerOut = new List<int>();
    }

    public void PlayerSpawn(int playerCount, int mapNumber, List<string> playerNames)
    {
        _positions = new List<float[]>();
        switch (mapNumber)
        {
            case (1):
                _positions = _spawn.getMapOnePositions();
                break;
            case (2):
                _positions = _spawn.getMapTwoPositions();
                break;

        }
        if (!PlayerSpawnPosition.playersSpawned) {
            var playerOne = Instantiate(player1, new Vector2(_positions[0][0], _positions[0][1]), Quaternion.identity);
            playerOne.SendMessage("PlayerName", playerNames[0]);
            var playerTwo = Instantiate(player2, new Vector2(_positions[1][0], _positions[1][1]), Quaternion.identity);
            playerTwo.SendMessage("PlayerName", playerNames[1]);

            if (playerCount > 2)
            {
                var playerThree = Instantiate(player3, new Vector2(_positions[2][0], _positions[2][1]), Quaternion.identity);
                playerThree.SendMessage("PlayerName", playerNames[2]);
            }

            if (playerCount > 3)
            {
                var playerFour = Instantiate(player4, new Vector2(_positions[3][0], _positions[3][1]), Quaternion.identity);
                playerFour.SendMessage("PlayerName", playerNames[3]);
            }
            PlayerSpawnPosition.playersSpawned = true;
        }
    }

    public static void Reset()
    {
        PlayerSpawnPosition.playersSpawned = false;
    }

    private void KilledByPlayer(int playerKilled, int killer)
    {
        RespawnPlayer(playerKilled);
    }

    private void RespawnPlayer(int playerNumber)
    {
        if (PlayerSpawnPosition.playersSpawned)
        {
            int random = UnityEngine.Random.Range(0, 4);
            float[] position = _positions[random];
            if(playerNumber == 1 && player1 != null)
            {
                if (!_haveRespawned.Contains(playerNumber))
                {
                    Instantiate(player1, new Vector2(position[0], position[1]), Quaternion.identity);
                    _haveRespawned.Add(playerNumber);
                }
                else _playerOut.Add(playerNumber);
            }
            else if (playerNumber == 2 && player2 != null)
            {
                if (!_haveRespawned.Contains(playerNumber))
                {
                    Instantiate(player2, new Vector2(position[0], position[1]), Quaternion.identity);
                    _haveRespawned.Add(playerNumber);
                }
                else _playerOut.Add(playerNumber);
            }
            else if (playerNumber == 3 && player3 != null)
            {
                if (!_haveRespawned.Contains(playerNumber))
                {
                    Instantiate(player3, new Vector2(position[0], position[1]), Quaternion.identity);
                    _haveRespawned.Add(playerNumber);
                }
                else _playerOut.Add(playerNumber);
            }
            else if (playerNumber == 4 && player4 != null && !_haveRespawned.Contains(playerNumber))
            {
                if (!_haveRespawned.Contains(playerNumber))
                {
                    Instantiate(player4, new Vector2(position[0], position[1]), Quaternion.identity);
                    _haveRespawned.Add(playerNumber);
                }
                else _playerOut.Add(playerNumber);
            }
        }
    }
}
