using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up,
    down,
    right,
    left
}

public class Player : MonoBehaviour
{
    List<Cell> path;
    private Grid grid;
    public Cell[,] gridArray;
    [SerializeField]
    private float moveSpeed = 1.5f;

    Vector3 targetPosition;

    Direction direction;

    public Vector2 GetPosition => transform.position;

    public event EventHandler OnPlayerMove;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    public void setGrid(Grid g)
    {
        grid = g;
    }

    void Start()
    {
        targetPosition = transform.position;
        direction = Direction.down;
        gridArray = grid.gridArray;
    }

    void Update()
    {
        move();
    }
    
    public void move()
    {
         Vector2 axisDirection =
            new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (
            axisDirection != Vector2.zero &&
            targetPosition == transform.position
        )
        {
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    direction = Direction.right;
                    targetPosition += Vector3.right;
                }
                else
                {
                    direction = Direction.left;
                    targetPosition -= Vector3.right;
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    direction = Direction.up;
                    targetPosition += Vector3.up;
                }
                else
                {
                    direction = Direction.down;
                    targetPosition -= Vector3.up;
                }
            }
        }
        //verify isWalkable to find walls
        
        int ini = 0, fin = grid.height-1;
        // ini > (int) targetPosition.x > fin && ini > (int)targetPosition.y > fin
        if(targetPosition.x >= ini && targetPosition.y >= ini && targetPosition.x <= fin && targetPosition.y <= fin)
        {
            Cell next = gridArray[(int) targetPosition.x, (int) targetPosition.y];
            if(next.isWalkable == true)
            {
                
                if(next.isEndPoint == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    Debug.Log("win level");
                    //Set the next Level
                }else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
                //transform.position
                OnPlayerMove?.Invoke(this, EventArgs.Empty);
            
            }else
            {
                targetPosition = transform.position;
            }
        }else
        {
            targetPosition = transform.position;
        }
    }

}
