using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private Transform _startPosition;
    private bool _isReserved;

    public event Action<Resource> Collected;

    public bool IsReserved => _isReserved;

    public void Init(Transform spawnPoint)
    {
        _startPosition = spawnPoint;
    }

    public Transform StartPosition => _startPosition;

    public void ReportCollection()
    {
        Collected?.Invoke(this);
    }

    public void Reserve()
    {
        _isReserved = true;
    }

    public void ResetState()
    {
        _isReserved = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            unit.PickUpResource(this);
        }
    }
}