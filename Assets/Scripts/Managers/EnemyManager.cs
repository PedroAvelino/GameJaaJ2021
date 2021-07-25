using UnityEngine;
using MyBox;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    [SerializeField][ReadOnly] List<Enemy> AllEnemies = new List<Enemy>();

    public void ClearAllEnemies()
    {

    }
}