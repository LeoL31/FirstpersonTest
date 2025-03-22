using UnityEngine;
using UnityEngine.AI;

public class EnamyAI : MonoBehaviour
{
    private NavMeshAgent agent = null;
    [SerializeField] private Transform target = null;

    private void Start()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(target.position);
        }
    }
}

