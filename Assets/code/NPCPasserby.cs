using UnityEngine;
using UnityEngine.AI;

public class NPCPasserby : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float stopDistance = 0.5f;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (startPoint != null && endPoint != null)
        {
            transform.position = startPoint.position;
            agent.SetDestination(endPoint.position);
        }
    }

    void Update()
    {
        if (agent == null || agent.pathPending) return;

        if (Vector3.Distance(transform.position, endPoint.position) <= stopDistance)
        {
            Destroy(gameObject);
        }

        if (animator != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }
}
