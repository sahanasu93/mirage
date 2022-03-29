﻿using System;
using System.Numerics;
using Mirage.Graphics;
using Mirage.Input;
using Silk.NET.OpenGL;

namespace Mirage;

public abstract class Entity : IDisposable, Transform, Boundable
{
    static readonly string _variableNameHere = "<VAR>";
    static readonly string _nullError = $"You can't access {_variableNameHere} before Game.Start() is invoked. Try sticking this in a World.OnAwake() or World.OnStart() callback, or in an Entity event method, e.g, Entity.OnAwake() or Entity.OnStart().";
    
    GL _gl;
    Renderer _renderer;
    World _world;
    Camera _camera;
    Window _window;
    Keyboard _keyboard;

    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Size { get; set; } = new(1000);
    public Vector2 Scale { get; set; } = new(1);
    Sprite _sprite;

    protected string Texture
    {
        get => _sprite.Texture.Path;
        set
        {
            if (_gl is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.Texture"));
            _sprite = new(value, _gl, this);
        }
    }
    protected int SortingOrder
    {
        get => _sprite.SortingOrder;
        set => _sprite.SortingOrder = value;
    }
    protected World World
    {
        get
        {
            if (_world is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.World"));
            return _world;
        }
        private set => _world = value;
    }
    protected Camera Camera
    {
        get
        {
            if (_camera is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.Camera"));
            return _camera;
        }
        private set => _camera = value;
    }
    protected Window Window
    {
        get
        {
            if (_window is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.Window"));
            return _window;
        }
        private set => _window = value;
    }
    protected Keyboard Keyboard
    {
        get
        {
            if (_keyboard is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.Keyboard"));
            return _keyboard;
        }
        private set => _keyboard = value;
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate(float dt) { }
    protected virtual void OnDestroy() { }

    public Bounds Bounds() => new(Position, Size);

    public void Dispose()
    {
        _sprite?.Dispose();
        GC.SuppressFinalize(this);
    }

    internal void Initialize(GL gl, World world, Renderer renderer, Camera camera, Window window, Keyboard keyboard)
    {
        _gl = gl;
        World = world;
        _renderer = renderer;
        Camera = camera;
        Window = window;
        Keyboard = keyboard;
    }

    internal void Awake() => OnAwake();
    internal void Start() => OnStart();
    internal void Update(float dt) => OnUpdate(dt);
    internal void Render()
    {
        if (_sprite is not null)
            _renderer.Queue(_sprite);
    }
    internal void Destroy() => OnDestroy();
}

public abstract class Entity<T> : Entity
{
    protected virtual void OnConfigure(T config) { }
    internal void Configure(T settings) => OnConfigure(settings);
}