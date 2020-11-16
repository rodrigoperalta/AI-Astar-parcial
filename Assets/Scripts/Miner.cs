using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinerStates
{
    IDLE,
    PATROL,
    MINING,
    RETURNING,
}

public class Miner : MonoBehaviour
{
    private float speed = 20f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    float timer;
    bool hasObjective;
    private GameObject targetMine;
    public GameObject homeBase;
    private float goldCapacity;
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
        MyState = MinerStates.IDLE;
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
                Mining();
                break;
            case MinerStates.RETURNING:
                Returning();
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Mine")
        {
            if (MyState == MinerStates.PATROL)
            {
                targetMine = col.gameObject;               
                StopMoving();
                hasObjective = false;
                MyState = MinerStates.MINING;
            }
        }
    }

    private void Mining()
    {
        if (targetMine!=null)
        {
            PathFinding.Instance.GetGrid().GetXY(new Vector3(targetMine.transform.position.x, targetMine.transform.position.y, 5), out int x, out int y);
            this.SetTargetPosition(PathFinding.Instance.GetGrid().GetWorldPosition(x, y));
            hasObjective = true;
            HandleMovement();
        }
        else        
            MyState = MinerStates.RETURNING;
       
        if (!hasObjective && targetMine!=null)
        {           
          goldCapacity = goldCapacity + 5 * Time.deltaTime;
          targetMine.GetComponent<GoldMine>().LoseGold(goldCapacity);         
        }
        if (goldCapacity >= 10)                    
            MyState = MinerStates.RETURNING;
    }

    private void Returning()
    {
        PathFinding.Instance.GetGrid().GetXY(new Vector3(homeBase.transform.position.x, homeBase.transform.position.y, 5), out int x, out int y);
        this.SetTargetPosition(PathFinding.Instance.GetGrid().GetWorldPosition(x, y));
        hasObjective = true;
        HandleMovement();
        if (!hasObjective)
        {
            GameManager.Instance.AddGold(goldCapacity);
            goldCapacity = 0;
            if (targetMine!=null)            
                MyState = MinerStates.MINING;            
            else            
                MyState = MinerStates.IDLE;
        }
    }

    private void GetObjective()
    {
        int _x = Random.Range(0, 20) * 10;
        int _y = Random.Range(0, 10) * 10;

        PathFinding.Instance.GetGrid().GetXY(new Vector3(_x, _y, 5), out int x, out int y);
        if (PathFinding.Instance.GetNode(x, y).GetIsWalkable())
        {
            this.SetTargetPosition(PathFinding.Instance.GetGrid().GetWorldPosition(x, y));
            hasObjective = true;
        }
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
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
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
                    if (MyState == MinerStates.PATROL)
                        MyState = MinerStates.IDLE;
                }
            }
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)        
            pathVectorList.RemoveAt(0);
    }
}
