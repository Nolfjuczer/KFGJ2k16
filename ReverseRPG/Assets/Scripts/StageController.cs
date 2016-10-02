using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using AStar;

public class StageController : Singleton<StageController>
{
    public float StageTime;
    public int StageExp;
    public List<Enemy> PossibleEnemies;
    public Enemy EnemyTemplate1;
    public Enemy EnemyTemplate2;

    private float _spawnTimer;
    private float _stageTimer;
    private bool _ended;

    public void ResetStage()
    {
        _stageTimer = 0.0f;
        _spawnTimer = 0.0f;
        _ended = false;
        foreach (Enemy enemy in PossibleEnemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        _spawnTimer += Time.deltaTime;
        _stageTimer += Time.deltaTime;
        if (_stageTimer > StageTime && !_ended)
        {
            OnStageOver();
        }
        if (_spawnTimer > 5f)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        _spawnTimer = 0.0f;
        Enemy enemy = GetEnemy();
        int x = 1, y = 1;
        GridElement grid = null;
        while (grid == null)
        {
            x = Random.Range(1, (int)GameController.Me.MainGrid.GridSize.x);
            y = Random.Range(1, (int)GameController.Me.MainGrid.GridSize.y);
            grid = GameController.Me.MainGrid.Elements[x, y, 0];
            if (!grid.Walkable) grid = null;
        }
        enemy.transform.localPosition = grid.transform.localPosition;
        enemy.gameObject.SetActive(true);
    }

    private void OnStageOver()
    {
        _ended = true;
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
        enemy.gameObject.SetActive(false);
        PossibleEnemies.Add(enemy);
    }
}
