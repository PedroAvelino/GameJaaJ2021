using UnityEngine;
using MyBox;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] Enemy[] EnemiesToSpawn;
    [SerializeField][ReadOnly] List<Enemy> _allEnemiesSpawned = new List<Enemy>();

    [SerializeField][ReadOnly] List<EnemySpawnPosition> spawnPositions = new List<EnemySpawnPosition>();

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += SpawnAmountEnemies;
    }

    private void SpawnAmountEnemies( Rule rule )
    {
        ClearAllEnemies();

        int amountOfEnemiesToSpawn = rule.AmountOfEnemiesToSpawn;

        int randomIndex= Random.Range( 0 , spawnPositions.Count );

        //EnemySpawnPosition randomRange = 

        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            Pooler.GetObject( EnemiesToSpawn[0], transform.position, Quaternion.identity );
        }
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
        RulesManager.OnNewRuleGiven -= SpawnAmountEnemies;
    }
}