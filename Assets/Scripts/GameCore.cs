using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    PreGame,
    Game,
    GameOver
}

[System.Serializable]
public class EnemiesWave
{
    [System.Serializable]
    public class WaveUnit
    {
        [SerializeField]
        private int _amount;
        public int Amount => _amount;

        [SerializeField]
        private Enemy _enemyPrefab;
        public Enemy EnemyPrefab => _enemyPrefab;
    }

    [SerializeField]
    private List<WaveUnit> _units;
    public List<WaveUnit> Units => _units;

    [SerializeField]
    private float _spawnDelay;
    public float SpawnDelay => _spawnDelay;
}
public class GameCore : MonoBehaviour
{
    [SerializeField]
    private SplineComputer _spline;
    [SerializeField]
    private GameObject _losePopup;
    [SerializeField]
    private GameObject _winPopup;
    [SerializeField]
    private GameObject _startMenu;
    [SerializeField]
    private Transform _enemiesCloneRoot;
    [SerializeField]
    private Transform _towersCloneRoot;
    [SerializeField]
    private Text _healthText;
    [SerializeField]
    private Text _coinsText;

    [SerializeField]
    private List<EnemiesWave> _waves;
    [SerializeField]
    private List<TowerController> _towers;
    [SerializeField]
    private List<TowerPoint> _towerPoints;

    private List<Enemy> _enemiesClones = new List<Enemy>();
    private List<TowerController> _towersClones = new List<TowerController>();

    public int TotalCoinsCount { get; set; }

    private int _startCoinsCount = 25;
    private int _healthCount;
    private float _spawnDelay;
    private bool _isSpawning;

    private GameState _gameState;

    private void Start()
    {
        _gameState = GameState.PreGame;

        SetStartData();
    }
    
    private void Update()
    {
        _enemiesClones.RemoveAll(e=>e==null);
        CheckWin();
    }
    private void SetStartData()
    {
        _healthCount = 10;
        _healthText.text = _healthCount.ToString();

        TotalCoinsCount = _startCoinsCount;
        _coinsText.text = TotalCoinsCount.ToString();
    }
    private IEnumerator SpawnEnemy()
    {
        _isSpawning = true;
        foreach (var wave in _waves)
        {
            foreach (var unit in wave.Units)
            {
                for (int i = 0; i < unit.Amount; i++)
                {
                    var enemy = Instantiate(unit.EnemyPrefab, _enemiesCloneRoot);
                    enemy.Init(_spline, CoinsCount);
                   
                    _enemiesClones.Add(enemy);
                    yield return new WaitForSeconds(wave.SpawnDelay);
                }
            }
        }
        _isSpawning = false;
    }
    public void InstantTower(Vector3 position, int towerId)
    {
        var tower = _towers.FirstOrDefault(tow => tow.TowerId == towerId);
        var towerСlone = Instantiate(tower, position, Quaternion.identity, _towersCloneRoot);
        _towersClones.Add(towerСlone);
    }

    public void HealthCount()
    {
        if (_healthCount > 0)
        {
            _healthCount--;
        }
        else 
        {
            _losePopup.SetActive(true);
            Time.timeScale = 0;
        }

        _healthText.text = _healthCount.ToString();
    }
    public void CoinsCount(int value)
    {
        if (TotalCoinsCount < 100)
        {
            TotalCoinsCount += value;
        }
        _coinsText.text = TotalCoinsCount.ToString();
    }


    public void CheckWin()
    {
        var enemyCurentAmount = _enemiesClones.Count;

        if (_gameState == GameState.Game && _healthCount > 0 && !_isSpawning && enemyCurentAmount == 0)
        {
            _winPopup.SetActive(true);
            Time.timeScale = 0;

            _gameState = GameState.GameOver;
        }
    }

    public void OnStartButtonDown()
    {
        _startMenu.SetActive(false);
        SetStartData();        
        StartCoroutine(SpawnEnemy());
        _gameState = GameState.Game;
        TurnOnPoint(_gameState);
        Time.timeScale = 1;
    }

    public void OnExitButtonDown()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif
    }

    public void OnExitMenuDown()
    {
        _losePopup.SetActive(false);
        _winPopup.SetActive(false);
        _startMenu.SetActive(true);
        ClearAllLists();
    }

    public  void OnPlayAgainDown()
    {
        _losePopup.SetActive(false);
        _winPopup.SetActive(false);
        ClearAllLists();
        OnStartButtonDown();
    }

    private void ClearAllLists()
    {
        foreach (var clon in _enemiesClones)
        {
            if (clon)
            {
                Destroy(clon.gameObject);
            }
        }
        foreach (var clon in _towersClones)
        {
            if (clon)
            {
                Destroy(clon.gameObject);
            }
        }
        foreach (var point in _towerPoints)
        {
            point.IsEmpty();
        }
        _enemiesClones.Clear();
        _towersClones.Clear();
    }

    private void TurnOnPoint(GameState state)
    {
        foreach (var point in _towerPoints)
        {
            point.StateGame(state);
        }
    }
}
