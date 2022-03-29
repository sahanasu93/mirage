﻿using System.Collections.Generic;

namespace Mirage.Utils.FSM;

public sealed class FiniteStateMachine<T>
{
    readonly Dictionary<T, State<T>> _states = new();
    State<T> _current = Empty<T>.State;

    public State<T> this[T index]
    {
        get
        {
            if (!_states.ContainsKey(index))
                throw new MissingStateException($"State of type {index} does not exist in Finite State Machine of type {this}!");
            return _states[index];
        }
    }

    public FiniteStateMachine(T first, params (T, State<T>)[] states)
    {
        foreach (var (key, value) in states) 
            _states.Add(key, value);
        foreach (var (_, value) in _states) 
            value.Init(this);
        SwitchTo(first);
    }

    public void SwitchTo(T enter) => SwitchTo(this[enter]);

    public void SwitchTo(State<T> enter)
    {
        _current.Exit();
        _current = enter ?? Empty<T>.State;
        _current.Enter();
    }

    public void Update(float dt) => _current.Update(dt);
}