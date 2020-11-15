using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{

    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public bool isASpot;
    public PathNode cameFromNode;
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void SetIsWalkable(bool b)
    {
        isWalkable = b;
    }

    public bool GetIsWalkable()
    {
        return isWalkable;
    }

    public void IsASport(bool s)
    {
        isASpot = s;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public override string ToString()
    {
        return x + ", " + y;
    }



}
