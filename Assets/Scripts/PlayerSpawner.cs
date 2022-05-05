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
    private bool _startSpawnCalled;
    private List<float[]> positions;

    // Start is called before the first frame update
    void Start()
    {
        _startSpawnCalled = false;
        _spawn = PlayerSpawnPosition.getInstance();
        MenuSelection.MapStartAction += StartSpawn;
        PlayerHandler.PlayerRespawnAction += RespawnPlayer;
        PlayerHandler.PlayerDeadAction += PlayerDeath;
    }

    private void StartSpawn(int playerCount, int mapNumber)
    {
        positions = new List<float[]>();
        switch (mapNumber)
        {
            case (1):
                positions = _spawn.getMapOnePositions();
                break;
            case (2):
                positions = _spawn.getMapTwoPositions();
                break;

        }

        Instantiate(player1, new Vector2(positions[0][0], positions[0][1]), Quaternion.identity);
        Instantiate(player2, new Vector2(positions[2][0], positions[2][1]), Quaternion.identity);

        if (playerCount > 2)
        {
            Instantiate(player3, new Vector2(positions[1][0], positions[1][1]), Quaternion.identity);
        }

        if (playerCount > 3)
        {
            Instantiate(player4, new Vector2(positions[3][0], positions[0][1]), Quaternion.identity);
        }

        _startSpawnCalled = true;
    }

    private void RespawnPlayer(int playerNumber)
    {
        if (_startSpawnCalled)
        {
        }
    }

    private void PlayerDeath(int playerNumber)
    {

    }
}
