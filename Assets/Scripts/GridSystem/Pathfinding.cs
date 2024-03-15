using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{
    const int MOVE_STRAIGHT_COST = 10;
    const int MOVE_DIAGONAL_COST = 14;

    Grid<PathNode> grid;
    List<PathNode> openList;
    List<PathNode> closedList;

    public Pathfinding(int width, int height) 
    {
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode>() {startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0) 
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode) 
            {
                //reachedFinalNode
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neigbourNode in GetNeighbourList(currentNode)) 
            {
                if (closedList.Contains(neigbourNode)) continue;
                if (!neigbourNode.isWalkable)
                {
                    closedList.Add(neigbourNode);
                    continue;
                }
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neigbourNode);
                if(tentativeGCost < neigbourNode.gCost)
                {
                    neigbourNode.cameFromNode = currentNode;
                    neigbourNode.gCost = tentativeGCost;
                    neigbourNode.hCost = CalculateDistanceCost(neigbourNode, endNode);
                    neigbourNode.CalculateFCost();

                    if (!openList.Contains(neigbourNode))
                    {
                        openList.Add(neigbourNode);
                    }
                }
            }
        }
        //out of nodes on the open list
        return null;
    }
    List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if(currentNode.x -1 >= 0)
        {
            //left
            neighbourList.Add(GetNode(currentNode.x-1, currentNode.y));
            //left down
            if(currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y -1));
            //left up
            if(currentNode.y + 1 >= grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if(currentNode.x + 1 < grid.GetWidth())
        {
            //right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //right down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            //right up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        //down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        //up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
    public Grid<PathNode> GetGrid() 
    {
        return grid;
    }

    List<PathNode> CalculatePath(PathNode endNode) 
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null) 
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
    int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance- yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    PathNode GetLowestFCostNode(List<PathNode> pathNodeList) 
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++) 
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost) 
            {
                lowestFCostNode= pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
