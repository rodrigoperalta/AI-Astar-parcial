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

    ChaseNode chaseNode;
    KillNode killNode;
    IsSafeAvailable safeAvailableNode;
    GoToSafeNode goToSafeNode;
    StaminaNode staminaNode;
    IsSafeNode isSafeNode;
    RangeNode chasingRangeNode;
    RangeNode killRangeNode;

    Sequence chaseSequence ;
    Sequence killSequence;

    Sequence goToSafeSequence;
    Selector findSafeSelector;
    Selector tryToGoSafeSelector;
    Sequence mainSafeSequence;

    Transform target;

    private Transform bestSafeSpot;    
    

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
            print("chaseNode node state: " + chaseNode.nodeState);
            print("killNode node state: " + killNode.nodeState);
            print("safeAvailableNode node state: " + safeAvailableNode.nodeState);
            print("goToSafeNode node state: " + goToSafeNode.nodeState);
            print("staminaNode node state: " + staminaNode.nodeState);
            print("isSafeNode node state: " + isSafeNode.nodeState);
            print("ckillRangeNode node state: " + killRangeNode.nodeState);
            print("chasingRangeNode node state: " + chasingRangeNode.nodeState);
            print("chaseSequence state: " + chaseSequence.nodeState);
            print("killSequence state: " + killSequence.nodeState);
            print("goToSafeSequence state: " + goToSafeSequence.nodeState);
            print("findSafeSelector state: " + findSafeSelector.nodeState);
            print("tryToGoSafeSelector state: " + tryToGoSafeSelector.nodeState);
            print("mainSafeSequence state: " + mainSafeSequence.nodeState);
            print("Tree failure");            
        }
        //currentStamina -= Time.deltaTime * 1;
    }

    private void ConstructBehaviourTree()
    {
        safeAvailableNode = new IsSafeAvailable(availableSafes, this);
        goToSafeNode = new GoToSafeNode(this, availableSafes);
        staminaNode = new StaminaNode(this, lowStaminaThreshold);
        isSafeNode = new IsSafeNode(availableSafes, this.gameObject);
        chaseNode = new ChaseNode(this.transform);
        chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        killRangeNode = new RangeNode(killRange, playerTransform, transform);
        killNode = new KillNode(this);

        chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        killSequence = new Sequence(new List<Node> { killRangeNode, killNode });

        goToSafeSequence = new Sequence(new List<Node> { safeAvailableNode, goToSafeNode });
        findSafeSelector = new Selector(new List<Node> { goToSafeSequence, chaseSequence });
        tryToGoSafeSelector = new Selector(new List<Node> { isSafeNode, findSafeSelector });
        mainSafeSequence = new Sequence(new List<Node> { staminaNode, tryToGoSafeSelector });

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

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public Transform GetTarget()
    {
        return target;
    }

}
