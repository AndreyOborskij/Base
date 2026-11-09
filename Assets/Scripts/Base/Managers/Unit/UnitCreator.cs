using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    [SerializeField] private Unit _prefabUnit;
    [SerializeField] private Casern _casern;

    private int _costCreate = 3;

    public int CostCreate => _costCreate;

    public Unit Create(Door door)
    {
        Unit unit = Instantiate(_prefabUnit, _casern.transform.position, Quaternion.identity);
        unit.SetDoor(door);

        return unit;
    }
}