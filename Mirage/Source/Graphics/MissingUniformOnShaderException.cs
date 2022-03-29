﻿using System;

namespace Mirage.Graphics;

sealed class MissingUniformOnShaderException : Exception
{
    public MissingUniformOnShaderException() { }
    public MissingUniformOnShaderException(string message) : base(message) { }
    public MissingUniformOnShaderException(string message, Exception inner) : base(message, inner) { }
}