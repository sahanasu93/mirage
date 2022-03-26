﻿using Pong;
using Guap;
using Guap.Rendering;
using Guap.Input;

var window = new Window("PONG", 1280, 720);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera);
var world = new World(window, keyboard, graphics, renderer, camera).Spawn<InputDebugger>();
new Game(world, window, keyboard, graphics, renderer).Start();
