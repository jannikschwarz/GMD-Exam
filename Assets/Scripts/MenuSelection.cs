using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    private int _playerCount;
    public int _mapNumber { get; private set; }
    public static event Action<int, List<string>> PlayerSpawnAction = delegate { };
    public static event Action<int, int> MapStartAction = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _playerCount = 2;
        _mapNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        string sceneName = SceneManager.GetActiveScene().name;
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

        PlayerSpawnAction(_playerCount,new List<string>());
        MapStartAction(_playerCount, _mapNumber);
    }
}
