namespace TransactionTask.Core.Dto
{
    public class UserResult
    {
        public UserResult() : this(true)
        {
            
        }
        
        public UserResult(bool success, string errorMsg = null)
        {
            Success = success;
            ErrorMsg = errorMsg;
        }
        
        public bool Success { get; }
        public string ErrorMsg { get; }
    }
}