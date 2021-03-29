using System;

namespace ProductApi.Exceptions
{
    public class TaskKeeperException : Exception
    {
        public TaskKeeperException(string message) : base(message)
        {
            
        }
    }
}