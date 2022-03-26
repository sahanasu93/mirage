﻿namespace Guap.Rendering;

sealed class ShaderFailedCompilationException : Exception
{
    public ShaderFailedCompilationException() { }
    public ShaderFailedCompilationException(string message) : base(message) { }
    public ShaderFailedCompilationException(string message, Exception inner) : base(message, inner) { }
}