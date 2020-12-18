using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviour
{
    [SerializeField] private float startingStamina;
    [SerializeField] private float lowStaminaThreshold;
    [SerializeField] private float staminaRegenRate;

    [SerializeField] private float chasingRange;
    [SerializeField] private float killRange;   

    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<GameObject> availableSafes;   

    private Transform bestSafeSpot;    
    private NavMeshAgent agent;

    private Node topNode;

    public float _currentStamina;

    public float currentStamina
    {
        get { return _currentStamina; }
        set { _currentStamina = value; }
    }

    public void GetStamina()
    {
        _currentStamina += Time.deltaTime * staminaRegenRate;
    }   

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _currentStamina = startingStamina;
        ConstructBehaviourTree();
    }

    private void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {            
            print("Tree failure");            
        }
        //currentStamina -= Time.deltaTime * 1;
    }

    private void ConstructBehaviourTree()
    {
        IsSafeAvailable safeAvailableNode = new IsSafeAvailable(availableSafes, this);
        GoToSafeNode goToSafeNode = new GoToSafeNode(this, availableSafes);
        StaminaNode staminaNode = new StaminaNode(this, lowStaminaThreshold);
        IsSafeNode isSafeNode = new IsSafeNode(availableSafes, this.gameObject);
        ChaseNode chaseNode = new ChaseNode(this.transform);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode killRangeNode = new RangeNode(killRange, playerTransform, transform);
        KillNode killNode = new KillNode(this);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence killSequence = new Sequence(new List<Node> { killRangeNode, killNode });

        Sequence goToSafeSequence = new Sequence(new List<Node> { safeAvailableNode, goToSafeNode });
        Selector findSafeSelector = new Selector(new List<Node> { goToSafeSequence, chaseSequence });
        Selector tryToGoSafeSelector = new Selector(new List<Node> { isSafeNode, findSafeSelector });
        Sequence mainSafeSequence = new Sequence(new List<Node> { staminaNode, tryToGoSafeSelector });

        topNode = new Selector(new List<Node> { mainSafeSequence, killSequence, chaseSequence });
    }

   

    public void SetBestSafeSpot(Transform bestSpot)
    {
        this.bestSafeSpot = bestSpot;
    }

    public Transform GetBestSafeSpot()
    {
        return bestSafeSpot;
    }  

    public void LoseStamina()
    {
        currentStamina -= 40;
    }
}
