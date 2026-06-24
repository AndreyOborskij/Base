using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action<Unit> Came;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            Came?.Invoke(unit);
        }
    }
}
