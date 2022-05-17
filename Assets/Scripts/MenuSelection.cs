using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{
    [SerializeField] private Text _player1Name;
    [SerializeField] private Text _player2Name;
    [SerializeField] private Text _player3Name;
    [SerializeField] private Text _player4Name;
    [SerializeField] private Text _winnerText;
    [SerializeField] private Text _selectedPlayers;
    [SerializeField] private Text _selectedMap;

    private int _playerCount;
    private bool _gameStart;
    private float _delay;
    private List<string> _playerNames;
    public int _mapNumber { get; private set; }
    public static event Action<int, int, List<string>> GameStart = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _playerCount = 2;
        _mapNumber = 1;
        _gameStart = false;
        _delay = 1.0f;
        _playerNames = new List<string>();
        _selectedPlayers.text = "Number Of players: 2";
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStart)
        {
            _delay -= Time.deltaTime;
            if(_delay <= 0.0f)
            {
                GameStart(_playerCount, _mapNumber, _playerNames);
                Destroy(gameObject);
            }
        }
    }

    public void TwoPlayers()
    {
        _playerCount = 2;
        _selectedPlayers.text = "Number Of players: 2";
    }

    public void ThreePlayers()
    {
        _playerCount = 3;
        _selectedPlayers.text = "Number Of players: 3";
    }

    public void FourPlayers()
    {
        _playerCount = 4;
        _selectedPlayers.text = "Number Of players: 4";
    }

    public void Map1()
    {
        _mapNumber = 1;
        _selectedMap.text = "Map Selected: 1";
    }

    public void Map2()
    {
        _mapNumber = 2;
        _selectedMap.text = "Map Selected: 2";
    }

    public void StartGame()
    {

        if(_mapNumber == 1)
        {
            SceneManager.LoadScene("Map1");
        }
        else if(_mapNumber == 2)
        {
            SceneManager.LoadScene("Map2");
        }

        List<string> playerNames = new List<string> {
            _player1Name.text.ToString(),
            _player2Name.text.ToString(),
            _player3Name.text.ToString(),
            _player4Name.text.ToString()
        };

        for (int i = 0; i < playerNames.Count; i++)
        {
            if(playerNames[i] == "" || playerNames[i] == " ")
            {
                playerNames[i] = "Lazy Person " + (i + 1);
            }

            if(playerNames[i].Length > 15)
            {
                playerNames[i] = playerNames[i].Substring(0, 15);
            }
        }

        _playerNames = playerNames;
        _gameStart = true;
    }
}
