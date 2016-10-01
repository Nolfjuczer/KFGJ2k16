using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StageController : Singleton<StageController>
{
    public float StageTime;
    public int StageExp;
    public List<Enemy> PossibleEnemies;
    public Enemy EnemyTemplate1;
    public Enemy EnemyTemplate2;

    private float _stageTimer;

    public void Update()
    {
        _stageTimer += Time.deltaTime;
        if (_stageTimer > StageTime)
        {
            OnStageOver();
        }
    }

    private void OnStageOver()
    {
        foreach (Enemy enemy in PossibleEnemies)
        {
            enemy.gameObject.SetActive(false);
        }
        foreach (PlayerController player in GameController.Me.Players)
        {
            player.DecreaseExp(StageExp);
        }
        GameController.Me.OnStageOver();
    }

    public Enemy GetEnemy()
    {
        Enemy enemy;
        if (PossibleEnemies.Count > 0)
        {
            enemy = PossibleEnemies.First();
            PossibleEnemies.Remove(enemy);
        }
        else
        {
            GameObject newEnemy;
            if (Random.Range(0, 3) > 0)
            {
                newEnemy = Instantiate(EnemyTemplate1.gameObject);
            }
            else
            {
                newEnemy = Instantiate(EnemyTemplate2.gameObject);
            }
            enemy = newEnemy.GetComponent<Enemy>();
        }
        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        PossibleEnemies.Add(enemy);
    }
}
