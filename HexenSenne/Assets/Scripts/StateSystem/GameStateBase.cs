using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBase: IState<GameStateBase>
{
    public const string PlayingState = "playing";
    public const string ReplayingState = "replaying";

    public StateMachine<GameStateBase> StateMachine => _stateMachine;

    private StateMachine<GameStateBase> _stateMachine;
    protected GameStateBase(StateMachine<GameStateBase> stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Forward() { }
    public virtual void Backward() { }
    public virtual void Select(BaseCard<Piece<HexTileHandler>, HexTileHandler> card) { }
    public virtual void DeselectAll() { }
    public virtual void Execute(HexTileHandler hexTileHandler) { }
}
