using System;

namespace TransactionTask.Core.Models
{
    public class LogEntry
    {
        public long TimeStamp { get; set; }
        public string ErrorMsg { get; set; }
        public string StackTrace { get; set; }
    }
}