using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [SerializeField]
    private Cell CellPrefab;
    [SerializeField]
    private Player PlayerPrefab;
    [SerializeField]
    private Enemy EnemyPrefab;
    private Grid grid;
    private Player player;
    private List<Enemy> enemies = new List<Enemy>();
    public int Mcells;
    public int Ncells;
    private int x;
    private int y;
    private Cell Point;
    private Vector2 startPoint;
    private Vector2 finishPoint;
    private Vector2[] enemiesPoint;
    private bool i;
    private int level = 1;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //set final point
        int yfinal = Random.Range(0, Ncells);
        int xfinal = Random.Range(0, Ncells);
        finishPoint = new Vector2(xfinal, yfinal);
        grid = new Grid(Ncells, Ncells, 1, Mcells, finishPoint,CellPrefab,CellPrefab);

        //set random start point only if the point is isWalkable
        while (i == false)
        {
            y = Random.Range(0, Ncells);
            x = Random.Range(0, Ncells);
            Debug.Log("X"+x+"y"+y);
            Point = grid.GetGridObject(x, y);
            if(Point.isWalkable == true && x != xfinal && y != yfinal)
            {
                i = true;
                startPoint = new Vector2(x, y);
            }
        }
        player = Instantiate(PlayerPrefab, startPoint, Quaternion.identity);
        player.setGrid(grid);

        //set random start point only if the point is isWalkable for enemy
        i = false;
        while (i == false)
        {
            y = Random.Range(0, Ncells);
            x = Random.Range(0, Ncells);
            Debug.Log("Enemy: X"+x+"y"+y);
            Point = grid.GetGridObject(x, y);
            if(Point.isWalkable == true)
            {
                i = true;
                startPoint = new Vector2(x, y);
                Enemy enemy = Instantiate(EnemyPrefab, startPoint, Quaternion.identity);
                enemies.Add(enemy);
                Debug.Log(enemies.Count);
                // List<Cell> path = PathManager.Instance.FindPath(grid, (int)player.GetPosition.x, (int)player.GetPosition.y, startPoint.x, startPoint.y);
                // enemy.SetPath(path);
            }
        }
        player.OnPlayerMove += PlayerMoveEnemiesUpdate;
    }

    // void Update()
    // {
    //     player.OnPlayerMove += PlayerMoveEnemiesUpdate;
    // }


    public void PlayerMoveEnemiesUpdate(object sender, EventArgs e)
    {
        Debug.Log("calculate path");
        // foreach (Enemy ene in enemies)
        // {
        //     //FindPath --> grid + startx,y + finishx,y
        //     List<Cell> path = PathManager.Instance.FindPath(grid, (int)ene.GetPosition.x, (int)ene.GetPosition.y, 3, 3);
        //     ene.SetPath(path);
        // }
    }

    // private void NextLevel()
    // {
            // if(level<=4){

            // }else{
            //     //loadScene final
            // }        
    //     grid = new Grid(Ncells, Ncells, 1, Mcells, CellPrefab);

    //     //set random start point only if the point is isWalkable
    //     while (i == false)
    //     {
    //         y = Random.Range(0, Ncells);
    //         x = Random.Range(0, Ncells);
    //         Point = grid.GetGridObject(x, y);
    //         if(Point.isWalkable == true)
    //         {
    //             i = true;
    //             startPoint = new Vector2(x, y);
    //         }
    //     }
    //     player = Instantiate(PlayerPrefab, startPoint, Quaternion.identity);
//     player.setGrid(grid);

//         i = false;
//         for (int j = 0; j < level; j++)
//             {
// while (i == false)
//         {
//             y = Random.Range(0, Ncells);
//             x = Random.Range(0, Ncells);
//             Debug.Log("Enemy: X"+x+"y"+y);
//             Point = grid.GetGridObject(x, y);
//             if(Point.isWalkable == true)
//             {
//                 i = true;
//                 startPoint = new Vector2(x, y);
//                 enemy = Instantiate(EnemyPrefab, startPoint, Quaternion.identity);
//                 // List<Cell> path = PathManager.Instance.FindPath(grid, (int)player.GetPosition.x, (int)player.GetPosition.y, startPoint.x, startPoint.y);
//                 // enemy.SetPath(path);
//             }
//             }
        
    // }
}
