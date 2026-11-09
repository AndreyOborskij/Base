using System;
using UnityEngine;
using static UnityEngine.UI.Image;

public class FlagController : MonoBehaviour
{
    [SerializeField] private Flag _prefabFlag;

    private Flag _flag;
    private Base _base;
    private Unit _builder; 

    public event Action<BaseState> FlagPlaced;
    public event Action<Base ,Vector3, Unit> BuildRequested;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    public Transform FlagPosotion => _flag.position;

    public void SetPositionBuild(Vector3 position)
    {
        if (_builder != null)
        {
            return;
        }    

        if (_flag == null)
        {
            _flag = Instantiate(_prefabFlag, position, Quaternion.identity);
            _flag.Came += CanBuild;

            FlagPlaced?.Invoke(BaseState.BuildBase);
        }
        else
        {
            _flag.transform.position = position;

            FlagPlaced?.Invoke(BaseState.BuildBase);
        }
    }

    public void DetermineBuilder(Unit unit)
    {
        _builder = unit;
    }

    public void ResetState()
    {
        _flag.Came -= CanBuild;
        _builder = null;

        Destroy(_flag.gameObject);
        _flag = null;
    }

    private void CanBuild(Vector3 position, Unit unit)
    {
        if (_builder == unit)
            BuildRequested?.Invoke(_base ,position, unit);
    }
}
