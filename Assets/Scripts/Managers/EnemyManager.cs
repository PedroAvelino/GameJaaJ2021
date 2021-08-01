using UnityEngine;
using MyBox;
using System.Collections.Generic;
using System.Collections;

//This also deals with spawners lmao my bad
public class EnemyManager : MonoBehaviour
{
    const int TOTAL_ACTIVE_SPAWNERS = 3;

    [Separator("Enemies Lol")]

    [ReadOnly][SerializeField]
    EnemyType _featuredEnemy;

    [SerializeField] 
    Enemy[] EnemiesToSpawn;

    [SerializeField] [ReadOnly] 
    List<Enemy> _allEnemiesSpawned;

    [Separator("Round Parameters")]
    [SerializeField] float _spawnInterval = 1f;

    [Separator("Spawners")]

    [SerializeField] [ReadOnly] 
    List<EnemySpawnPosition> _spawnPositions;

    [SerializeField] [ReadOnly]
    List<EnemySpawnPosition> _activeSpawners;

    int enemiesToSpawnOnThisRound;
    IEnumerator currentRoutine;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += PrepareRound;
        GameManager.OnStartRule += StartSpawning;
    }

    private void Awake()
    {
        GetSpawnPositions();
    }

    private void GetSpawnPositions()
    {
        _spawnPositions = new List<EnemySpawnPosition>();
        _activeSpawners = new List<EnemySpawnPosition>();

        //I do this because i don't want to deal with arrays
        EnemySpawnPosition[] allSpawnPosition = FindObjectsOfType<EnemySpawnPosition>();
        foreach (var sp in allSpawnPosition)
        {
            _spawnPositions.Add(sp);
        }
    }

    private void PrepareRound( Rule rule )
    {
        ClearAllEnemies();

        SelectSpawners();

        _featuredEnemy = rule.TargetEnemy;

        enemiesToSpawnOnThisRound = rule.AmountOfEnemiesToSpawn;
    }

    private void StartSpawning()
    {
        currentRoutine = null;
        currentRoutine = SpawnRoutine();
        StartCoroutine( currentRoutine );
    }

    private void SelectSpawners()
    {
        foreach (var s in _activeSpawners)
        {
            s.Spawning = false;
        }

        _activeSpawners.Clear();

        int selectedAmount = 0;
        List<EnemySpawnPosition> availableSpawners = new List<EnemySpawnPosition>();
        availableSpawners = _spawnPositions;

        while ( selectedAmount != TOTAL_ACTIVE_SPAWNERS )
        {
            int randomIndex = Random.Range( 0, availableSpawners.Count );
            EnemySpawnPosition pos = availableSpawners[ randomIndex ];

            if( _activeSpawners.Contains( pos ) == false )
            {

                EnemySpawnPosition toRemove = pos.destinationSpawn;

                _activeSpawners.Add( pos );

                availableSpawners.Remove(toRemove);
                availableSpawners.Remove(pos);
                selectedAmount++;
            }
        }

        foreach (var found in _activeSpawners)
        {
            found.Spawning = true;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while ( true )
        {
            yield return new WaitForSeconds( _spawnInterval );

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        EnemySpawnPosition spawnPosition = GetRandomActiveSpawn();

        Enemy enemyToSpawn = GetFeaturedEnemy();

        Enemy enemy = (Enemy)Pooler.GetObject( enemyToSpawn , spawnPosition.transform.position, Quaternion.identity);

        enemy.destination = spawnPosition.destinationSpawn.transform;
    }

    private Enemy GetFeaturedEnemy()
    {
        Enemy foundEnemy;

        foreach (var e in EnemiesToSpawn)
        {
            if( e.Type == _featuredEnemy )
            {
                foundEnemy = e;
                return foundEnemy;
            }
        }

        return new BasicEnemy();
    }

    private EnemySpawnPosition GetRandomActiveSpawn()
    {
        int randomIndex = Random.Range( 0 , _activeSpawners.Count );
        EnemySpawnPosition pos = _activeSpawners[randomIndex];

        return pos;
    }

    public void ClearAllEnemies()
    {
        _allEnemiesSpawned = new List<Enemy>();

        foreach (Enemy enemy in _allEnemiesSpawned)
        {
            enemy.ReturnToPool();
        }
    }

    private void OnDisable()
    {
        RulesManager.OnNewRuleGiven -= PrepareRound;
        GameManager.OnStartRule -= StartSpawning;

        Pooler.ClearPool();
    }
}