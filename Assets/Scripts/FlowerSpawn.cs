using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FlowerSpawn : MonoBehaviour
{
    public GameObject _flower;
    public Transform[] _spawnPointTransforms;
    public GameObject _gameLogicObject;

    public float _translationLeeway = 0.1f;
    public float _rotationLeeway = 20.0f;
    public float _spawnTime = 10.0f;
    public float _timeTillSeed = 10.0f;

    private struct SpawnPoint
    {
        public bool _isOccupied;
        public bool _isSeeded;
        public float _seedTime;
    }

    private GameLogic _gameLogic;
    private SpawnPoint[] _spawnPoints;
    private int[] _spawnPointIndexList;
    private int _currentSpawnPointIndex = 0;
    private int _spawnPointsCount = 0;
    private float _timeSinceSpawn = 0.0f;

    // Use this for initialization
    void Start()
    {
        PopulateSpawnPoints();
        ShuffleSpawnPoints();
        _gameLogic = _gameLogicObject.GetComponent("GameLogic") as GameLogic;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
        if (_timeSinceSpawn >= _spawnTime)
        {
            _timeSinceSpawn = 0;
            Spawn();
        }

        for(int i=0; i< _spawnPointsCount; ++i)
        {
            if(_spawnPoints[i]._isOccupied)
            {
                float timeTilSeed = _spawnPoints[i]._seedTime;
                timeTilSeed -= Time.deltaTime;
                if(timeTilSeed <= 0.0f)
                {
                    _spawnPoints[i]._isSeeded = true;
                    Seed(i);
                }
                _spawnPoints[i]._seedTime = timeTilSeed;
            }
        }
    }

    void PopulateSpawnPoints()
    {
        _spawnPointsCount = _spawnPointTransforms.Length;
        _spawnPointIndexList = new int[_spawnPointsCount];
        _spawnPoints = new SpawnPoint[_spawnPointsCount];

        for (int i = 0; i < _spawnPointsCount; ++i)
        {
            _spawnPointIndexList[i] = i;
            _spawnPoints[i] = new SpawnPoint { _isOccupied = false, _isSeeded = false, _seedTime = _timeTillSeed };
        }
    }

    void ShuffleSpawnPoints()
    {
        _currentSpawnPointIndex = 0;
        _spawnPointIndexList = _spawnPointIndexList.OrderBy(s => Random.value).ToArray();
    }

    int GetNextSpawnpointIndex()
    {
        int misses = 0;
        while (misses < _spawnPointsCount)
        {
            int index = _spawnPointIndexList[_currentSpawnPointIndex];

            if (!_spawnPoints[index]._isSeeded && !_spawnPoints[index]._isOccupied)
            {
                IncrementSpawnpointIndex();
                return index;
            }
            else
            {
                IncrementSpawnpointIndex();
                ++misses;
            }
        }

        return -1;
    }

    void Seed(int index)
    {
        Debug.Log("seed");
    }

    void IncrementSpawnpointIndex()
    {
        ++_currentSpawnPointIndex;
        if (_currentSpawnPointIndex >= _spawnPointsCount)
        {
            ShuffleSpawnPoints();
        }
    }
    void Spawn()
    {
        int index = GetNextSpawnpointIndex();
        if( index == -1 )
        {
            _gameLogic.GameOver();
            return;
        }

        _spawnPoints[index]._isOccupied = true;

        var spawnTransform = _spawnPointTransforms[index];
        var translationMod = Random.Range(-_translationLeeway, _translationLeeway);
        var rotationMod = Random.Range(-_rotationLeeway, _rotationLeeway);

        float angle;
        Vector3 axis;
        spawnTransform.rotation.ToAngleAxis(out angle, out axis);
        angle += rotationMod;

        Instantiate(_flower, spawnTransform.position + (new Vector3(1.0f, 1.0f, 0.0f) * translationMod), Quaternion.AngleAxis(angle, axis));
    }
}
