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
        if (target == null)
        {
            StopMoving();
            return;
        }

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
        StopMoving();

        return _sentResource;
    }

    public void SetDoor(Door door)
    {
        _door = door;
    }

    private void StopMoving()
    {
        _mover.Stop();
        _isBusy = false;
        _target = null;
    }
}
