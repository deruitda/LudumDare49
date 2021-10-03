using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnRange = 50;
    
    [SerializeField]
    private LayerMask _playerMask;

    [SerializeField]
    private int _maxEnemies = 5;

    [SerializeField]
    private int _currentEnemies = 0;

    [SerializeField]
    private float _spawnDelayInSeconds = 2;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] _spawnPoints;

    private float _nextSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _nextSpawnTime 
            && Physics.CheckSphere(transform.position, _spawnRange, _playerMask)
            && _currentEnemies < _maxEnemies)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnDelayInSeconds;
        }
    }

    public void RecordDeadEnemy()
    {
        _currentEnemies--;
    }

    private void SpawnEnemy()
    {
        var spawns = _spawnPoints.Where(sp => !sp.GetComponent<SpawnPoint>().IsVisible);

        foreach(var spawn in spawns)
        {
            var enemy = Instantiate(_enemyPrefab, spawn.transform);
            enemy.GetComponent<EnemyMortal>().Spawner = this;

            _currentEnemies++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _spawnRange);
    }
}
