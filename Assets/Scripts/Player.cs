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
    [SerializeField]
    private float moveSpeed = 1.5f;

    Vector3 targetPosition;

    Direction direction;

    public Vector2 GetPosition => transform.position;

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
        // Cell nextCell = getNext( (int)targetPosition.x , (int)targetPosition.y);
        // if(nextCell.isWalkable == true)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        // }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        // Debug.Log("X"+(int) targetPosition.x+"Y"+(int) targetPosition.y);
        // Debug.Log(targetPosition);
    }

    // public Cell getNext(int x, int y)
    // {
    //     return Grid.GetGridObject(x,y);
    // }

}
