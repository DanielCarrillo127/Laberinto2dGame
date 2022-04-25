using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [SerializeField]
    private Cell CellPrefab;
    [SerializeField]
    private Player PlayerPrefab;
    private Grid grid;
    private Player player;
    public int Mcells;
    public int Ncells;
    private int x;
    private int y;
    private Cell Point;
    private Vector2 startPoint;
    private Vector2 finishPoint;
    private Vector2[] enemiesPoint;
    private bool i;
    private int levels = 4;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        y = Random.Range(0, Ncells);
        x = Random.Range(0, Ncells);
        finishPoint = new Vector2(3, 5);
        grid = new Grid(Ncells, Ncells, 1, Mcells, finishPoint,CellPrefab,CellPrefab);

        //set random start point only if the point is isWalkable
        while (i == false)
        {
            y = Random.Range(0, Ncells);
            x = Random.Range(0, Ncells);
            Debug.Log("X"+x+"y"+y);
            Point = grid.GetGridObject(x, y);
            if(Point.isWalkable == true)
            {
                i = true;
                startPoint = new Vector2(x, y);
            }
        }
        player = Instantiate(PlayerPrefab, startPoint, Quaternion.identity);
        player.setGrid(grid);    
    }

    // private void NextLevel()
    // {
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
    // }
}
