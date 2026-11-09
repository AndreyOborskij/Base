using System;
using UnityEngine;

public class Flag : MonoBehaviour 
{
    public Transform position => transform;

    public event Action<Vector3, Unit> Came;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            Came?.Invoke(transform.position, unit);
        }
    }
}
