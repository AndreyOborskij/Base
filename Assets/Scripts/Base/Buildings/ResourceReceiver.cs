using UnityEngine;

public class ResourceReceiver : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private ResourceDatabase _resourceDatabase;
    [SerializeField] private Door _door;

    private void OnEnable()
    {
        _door.Came += AcceptResource;
    }

    private void OnDisable()
    {
        _door.Came -= AcceptResource;
    }

    public void Init(ResourceDatabase resourceDatabase)
    {
        _resourceDatabase = resourceDatabase;
    }

    private void AcceptResource(Unit unit)
    {
        if (unit.HasResource == false)
            return;

        Resource resource = unit.GiveResource();

        _storage.TakeResource();
        _resourceDatabase.RemoveResource(resource);
        resource.ReportCollection();

        unit.ReportFreed();
    }
}
