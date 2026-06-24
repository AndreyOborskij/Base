using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private Transform _startPosition;

    public event Action<Resource> Collected;

    public void Init(Transform spawnPoint)
    {
        _startPosition = spawnPoint;
    }

    public Transform StartPosition => _startPosition;

    public void ReportCollection()
    {
        Collected?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            unit.PickUpResource(this);
        }
    }
}