using UnityEngine;

public class ResourceReceiver : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private ResourceDatabase _resourceDatabase;

    public void Init(ResourceDatabase resourceDatabase)
    {
        _resourceDatabase = resourceDatabase;
    }

    public void AcceptResource(Unit unit)
    {
        if (unit.HasResource == false)
            return;

        Resource resource = unit.GiveResource();

        _storage.TakeResource();
        _resourceDatabase.RemoveResource(resource);
        resource.ReportCollection();
    }
}
