using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TankController : MonoBehaviour
{
    public Transform targetPoint;
    private NavMeshAgent agent;
    private bool canMove = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true; // Tank tidak bergerak di awal
    }

    void Update()
    {
        if (canMove && targetPoint != null)
        {
            agent.SetDestination(targetPoint.position);
        }
    }

    public void StartMoving()
    {
        canMove = true;
        agent.isStopped = false;
    }
}
