using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private SpawnPointContainer _points;

    private int _maxResources;

    private void OnEnable()
    {
        _spawner.Returned += Recreate;
    }

    private void Start()
    {
        _maxResources = _points.AllPoints;
        CreateResources();
    }

    private void OnDisable()
    {
        _spawner.Returned -= Recreate;
    }

    private void Recreate(Resource resource)
    {
        _points.ReturnPoint(resource.StartPosition);

        CreateResources();
    }

    private void CreateResources()
    {
        while (_points.BusyPoints < _maxResources)
        {
            Transform point = _points.GetFreePoint();

            if (point == null)
                return;

            _spawner.Spawn(point);
        }
    }
}