using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PathFinding pathfinding;
    public GameObject spot;
    public GameObject miner;
    public GameObject explorer;
    private int spotCount;
    public float gold;
    public int population;

    private void Start()
    {
        Instance = this;
        pathfinding = new PathFinding(20, 10);
        SetObstacles();
        gold = 1500;
        population = 0;
        SpawnMiner();
        SpawnExplorer();
    }

    private void Update()
    {
        SpawnSpot();
        if (Input.GetMouseButtonDown(0))
            if (gold >= 50 && population < 20)
                SpawnMiner();

       /* if (Input.GetMouseButtonDown(1))                
            if (gold >= 50 && population < 20)
                SpawnExplorer();*/
    }

    void SetObstacles()
    {
        pathfinding.GetNode(0, 0).SetIsWalkable(false);
        pathfinding.GetNode(1, 0).SetIsWalkable(false);
        pathfinding.GetNode(0, 1).SetIsWalkable(false);
        pathfinding.GetNode(3, 6).SetIsWalkable(false);
        pathfinding.GetNode(2, 6).SetIsWalkable(false);
        pathfinding.GetNode(2, 7).SetIsWalkable(false);
        pathfinding.GetNode(3, 7).SetIsWalkable(false);
        pathfinding.GetNode(5, 2).SetIsWalkable(false);
        pathfinding.GetNode(8, 6).SetIsWalkable(false);
        pathfinding.GetNode(9, 6).SetIsWalkable(false);
        pathfinding.GetNode(10, 6).SetIsWalkable(false);
        pathfinding.GetNode(12, 2).SetIsWalkable(false);
        pathfinding.GetNode(13, 3).SetIsWalkable(false);
        pathfinding.GetNode(14, 4).SetIsWalkable(false);
        pathfinding.GetNode(13, 9).SetIsWalkable(false);
        pathfinding.GetNode(14, 9).SetIsWalkable(false);
        pathfinding.GetNode(15, 9).SetIsWalkable(false);
        pathfinding.GetNode(16, 9).SetIsWalkable(false);
        pathfinding.GetNode(17, 9).SetIsWalkable(false);
        pathfinding.GetNode(17, 8).SetIsWalkable(false);
        pathfinding.GetNode(17, 7).SetIsWalkable(false);
        pathfinding.GetNode(17, 6).SetIsWalkable(false);
        pathfinding.GetNode(18, 2).SetIsWalkable(false);
        pathfinding.GetNode(19, 2).SetIsWalkable(false);
        pathfinding.GetNode(18, 1).SetIsWalkable(false);
        pathfinding.GetNode(19, 1).SetIsWalkable(false);
        pathfinding.GetNode(12, 4).SetIsWalkable(false);
        pathfinding.GetNode(13, 4).SetIsWalkable(false);
        pathfinding.GetNode(12, 3).SetIsWalkable(false);
        pathfinding.GetNode(14, 3).SetIsWalkable(false);
        pathfinding.GetNode(13, 2).SetIsWalkable(false);
        pathfinding.GetNode(14, 2).SetIsWalkable(false);
    }

    void SpawnMiner()
    {
        pathfinding.GetGrid().GetXY(new Vector3(80, 40, 5), out int x, out int y);
        Instantiate(miner, pathfinding.GetGrid().GetWorldPosition(x, y), Quaternion.identity);
        gold -= 50;
        population++;
    }

    void SpawnExplorer()
    {
        pathfinding.GetGrid().GetXY(new Vector3(100, 40, 5), out int x, out int y);
        Instantiate(explorer, pathfinding.GetGrid().GetWorldPosition(x, y), Quaternion.identity);
        gold -= 50;
        population++;
    }

    void SpawnSpot()
    {
        if (spotCount < 3)
        {
            int _x = Random.Range(0, 20) * 10;
            int _y = Random.Range(0, 10) * 10;
            pathfinding.GetGrid().GetXY(new Vector3(_x, _y, 5), out int x, out int y);
            if (pathfinding.GetNode(x,y).GetIsWalkable() && new Vector2(x,y)!=new Vector2(9,5) )
            {
                Instantiate(spot, pathfinding.GetGrid().GetWorldPosition(x, y), Quaternion.identity);
                spotCount++;
            }
        }
    }

    public void RemoveSpot()
    {
        spotCount--;
    }

    public void AddGold(float g)
    {
        gold += g;
    }

    public float GetGold()
    {
        return Mathf.RoundToInt(gold);
    }
    
    public int GetPopulation()
    {
        return population;
    }
}
