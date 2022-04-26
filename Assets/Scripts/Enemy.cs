using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    List<Cell> path;
    [SerializeField]
    private float moveSpeed = 1f;

    public Vector2 GetPosition => transform.position;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    // Update is called once per frame
    // void Update()
    // {
    //     Move();
    // }

    public void SetPath(List<Cell> path)
    {
        //ResetPosition();
        waypointIndex = 0;
        this.path = path;
        Move();
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(0, 0);
    }

    private void Move()
    {
        // If player didn't reach last waypoint it can move
        // If player reached last waypoint then it stops
        
        if (path == null)
            return;

        if (waypointIndex <= path.Count - 1)
        {

            // Move player from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               path[path.Count - 1].transform.position,
               moveSpeed * Time.deltaTime);

            // If player reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and player starts to walk to the next waypoint
            // if (transform.position == path[waypointIndex].transform.position)
            // {
            //     waypointIndex += 1;
            // }
        }
    }
}
