using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<TState> where TState : IState<TState>
{
    void OnEnter();

    void OnExit();

    StateMachine<TState> StateMachine { get; }
}
