
using System.Collections.Generic;
using System;

public interface IControllable
{
    public IReadOnlyList<Command> Commands { get; }
}