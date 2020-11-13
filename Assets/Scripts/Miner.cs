using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinerStates
{
    IDLE,
    PATROL,
    MINING,
}




public class Miner : MonoBehaviour
{
    private float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    float timer;    
    bool hasObjective;
    private Vector3 targetMine;

    MinerStates _myState;
    public MinerStates MyState
    {
        get { return _myState; }
        set
        {
            _myState = value;
            timer = 0;
        }
    }

    private void Start()
    {
    }
   


    void Update()
    {      
        timer += 1 * Time.deltaTime;        
        switch (MyState)
        {
            case MinerStates.IDLE:
                BeIdle();
                break;
            case MinerStates.PATROL:
                Patrol();
                break;
            case MinerStates.MINING:
                
                break;
           
            default:
                break;
        }
          
    }
    void BeIdle()
    {
        if (timer > 2)
            MyState = MinerStates.PATROL;
    }

    void Patrol()
    {
        if (!hasObjective)       
            GetObjective();
        HandleMovement();       
    }

    void Mining()
    {
        this.SetTargetPosition(targetMine);
    }

    void LookForMine()
    {

    }

    private void GetObjective()
    {
        int _x = Random.Range(0, 20)*10;
        int _y = Random.Range(0, 10)*10;
        
        PathFinding.Instance.GetGrid().GetXY(new Vector3(_x, _y, 5), out int x, out int y);
        this.SetTargetPosition(PathFinding.Instance.GetGrid().GetWorldPosition(x, y));
        hasObjective = true;
    }

    

   


    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition)>1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);                
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                    hasObjective = false;
                    MyState = MinerStates.IDLE;
                }
                    
                
            }
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList !=null && pathVectorList.Count>1)
        {
            pathVectorList.RemoveAt(0);
        }

    }
}
