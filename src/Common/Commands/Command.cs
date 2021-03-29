using System;

namespace Common.Commands
{
    public abstract class Command
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
    }
}