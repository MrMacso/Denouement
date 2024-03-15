using UnityEngine;
using System.Collections.Generic;

public class Testing : MonoBehaviour
{
    Pathfinding pathfinding;
    void Start()
    {
        pathfinding = new Pathfinding(10, 10);
        pathfinding.GetNode(2, 0).SetIsWalkable(false);
        pathfinding.GetNode(2, 1).SetIsWalkable(false);
        pathfinding.GetNode(2, 2).SetIsWalkable(false);
        pathfinding.GetNode(2, 3).SetIsWalkable(false);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            
        }
    }
    public void SetDestination( int x)
    {
        int y = 1;
        List<PathNode> path = pathfinding.FindPath(0,0,x,y);
        if(path != null)
        {
            for(int i=1; i<path.Count -1; i++) {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 20f);
            }
        }
    }

}
