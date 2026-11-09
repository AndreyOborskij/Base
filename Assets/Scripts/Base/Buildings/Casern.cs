using System;
using System.Collections.Generic;
using UnityEngine;

public class Casern : MonoBehaviour
{
    [SerializeField] private List<Unit> _units =  new();
    [SerializeField] private Storage _storage;
    [SerializeField] private Door _door;
    [SerializeField] private UnitCreator _unitCreator;

    public List<Unit> Units => _units;

    public event Action<Unit> Created;

    public void Create()
    {
        Unit unit = _unitCreator.Create(_door);

        _units.Add(unit);

        Created?.Invoke(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public void AddUnit(Unit unit)
    {
        unit.SetDoor(_door);

        _units.Add(unit);
    }
}
