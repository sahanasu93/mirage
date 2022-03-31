﻿using Mirage.Utils.FSM;

namespace Samples.Pong;

sealed class Playing : State<PlayerState>
{
    const float SPEED = 500f;
    
    readonly PlayerConfig _config;
    readonly Moveable _moveable;
    readonly Boundable _boundable;
    readonly Boundable _window;
    FiniteStateMachine<PlayerState> _fsm;

    public Playing(PlayerConfig config, Moveable moveable, Boundable boundable, Boundable window)
    {
        _config = config;
        _moveable = moveable;
        _boundable = boundable;
        _window = window;
    }
    
    void State<PlayerState>.Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    void State<PlayerState>.Enter() => _config.Ball.OnServeStart += OnScore;

    void State<PlayerState>.Update(float deltaTime)
    {
        _moveable.Position += new Vector2(0f, _config.MoveDirection(_moveable) * SPEED * deltaTime);
        var top = _window.Bounds.Top.Y - _boundable.Bounds.Extents.Y;
        var bottom = _window.Bounds.Bottom.Y + _boundable.Bounds.Extents.Y;
        _moveable.Position = new(_moveable.Position.X, Math.Clamp(_moveable.Position.Y, bottom, top));
        if (_boundable.Bounds.Overlaps(_config.Ball.Bounds))
            _config.Ball.Hit();
    }

    void State<PlayerState>.Exit() => _config.Ball.OnServeStart -= OnScore;

    void OnScore(PlayerIndex server) => _fsm.SwitchTo(_config.Index == server ? PlayerState.MyServe : PlayerState.TheirServe);
}