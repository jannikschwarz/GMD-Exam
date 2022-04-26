using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;


    private int _playerCount;
    public int _mapNumber { get; private set; }
    private bool hasSpawnedPLayers;
    private PlayerSpawnPosition spawn;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _playerCount = 2;
        _mapNumber = 1;
        hasSpawnedPLayers = false;
        spawn = PlayerSpawnPosition.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "MainMenu")
        {
            if (sceneName.Equals("Map1")) SpawnPlayers(spawn.getMapOnePositions());
            else if (sceneName.Equals("Map2")) SpawnPlayers(spawn.getMapTwoPositions());
        }
    }

    private void SpawnPlayers(List<float[]> positions)
    {
        if (!hasSpawnedPLayers)
        {
            Instantiate(player1, new Vector2(positions[0][0], positions[0][1]), Quaternion.identity);
            Instantiate(player2, new Vector2(positions[2][0], positions[2][1]), Quaternion.identity);

            if (_playerCount > 2)
            {
                Instantiate(player3, new Vector2(positions[1][0], positions[1][1]), Quaternion.identity);
            }

            if (_playerCount > 3)
            {
                Instantiate(player4, new Vector2(positions[3][0], positions[0][1]), Quaternion.identity);
            }
            hasSpawnedPLayers = true;
        }
    }

    public void TwoPlayers()
    {
        _playerCount = 2;
    }

    public void ThreePlayers()
    {
        _playerCount = 3;
    }

    public void FourPlayers()
    {
        _playerCount = 4;
    }

    public void Map1()
    {
        _mapNumber = 1;
    }

    public void Map2()
    {
        _mapNumber = 2;
    }

    public void StartGame()
    {
        if(_mapNumber == 1)
        {
            SceneManager.LoadScene("Map2");
        }
        else if(_mapNumber == 2)
        {
            SceneManager.LoadScene("Map2");
        }
    }
}
