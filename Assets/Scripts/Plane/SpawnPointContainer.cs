using System.Collections.Generic;
using UnityEngine;

public class SpawnPointContainer : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private List<Transform> _freePoints;
    private List<Transform> _busyPoints;

    public int BusyPoints => _busyPoints.Count;
    public int AllPoints => _spawnPoints.Length;

    private void Awake()
    {
        _freePoints = new List<Transform>(_spawnPoints);
        _busyPoints = new List<Transform>();
    }

    public Transform GetFreePoint()
    {
        if (_freePoints.Count == 0)
            return null;

        int index = Random.Range(0, _freePoints.Count);

        Transform point = _freePoints[index];

        _freePoints.RemoveAt(index);
        _busyPoints.Add(point);

        return point;
    }

    public void ReturnPoint(Transform point)
    {
        _busyPoints.Remove(point);
        _freePoints.Add(point);
    }
}