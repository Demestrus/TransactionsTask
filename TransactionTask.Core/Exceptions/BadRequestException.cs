using System;

namespace TransactionTask.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message):base(message)
        {
        }
    }
}