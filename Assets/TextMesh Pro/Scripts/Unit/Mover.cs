using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    public void GoTo(Vector3 targetPosition)
    {
        _agent.SetDestination(targetPosition);
    }
}
