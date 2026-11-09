using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private FlagController _flagController;
    [SerializeField] private BaseBehaviour _baseBehaviour;
    [SerializeField] private Headquarters _headquarters;

    private void OnEnable()
    {
        _baseBehaviour.StateChanged += OnStateChange;
        _headquarters.SendBuild += SentUnit;
    }

    private void OnDisable()
    {
        _baseBehaviour.StateChanged -= OnStateChange;
        _headquarters.SendBuild -= SentUnit;
    }

    public FlagController FlagController => _flagController;

    public void TakePositionForBuild(Vector3 position)
    {
        if (_headquarters.CanBuild == false)
            return;

        _flagController.SetPositionBuild(position);
    }

    public void RemoveUnit(Unit unit)
    {
        _headquarters.RemoveUnit(unit);
    }

    public void AddUnit(Unit unit)
    {
        _headquarters.AddUnit(unit);
    }

    public void InitBuilder(BaseBuilder baseBuilder, ResourceDatabase resourceDatabase)
    {
        _headquarters.Init(baseBuilder, resourceDatabase);
    }

    public void BuildComplete()
    {
        _headquarters.BuildComplete();
    }

    private void SentUnit()
    {
        _baseBehaviour.ChangeState(BaseState.CreateUnit);
    }

    private void OnStateChange(BaseState baseState)
    {
        switch (baseState)
        {
            case BaseState.BuildBase:
                _headquarters.SetState(baseState);
                break;

            case BaseState.CreateUnit:
                _headquarters.SetState(baseState);
                break;
        }
    }
}