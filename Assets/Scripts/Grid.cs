using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Grid : ScriptableObject
{
    public static Grid Instance;

    public int width;

    public int height;

    private int cellSize;

    private int cellM;

    private Cell cellPrefab;

    private Cell cellPrefabfinish;

    private Cell cellWallPrefab;

    public Cell[,] gridArray;

    public Vector2 finishPoint;

    public Grid(
        int width,
        int height,
        int cellSize,
        int cellM,
        Vector2 FinishPoint,
        Cell cellPrefabfinish,
        Cell cellWallPrefab,
        Cell cellPrefab
    )
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.finishPoint = FinishPoint;
        this.cellPrefab = cellPrefab;
        this.cellPrefabfinish = cellPrefabfinish;
        this.cellWallPrefab = cellWallPrefab;
        this.cellM = cellM;

        generateBoard();
    }

    private void generateBoard()
    {
        Cell cell;
        gridArray = new Cell[width, height];
        int cont = cellM;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var p = new Vector2(i, j) * cellSize;
                if (i == finishPoint.x && j == finishPoint.y)
                {
                    //change cellprefabfinish
                    cell = Instantiate(cellPrefabfinish, p, Quaternion.identity);
                    cell.Init(this, (int) p.x, (int) p.y, true);
                    cell.SetEndPoint(true);
                }
                else
                {
                    
                    if (Random.Range(0, height) <= 2 && cont != 0)
                    {
                        cell = Instantiate(cellWallPrefab, p, Quaternion.identity);
                        cell.Init(this, (int) p.x, (int) p.y, true);
                        cell.SetWalkable(false);
                        cont--;
                    }
                    else
                    {
                        cell = Instantiate(cellPrefab, p, Quaternion.identity);
                        cell.Init(this, (int) p.x, (int) p.y, true);
                    }
                }

                gridArray[i, j] = cell;
            }
        }

        var center =
            new Vector2((float) height / 2 - 0.5f, (float) width / 2 - 0.5f);

        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
    }

    internal int GetHeight()
    {
        return height;
    }

    internal int GetWidth()
    {
        return width;
    }

    // public void CellMouseClick(Cell cell)
    // {
    //     //cell.SetText("Click on cell "+cell.x+ " "+ cell.y);
    //     BoardManager.Instance.CellMouseClick(cell.x, cell.y);
    // }
    public Cell GetGridObject(int x, int y)
    {
        return gridArray[x, y];
    }

    internal float GetCellSize()
    {
        return cellSize;
    }
}
