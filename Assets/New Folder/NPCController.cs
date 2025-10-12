using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    private Animator animator;
    public NPCManager manager;
    private NavMeshAgent agent;

    public Text orderText;
    public NPCOrderDisplay orderDisplay;

    private string currentOrder;
    private bool waitingForFood = false;
    public bool receivedFood = false;
    public bool isAtCashier = false;

    private Transform exitWaypoint;
    private Transform[] pathWaypoints;
    private int currentPathIndex = 0;
    private bool isInQueue = false;

    public float stopDistance = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        DisableRagdoll();

        if (orderDisplay == null)
            orderDisplay = GetComponentInChildren<NPCOrderDisplay>();

        SetRandomOrder();
        if (orderDisplay != null) orderDisplay.ClearOrder();
        if (orderText != null) orderText.gameObject.SetActive(false);
    }

    void Update()
{
    if (agent == null || agent.pathPending) return;

    if (!receivedFood && agent.remainingDistance < stopDistance)
    {
        // Jika sudah di kasir tapi belum diset statusnya
        if (!waitingForFood && !isAtCashier && IsAtCashier())
        {
            isInQueue = true;
            agent.isStopped = true;
            isAtCashier = true;
            waitingForFood = true;

            if (orderDisplay != null)
                orderDisplay.SetOrder(currentOrder);
            if (orderText != null)
                orderText.gameObject.SetActive(true);

            LookForward();
            return; // ⛔ Hentikan eksekusi agar tidak lanjut ke jalur antre ulang
        }

        // ✅ Lanjut ke waypoint berikutnya jika masih dalam jalur antre
        if (currentPathIndex < pathWaypoints.Length)
        {
            agent.SetDestination(pathWaypoints[currentPathIndex].position);
            currentPathIndex++;
        }
    }

    if (receivedFood && agent.remainingDistance < stopDistance)
    {
        manager.RemoveNPC(this);
    }

    if (animator != null && agent != null)
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }
}


    public void SetPath(Transform[] path, Transform exit)
    {
        agent = GetComponent<NavMeshAgent>();
        pathWaypoints = path;
        exitWaypoint = exit;
        isInQueue = false;
        isAtCashier = false;

        if (agent != null && pathWaypoints.Length > 0)
        {
            agent.isStopped = false;
            agent.ResetPath();
            agent.SetDestination(pathWaypoints[0].position);
            currentPathIndex = 1;
        }
    }

    public void MoveToNextPosition(Transform targetWaypoint)
    {
        if (targetWaypoint != null && !receivedFood)
        {
            if (agent == null) agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;
            agent.ResetPath();
            agent.SetDestination(targetWaypoint.position);

            isInQueue = false;

            if (!IsAtCashier())
            {
                waitingForFood = false;
                isAtCashier = false;

                if (orderDisplay != null)
                    orderDisplay.ClearOrder();
                if (orderText != null)
                    orderText.gameObject.SetActive(false);
            }
        }
    }

    bool IsAtCashier()
    {
        if (manager != null && manager.waypoints.Length > 4)
        {
            float distanceToCashier = Vector3.Distance(transform.position, manager.waypoints[4].position);
            return distanceToCashier < stopDistance * 2f;
        }
        return false;
    }

    void SetRandomOrder()
    {
        string[] orders = { "Pizza", "Cake", "Fries", "Soup", "Sandwich" };
        currentOrder = orders[Random.Range(0, orders.Length)];
        if (orderText != null)
            orderText.text = "I want " + currentOrder;
    }

    public bool TryReceiveFood(string foodTag)
    {
        if (IsWaitingForFood() && foodTag == currentOrder)
        {
            receivedFood = true;
            waitingForFood = false;
            isInQueue = false;
            isAtCashier = false;

            if (orderText != null)
            {
                orderText.text = "Thank you!";
                orderText.gameObject.SetActive(true);
            }

            if (orderDisplay != null)
                orderDisplay.ClearOrder();

            agent.isStopped = false;
            agent.SetDestination(exitWaypoint.position);

            CustomerCounter.Instance?.AddCustomerServed();
            manager.UpdateQueue();
            return true;
        }

        return false;
    }

    public bool IsWaitingForFood() => waitingForFood && !receivedFood;

    void LookForward()
    {
        Vector3 forward = transform.forward;
        forward.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(forward);
        transform.rotation = rotation;
    }

    void DisableRagdoll()
    {
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
            if (rb != GetComponent<Rigidbody>()) rb.isKinematic = true;
        foreach (var col in GetComponentsInChildren<Collider>())
            if (col != GetComponent<Collider>()) col.enabled = false;
    }
    public void MakeRagdoll()
{
    if (TryGetComponent<NavMeshAgent>(out var nav)) nav.enabled = false;
    if (TryGetComponent<Animator>(out var anim)) anim.enabled = false;
    this.enabled = false;

    foreach (var rb in GetComponentsInChildren<Rigidbody>())
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    foreach (var col in GetComponentsInChildren<Collider>())
    {
        col.enabled = true;
    }

    if (manager != null)
    {
        manager.RemoveNPCWithoutDestroy(this);
        manager.UpdateQueue();
    }

    Destroy(gameObject, 5f);
}

}
