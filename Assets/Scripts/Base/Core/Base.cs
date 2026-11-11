using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private FlagController _flagController;
    [SerializeField] private Headquarters _headquarters;

    private Action<Base, Vector3, Unit> _buildAction;

    public FlagController FlagController => _flagController;

    private void OnEnable()
    {
        _flagController.FlagPlaced += StateChange;
        _flagController.BuildRequested += BuildStart;
        _headquarters.SendBuild += SentUnit;
    }

    private void Start()
    {
        _headquarters.SetState(BaseState.CreateUnit);
    }

    private void OnDisable()
    {
        _flagController.FlagPlaced -= StateChange;
        _flagController.BuildRequested -= BuildStart;
        _headquarters.SendBuild -= SentUnit;
    }

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

    public void InitBuilder(BaseBuilder baseBuilder, ResourceDatabase resourceDatabase, Action<Base, Vector3, Unit> buildAction)
    {
        _headquarters.Init(baseBuilder, resourceDatabase);
        _buildAction = buildAction;
    }

    public void BuildComplete()
    {
        _headquarters.BuildComplete();
    }

    private void BuildStart(Vector3 position, Unit unit)
    {
        _buildAction?.Invoke(this, position, unit);
    }

    private void SentUnit()
    {
        _headquarters.SetState(BaseState.CreateUnit);
    }

    private void StateChange(BaseState baseState)
    {
        _headquarters.SetState(baseState);
    }
}