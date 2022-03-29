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
    private int _mapNumber;

    private bool hasSpawnedPLayers;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _playerCount = 2;
        _mapNumber = 1;
        hasSpawnedPLayers = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Map1")
        {
            SpawnPlayersForMapOne();
        }
    }

    private void SpawnPlayersForMapOne()
    {
        if (!hasSpawnedPLayers)
        {
            List<float[]> positions = PlayerSpawnPosition.getInstance().getMapOnePositions();
            Instantiate(player1, new Vector2(positions[0][0], positions[0][1]), Quaternion.identity);
            Instantiate(player2, new Vector2(positions[2][0], positions[2][1]), Quaternion.identity);

            if(_playerCount > 2)
            {
                Instantiate(player3, new Vector2(positions[1][0], positions[1][1]), Quaternion.identity);
            }

            if(_playerCount > 3)
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

    public void StartGame()
    {
        if(_mapNumber == 1)
        {
            SceneManager.LoadScene("Map1");
        }
        else
        {
            SceneManager.LoadScene("Map1");
        }
    }
}
