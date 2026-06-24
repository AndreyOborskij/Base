using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Resource _prefabResourse;

    public event Action<Resource> Returned;

    private ObjectPool<Resource> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Resource>(
            createFunc: () => CreateResource(),
            actionOnGet: (resource) => ActivateResource(resource),
            actionOnRelease: (resource) => DeactivateResource(resource)
            );
    }

    public Resource Spawn(Transform spawnPoint)
    {
        Resource resource = _pool.Get();

        resource.transform.position = spawnPoint.position;
        resource.Init(spawnPoint);

        return resource;
    }

    private Resource CreateResource()
    {
        Resource resource = Instantiate(_prefabResourse);
        resource.Collected += ReturnResource;

        return resource;
    }

    private void ActivateResource(Resource resource)
    {
        resource.gameObject.SetActive(true);
    }

    private void DeactivateResource(Resource resource)
    {
        resource.gameObject.SetActive(false);
    }

    private void ReturnResource(Resource resource)
    {
        resource.transform.SetParent(null);

        Returned?.Invoke(resource);

        _pool.Release(resource);
    }
}
