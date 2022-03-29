﻿using System;

namespace Mirage.Graphics;

sealed class ShaderProgramFailedLinkException : Exception
{
    public ShaderProgramFailedLinkException() { }
    public ShaderProgramFailedLinkException(string message) : base(message) { }
    public ShaderProgramFailedLinkException(string message, Exception inner) : base(message, inner) { }
}