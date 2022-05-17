using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Text theKilledTextFeed;
    [SerializeField]
    public Text killedByTextFeed;
    [SerializeField]
    public Text winnerText;

    private int _mapNumber;
    private int _playerCount;
    private PlayerSpawner _playerSpawner;
    private WeaponSpawner _weaponSpawner;

    private List<string> _edgeDeathText;
    private List<string> _shootDeath;
    private List<string> _playerNames = null;
    private List<int> _playerKilled = new List<int>();
    private float _targetTime = 5.0f;
    private bool _timerStarted = false;

    void Start()
    {
        _weaponSpawner = GetComponent<WeaponSpawner>();
        MenuSelection.GameStart += GameStart;
        PlayerInfo.KilledByPlayer += KilledByPlayer;
        PlayerInfo.MapDeath += KilledByEdge;
        _playerSpawner = GetComponent<PlayerSpawner>();
        TextGenerator();
        theKilledTextFeed.text = "GAME HAS ";
        killedByTextFeed.text = "STARTED";
    }

    private void Update()
    {
        if((_playerKilled.Count == 1 || CheckKilledList()) && !_timerStarted)
        {
            winnerText.text = "The Winner is: " + "\n" + _playerNames[_playerKilled[0] - 1];
            _timerStarted = true;
        }

        if (_timerStarted)
        {
            _targetTime -= Time.deltaTime;
            if(_targetTime <= 0.0f)
            {
                timerEnded();
            }
        }
    }

    private void timerEnded()
    {
        SceneManager.LoadScene("MainMenu");
        WeaponSpawner.Reset();
        PlayerSpawner.Reset();
    }

    private bool CheckKilledList()
    {
        if(_playerKilled.Count == 2)
        {
            if (_playerKilled[0] == _playerKilled[1]) return true;
        }
        return false;
    }

    private void KilledByPlayer(int killed, int theKiller)
    {
        Debug.Log(killed + " Killer: " + theKiller);
        Debug.Log(_playerNames.Count);
        string playerKilled = _playerNames[killed - 1];
        string killer = _playerNames[theKiller - 1];
        int random = UnityEngine.Random.Range(0, _edgeDeathText.Count);
        theKilledTextFeed.text = playerKilled;
        killedByTextFeed.text = _shootDeath[random] + killer;
        _playerKilled.Remove(killed);
    }

    private void GameStart(int playerCount, int mapNumber, List<string> playerNames)
    {
        _playerCount = playerCount;
        _mapNumber = mapNumber;
        _playerNames = playerNames;
        _playerSpawner.PlayerSpawn(_playerCount, _mapNumber, _playerNames);
        _weaponSpawner.WeaponMapSpawn(_mapNumber);

        for(int i = 0; i < _playerCount; i++)
        {
            _playerKilled.Add(i + 1);
            _playerKilled.Add(i + 1);
        }
    }

    private void KilledByEdge(int playerNumber)
    {
        string playerName = _playerNames[playerNumber - 1];
        int random = UnityEngine.Random.Range(0, _edgeDeathText.Count);
        string text = _edgeDeathText[random];
        theKilledTextFeed.text = playerName;
        killedByTextFeed.text = text;
        _playerKilled.Remove(playerNumber);
    }

    private void TextGenerator()
    {
        _edgeDeathText = new List<string>
        {
            " choose the easy way out",
            " went for a dive",
            " lost to gravity",
            " dug straight down",
            " forgot a water bucket",
            " forgot the thumbs up",
        };

        _shootDeath = new List<string>
        {
            " pulverated ",
            " got blasted by ",
            " looked down the barrel of ",
            " meet an enemy ",
            " waved rudely at "
        };
    }
}
