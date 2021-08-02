using UnityEngine;
using MyBox;
using System.Collections.Generic;
using System.Collections;

//This also deals with spawners lmao my bad
public class EnemyManager : MonoBehaviour
{
    const int TOTAL_ACTIVE_SPAWNERS = 4;

    [Separator("Enemies Lol")]

    [ReadOnly][SerializeField]
    EnemyType _featuredEnemy;

    [SerializeField] 
    Enemy[] EnemiesToSpawn;

    [SerializeField] [ReadOnly] 
    List<Enemy> _allEnemiesSpawned;

    [Separator("Round Parameters")]
    [SerializeField] float _spawnInterval = .6f;

    [Separator("Spawners")]

    [SerializeField] [ReadOnly] 
    List<EnemySpawnPosition> _spawnPositions;

    [SerializeField] [ReadOnly]
    List<EnemySpawnPosition> _activeSpawners;

    IEnumerator currentRoutine;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += PrepareRound;
        GameManager.OnStartRule += StartSpawning;
        Dot.OnDeath += StopEverything;
        RuleTypeBase.OnRuleCompleted += CollectEnemies;

    }

    private void CollectEnemies()
    {
        foreach (var enemy in _allEnemiesSpawned)
        {
            enemy.ReturnToPool();
        }
    }

    private void Awake()
    {
        GetSpawnPositions();
    }

    private void GetSpawnPositions()
    {
        _spawnPositions = new List<EnemySpawnPosition>();
        _activeSpawners = new List<EnemySpawnPosition>();
        _allEnemiesSpawned = new List<Enemy>();

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
    }

    private void StartSpawning()
    {
        StopAllCoroutines();
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

        while ( selectedAmount != TOTAL_ACTIVE_SPAWNERS )
        {
            int randomIndex = Random.Range( 0, _spawnPositions.Count );
            EnemySpawnPosition pos = _spawnPositions[ randomIndex ];

            if( _activeSpawners.Contains( pos ) == false )
            {
                bool foundAdjacent = false;
                foreach (var n in _activeSpawners)
                {
                    if( pos == n.destinationSpawn ) 
                    {
                        foundAdjacent = true;
                    }
                }

                if( foundAdjacent ) continue;
                EnemySpawnPosition toRemove = pos.destinationSpawn;

                _activeSpawners.Add( pos );

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

    private void SpawnEnemy(  )
    {
        EnemySpawnPosition spawnPosition = GetRandomActiveSpawn();
        Enemy enemyToSpawn;

        bool isFeaturedNext = (Random.Range(1, 3) == 1); 

        if( isFeaturedNext )
        {
            enemyToSpawn = GetFeaturedEnemy();
        }else
        {
            enemyToSpawn = GetRandomEnemy();
        }

        Enemy enemy = (Enemy)Pooler.GetObject( enemyToSpawn , spawnPosition.transform.position, Quaternion.identity);
        _allEnemiesSpawned.Add( enemy );
        enemy.destination = spawnPosition.destinationSpawn.transform;
    }

    Enemy GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, EnemiesToSpawn.Length);
        Enemy toSpawn = EnemiesToSpawn[randomIndex];
        return toSpawn;
    }

    private Enemy GetFeaturedEnemy()
    {

        if( _featuredEnemy == EnemyType.Any)
        {
            return GetRandomEnemy();
        }

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
        foreach (Enemy enemy in _allEnemiesSpawned)
        {
            enemy.ReturnToPool();
        }
    }

    private void OnDisable()
    {
        RulesManager.OnNewRuleGiven -= PrepareRound;
        GameManager.OnStartRule -= StartSpawning;
        RuleTypeBase.OnRuleCompleted -= CollectEnemies;
        Dot.OnDeath -= StopEverything;

        Pooler.ClearPool();
    }

    private void StopEverything()
    {
        ClearAllEnemies();
        StopCoroutine(currentRoutine);
    }
}