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

    public bool IsBusy => _isBusy;

    public event Action<Unit> Freed;

    private void Update()
    {
        if (_isBusy == true)
        {
            _mover.GoTo(_target.position);
        }    
    }

    public bool HasResource => _bag.CarriedResource != null;

    public void TakeDirection(Transform target)
    {
        if(target  == null)
            return;

        _target = target;
        _isBusy = true;
    }

    public void PickUpResource(Resource resource)
    {
        if (HasResource == true)
            return;

        resource.transform.SetParent(_bag.CarryPoint);
        resource.transform.localPosition = Vector3.zero;

        _bag.PutResource(resource);
        _target = _door.transform;
    }

    public Resource GiveResource()
    {
        _sentResource = _bag.CarriedResource;
        _bag.Clear();

        return _sentResource;
    }

    public void ReportFreed()
    {
        _isBusy = false;
        Freed?.Invoke(this);
    }

    public void SetDoor(Door door)
    {
        _door = door;
    }
}