using System;

namespace TransactionTask.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message):base(message)
        {
            
        }
    }
}