using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMovement : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
