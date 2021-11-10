using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotoDestination : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public Transform target;
    bool targetHasPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(target.position);
        targetHasPowerUp = target.gameObject.GetComponent<Target>().hasPowerUp;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetHasPowerUp) {
            navMeshAgent.destination = -target.position;
        } else {
            navMeshAgent.destination = target.position;
        }
        print(navMeshAgent.isPathStale);
        if (navMeshAgent.isPathStale) {
            navMeshAgent.destination = target.position;
        }
    }
}
