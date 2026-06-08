using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Bag _bag;
    [SerializeField] private Door _door;

    private bool _isBusy = false;
    private Transform _target;
    private Resource _sentResource;

    public event Action<Unit> Freed;

    private void Update()
    {
        if (_isBusy == true)
            _mover.GoTo(_target.position);
    }

    public void TakeDirection(Resource resource)
    {
        _target = resource.transform;
        _isBusy = true;
    }

    public void PickUpResource(Resource resource)
    {
        resource.transform.SetParent(_bag.CarryPoint);
        resource.transform.localPosition = Vector3.zero;

        _bag.PutResource(resource);
        _target = _door.transform;
    }

    public Resource GiveResourse()
    {
        _isBusy = false;
        _sentResource = _bag.CarriedResource;
        _bag.Clear();

        return _sentResource;
    }

    public void ReportFreed()
    {
        Freed?.Invoke(this);
    }
}