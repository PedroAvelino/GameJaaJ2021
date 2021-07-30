using UnityEngine;
using MyBox;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] Enemy[] EnemiesToSpawn;
    [SerializeField] [ReadOnly] List<Enemy> _allEnemiesSpawned;

    [SerializeField] [ReadOnly] List<EnemySpawnPosition> _spawnPositions;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += SpawnAmountEnemies;
    }

    private void Awake()
    {
        GetSpawnPositions();
    }

    private void GetSpawnPositions()
    {
        _spawnPositions = new List<EnemySpawnPosition>();
        //I do this because i don't want to deal with arrays
        EnemySpawnPosition[] allSpawnPosition = FindObjectsOfType<EnemySpawnPosition>();
        foreach (var sp in allSpawnPosition)
        {
            _spawnPositions.Add(sp);
        }
    }

    private void SpawnAmountEnemies( Rule rule )
    {
        ClearAllEnemies();

        int amountOfEnemiesToSpawn = rule.AmountOfEnemiesToSpawn;

        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            if (EnemiesToSpawn[0] == null) continue;
            
            int randomIndex= Random.Range( 0 , _spawnPositions.Count);
            EnemySpawnPosition randomRange = _spawnPositions[randomIndex];
            Pooler.GetObject( EnemiesToSpawn[0], randomRange.transform.position, Quaternion.identity );
        }
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
        RulesManager.OnNewRuleGiven -= SpawnAmountEnemies;
        Pooler.ClearPool();
    }
}