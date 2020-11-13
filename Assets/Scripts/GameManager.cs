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
    public int gold;
    public int population;

    private void Start()
    {
        Instance = this;
        pathfinding = new PathFinding(20, 10);
        gold = 10000;
        population = 0;
    }
    private void Update()
    {
        SpawnSpot();        
        if (Input.GetMouseButtonDown(0))
        {
            /*Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);

            minerPathing.SetTargetPosition(mouseWorldPosition);*/
            if (gold >= 50 && population < 20)
                SpawnMiner();
            else if (gold < 50)            
                print("Not enough gold");            
            else           
                print("Population limit reach");
        }

        if (Input.GetMouseButtonDown(1))
        {           
            if (gold >= 50 && population < 20)
                SpawnExplorer();
            else if (gold < 50)           
                print("Not enough gold");            
            else            
                print("Population limit reach");  
        }
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
            Instantiate(spot, pathfinding.GetGrid().GetWorldPosition(x, y), Quaternion.identity);
            spotCount++;
        }
    }

    public void RemoveSpot()
    {
        spotCount--;
    }

    public int GetGold()
    {
        return gold;
    }    
    public int GetPopulation()
    {
        return population;
    }
}
